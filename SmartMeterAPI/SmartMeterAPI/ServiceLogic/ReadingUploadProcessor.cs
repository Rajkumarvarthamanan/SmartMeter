using AutoMapper;
using CsvHelper;
using SmartMeterAPI.Data;
using SmartMeterAPI.Dto;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SmartMeterAPI.ServiceLogic
{
    public class ReadingUploadProcessor : IReadingUploadProcessor
    {
        private readonly IMapper _mapper;
        private readonly SmartMeterContext _context;
        private readonly ILogger<ReadingUploadProcessor> _logger;

        public ReadingUploadProcessor(IMapper mapper, SmartMeterContext context, ILogger<ReadingUploadProcessor> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public async Task<string> Upload(string path)
        {
            int success = 0, failure = 0;
            try
            {
                using var streamReader = new StreamReader(path);
                using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                var records = csvReader.GetRecords<ReadingDto>().ToList();
                foreach (var record in records)
                {
                    if (this.Validate(record) && this.NewReadingValidator(record))
                    {
                        await _context.MeterReadings.AddAsync(_mapper.Map<MeterReading>(record));
                        await _context.SaveChangesAsync();
                        success++;

                    }
                    else
                    {
                        failure++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting data from CSV and validating", ex);
            }
            return $"{success} values inserted successfully and {failure} failed.";
        }

        private bool NewReadingValidator(ReadingDto record)
        {
            if (_context.MeterReadings.Any(i => i.AccountId == record.AccountId))
            {
                int oldValue = _context.MeterReadings.Where(i => i.AccountId == record.AccountId)
                               .Select(i => i.MeterReadValue).FirstOrDefault();
                if (oldValue < int.Parse(record.MeterReadValue))
                {
                    return true;
                }
                else
                {
                    _logger.LogWarning("New Meter reading is less than available reading for account", record.AccountId);
                    return false;
                }
            }
            return true;
        }

        private bool Validate(ReadingDto record)
        {
            bool result = (this.ValueValidation(record.MeterReadValue) && !this.DuplicateValidation(record) && this.AccountIDValidation(record.AccountId));
            if (!result)
            {
                _logger.LogWarning("Validation failed for account number , please check all details related to this account", record.AccountId);
            }
            return result;
        }

        private bool AccountIDValidation(int accountId)
        {
            bool result = _context.Accounts.Any(i => i.AccountId == accountId);
            return result;
        }

        private bool DuplicateValidation(ReadingDto record)
        {
            bool result = _context.MeterReadings.Any(i => i.AccountId == record.AccountId &&
             i.MeterReadValue == int.Parse(record.MeterReadValue));
            return result;
        }

        public bool ValueValidation(string value)
        {
            var regex = Regex.Match(value, @"[0-9]{5}");
            bool result = regex.Success;
            return result;
        }
    }
}
