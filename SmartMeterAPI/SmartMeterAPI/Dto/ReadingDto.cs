using CsvHelper.Configuration.Attributes;

namespace SmartMeterAPI.Dto
{ 
    public class ReadingDto
    {
        [Name("AccountId")]
        public int AccountId { get; set; }

        [Name("MeterReadingDateTime")]
        public string MeterReadingDateTime { get; set; }

        [Name("MeterReadValue")]
        public string MeterReadValue { get; set; }
    }
}
