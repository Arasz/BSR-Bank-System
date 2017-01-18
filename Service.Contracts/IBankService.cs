using System.Collections.Generic;
using System.ServiceModel;
using Data.Core;
using Service.Dto;
using Shared.Transfer;

namespace Service.Contracts
{
    /// <summary>
    /// Responsible for user bank account management 
    /// </summary>
    [ServiceContract]
    public interface IBankService
    {
        /// <summary>
        /// Returns logged user 
        /// </summary>
        /// <returns> Authentication token </returns>
        [OperationContract]
        User Authentication(string userName, string password);

        /// <summary>
        /// Increases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Deposit(string accountNumber, decimal amount);

        /// <summary>
        /// Transfers money between accounts from different banks 
        /// </summary>
        /// <param name="transferDescription"> All necessary informations for transfer </param>
        [OperationContract]
        void ExternalTransfer(TransferDescription transferDescription);

        /// <summary>
        /// Transfers money between accounts in the same bank 
        /// </summary>
        /// <param name="transferDescription"> All necessary informations for transfer </param>
        [OperationContract]
        void InternalTransfer(TransferDescription transferDescription);

        /// <summary>
        /// </summary>
        /// <param name="accountHistoryQuery"> Query for operation history </param>
        /// <returns> History of operations </returns>
        [OperationContract]
        IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery);

        /// <summary>
        /// Decreases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Withdraw(string accountNumber, decimal amount);
    }
}