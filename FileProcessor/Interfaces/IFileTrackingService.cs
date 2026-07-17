using FileProcessor.Models;

namespace FileProcessor.Interfaces
{
    public interface IFileTrackingService
    {
        public Task TrackFileUploadAsync(FileProcessorRecord record);
    }
}
