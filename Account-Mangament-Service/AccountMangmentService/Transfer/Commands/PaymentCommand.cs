using CQRS.Commands;

namespace AccountMangementService.Transfer.Commands
{
    /// <summary>
    /// Minimal common part of all payment commands 
    /// </summary>
    public abstract class PaymentCommand : ICommand
    {
        public decimal Amount { get; set; }
    }
}