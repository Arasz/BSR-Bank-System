using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Registers transfer received from other bank 
    /// </summary>
    public class RegisterExternalTransferCommand : TransferCommand
    {
        public RegisterExternalTransferCommand(TransferDescription transferDescription) : base(transferDescription)
        {
        }
    }
}