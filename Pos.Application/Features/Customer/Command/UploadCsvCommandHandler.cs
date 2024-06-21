using MediatR;
using Microsoft.AspNetCore.Http;
using Pos.Application.Common.Interfaces;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Pos.Application.Features.Customer.Command;

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
        try
        {
            using var reader = new StreamReader(request.CsvFile.OpenReadStream());
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<CustomerCsvRecord>().ToList();

            foreach (var record in records)
            {
                var existingCustomer = await _customerRepository.FindByEmail(record.Email);
                if (existingCustomer != null)
                {
                    if (HasDifferences(existingCustomer, record))
                    {
                        // Update existing customer
                        existingCustomer.Name = record.Name;
                        existingCustomer.PhoneNo = record.PhoneNo;
                        existingCustomer.CompanyName = record.CompanyName;
                        existingCustomer.Address = record.Address;

                        _customerRepository.Update(existingCustomer);
                    }
                }
                else
                {
                    // Add new customer
                    var newCustomer = new Domain.Entities.Entities.Customer
                    {
                        Name = record.Name,
                        PhoneNo = record.PhoneNo,
                        Email = record.Email,
                        CompanyName = record.CompanyName,
                        Address = record.Address
                    };

                    await _customerRepository.Add(newCustomer);
                }
            }

            await _customerRepository.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use any logging framework)
            // For example: _logger.LogError(ex, "An error occurred while uploading the CSV.");
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    private bool HasDifferences(Domain.Entities.Entities.Customer existingCustomer, CustomerCsvRecord csvRecord)
    {
        return existingCustomer.Name != csvRecord.Name ||
               existingCustomer.PhoneNo != csvRecord.PhoneNo ||
               existingCustomer.CompanyName != csvRecord.CompanyName ||
               existingCustomer.Address != csvRecord.Address;
    }

    private class CustomerCsvRecord
    {
        public string Name { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
    }
}