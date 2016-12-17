using AccountMangmentService.History.Queries;
using AccountMangmentService.Operations;
using AccountMangmentService.Transfer;
using System.Collections.Generic;
using System.ServiceModel;

namespace AccountMangmentService.Service
{
    /// <summary>
    /// Responsible for user bank account management 
    /// </summary>
    [ServiceContract]
    public interface IAccountManagmentService
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
        /// </summary>
        /// <param name="operationsHistoryQuery"> Query for operation history </param>
        /// <returns> History of operations </returns>
        [OperationContract]
        IEnumerable<Operation> OperationsHistory(HistoryQuery operationsHistoryQuery);

        /// <summary>
        /// Performs payment specified by payment command 
        /// </summary>
        /// <param name="transferDescription"></param>
        [OperationContract]
        void Transfer(TransferDescription transferDescription);

        /// <summary>
        /// Decreases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Withdraw(decimal amount);
    }
}