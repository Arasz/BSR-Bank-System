using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.CommandHandlers.Base;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.CommandHandlers.External
{
    public class ExternalTransferCommandHandler : BankOperationCommandHandler<ExternalTransferCommand>
    {
        private readonly ICommandBus _commandBus;
        private readonly BankDataContext _dataContext;
        private readonly IInterbankTransferService _interbankTransferService;

        public ExternalTransferCommandHandler(BankDataContext dataContext, IInterbankTransferService interbankTransferService, ICommandBus commandBus, IOperationRegister operationRegister) : base(dataContext, operationRegister)
        {
            _dataContext = dataContext;
            _interbankTransferService = interbankTransferService;
            _commandBus = commandBus;
        }

        public override void HandleCommand(ExternalTransferCommand command)
        {
            _transferDescription = command.TransferDescription;

            MakeInterbankTransfer();

            UpdateAccountBalance();

            RegisterOperation();

            _commandBus.Send(new ExternalTransferChargeCommand(command.TransferDescription));
        }

        protected override void ChangeAccountBalance(decimal amount) => Account.Balance -= amount;

        private void MakeInterbankTransfer()
        {
            var interbankTransferDescription = (InterbankTransferDescription)_transferDescription;
            _interbankTransferService.Transfer(interbankTransferDescription);
        }

        private void UpdateAccountBalance()
        {
            Account = _dataContext.Accounts
                .Single(account => account.Number == _transferDescription.From);

            ChangeAccountBalance(_transferDescription.Amount);

            _dataContext.SaveChanges();
        }
    }
}