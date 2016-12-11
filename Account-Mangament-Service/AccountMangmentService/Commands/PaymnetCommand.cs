namespace AccountMangmentService.Commands
{
    public abstract class PaymnetCommand : MinimalPaymentCommand
    {
        public string From { get; set; }

        public string Title { get; set; }

        public string To { get; set; }
    }
}