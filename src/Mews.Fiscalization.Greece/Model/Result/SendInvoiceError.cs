﻿using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceError
    {
        public SendInvoiceError(string code, string message)
        {
            Message = message;
            Code = MapErrorCode(code);
        }

        public ErrorCode Code { get; }

        public string Message { get; }

        private ErrorCode MapErrorCode(string code)
        {
            switch (code)
            {
                case SendInvoiceErrorCodes.InternalServerErrorCode:
                case "TechnicalError":
                    return ErrorCode.TechnicalError;
                case SendInvoiceErrorCodes.TimeoutErrorCode:
                    return ErrorCode.NetworkError;
                case SendInvoiceErrorCodes.ForbiddenErrorCode:
                    return ErrorCode.InvalidCredentials;
                case "ValidationError":
                case "XMLSyntaxError":
                    return ErrorCode.ValidationError;
                default:
                    throw new NotImplementedException($"Error code: {code} is not implemented.");
            }
        }
    }
}
