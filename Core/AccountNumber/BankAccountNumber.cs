namespace Shared.AccountNumber
{
    public struct BankAccountNumber
    {
        public string AccountId { get; }

        public string BankId { get; }

        public string Checksum { get; }

        public string FullNumber => Checksum + BankId + AccountId;

        public BankAccountNumber(string accountId, string bankId, string checksum)
        {
            AccountId = accountId;
            BankId = bankId;
            Checksum = checksum;
        }
    }
}