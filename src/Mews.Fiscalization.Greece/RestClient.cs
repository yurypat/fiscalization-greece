using Mews.Fiscalization.Greece.Dto.Xsd;
using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Greece
{
    internal class RestClient
    {
        private static readonly Uri ProductionBaseUri = new Uri("https://mydata-dev.azure-api.net");
        private static readonly Uri SandboxBaseUri = new Uri("https://mydata-dev.azure-api.net");
        private static readonly string SendInvoicesEndpointMethodName = "SendInvoices";
        private static readonly string UserIdHeaderName = "aade-user-id";
        private static readonly string SubscriptionKeyHeaderName = "Ocp-Apim-Subscription-Key";
        private static readonly string XmlMediaType = "application/xml";

        public string UserId { get; }

        public string SubscriptionKey { get; }

        public AadeLogger Logger { get; }

        public Uri EndpointUri { get; }

        private HttpClient HttpClient { get; }

        internal RestClient(string userId, string subscriptionKey, AadeEnvironment environment, AadeLogger logger = null)
        {
            UserId = userId ?? throw new ArgumentNullException(userId);
            SubscriptionKey = subscriptionKey ?? throw new ArgumentNullException(subscriptionKey);

            var baseUri = environment == AadeEnvironment.Production
                ? ProductionBaseUri
                : SandboxBaseUri;
            EndpointUri = new Uri(baseUri, SendInvoicesEndpointMethodName);

            Logger = logger;

            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add(UserIdHeaderName, $"{UserId}");
            HttpClient.DefaultRequestHeaders.Add(SubscriptionKeyHeaderName, $"{SubscriptionKey}");
        }

        internal async Task<ResponseDoc> SendRequestAsync(InvoicesDoc invoicesDoc)
        {
            var requestContent = XmlManipulator.Serialize(invoicesDoc).DocumentElement.OuterXml;
            Logger?.Debug("Created XML document from DTOs.", new { XmlString = requestContent });

            var requestMessage = BuildHttpPostMessage(requestContent);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpResponseMessage response;

            try
            {
                response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.Info($"HTTP request failed after {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = stopwatch.ElapsedMilliseconds });

                return BuildResponseDocWithErrors(SendInvoiceErrorCodes.TimeoutErrorCode, ex.Message, invoicesDoc.Invoices);
            }

            stopwatch.Stop();
            Logger?.Info($"HTTP request finished in {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = stopwatch.ElapsedMilliseconds });

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return BuildResponseDocWithErrors(SendInvoiceErrorCodes.ForbiddenErrorCode, "Authorization error", invoicesDoc.Invoices);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return BuildResponseDocWithErrors(SendInvoiceErrorCodes.InternalServerErrorCode, "Internal server error", invoicesDoc.Invoices);
            }

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

            var responseDoc = XmlManipulator.Deserialize<ResponseDoc>(responseContent);
            Logger?.Debug("Result received and successfully deserialized.", responseDoc);

            return responseDoc;
        }

        private HttpRequestMessage BuildHttpPostMessage(string messageContent)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, EndpointUri);

            message.Content = new StringContent(content: messageContent, encoding: Encoding.UTF8, mediaType: XmlMediaType);

            return message;
        }

        private ResponseDoc BuildResponseDocWithErrors(string errorCode, string errorMessage, Dto.Xsd.Invoice[] invoices)
        {
            return new ResponseDoc
            {
                Responses = invoices.Select((_, index) => new Response
                {
                    Index = index + 1,
                    Errors = new[]
                    {
                        new Error
                        {
                            Code = errorCode,
                            Message = errorMessage
                        }
                    }
                }).ToArray()
            };
        }
    }
}
