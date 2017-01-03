using System.ServiceModel;
using System.ServiceModel.Web;
using Service.InterbankTransaction.Dto;
using Shared.Exceptions;

namespace Service.InterbankTransaction.Contract
{
    /// <summary>
    ///     Rest service for interbank transactions
    /// </summary>
    [ServiceContract]
    public interface IInterbankTransactionService
    {
        /// <summary>
        ///     Make transfer to local bank account
        /// </summary>
        /// <param name="transferDescription"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "transfer")]
        [FaultContract(typeof(WebFaultException<TransferDataFormatException>))]
        void Transfer(InterbankTransferDescription transferDescription);
    }
}