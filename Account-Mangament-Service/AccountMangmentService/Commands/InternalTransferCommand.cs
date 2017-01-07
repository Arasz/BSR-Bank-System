namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment to the account in the same bank 
    /// </summary>
    public class InternalTransferCommand : TransferCommand
    {
        public InternalTransferCommand(string from, string to, string title, decimal amount)
        {
            From = from;
            To = to;
            Title = title;
            Amount = amount;
        }
    }
}