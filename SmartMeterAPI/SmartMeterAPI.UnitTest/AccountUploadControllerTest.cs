using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMeterAPI.Controllers;
using SmartMeterAPI.Helpers;
using SmartMeterAPI.ServiceLogic;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace SmartMeterAPI.UnitTest
{
    public class AccountUploadControllerTest
    {
        private readonly Mock<ISaveFile> saveFileStub = new();

        private readonly Mock<ILogger<AccountUploadController>> loggerStub = new();

        private readonly Mock<IAccountUploadProcessor> processorStub = new();


        [Fact]
        public void AccountUploadAsync_WithNoFile_ReturnsBadRequest()
        {
            
            saveFileStub.Setup(x => x.SaveFileToTempLLocation(It.IsAny<IFormFile>())).Throws(new Exception());
            
            var accountUploadController = new AccountUploadController(processorStub.Object, saveFileStub.Object, loggerStub.Object);

            var result = accountUploadController.AccountUploadAsync(It.IsAny<IFormFile>());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AccountUploadAsync_WithFileProcessErrorr_ReturnsBadRequest()
        {

            processorStub.Setup(x =>x.Upload(It.IsAny<string>())).Throws(new Exception());

            var accountUploadController = new AccountUploadController(processorStub.Object, saveFileStub.Object, loggerStub.Object);

            var result = accountUploadController.AccountUploadAsync(It.IsAny<IFormFile>());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AccountUploadAsync_WithFile_ReturnsOK()
        {
            var accountUploadController = new AccountUploadController(processorStub.Object, saveFileStub.Object, loggerStub.Object);

            var result = accountUploadController.AccountUploadAsync(It.IsAny<IFormFile>());

            Assert.IsType<OkObjectResult>(result);
        }
    }
}