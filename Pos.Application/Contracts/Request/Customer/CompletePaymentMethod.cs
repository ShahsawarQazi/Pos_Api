using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pos.Domain.Entities.Enums;

namespace Pos.Application.Contracts.Request.Customer
{
    public class CompletePaymentMethod
    {
        [DefaultValue("Domestic")]
        public  MethodType Method { get; set; }

        [Range(1, int.MaxValue)]
        public  int? RegNo { get; set; }

        [Range(1, int.MaxValue)]
        public  int? AccountNo { get; set; }

        [DefaultValue("IHJ134567846254")]
        public  string? Iban { get; set; }

        [DefaultValue("BIC0145")]
        public  string? Bic { get; set; }
    }
}
