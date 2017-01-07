using CQRS.Commands;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Represents money transfer from one account to other account 
    /// </summary>
    public abstract class TransferCommand : ICommand
    {
        public decimal Amount { get; set; }

        public string From { get; set; }

        public string Title { get; set; }

        public string To { get; set; }
    }
}