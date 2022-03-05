using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SmartMeterAPI.Dto
{
    public class AccountDto
    {
        [Name("AccountId")]
        public int AccountId { get; set; }

        [Name("FirstName")]
        public string FirstName { get; set; }

        [Name("LastName")]
        public string LastName { get; set; }
    }
}
