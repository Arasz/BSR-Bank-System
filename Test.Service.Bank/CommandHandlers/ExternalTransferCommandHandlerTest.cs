using Autofac;
using Core.CQRS.Commands;
using Data.Core;
using FluentAssertions;
using Moq;
using Service.Bank.CommandHandlers.External;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Bank.Proxy;
using Service.Dto;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Data.Core.Entities;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.CommandHandlers
{
    public class ExternalTransferCommandHandlerTest : HandlerTestBase<ExternalTransferCommandHandler, Account>
    {
        private readonly string _receiverAccountNumber = "78001122418528164913108073";
        private readonly string _senderAccountNumber = "78001122418528164913108077";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(long.MaxValue, 0)]
        [InlineData(2323232321242441L, 5000102002020220223L)]
        public void HandleExternalTransferCommand_CheckAmountCastForTooLargeAmounts_ShouldThrowOverflowException(
            decimal transferAmount, decimal accountBalance)
        {
            var externalTransferCommandHandler = Handler;

            var externalTransferCommand = CreateCommand(transferAmount);

            var receiverAccount = AddSenderAccount(accountBalance);

            Action handleCommandAction = () => externalTransferCommandHandler.HandleCommand(externalTransferCommand);

            handleCommandAction.ShouldThrow<OverflowException>();
        }

        [Theory]
        [InlineData(10, 100)]
        [InlineData(2, 55)]
        [InlineData(2322223, 100000000233232)]
        public void HandleExternalTransferCommand_DecreaseSenderAccountBalance_BalanceShouldBeDecreasedByTransferAmount(
            decimal transferAmount, decimal accountBalance)
        {
            var externalTransferCommandHandler = Handler;

            var externalTransferCommand = CreateCommand(transferAmount);

            var senderAccount = AddSenderAccount(accountBalance);

            externalTransferCommandHandler.HandleCommand(externalTransferCommand);

            ThrowIsTransferWasNotCalled();

            senderAccount.Balance.Should()
                .Be(accountBalance - transferAmount);

            ThrowIsChargeCommandWasNotSend();
        }

        protected override void RegisterComponents(ContainerBuilder builder)
        {
            base.RegisterComponents(builder);

            builder.Register(componentContext => CreateServiceProxyMock())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(context => CreateOperationRegisterMock())
                .AsImplementedInterfaces();

            builder.Register(context => CreateCommandBusMock())
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private Account AddSenderAccount(decimal accountBalance)
        {
            var createdAccount = CreateAccount(accountBalance);
            MockDataSource.Add(createdAccount);
            return createdAccount;
        }

        private Account CreateAccount(decimal balance) => new Account
        {
            Balance = balance,
            Number = _senderAccountNumber
        };

        private ExternalTransferCommand CreateCommand(decimal transferAmount) => new ExternalTransferCommand
        (
            new TransferDescription
            {
                Amount = transferAmount,
                SourceAccountNumber = _senderAccountNumber,
                TargetAccountNumber = _receiverAccountNumber,
                Title = "TransferToExternalBank"
            }
        );

        private ICommandBus CreateCommandBusMock()
        {
            var mock = new Mock<ICommandBus>();

            mock.Setup(bus => bus.Send(It.IsAny<ExternalTransferChargeCommand>()))
                .Verifiable();

            return mock.Object;
        }

        private IOperationRegister CreateOperationRegisterMock()
        {
            var mock = new Mock<IOperationRegister>();

            return mock.Object;
        }

        private IInterbankTransferServiceProxy CreateServiceProxyMock()
        {
            var interbankTransactionServiceMock = new Mock<IInterbankTransferServiceProxy>();
            interbankTransactionServiceMock
                .Setup(service => service.Transfer(It.IsAny<InterbankTransferDescription>()))
                .Verifiable();

            return interbankTransactionServiceMock.Object;
        }

        private void ThrowIsChargeCommandWasNotSend()
        {
            var commandBus = Container.Resolve<ICommandBus>();
            var mock = Mock.Get(commandBus);
            mock.Verify();
        }

        private void ThrowIsTransferWasNotCalled()
        {
            var proxy = Container.Resolve<IInterbankTransferServiceProxy>();
            var mock = Mock.Get(proxy);
            mock.Verify();
        }
    }
}