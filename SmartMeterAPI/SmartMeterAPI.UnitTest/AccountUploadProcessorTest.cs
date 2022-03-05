using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMeterAPI.Data;
using SmartMeterAPI.ServiceLogic;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace SmartMeterAPI.UnitTest
{
    public class AccountUploadProcessorTest
    {
        private readonly Mock<ILogger<AccountUploadProcessor>> loggerStub;

        private readonly Mock<IMapper> mapperStub;

        private readonly SmartMeterContext _context;

        public AccountUploadProcessorTest()
        {
            loggerStub = new Mock<ILogger<AccountUploadProcessor>>();
            mapperStub = new Mock<IMapper>();
            _context = new SmartMeterContext(new DbContextOptionsBuilder<SmartMeterContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
        }


        [Fact]
        public void Upload_ErrorInSaving_ReturnsException()
        {
            var accountUploadProcessor = new AccountUploadProcessor(mapperStub.Object,_context, loggerStub.Object);
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("\\bin\\Debug\\net6.0", "\\");
            string path = Path.Combine(dir, "testfiles\\invalid_account_testfile.csv");
            accountUploadProcessor.Upload(path);
            loggerStub.Verify(
            m => m.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Account details seed data upload failed due to error ")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
        }

        
    }
}
