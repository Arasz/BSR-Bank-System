namespace Core.Common.AccountNumber.Parser
{
    public class AccountNumberParser : IAccountNumberParser
    {
        private const int BankIdLength = 8;
        private const int ControlSumLength = 2;

        public BankAccountNumber Parse(string accountNumber)
        {
            var checksum = accountNumber.Substring(0, ControlSumLength);
            var bankId = accountNumber.Substring(ControlSumLength, BankIdLength);
            var accountId = accountNumber.Substring(ControlSumLength + BankIdLength);

            return new BankAccountNumber(accountId, bankId, checksum);
        }
    }
}