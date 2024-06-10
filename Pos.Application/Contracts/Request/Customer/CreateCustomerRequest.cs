namespace Pos.Application.Contracts.Request.Customer
{
    public class CreateCustomerRequest
    {

        public string Name { get; set; }
        public  string Email { get; set; }
        public string Address { get; set; }

        public string CompanyName { get; set; }
        public DateTime? Date { get; set; }

    }
}
