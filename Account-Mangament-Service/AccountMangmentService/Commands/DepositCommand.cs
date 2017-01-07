namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment to user account 
    /// </summary>
    public class DepositCommand : TransferCommand
    {
        public DepositCommand(string accountNumber, decimal depositAmount)
        {
            From = accountNumber;
            Amount = depositAmount;
        }
    }
}