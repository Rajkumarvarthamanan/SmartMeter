using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using SmartMeterAPI.Helpers;
using SmartMeterAPI.ServiceLogic;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SmartMeterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUploadController : ControllerBase
    {
        private readonly IAccountUploadProcessor _accountUploadProcessor;
        private readonly ISaveFile _saveFile;
        private readonly ILogger<AccountUploadController> _logger;   

        public AccountUploadController(IAccountUploadProcessor accountUploadProcessor, ISaveFile saveFile, ILogger<AccountUploadController> logger)
        {
            _accountUploadProcessor = accountUploadProcessor;
            _saveFile = saveFile;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AccountUploadAsync(IFormFile file)
        {
            try
            {
                var path = _saveFile.SaveFileToTempLLocation(file);
                _accountUploadProcessor.Upload(path);
                return Ok(200);
            }
            catch (Exception ex)
            {
                _logger.LogError("Account Upload failed", ex.Message);
                return BadRequest("Check File uploaded");
            }
        }

    }
}
