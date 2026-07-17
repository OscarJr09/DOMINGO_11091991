using FileProcessor.Models;

namespace FileProcessor.Interfaces
{
    public interface IFileProcessService
    {
        public Task<FileProcessorRecord> ProcessFileAsync(IFormFile file);
    }
}
