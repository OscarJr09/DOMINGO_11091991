namespace FileProcessor.Helpers
{
    public class ConfigurationHelper
    {
        private static IConfigurationRoot? _configuration;

        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var directory = Directory.GetCurrentDirectory();
                    _configuration = new ConfigurationBuilder().SetBasePath(directory).AddJsonFile("appsettings.json").Build();
                }

                return _configuration;
            }
        }

        public static string GetFileUploadApiKey
        {
            get { { return Configuration["FileUpload:ApiKey"]!; } }
        }

    }
}
