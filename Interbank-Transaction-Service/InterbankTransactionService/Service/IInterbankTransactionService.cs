using InterbankTransactionService.DataStructures;
using InterbankTransactionService.Exceptions;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace InterbankTransactionService.Service
{
    /// <summary>
    /// Rest service for interbank transactions 
    /// </summary>
    [ServiceContract]
    public interface IInterbankTransactionService
    {
        /// <summary>
        /// Make transfer to local bank account 
        /// </summary>
        /// <param name="transferDescription"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "transfer")]
        [FaultContract(typeof(TransferValidationException))]
        void Transfer(InterbankTransferDescription transferDescription);
    }
}