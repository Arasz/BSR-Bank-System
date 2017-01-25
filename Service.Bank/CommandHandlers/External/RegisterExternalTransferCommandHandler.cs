using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.External
{
    public class RegisterExternalTransferCommandHandler : BankOperationCommandHandler<RegisterExternalTransferCommand>
    {
        public RegisterExternalTransferCommandHandler(BankDataContext dataContext, IOperationRegister operationRegister)
            : base(dataContext, operationRegister)
        {
        }

        /// <summary>
        /// Registers received external transfer 
        /// </summary>
        /// <param name="command"></param>
        public override void HandleCommand(RegisterExternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

            UpdateAccountBalance(_transferDescription.Amount, _transferDescription.ReceiverAccountNumber);

            RegisterOperation();
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance += amount;
    }
}