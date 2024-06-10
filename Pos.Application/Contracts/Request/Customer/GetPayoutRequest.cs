using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pos.Application.Common;

namespace Pos.Application.Contracts.Request.Customer
{
    public class GetPayoutRequest
    {
        /// <summary>
        /// unique id of client making Payout request, it is configured in database of service
        /// </summary>
        [Range(1, int.MaxValue)]
        public required int ClientId { get; set; }


        /// Required, start date of the transaction.
        /// </summary>
        public required DateRange? DateRange { get; set; }

        /// <summary>
        /// Number of records that will be shown 
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; }
        public required string Region { get; set; }
        public required string FirstApprover { get; set; }
        public required string SecondApprover { get; set; }
        /// <summary>
        /// <summary>
        /// Number of records per page
        /// </summary>
        [DefaultValue(1)]
        public int PageNumber { get; set; }
    }
}
