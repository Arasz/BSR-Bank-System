using System.Collections.Generic;
using Data.Core;
using Service.Bank.Queries;
using Service.Contracts;
using Service.Dto;
using Shared.Transfer;
using AccountHistoryQuery = Service.Dto.AccountHistoryQuery;

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

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            throw new System.NotImplementedException();
        }

        public void Withdraw(decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}