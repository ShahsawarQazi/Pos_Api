using System.Text;
using Pos.Application.Common.Extensions;
using Pos.Application.Common.Models;

namespace Pos.Application.Common.Exceptions
{
    public class ApiException : PayoutWebException
    {
        public ApiException(ulong errorCode, string description) : base(errorCode, description)
        {
        }

        public List<ErrorDetail> Errors { get; set; }

        public override string ToString()
        {
            var retVal = new StringBuilder();

            retVal.AppendLine(ErrorCode.CheckIfNullThenDefault(nameof(ErrorCode)));
            retVal.AppendLine(Description.CheckIfNullThenDefault(nameof(Description)));
            retVal.AppendLine();

            if (Errors != null && Errors.Any())
            {
                retVal.AppendLine($"{nameof(Errors).SplitWords()}: ");
                for (var i = 0; i < Errors.Count; i++)
                {
                    var error = Errors[i];
                    retVal.AppendLine($"Api Exception Result {i + 1}: ".AppendTabAtStartOfLine());
                    retVal.AppendLine(error.ToString().AppendTabAtStartOfLine(2));
                }
            }

            retVal.AppendLine();
            retVal.AppendLine("Stack Trace:");
            retVal.AppendLine(BaseToString());

            return retVal.ToString();
        }
    }
}
