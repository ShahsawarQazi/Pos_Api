using Microsoft.AspNetCore.Http;

namespace Pos.Application.Contracts.Request.Customer
{
    public class UploadCsvRequest
    {
        public IFormFile CsvFile { get; set; }
    }
}
