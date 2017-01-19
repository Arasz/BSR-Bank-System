using Core.Common.AccountNumber.Parser;
using FluentAssertions;
using Xunit;

namespace SharedTests
{
    public class AccountNumberParserTest
    {
        [Theory]
        [InlineData("78112241008528164913108077", "78", "11224100", "8528164913108077")]
        public void ParseAccountNumber_CheckAccountNumberObject_ShouldBeEqualToGivenNumber(string accountNumber, string checksum, string bankId, string accountId)
        {
            var parser = new AccountNumberParser();

            var parsedAccountNumber = parser.Parse(accountNumber);

            parsedAccountNumber.Checksum.Should()
                .HaveLength(2)
                .And.Be(checksum);

            parsedAccountNumber.BankId.Should()
                .HaveLength(8)
                .And.Be(bankId);

            parsedAccountNumber.AccountId.Should()
                .HaveLength(16)
                .And.Be(accountId);
        }
    }
}