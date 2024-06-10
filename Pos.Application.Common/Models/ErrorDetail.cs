using System.Text;
using Pos.Application.Common.Extensions;

namespace Pos.Application.Common.Models
{
    public class ErrorDetail
    {
        /// <summary>
        /// The error code
        /// </summary>
        public ErrorCodes Code { get; set; }

        /// <summary>
        /// The description of the error
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The originating system from which error was generated
        /// </summary>


        /// <summary>
        /// Creates string representation of object based upon its properties.
        /// </summary>
        /// <returns>String representation of object based upon its properties value</returns>
        public override string ToString()
        {
            var retVal = new StringBuilder();

            retVal.AppendLine(((ulong)Code).CheckIfNullThenDefault(nameof(Code)));
            retVal.AppendLine(Description.CheckIfNullThenDefault(nameof(Description)));

            return retVal.ToString().RemoveEmptyLines();
        }
    }
}
