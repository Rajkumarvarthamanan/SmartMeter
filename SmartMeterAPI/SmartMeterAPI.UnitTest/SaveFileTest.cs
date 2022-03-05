using Microsoft.AspNetCore.Http;
using Moq;
using SmartMeterAPI.Helpers;
using System;
using System.IO;
using Xunit;

namespace SmartMeterAPI.UnitTest
{
    public class SaveFileTest
    {
        [Fact]
        public void SaveFileToTempLLocation_Complete_ReturnPath()
        {
            var fileMock = new Mock<IFormFile>();
            //Setting up file
            //Setup mock file using a memory stream
            var content = "12345,abcd,12345";
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var sut = new SaveFile();
            var result = sut.SaveFileToTempLLocation(fileMock.Object);
            Assert.NotEmpty(result);
        }

    }
}
