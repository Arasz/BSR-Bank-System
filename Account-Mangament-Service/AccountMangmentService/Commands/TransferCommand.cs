namespace AccountMangmentService.Commands
{
    /// <summary>
    /// Represents money transfer from one account to other account 
    /// </summary>
    public abstract class TransferCommand : PaymentCommand
    {
        public string From { get; set; }

        public string Title { get; set; }

        public string To { get; set; }
    }
}