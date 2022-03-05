using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMeterAPI.Controllers;
using SmartMeterAPI.Helpers;
using SmartMeterAPI.ServiceLogic;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartMeterAPI.UnitTest
{
    public class MeterReadingUploadControllerTest
    {
        private readonly Mock<ISaveFile> saveFileStub = new();

        private readonly Mock<ILogger<MeterReadingUploadController>> loggerStub = new();

        private readonly Mock<IReadingUploadProcessor> processorStub = new();

        [Fact]
        public async Task ReadingUploadAsync_WithNoFile_ReturnsBadRequest()
        {

            saveFileStub.Setup(x => x.SaveFileToTempLLocation(It.IsAny<IFormFile>())).Throws(new Exception());

            var meterReadingUploadController = new MeterReadingUploadController(processorStub.Object, saveFileStub.Object, loggerStub.Object);

            var result = await meterReadingUploadController.ReadingUploadAsync(It.IsAny<IFormFile>());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ReadingUploadAsync_WithFileProcessErrorr_ReturnsBadRequestAsync()
        {

            processorStub.Setup(x => x.Upload(It.IsAny<string>())).Throws(new Exception());

            var meterReadingUploadController = new MeterReadingUploadController(processorStub.Object, saveFileStub.Object, loggerStub.Object);

            var result = await meterReadingUploadController.ReadingUploadAsync(It.IsAny<IFormFile>());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ReadingUploadAsync_WithFile_ReturnsOKAsync()
        {
            var meterReadingUploadController = new MeterReadingUploadController(processorStub.Object, saveFileStub.Object, loggerStub.Object);

            var result = await meterReadingUploadController.ReadingUploadAsync(It.IsAny<IFormFile>());

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
