using Data.Core;
using Service.Contracts;
using Service.Dto;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Client.Proxy.BankService
{
    public interface IBankServiceProxy : IBankService
    {
        ClientBase<IBankService> ClientBase { get; }

        Task<User> LoginAsync(string userName, string password);

        void Deposit(string accountNumber, decimal amount);

        Task DepositAsync(string accountNumber, decimal amount);

        void ExternalTransfer(TransferDescription transferDescription);

        Task ExternalTransferAsync(TransferDescription transferDescription);

        void InternalTransfer(TransferDescription transferDescription);

        Task InternalTransferAsync(TransferDescription transferDescription);

        User Login(string userName, string password);

        IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery);

        Task<IEnumerable<Operation>> OperationsHistoryAsync(AccountHistoryQuery accountHistoryQuery);

        void Withdraw(string accountNumber, decimal amount);

        Task WithdrawAsync(string accountNumber, decimal amount);
    }
}