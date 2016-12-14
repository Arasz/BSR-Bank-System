using CQRS.Commands;

namespace AccountMangmentService.Commands
{
    /// <summary>
    /// Minimal common part of all payment commands 
    /// </summary>
    public abstract class MinimalPaymentCommand : ICommand
    {
        public decimal Amount { get; set; }
    }
}