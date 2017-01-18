using Service.Dto;

namespace Service.Bank.Proxy.ServiceHttpClient
{
    public interface ITransferServiceHttpClient
    {
        void SendTransfer(InterbankTransferDescription transferDescription, string transferServiceAddress);
    }
}