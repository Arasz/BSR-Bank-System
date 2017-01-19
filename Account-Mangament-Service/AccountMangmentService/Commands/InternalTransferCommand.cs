using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment to the account in the same bank 
    /// </summary>
    public class InternalTransferCommand : TransferCommand
    {
        public InternalTransferCommand(TransferDescription description) : base(description)
        {
        }
    }
}