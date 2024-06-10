using System.ComponentModel;

namespace Pos.Application.Contracts.Error
{
    public class ErrorDetail
    {
        [DefaultValue(5)]
        public ulong Code { get; set; }

        [DefaultValue("Provided RecieptId is not unique.")]
        public string Description { get; set; }
        //public override string ToString()
        //{
        //    var stringBuilder = new StringBuilder();
        //    stringBuilder.AppendLine(Code.CheckIfNullThenDefault("Code"));
        //    stringBuilder.AppendLine(Description.CheckIfNullThenDefault("Description"));
        //    return stringBuilder.ToString().RemoveEmptyLines();
        //}
    }
}
