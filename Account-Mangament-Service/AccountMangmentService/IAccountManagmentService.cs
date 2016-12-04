using AccountMangmentService.History;
using AccountMangmentService.Operations;
using System.Collections.Generic;
using System.ServiceModel;

namespace AccountMangmentService
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
        /// </summary>
        /// <param name="operationsHistoryQuery"> Query for operation history </param>
        /// <returns> History of operations </returns>
        [OperationContract]
        IEnumerable<IOperation> OperationsHistory(HistoryQuery operationsHistoryQuery);

        /// <summary>
        /// Performs payment specified by payment command 
        /// </summary>
        /// <param name="paymentCommand"></param>
        [OperationContract]
        void Payment(IPaymentCommand paymentCommand);

        /// <summary>
        /// Increases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Payment(decimal amount);

        /// <summary>
        /// Decreases user account balance by given amount 
        /// </summary>
        [OperationContract]
        void Withdraw(decimal amount);
    }
}