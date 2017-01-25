using Service.Dto;
using System.ServiceModel;
using System.ServiceModel.Web;

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
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "transfer", ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(WebFaultException))]
        void Transfer(InterbankTransferDescription transferDescription);
    }
}