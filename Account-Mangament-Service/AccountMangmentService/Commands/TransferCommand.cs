using CQRS.Commands;
using Service.Dto;

namespace Service.Bank.Commands
{
    /// <summary>
    /// Represents money transfer from one account to other account 
    /// </summary>
    public abstract class TransferCommand : ICommand
    {
        public TransferDescription TransferDescription { get; protected set; }

        protected TransferCommand(TransferDescription description)
        {
            TransferDescription = description;
        }

        protected TransferCommand()
        {
        }
    }
}