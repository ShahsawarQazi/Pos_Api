namespace Pos.Application.Contracts.Response.Customer
{
  
    public class GetCustomerResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string CompanyName { get; set; }
        public DateTime? Date { get; set; }

    }
}
