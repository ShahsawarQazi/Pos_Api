using System.ComponentModel;
using Pos.Domain.Entities.Enums;

namespace Pos.Application.Contracts.Response.Customer
{
    public class GetPayoutsResponse
    {
        [DefaultValue(10)]
        public required int TotalCount { get; set; }
        /// <summary>
        /// API generated Unique Id for payment
        /// </summary>
        public required List<GetPayoutResponse> Payouts { get; set; }

    }
    public class GetPayoutResponse
    {
        [DefaultValue(10.20)]
        public required decimal Amount { get; set; }
        /// <summary>
        /// API generated Unique Id for Payout
        /// </summary>
        public required Guid PayoutId { get; set; }

        public MethodType Method { get; set; }
        
        public int? Number { get; set; }
        public int? RegNo { get; set; }
        public int? AccountNo { get; set; }
        
        public string? Iban { get; set; }
        public string? Bic { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// Unique Id provided by Client at the time of Payout creation
        /// </summary>
        [DefaultValue(null)]
        public string? ReceiptId { get; set; }

    }
}
