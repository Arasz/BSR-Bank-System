using Service.Dto;

namespace Service.Bank.Proxy.ServiceHttpClient
{
    public interface ITransferServiceHttpClient
    {
        /// <summary>
        /// Sends transfer order over HTTP 
        /// </summary>
        void SendTransfer(InterbankTransferDescription transferDescription, string transferServiceAddress);
    }
}