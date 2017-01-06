namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment from user account 
    /// </summary>
    public class WithdrawCommand : TransferCommand
    {
        public WithdrawCommand(decimal amount)
        {
            Amount = amount;
        }
    }
}