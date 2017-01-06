using System.Collections.Generic;
using Data.Core;
using Service.Bank.Contract;
using Service.Bank.History.Queries;

using Shared.Transfer;

namespace Service.Bank.Implementation
{
    public class BankService : IBankService
    {
        public string Authentication(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Deposit(decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            throw new System.NotImplementedException();
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Operation> OperationsHistory(HistoryQuery operationsHistoryQuery)
        {
            throw new System.NotImplementedException();
        }

        public void Withdraw(decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}