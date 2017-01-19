using Core.Common.AccountNumber.Parser;
using CQRS.Commands;
using CQRS.Events;
using Moq;
using Service.Bank.Commands;
using Service.Bank.Router;
using Service.Dto;
using Xunit;

namespace Test.Service.Bank.Router
{
    public class InterbankTransferRouterTest
    {
        private IAccountNumberParser _accountNumberParser;
        private ICommandBus _commandBus;
        private IEventBus _eventBus;

        private string _externalBankAccount = "47001297250000000000000001";
        private string _internalBankAccount = "78112241008528164913108077";

        public InterbankTransferRouterTest()
        {
            MockCommandBus();

            MockParser();
        }

        [Fact]
        public void RouteTransferFromExternalBank_CheckEventHandlerCalled_ShouldPublishExternalTransferReceived()
        {
            var transferRouter = new ExternalTransferRouter(_commandBus, _eventBus, _accountNumberParser);

            var fromExternalBankTransferDescription = CreateTransferDescription(fromInternalAccount: false);

            transferRouter.Route(fromExternalBankTransferDescription);

            var routerMock = Mock.Get(_commandBus);
            routerMock.Verify(bus => bus.Send(It.IsAny<BookExternalTransferCommand>()));
        }

        [Fact]
        public void RouteTransferFromInternalBank_CheckIfCommandHandlerCalled_ShouldSendExternalTransferCommand()
        {
            var transferRouter = new ExternalTransferRouter(_commandBus, _eventBus, _accountNumberParser);

            var toExternalBankAccountTransfer = CreateTransferDescription(fromInternalAccount: true);

            transferRouter.Route(toExternalBankAccountTransfer);

            var routerMock = Mock.Get(_commandBus);
            routerMock.Verify(bus => bus.Send(It.IsAny<ExternalTransferCommand>()));
        }

        private TransferDescription CreateTransferDescription(bool fromInternalAccount)
        {
            if (fromInternalAccount)
            {
                return new TransferDescription
                {
                    Amount = 100,
                    To = _externalBankAccount,
                    From = _internalBankAccount,
                    Title = "ToExternalAccount"
                };
            }

            return new TransferDescription
            {
                Amount = 100,
                To = _internalBankAccount,
                From = _externalBankAccount,
                Title = "FromExternalAccount"
            };
        }

        private void MockCommandBus()
        {
            var commandBusMock = new Mock<ICommandBus>();

            commandBusMock.Setup(bus => bus.Send(It.IsAny<ExternalTransferCommand>()))
                .Verifiable();

            commandBusMock.Setup(bus => bus.Send(It.IsAny<BookExternalTransferCommand>()))
                .Verifiable();

            _commandBus = commandBusMock.Object;
        }

        private void MockParser()
        {
            _accountNumberParser = new AccountNumberParser();
        }
    }
}