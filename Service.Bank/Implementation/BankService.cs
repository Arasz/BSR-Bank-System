using Core.CQRS.Bus;
using Data.Core.Entities;
using Service.Bank.Commands;
using Service.Bank.Queries;
using Service.Bank.Router;
using Service.Contracts;
using Service.Dto;
using System.Collections.Generic;

namespace Service.Bank.Implementation
{
    public class BankService : IBankService
    {
        private readonly IBus _commonBus;
        private readonly IExternalTransferRouter _externalTransferRouter;

        public BankService(IBus commonBus, IExternalTransferRouter externalTransferRouter)
        {
            _commonBus = commonBus;
            _externalTransferRouter = externalTransferRouter;
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            _commonBus.Send(new DepositCommand(accountNumber, amount));
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            _externalTransferRouter.Route(transferDescription);
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            _commonBus.Send(new InternalTransferCommand(transferDescription));
        }

        public User Login(string userName, string password)
        {
            return _commonBus.QueryBus.Send<User, UserQuery>(new UserQuery(userName, password));
        }

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            return
                _commonBus.Send<IEnumerable<Operation>, AccountOperationsHistoryQuery>(
                    new AccountOperationsHistoryQuery(accountHistoryQuery));
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            _commonBus.Send(new WithdrawCommand(accountNumber, amount));
        }
    }
}