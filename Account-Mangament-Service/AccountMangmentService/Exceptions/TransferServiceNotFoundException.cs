using System;

namespace Service.Bank.Exceptions
{
    public class TransferServiceNotFoundException : Exception
    {
        public TransferServiceNotFoundException(string bankId)
        {
            BankId = bankId;
        }

        public string BankId { get; }

        public override string Message => $"Transfer service for bank with id: {BankId} is not registered";
    }
}