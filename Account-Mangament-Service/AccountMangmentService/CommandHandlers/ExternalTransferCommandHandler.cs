using System;
using System.Linq;
using CQRS.Commands;
using Data.Core;
using Service.Bank.Commands;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.CommandHandlers
{
    public class ExternalTransferCommandHandler : ICommandHandler<ExternalTransferCommand>
    {
        private readonly BankDataContext _dataContext;
        private readonly IInterbankTransferService _interbankTransferService;

        public ExternalTransferCommandHandler(BankDataContext dataContext, IInterbankTransferService interbankTransferService)
        {
            _dataContext = dataContext;
            _interbankTransferService = interbankTransferService;
        }

        public void HandleCommand(ExternalTransferCommand command)
        {
            MakeInterbankTransfer(command);

            UpdateAccountBalance(command);
        }

        private void MakeInterbankTransfer(ExternalTransferCommand command)
        {
            var interbankTransferDescription = (InterbankTransferDescription)command.TransferDescription;
            _interbankTransferService.Transfer(interbankTransferDescription);
        }

        private void UpdateAccountBalance(ExternalTransferCommand command)
        {
            var interbankTransferDescription = command.TransferDescription;

            var senderAccount = _dataContext.Accounts
                .Single(account => account.Number == interbankTransferDescription.From);

            senderAccount.Balance -= interbankTransferDescription.Amount;
            _dataContext.SaveChanges();
        }
    }
}