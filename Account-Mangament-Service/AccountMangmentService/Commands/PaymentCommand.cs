using CQRS.Commands;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Minimal common part of all payment commands 
    /// </summary>
    public abstract class PaymentCommand : ICommand
    {
        public virtual decimal Amount { get; set; }
    }
}