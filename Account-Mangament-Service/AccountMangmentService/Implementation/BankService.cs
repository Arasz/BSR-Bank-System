using System.Collections.Generic;
using CQRS.Bus;
using Data.Core;
using Service.Bank.Commands;
using Service.Bank.Queries;
using Service.Bank.Router;
using Service.Contracts;
using Shared.Transfer;
using AccountHistoryQuery = Service.Dto.AccountHistoryQuery;

namespace Service.Bank.Implementation
{
    public class BankService : IBankService
    {
        private readonly IBus _commonBus;
        private readonly IInterbankTransferRouter _interbankTransferRouter;

        public BankService(IBus commonBus, IInterbankTransferRouter interbankTransferRouter)
        {
            _commonBus = commonBus;
            _interbankTransferRouter = interbankTransferRouter;
        }

        public string Authentication(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            _commonBus.Send(new DepositCommand(accountNumber, amount));
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            _interbankTransferRouter.Route(transferDescription);
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            _commonBus.Send(new InternalTransferCommand(transferDescription));
        }

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            return _commonBus.Send<IEnumerable<Operation>, AccountOperationsHistoryQuery>(new AccountOperationsHistoryQuery(accountHistoryQuery));
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            _commonBus.Send(new WithdrawCommand(accountNumber, amount));
        }
    }
}