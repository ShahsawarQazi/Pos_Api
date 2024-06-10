using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Common.Extensions;
using Pos.Application.Common.Models;
using Pos.Application.Common.ValidationDefinitions;

namespace PosApi.Common
{
    public class PayoutValidationProblemDetails : ValidationProblemDetails
    {
        public Dictionary<string, List<ErrorDetail>> ErrorDetails { get; set; }
        public PayoutValidationProblemDetails(IEnumerable<ValidationFailure> failures, IDictionary<string, string[]> errors) : base(errors)
        {
            ErrorDetails = failures
                .GroupBy(e => e.PropertyName, e => e)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup
                    .Select(i => new ErrorDetail
                    {
                        Description = i.ErrorMessage,
                        Code = (ErrorCodes)(ulong)i.ErrorCode.ToEnum(ValidationErrorCode.DefaultValidator)
                    }).ToList());
        }
    }
}
