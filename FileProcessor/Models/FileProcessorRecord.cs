namespace FileProcessor.Models
{
    public class FileProcessorRecord
    {
        public string FileName { get; set; }
        public DateTime UploadTimestamp { get; set; }
        public string Status { get; set; }
        public double AveragePrice { get; set; }
        public int ProcessedProductCount { get; set; }
        public List<Product> ProcessedProducts { get; set; }
        public string ErrorMessage { get; set; }
    }
}
