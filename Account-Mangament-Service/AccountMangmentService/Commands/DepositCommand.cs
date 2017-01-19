using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment to user account 
    /// </summary>
    public class DepositCommand : TransferCommand
    {
        public DepositCommand(string to, decimal amount)
        {
            TransferDescription = new TransferDescription("from", to, "title", amount);
        }
    }
}