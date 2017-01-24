using Service.Dto;

namespace Service.Bank.Proxy
{
    public interface IInterbankTransferServiceProxy
    {
        void Transfer(InterbankTransferDescription transferDescription);
    }
}