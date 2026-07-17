using FileProcessor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileProcessorController : ControllerBase
    {
        private readonly IFileProcessService _fileProcessService;
        private readonly ILogger<FileProcessorController> _logger;

        public FileProcessorController(IFileProcessService fileProcessService, 
            ILogger<FileProcessorController> logger)
        {
            _fileProcessService = fileProcessService;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("FileUpload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
                    return BadRequest("Only .csv files are supported.");
                
                var record = _fileProcessService.ProcessFile(file);

                return Ok(new { Message = "File processed successfully.", Data = record });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
