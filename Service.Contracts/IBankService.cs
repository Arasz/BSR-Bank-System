using Data.Core.Entities;
using Service.Dto;
using System.Collections.Generic;
using System.ServiceModel;

namespace Service.Contracts
{
    /// <summary>
    /// Responsible for user bank account management 
    /// </summary>
    [ServiceContract]
    public interface IBankService
    {
        /// <summary>
        /// Increases user account balance by given amount 
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void Deposit(string accountNumber, decimal amount);

        /// <summary>
        /// Transfers money between accounts from different banks 
        /// </summary>
        /// <param name="transferDescription"> All necessary informations for transfer </param>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void ExternalTransfer(TransferDescription transferDescription);

        /// <summary>
        /// Transfers money between accounts in the same bank 
        /// </summary>
        /// <param name="transferDescription"> All necessary informations for transfer </param>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void InternalTransfer(TransferDescription transferDescription);

        /// <summary>
        /// Returns logged user 
        /// </summary>
        /// <returns> Authentication token </returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        User Login(string userName, string password);

        /// <summary>
        /// </summary>
        /// <param name="accountHistoryQuery"> Query for operation history </param>
        /// <returns> History of operations </returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        IEnumerable<Operation> OperationsHistory(AccountHistoryQuery accountHistoryQuery);

        /// <summary>
        /// Decreases user account balance by given amount 
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        void Withdraw(string accountNumber, decimal amount);
    }
}