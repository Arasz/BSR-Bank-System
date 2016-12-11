namespace AccountMangmentService.Commands
{
    /// <summary>
    /// Minimal common part of all payment commands 
    /// </summary>
    public abstract class MinimalPaymentCommand
    {
        public decimal Amount { get; set; }
    }
}