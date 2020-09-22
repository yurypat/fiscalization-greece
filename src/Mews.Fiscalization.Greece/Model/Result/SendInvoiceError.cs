using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceError
    {
        public SendInvoiceError(Error error)
        {
            Message = error.Message;
            Code = MapErrorCode(error.Code);
        }

        public ErrorCode Code { get; }

        public string Message { get; }

        private ErrorCode MapErrorCode(string code)
        {
            switch (code)
            {
                case SendInvoiceErrorCodes.InternalServerErrorCode:
                case "TechnicalError":
                    return ErrorCode.InternalServerError;

                case SendInvoiceErrorCodes.TimeoutErrorCode:
                    return ErrorCode.Network;

                case SendInvoiceErrorCodes.ForbiddenErrorCode:
                    return ErrorCode.InvalidCredentials;

                case "ValidationError":
                case "XMLSyntaxError":
                    return ErrorCode.UnhandledValidation;

                default:
                    throw new NotImplementedException($"Error code: {code} is not implemented.");
            }
        }
    }
}
