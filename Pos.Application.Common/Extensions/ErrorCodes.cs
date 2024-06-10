using System.ComponentModel;

namespace Pos.Application.Common.Extensions
{
    public enum ErrorCodes : ulong
    {
        [Description("The error code provided is not valid")]
        InvalidErrorCode = 1,
        [Description("The system has encountered a general exception and cannot process current request.")]
        GeneralError = 2,
        [Description("Payment Not Found.")]
        PaymentNotFound = 3,
        [Description("Underlying service error.")]
        UnderlyingServiceError = 4,
        [Description("Payment Not Found In AltaPay.")]
        PaymentNotFoundInAltaPay = 5,
        [Description("Currency must be in DKK")]
        InvalidCurrency = 6,
        [Description("Invalid Client Id")]
        InvalidClient = 7,
        [Description("One or more validation failures have occurred.")]
        ValidationError = 8
    }
}
