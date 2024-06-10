using System.ComponentModel;
using System.Reflection;

namespace Pos.Application.Common.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Get the the description of the enumeration
        /// </summary>
        /// <param name="value">The enumeration value</param>
        /// <returns>The </returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }
        public static string ToIntegerValueString(this Enum enumeration)
        {
            var field = enumeration.GetType().GetField(enumeration.ToString());
            return ((int)field.GetValue(enumeration)).ToString();
        }

    }
}
