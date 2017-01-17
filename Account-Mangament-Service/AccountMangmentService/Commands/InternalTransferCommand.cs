using Shared.Transfer;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Payment to the account in the same bank 
    /// </summary>
    public class InternalTransferCommand : TransferCommand
    {
        public InternalTransferCommand(TransferDescription description)
        {
            From = description.SenderAccount;
            To = description.ReceiverAccount;
            Amount = description.Amount;
            Title = description.Title;
        }

        public InternalTransferCommand(string from, string to, string title, decimal amount)
        {
            From = from;
            To = to;
            Title = title;
            Amount = amount;
        }
    }
}