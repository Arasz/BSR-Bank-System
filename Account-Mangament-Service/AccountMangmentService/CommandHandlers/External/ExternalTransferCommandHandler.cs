using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.CommandHandlers.External
{
    public class ExternalTransferCommandHandler : ICommandHandler<ExternalTransferCommand>
    {
        private readonly ICommandBus _commandBus;
        private readonly BankDataContext _dataContext;
        private readonly IInterbankTransferService _interbankTransferService;

        public ExternalTransferCommandHandler(BankDataContext dataContext, IInterbankTransferService interbankTransferService, ICommandBus commandBus)
        {
            _dataContext = dataContext;
            _interbankTransferService = interbankTransferService;
            _commandBus = commandBus;
        }

        public void HandleCommand(ExternalTransferCommand command)
        {
            MakeInterbankTransfer(command);

            UpdateAccountBalance(command);

            _commandBus.Send(new ExternalTransferChargeCommand(command.TransferDescription));
        }

        private void MakeInterbankTransfer(ExternalTransferCommand command)
        {
            var interbankTransferDescription = (InterbankTransferDescription)command.TransferDescription;
            _interbankTransferService.Transfer(interbankTransferDescription);
        }

        private void UpdateAccountBalance(ExternalTransferCommand command)
        {
            var transferDescription = command.TransferDescription;

            var senderAccount = _dataContext.Accounts
                .Single(account => account.Number == transferDescription.From);

            senderAccount.Balance -= transferDescription.Amount;
            _dataContext.SaveChanges();
        }
    }
}