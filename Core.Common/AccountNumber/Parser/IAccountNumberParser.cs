namespace Core.Common.AccountNumber.Parser

{
    public interface IAccountNumberParser
    {
        /// <summary>
        /// Parse account number 
        /// </summary>
        BankAccountNumber Parse(string accountNumber);
    }
}