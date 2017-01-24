namespace Core.Common.AccountNumber.Parser

{
    public interface IAccountNumberParser
    {
        BankAccountNumber Parse(string accountNumber);
    }
}