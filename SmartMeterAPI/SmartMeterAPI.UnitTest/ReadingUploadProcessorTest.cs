using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMeterAPI.Data;
using SmartMeterAPI.ServiceLogic;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SmartMeterAPI.UnitTest
{
    public class ReadingUploadProcessorTest
    {
        private readonly Mock<ILogger<ReadingUploadProcessor>> loggerStub;

        private readonly Mock<IMapper> mapperStub;

        private readonly SmartMeterContext _context;

        public ReadingUploadProcessorTest()
        {
            loggerStub = new Mock<ILogger<ReadingUploadProcessor>>();
            mapperStub = new Mock<IMapper>();
            _context = new SmartMeterContext(new DbContextOptionsBuilder<SmartMeterContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
        }


        [Fact]
        public async Task Upload_ErrorInSaving_ReturnsExceptionAsync()
        {
            var readingUploadProcessor = new ReadingUploadProcessor(mapperStub.Object, _context, loggerStub.Object);
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("\\bin\\Debug\\net6.0", "\\");
            string path = Path.Combine(dir, "testfiles\\invalid_reading_testfile.csv");
            await readingUploadProcessor.Upload(path);
            loggerStub.Verify(
            m => m.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Error in getting data from CSV and validating")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
        }

        [Fact]
        public void ValueValidation_valid_ReturnsTrue()
        {
            var readingUploadProcessor = new ReadingUploadProcessor(mapperStub.Object, _context, loggerStub.Object);
            var result = readingUploadProcessor.ValueValidation("12345");
            Assert.True(result);

        }

        [Fact]
        public void ValueValidation_valid_ReturnsFalse()
        {
            var readingUploadProcessor = new ReadingUploadProcessor(mapperStub.Object, _context, loggerStub.Object);
            var result = readingUploadProcessor.ValueValidation("abcde");
            Assert.False(result);
        }



    }
}
