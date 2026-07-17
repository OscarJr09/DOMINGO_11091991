using CsvHelper;
using FileProcessor.Interfaces;
using FileProcessor.Models;
using System.Globalization;

namespace FileProcessor.Services
{
    public class FileProcessService : IFileProcessService
    {
        public FileProcessorRecord ProcessFile(IFormFile file)
        {
            FileProcessorRecord record = new();

            try
            {
                record = new FileProcessorRecord
                {
                    FileName = file.FileName,
                    UploadTimestamp = DateTime.UtcNow,
                    Status = "Processing"
                };

                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<Product>().ToList();

                if (records != null && records.Count > 0)
                {
                    record.ProcessedProducts = records;
                    record.AveragePrice = records.Average(p => p.Price);
                }

                record.Status = "Success";
                record.ProcessedProductCount = records?.Count ?? 0;

                return record;
            }
            catch (Exception ex)
            {
                record.Status = "Failed";
                record.ErrorMessage = ex.Message;
            }

            return record;
        }
    }
}
