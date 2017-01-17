using CQRS.Commands;
using CQRS.Events;
using Service.Bank.Commands;
using Service.Bank.Events;
using Shared.AccountNumber.Parser;
using Shared.Transfer;

namespace Service.Bank.Router
{
    public class InterbankTransferRouter : IInterbankTransferRouter
    {
        private readonly IAccountNumberParser _accountNumberParser;
        private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private string LocalBankId { get; } = "112241";

        public InterbankTransferRouter(ICommandBus commandBus, IEventBus eventBus, IAccountNumberParser accountNumberParser)
        {
            _commandBus = commandBus;
            _eventBus = eventBus;
            _accountNumberParser = accountNumberParser;
        }

        public void Route(TransferDescription routedTransferDescription)
        {
            var senderAccount = routedTransferDescription.SenderAccount;
            var receiverAccount = routedTransferDescription.ReceiverAccount;

            if (IsExternal(senderAccount) && !IsExternal(receiverAccount))
                _eventBus.Publish(new ExternalTransferReceivedEvent(routedTransferDescription));
            else
                _commandBus.Send(new ExternalTransferCommand());
        }

        private bool IsExternal(string accountNumber)
        {
            var parsedAccountNumber = _accountNumberParser.Parse(accountNumber);

            return !parsedAccountNumber.BankId.Contains(LocalBankId);
        }
    }
}