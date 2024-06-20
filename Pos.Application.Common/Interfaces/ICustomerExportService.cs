namespace Pos.Application.Common.Interfaces
{
    public interface ICustomerExportService
    {
        Task<IEnumerable<CustomerExportDto>> GetCustomersForExportAsync();
    }
}
