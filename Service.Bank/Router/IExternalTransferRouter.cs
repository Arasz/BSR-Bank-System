using Service.Dto;

namespace Service.Bank.Router
{
    public interface IExternalTransferRouter
    {
        /// <summary>
        /// Analyzes transfer description and selects correct processing route 
        /// </summary>
        void Route(TransferDescription routedTransferDescription);
    }
}