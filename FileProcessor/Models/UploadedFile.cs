namespace FileProcessor.Models
{
    public class FileUploadTracking
    {
        public double FileUploadedCount { get; set; }
        public List<UploadedFile> UploadedFileList { get; set; }
    }

    public class UploadedFile
    {
        public string FileName { get; set; }
        public DateTime UploadTimestamp { get; set; }
    }
}
