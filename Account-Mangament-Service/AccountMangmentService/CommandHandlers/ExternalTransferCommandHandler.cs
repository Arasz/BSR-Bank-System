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
        private readonly IInterbankTransactionService _interbankTransactionService;

        public ExternalTransferCommandHandler(BankDataContext dataContext, IInterbankTransactionService interbankTransactionService)
        {
            _dataContext = dataContext;
            _interbankTransactionService = interbankTransactionService;
        }

        public void HandleCommand(ExternalTransferCommand command)
        {
            MakeInterbankTransfer(command);

            UpdateAccountBalance(command);
        }

        private InterbankTransferDescription CommandToDescription(ExternalTransferCommand command) => new InterbankTransferDescription
        {
            Amount = Convert.ToInt32(Math.Round(command.Amount * 100)),
            ReceiverAccount = command.To,
            SenderAccount = command.From,
            Title = command.Title,
        };

        private void MakeInterbankTransfer(ExternalTransferCommand command)
        {
            var interbankTransferDescription = CommandToDescription(command);
            _interbankTransactionService.Transfer(interbankTransferDescription);
        }

        private void UpdateAccountBalance(ExternalTransferCommand command)
        {
            var senderAccount = _dataContext.Accounts.Single(account => account.Number == command.From);
            senderAccount.Balance -= command.Amount;
            _dataContext.SaveChanges();
        }
    }
}