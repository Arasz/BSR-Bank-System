using Core.Common.AccountNumber.Parser;
using Core.CQRS.Commands;
using Service.Bank.Commands;
using Service.Bank.Exceptions;
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
            var senderAccount = routedTransferDescription.SourceAccountNumber;
            var receiverAccount = routedTransferDescription.TargetAccountNumber;

            if (IsFromExternalBank(senderAccount, receiverAccount))
                _commandBus.Send(new BookExternalTransferCommand(routedTransferDescription));
            else if (!IsFromExternalBank(senderAccount, receiverAccount))
                _commandBus.Send(new ExternalTransferCommand(routedTransferDescription));
            else
                throw new AmbiguousTransferDescriptionException(senderAccount, receiverAccount);
        }

        private bool IsExternal(string accountNumber)
        {
            var parsedAccountNumber = _accountNumberParser.Parse(accountNumber);

            return !parsedAccountNumber.BankId.Contains(LocalBankId);
        }

        private bool IsFromExternalBank(string senderAccount, string receiverAccount) => IsExternal(senderAccount) && !IsExternal(receiverAccount);
    }
}