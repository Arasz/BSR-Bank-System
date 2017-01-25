using Service.Dto;

namespace Service.Bank.Proxy
{
    public interface IInterbankTransferServiceProxy
    {
        /// <summary>
        /// Sends transfer order to other bank 
        /// </summary>
        void Transfer(InterbankTransferDescription transferDescription);
    }
}