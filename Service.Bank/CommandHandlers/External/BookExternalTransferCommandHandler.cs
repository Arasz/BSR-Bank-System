using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;

namespace Service.Bank.CommandHandlers.External
{
    public class BookExternalTransferCommandHandler : BankOperationCommandHandler<BookExternalTransferCommand>
    {
        public BookExternalTransferCommandHandler(BankDataContext dataContext, IOperationRegister operationRegister)
            : base(dataContext, operationRegister)
        {
        }

        public void HandleCommand(BookExternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

            UpdateAccountBalance(_transferDescription.Amount, _transferDescription.To);

            RegisterOperation();
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance += amount;
    }
}