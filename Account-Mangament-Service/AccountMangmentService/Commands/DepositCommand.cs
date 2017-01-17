namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment to user account 
    /// </summary>
    public class DepositCommand : TransferCommand
    {
        public DepositCommand(string targetAccountNumber, decimal depositAmount)
        {
            From = targetAccountNumber;
            Amount = depositAmount;
        }
    }
}