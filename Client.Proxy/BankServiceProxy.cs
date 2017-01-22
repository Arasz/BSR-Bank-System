﻿using Data.Core;
using Service.Contracts;
using Service.Dto;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Client.Proxy
{
    public class BankServiceProxy : ClientBase<IBankService>, IBankService
    {
        public Task<User> AuthenticationAsync(string userName, string password)
            => Task.Run(() => Channel.Login(userName, password));

        public void Deposit(string accountNumber, decimal amount) => Channel.Deposit(accountNumber, amount);

        public Task DepositAsync(string accountNumber, decimal amount)
            => Task.Run(() => Channel.Deposit(accountNumber, amount));

        public void ExternalTransfer(TransferDescription transferDescription) => Channel.ExternalTransfer(transferDescription);

        public Task ExternalTransferAsync(TransferDescription transferDescription)
            => Task.Run(() => Channel.ExternalTransfer(transferDescription));

        public void InternalTransfer(TransferDescription transferDescription) => Channel.InternalTransfer(transferDescription);

        public Task InternalTransferAsync(TransferDescription transferDescription)
            => Task.Run(() => Channel.InternalTransfer(transferDescription));

        public User Login(string userName, string password) => Channel.Login(userName, password);

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery) => Channel.OperationsHistory(accountHistoryQuery);

        public Task<IEnumerable<Operation>> OperationsHistoryAsync(AccountHistoryQuery accountHistoryQuery)
            => Task.Run(() => Channel.OperationsHistory(accountHistoryQuery));

        public void Withdraw(string accountNumber, decimal amount) => Channel.Withdraw(accountNumber, amount);

        public Task WithdrawAsync(string accountNumber, decimal amount)
            => Task.Run(() => Withdraw(accountNumber, amount));
    }
}