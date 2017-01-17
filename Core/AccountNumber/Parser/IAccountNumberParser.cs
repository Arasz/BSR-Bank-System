namespace Shared.AccountNumber.Parser

{
    public interface IAccountNumberParser
    {
        BankAccountNumber Parse(string accountNumber);
    }
}