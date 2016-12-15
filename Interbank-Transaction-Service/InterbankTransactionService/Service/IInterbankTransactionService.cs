using InterbankTransactionService.DataStructers;
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
        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "transfer")]
        void Transfer(InterbankTransferDescription transferDescription);
    }
}