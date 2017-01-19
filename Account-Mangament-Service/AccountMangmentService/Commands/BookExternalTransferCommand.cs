using Service.Dto;

namespace Service.Bank.Commands
{
    public class BookExternalTransferCommand : TransferCommand
    {
        public BookExternalTransferCommand(TransferDescription transferDescription) : base(transferDescription)
        {
        }
    }
}