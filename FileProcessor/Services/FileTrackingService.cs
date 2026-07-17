using FileProcessor.Interfaces;
using FileProcessor.Models;
using System.Text.Json;

namespace FileProcessor.Services
{
    public class FileTrackingService : IFileTrackingService
    {
        private readonly string _baseDir;
        private readonly string _filePath;

        private readonly ILogger<FileTrackingService> _logger;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FileTrackingService(ILogger<FileTrackingService> logger)
        {
            _baseDir = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            _filePath = Path.Combine($"{_baseDir}", "FileUploadTracking.json");
            _logger = logger;
        }

        public async Task TrackFileUploadAsync(FileProcessorRecord record)
        {
            _logger.LogInformation("TrackFileUpload start ...");

            try
            {
                _logger.LogInformation($"TrackFileUpload file path: {_filePath}");

                FileUploadTracking records = new()
                {
                    UploadedFileList = []
                };

                if (File.Exists(_filePath))
                {
                    using FileStream readStream = File.OpenRead(_filePath);
                    var existingData = await JsonSerializer.DeserializeAsync<FileUploadTracking>(readStream);
                    if (existingData != null)
                        records = existingData;
                }

                _logger.LogInformation($"TrackFileUpload - Uploaded file count: {records.FileUploadedCount}");

                var uploadedFile = new UploadedFile
                {
                    FileName = record.FileName,
                    UploadTimestamp = record.UploadTimestamp
                };

                records.UploadedFileList.Add(uploadedFile);
                records.FileUploadedCount++;

                using FileStream writeStream = File.Open(_filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                await JsonSerializer.SerializeAsync(writeStream, records, new JsonSerializerOptions() { WriteIndented = true });

                _logger.LogInformation($"TrackFileUpload - Uploaded File: {record.FileName} - Uploaded file count: {records.FileUploadedCount}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

            _logger.LogInformation("TrackFileUpload end ...");
        }
    }
}
