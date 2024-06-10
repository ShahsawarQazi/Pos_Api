using System.Text;
using Pos.Application.Common.Extensions;

namespace Pos.Application.Common.Exceptions
{
    public class PayoutWebException : Exception
    {
        public ulong ErrorCode { get; set; }
        public string Description { get; set; }
        public PayoutWebException(ulong errorCode, string description)
        {
            ErrorCode = errorCode;
            Description = description;
        }
        public override string ToString()
        {
            var retVal = new StringBuilder();

            retVal.AppendLine(ErrorCode.CheckIfNullThenDefault(nameof(ErrorCode)));
            retVal.AppendLine(Description.CheckIfNullThenDefault(nameof(Description)));
            retVal.AppendLine();
            retVal.AppendLine("Stack Trace: ");
            retVal.AppendLine(base.ToString());
            return retVal.ToString().RemoveEmptyLines();
        }
        protected string BaseToString()
        {
            return
                base.ToString();
        }
    }
}
