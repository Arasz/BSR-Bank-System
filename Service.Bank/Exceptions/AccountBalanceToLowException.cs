using System;
using System.Runtime.Serialization;

namespace Service.Bank.Exceptions
{
    [DataContract]
    public class AccountBalanceToLowException : Exception
    {
        public AccountBalanceToLowException(string accountNumber, decimal accountBalance, decimal transferAmount)
        {
            AccountNumber = accountNumber;
            AccountBalance = accountBalance;
            TransferAmount = transferAmount;
        }

        [DataMember]
        public decimal AccountBalance { get; }

        [DataMember]
        public string AccountNumber { get; }

        [DataMember]
        public override string Message => $"Not enough founds on account with number {AccountNumber}";

        [DataMember]
        public decimal TransferAmount { get; }
    }
}