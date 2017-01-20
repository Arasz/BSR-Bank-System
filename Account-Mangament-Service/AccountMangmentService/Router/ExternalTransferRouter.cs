﻿using Core.Common.AccountNumber.Parser;
using Core.CQRS.Commands;
using Service.Bank.Commands;
using Service.Dto;

namespace Service.Bank.Router
{
    public class ExternalTransferRouter : IExternalTransferRouter
    {
        private readonly IAccountNumberParser _accountNumberParser;
        private readonly ICommandBus _commandBus;
        private string LocalBankId { get; } = "112241";

        public ExternalTransferRouter(ICommandBus commandBus, IAccountNumberParser accountNumberParser)
        {
            _commandBus = commandBus;
            _accountNumberParser = accountNumberParser;
        }

        public void Route(TransferDescription routedTransferDescription)
        {
            var senderAccount = routedTransferDescription.From;
            var receiverAccount = routedTransferDescription.To;

            if (IsExternal(senderAccount) && !IsExternal(receiverAccount))
                _commandBus.Send(new BookExternalTransferCommand(routedTransferDescription));
            else
                _commandBus.Send(new ExternalTransferCommand(routedTransferDescription));
        }

        private bool IsExternal(string accountNumber)
        {
            var parsedAccountNumber = _accountNumberParser.Parse(accountNumber);

            return !parsedAccountNumber.BankId.Contains(LocalBankId);
        }
    }
}