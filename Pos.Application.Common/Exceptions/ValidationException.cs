using FluentValidation.Results;
using Pos.Application.Common.Extensions;

namespace Pos.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationFailure> DetailedErrors { get; }
        public string Instance { get; }
        public ValidationException() : base(ErrorCodes.ValidationError.GetDescription())
        {
            DetailedErrors = new List<ValidationFailure>();
        }

        public ValidationException(string instance, List<ValidationFailure> failures) : this()
        {
            Instance = instance;
            DetailedErrors = failures.ToList();
        }
    }
}
