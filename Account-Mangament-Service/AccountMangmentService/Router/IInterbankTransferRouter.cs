using Service.Dto;

namespace Service.Bank.Router
{
    public interface IInterbankTransferRouter
    {
        void Route(TransferDescription routedTransferDescription);
    }
}