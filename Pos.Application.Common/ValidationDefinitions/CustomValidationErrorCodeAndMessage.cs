using System.Globalization;
using Pos.Application.Common.Extensions;

namespace Pos.Application.Common.ValidationDefinitions
{
    public class CustomValidationErrorCodeAndMessage : FluentValidation.Resources.LanguageManager
    {
        public CustomValidationErrorCodeAndMessage()
        {
            Culture = CultureInfo.CurrentCulture;

            AddTranslation(Culture.Name, ValidationErrorCode.NotNullValidator.ToIntegerValueString(), "'{PropertyName}' is required.");
            AddTranslation(Culture.Name, ValidationErrorCode.IsEnumValidator.ToIntegerValueString(), "'{PropertyName}' has a range of values which does not include '{PropertyValue}'.");
            AddTranslation(Culture.Name, ValidationErrorCode.EmptyValidator.ToIntegerValueString(), "'{PropertyName}' must not be empty.");
            AddTranslation(Culture.Name, ValidationErrorCode.InvalidCurrency.ToIntegerValueString(), "currency should be DKK.");
            AddTranslation(Culture.Name, ValidationErrorCode.ClientNotExist.ToIntegerValueString(), "client doesn't exist.");
        }
    }
}
