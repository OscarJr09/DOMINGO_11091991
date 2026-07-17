using FileProcessor.Helpers;

namespace FileProcessor.Middlewares
{
    public class ApiKeyValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        private const string API_KEY_HEADER = "X-Api-Key";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ApiKeyValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var fileUploadApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("File Upload API Key was not provided.");
                    return;
                }

                // In production, retrieve this from a secure configuration or Key Vault
                var fileUploadAPIKey = ConfigurationHelper.GetFileUploadApiKey;

                if (!fileUploadAPIKey.Equals(fileUploadApiKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized client.");
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
