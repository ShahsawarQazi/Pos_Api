using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pos.Domain.Entities.Enums;

namespace Pos.Application.Contracts.Request.Customer
{
    public class InCompletePaymentMethod
    {
        [DefaultValue("CPR")]
        public  MethodType Method { get; set; }

        [Range(1, int.MaxValue)]
        public  int? Number { get; set; }
    }
}
