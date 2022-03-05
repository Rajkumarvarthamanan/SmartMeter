using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMeterAPI.Helpers;
using SmartMeterAPI.ServiceLogic;

namespace SmartMeterAPI.Controllers
{
    [ApiController]
    public class MeterReadingUploadController : ControllerBase
    {
        private readonly ISaveFile _saveFile;
        private readonly IReadingUploadProcessor _readingUploadProcessor;
        private readonly ILogger<MeterReadingUploadController> _logger;
        public MeterReadingUploadController(IReadingUploadProcessor readingUploadProcessor, ISaveFile saveFile, ILogger<MeterReadingUploadController> logger)
        {
            _readingUploadProcessor = readingUploadProcessor;
            _saveFile = saveFile;
            _logger = logger;
        }

        [HttpPost("api/meter-reading-uploads", Name = "meter-reading-uploads")]
        public async Task<IActionResult> ReadingUploadAsync(IFormFile file)
        {
            try
            {
                var path = _saveFile.SaveFileToTempLLocation(file);
                var result = await _readingUploadProcessor.Upload(path);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Meter Reading Uplod Failed", ex.Message);
                return BadRequest("Check File uploaded");
            }
        }
    }
}
