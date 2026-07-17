using FileProcessor.Models;

namespace FileProcessor.Interfaces
{
    public interface IFileProcessService
    {
        public FileProcessorRecord ProcessFile(IFormFile file);
    }
}
