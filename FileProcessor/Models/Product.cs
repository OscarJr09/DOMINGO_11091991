using System.Drawing;

namespace FileProcessor.Models
{
    public class Product
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public int Stock { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Availability { get; set; }
    }
}
