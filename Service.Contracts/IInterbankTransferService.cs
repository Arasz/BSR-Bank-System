using System.ServiceModel;
using System.ServiceModel.Web;
using Core.Common.Exceptions;
using Service.Dto;

namespace Service.Contracts
{
    /// <summary>
    /// Rest service for interbank transactions 
    /// </summary>
    [ServiceContract]
    public interface IInterbankTransferService
    {
        /// <summary>
        /// Make transfer to local bank account 
        /// </summary>
        /// <param name="transferDescription"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "transfer")]
        [FaultContract(typeof(WebFaultException<TransferDataFormatException>))]
        void Transfer(InterbankTransferDescription transferDescription);
    }
}