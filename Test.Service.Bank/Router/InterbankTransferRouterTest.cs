using Core.Common.AccountNumber.Parser;
using Core.CQRS.Commands;
using Moq;
using Service.Bank.Commands;
using Service.Bank.Router;
using Service.Dto;
using Xunit;

namespace Test.Service.Bank.Router
{
    public class InterbankTransferRouterTest
    {
        private readonly string _externalBankAccount = "47001297250000000000000001";

        private readonly string _internalBankAccount = "78112241008528164913108077";

        private IAccountNumberParser _accountNumberParser;

        private ICommandBus _commandBus;

        public InterbankTransferRouterTest()
        {
            MockCommandBus();

            MockParser();
        }

        [Fact]
        public void RouteTransferFromExternalBank_CheckEventHandlerCalled_ShouldPublishExternalTransferReceived()
        {
            var transferRouter = new ExternalTransferRouter(_commandBus, _accountNumberParser);

            var fromExternalBankTransferDescription = CreateTransferDescription(false);

            transferRouter.Route(fromExternalBankTransferDescription);

            var routerMock = Mock.Get(_commandBus);
            routerMock.Verify(bus => bus.Send(It.IsAny<BookExternalTransferCommand>()));
        }

        [Fact]
        public void RouteTransferFromInternalBank_CheckIfCommandHandlerCalled_ShouldSendExternalTransferCommand()
        {
            var transferRouter = new ExternalTransferRouter(_commandBus, _accountNumberParser);

            var toExternalBankAccountTransfer = CreateTransferDescription(true);

            transferRouter.Route(toExternalBankAccountTransfer);

            var routerMock = Mock.Get(_commandBus);
            routerMock.Verify(bus => bus.Send(It.IsAny<ExternalTransferCommand>()));
        }

        private TransferDescription CreateTransferDescription(bool fromInternalAccount)
        {
            if (fromInternalAccount)
                return new TransferDescription
                {
                    Amount = 100,
                    TargetAccountNumber = _externalBankAccount,
                    SourceAccountNumber = _internalBankAccount,
                    Title = "ToExternalAccount"
                };

            return new TransferDescription
            {
                Amount = 100,
                TargetAccountNumber = _internalBankAccount,
                SourceAccountNumber = _externalBankAccount,
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