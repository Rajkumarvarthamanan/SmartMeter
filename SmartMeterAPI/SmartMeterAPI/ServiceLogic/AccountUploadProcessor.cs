using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using SmartMeterAPI.Dto;
using AutoMapper;
using SmartMeterAPI.Data;

namespace SmartMeterAPI.ServiceLogic
{
    public class AccountUploadProcessor : IAccountUploadProcessor
    {
        private readonly IMapper _mapper;
        private readonly SmartMeterContext _context;
        private readonly ILogger<AccountUploadProcessor> _logger;

        public AccountUploadProcessor(IMapper mapper , SmartMeterContext context, ILogger<AccountUploadProcessor> logger)
        {
           _mapper = mapper;
           _context = context;
           _logger = logger;  
        }
        public void Upload(string path)
        {
            try
            {
                using var streamReader = new StreamReader(path);
                using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                var records = csvReader.GetRecords<AccountDto>().ToList();
                foreach (var record in records)
                {
                     _context.Accounts.Add(_mapper.Map<Account>(record));
                     _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Account details seed data upload failed due to error ", ex);
            }
        }
    }
}
