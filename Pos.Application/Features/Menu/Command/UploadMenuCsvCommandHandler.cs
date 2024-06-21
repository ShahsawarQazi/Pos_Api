using MediatR;
using Microsoft.AspNetCore.Http;
using Pos.Application.Common.Interfaces;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Pos.Application.Features.Menu.Command;

public class UploadMenuCsvCommand : IRequest<bool>
{
    public IFormFile CsvFile { get; set; }

    public UploadMenuCsvCommand(IFormFile csvFile)
    {
        CsvFile = csvFile;
    }
}
public class UploadMenuCsvCommandHandler : IRequestHandler<UploadMenuCsvCommand, bool>
{
    private readonly IMenuRepository _menuRepository;

    public UploadMenuCsvCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<bool> Handle(UploadMenuCsvCommand request, CancellationToken cancellationToken)
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
            var records = csv.GetRecords<MenuCsvRecord>().ToList();

            foreach (var record in records)
            {
                var existingMenu = await _menuRepository.FindByMenu(record.Name);
                if (existingMenu == null)
                {
                    // Add new record if not found
                    var newMenu = new Domain.Entities.Entities.Menu
                    {
                        Name = record.Name,
                        Item = record.Item,
                        Variant = record.Variant,
                        Size = record.Size,
                        price = record.price
                    };
                    await _menuRepository.AddMenu(newMenu);
                }
                else if (HasDifferences(existingMenu, record))
                {
                    // Update existing record if differences found
                    existingMenu.Name = record.Name;
                    existingMenu.Item = record.Item;
                    existingMenu.Variant = record.Variant;
                    existingMenu.Size = record.Size;
                    existingMenu.price = record.price;

                    await _menuRepository.UpdateMenu(existingMenu);
                }
            }

            await _menuRepository.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    private bool HasDifferences(Domain.Entities.Entities.Menu existingMenu, MenuCsvRecord csvRecord)
    {
        return existingMenu.Name != csvRecord.Name ||
               existingMenu.Item != csvRecord.Item ||
               existingMenu.Variant != csvRecord.Variant ||
               existingMenu.Size != csvRecord.Size ||
               existingMenu.price != csvRecord.price;
    }

    private class MenuCsvRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Item { get; set; }
        public string Variant { get; set; }
        public string Size { get; set; }
        public double price { get; set; }
    }


}