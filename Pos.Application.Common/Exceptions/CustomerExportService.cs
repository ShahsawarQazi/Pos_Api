using Pos.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Application.Common.Exceptions
{
    public class CustomerExportService : ICustomerExportService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerExportService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerExportDto>> GetCustomersForExportAsync()
        {
            var customers = await _customerRepository.GetAll();
            return customers.Select(c => new CustomerExportDto
            {
                Name = c.Name,
                PhoneNo = c.PhoneNo,
                Email = c.Email,
                CompanyName = c.CompanyName,
                Address = c.Address
            });
        }
    }
}
