using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Autofac;
using Data.Core;
using FluentAssertions;
using Moq;
using Service.Bank.CommandHandlers;
using Service.Bank.Commands;
using Service.Contracts;
using Service.Dto;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.CommandHandlers
{
    public class ExternalTransferCommandHandlerTest : HandlerTestBase<ExternalTransferCommandHandler, Account>
    {
        private string _receiverAccountNumber = "2";
        private string _senderAccountNumber = "1";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(long.MaxValue, 0)]
        [InlineData(2323232321242441L, 5000102002020220223L)]
        public void HandleExternalTransferCommand_CheckAmountCastForTooLargeAmounts_ShouldThrowOverflowException(decimal transferAmount, decimal accountBalance)
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
        public void HandleExternalTransferCommand_DecreaseSenderAccountBalance_BalanceShouldBeDecreasedByTransferAmount(decimal transferAmount, decimal accountBalance)
        {
            var externalTransferCommandHandler = Handler;

            var externalTransferCommand = CreateCommand(transferAmount);

            var senderAccount = AddSenderAccount(accountBalance);

            externalTransferCommandHandler.HandleCommand(externalTransferCommand);

            ThrowIsTransferWasNotCalled();

            senderAccount.Balance.Should()
                .Be(accountBalance - transferAmount);
        }

        protected override void RegisterComponents(ContainerBuilder builder)
        {
            base.RegisterComponents(builder);

            builder.Register(componentContext => CreateServiceProxyMock())
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
            Number = _senderAccountNumber,
        };

        private ExternalTransferCommand CreateCommand(decimal transferAmount) => new ExternalTransferCommand
        (
             new TransferDescription
             {
                 Amount = transferAmount,
                 From = _senderAccountNumber,
                 To = _receiverAccountNumber,
                 Title = "TransferToExternalBank"
             }
        );

        private IInterbankTransferService CreateServiceProxyMock()
        {
            var interbankTransactionServiceMock = new Mock<IInterbankTransferService>();
            interbankTransactionServiceMock
                .Setup(service => service.Transfer(It.IsAny<InterbankTransferDescription>()))
                .Verifiable();

            return interbankTransactionServiceMock.Object;
        }

        private void ThrowIsTransferWasNotCalled()
        {
            var proxy = Container.Resolve<IInterbankTransferService>();
            var mock = Mock.Get(proxy);
            mock.Verify();
        }
    }
}