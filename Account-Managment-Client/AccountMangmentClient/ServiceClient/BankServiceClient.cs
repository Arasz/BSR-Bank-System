using System.Collections.Generic;
using System.ServiceModel;
using Data.Core;
using Service.Contracts;
using Service.Dto;

namespace Client.LightClient.ServiceClient
{
    public class BankServiceClient : ClientBase<IBankService>, IBankService
    {
        public BankServiceClient() : this("BankServiceEndpoint")
        {
        }

        protected BankServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public User Authentication(string userName, string password)
        {
            return Channel.Authentication(userName, password);
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            Channel.Deposit(accountNumber, amount);
        }

        public void ExternalTransfer(TransferDescription transferDescription)
        {
            Channel.ExternalTransfer(transferDescription);
        }

        public void InternalTransfer(TransferDescription transferDescription)
        {
            Channel.InternalTransfer(transferDescription);
        }

        public IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery)
        {
            return Channel.OperationsHistory(accountHistoryQuery);
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            Channel.Withdraw(accountNumber, amount);
        }
    }
}