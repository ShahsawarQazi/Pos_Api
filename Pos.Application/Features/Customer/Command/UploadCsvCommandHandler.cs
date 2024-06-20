using MediatR;
using Microsoft.AspNetCore.Http;
using Pos.Application.Common.Interfaces;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Pos.Application.Features.Customer.Command
{
    public class UploadCsvCommand : IRequest<bool>
    {
        public IFormFile CsvFile { get; set; }

        public UploadCsvCommand(IFormFile csvFile)
        {
            CsvFile = csvFile;
        }
    }
    public class UploadCsvCommandHandler : IRequestHandler<UploadCsvCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UploadCsvCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UploadCsvCommand request, CancellationToken cancellationToken)
        {
            using var reader = new StreamReader(request.CsvFile.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<Domain.Entities.Entities.Customer>();

            foreach (var record in records)
            {
                // You may want to add additional validation here
                await _customerRepository.AddAsync(record);
            }

            await _customerRepository.SaveChangesAsync();

            return true;
        }
    }
}
