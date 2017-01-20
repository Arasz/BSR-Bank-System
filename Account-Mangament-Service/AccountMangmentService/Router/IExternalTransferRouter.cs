using Service.Dto;

namespace Service.Bank.Router
{
    public interface IExternalTransferRouter
    {
        void Route(TransferDescription routedTransferDescription);
    }
}