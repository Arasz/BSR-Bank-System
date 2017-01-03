using System.Collections.Generic;
using System.ServiceModel;
using Service.Bank.History.Queries;
using Service.Bank.Operations;
using Shared.Transfer;

namespace Service.Bank.Contract
{
    /// <summary>
    /// Responsible for user bank account management 
    /// </summary>
    [ServiceContract]
    public interface IBankService
    {
        /// <summary>
        /// Returns authentication token for given user 
        /// </summary>
        /// <returns> Authentication token </returns>
        [OperationContract]
        string Authentication(string userName, string password);

        /// <summary>
        /// Increases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Deposit(decimal amount);

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
        /// <param name="operationsHistoryQuery"> Query for operation history </param>
        /// <returns> History of operations </returns>
        [OperationContract]
        IEnumerable<Operation> OperationsHistory(HistoryQuery operationsHistoryQuery);

        /// <summary>
        /// Decreases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Withdraw(decimal amount);
    }
}