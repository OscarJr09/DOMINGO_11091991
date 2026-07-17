using CsvHelper;
using FileProcessor.Interfaces;
using FileProcessor.Models;
using System.Globalization;

namespace FileProcessor.Services
{
    public class FileProcessService : IFileProcessService
    {
        private readonly ILogger<IFileProcessService> _logger;
        private readonly IFileTrackingService _fileTrackingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="fileTrackingService"></param>
        public FileProcessService(ILogger<IFileProcessService> logger,
            IFileTrackingService fileTrackingService)
        {
            _logger = logger;
            _fileTrackingService = fileTrackingService;
        }

        public async Task<FileProcessorRecord> ProcessFileAsync(IFormFile file)
        {
            _logger.LogInformation("ProcessFile start...");

            FileProcessorRecord record = new();

            try
            {
                record = new FileProcessorRecord
                {
                    FileName = file.FileName,
                    UploadTimestamp = DateTime.UtcNow,
                    Status = "Processing"
                };

                _logger.LogInformation("ProcessFile OpenReadStream...");

                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                _logger.LogInformation("ProcessFile CSV GetRecords...");

                var records = csv.GetRecords<Product>().ToList();

                if (records != null && records.Count > 0)
                {
                    record.ProcessedProducts = records;
                    record.AveragePrice = records.Average(p => p.Price);
                }

                record.Status = "Success";
                record.ProcessedProductCount = records?.Count ?? 0;

                await _fileTrackingService.TrackFileUploadAsync(record);

                _logger.LogInformation($"ProcessFile Record Status: {record?.Status}...");
            }
            catch (Exception ex)
            {
                record.Status = "Failed";
                record.ErrorMessage = ex.Message;

                _logger.LogError(ex.Message, ex);
            }

            _logger.LogInformation("ProcessFile end ...");

            return record!;
        }
    }
}
