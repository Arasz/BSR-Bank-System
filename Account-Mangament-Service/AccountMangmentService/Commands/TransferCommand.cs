namespace Service.Bank.Commands
{
    /// <summary>
    /// Represents money transfer from one account to other account 
    /// </summary>
    public abstract class TransferCommand : PaymentCommand
    {
        public virtual string From { get; set; }

        public virtual string Title { get; set; }

        public virtual string To { get; set; }
    }
}