namespace Pos.Application.Common
{
    public class DateRange
    {
        /// <summary>
        /// Required, start date of the transaction.
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Required, End date for the transaction.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
