using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using Data.Core;
using FluentAssertions;
using Service.Bank.EventHandlers;
using Service.Bank.Events;
using Shared.Transfer;
using Test.Common;
using Xunit;

namespace AccountManagementServiceTest
{
    public class ExternalTransferReceivedEventHandlerTest : HandlerTestBase<ExternalTransferReceivedEventHandler, Account>
    {
        private string _receiverAccountNumber = "2";
        private string _senderAccountNumber = "1";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(10, 100)]
        [InlineData(1, 0)]
        [InlineData(2322223323232, 100000000233232)]
        public void HandleExternalTransferReceivedEvent_CheckTargetAccountBalance_ShouldIncreaseTargetAccountBalance(decimal transferAmount, decimal accountBalance)
        {
            var externalTransferReceivedEventHandler = Handler;

            var externalTransferReceivedEvent = CreateEvent(transferAmount);

            var receiverAccount = AddReceiverAccount(accountBalance);

            externalTransferReceivedEventHandler.HandleEvent(externalTransferReceivedEvent);

            receiverAccount.Balance.Should()
                .Be(transferAmount + accountBalance);
        }

        [Theory]
        [InlineData(10, "79228162514264337593543950335")]
        [InlineData(1, "79228162514264337593543950335")]
        public void HandleExternalTransferReceivedEvent_TooBigAmount_ShouldThrowOverflowException(decimal transferAmount, decimal accountBalance)
        {
            var externalTransferReceivedEventHandler = Handler;

            var externalTransferReceivedEvent = CreateEvent(transferAmount);

            var receiverAccount = AddReceiverAccount(accountBalance);

            Action handleEventAction = () => externalTransferReceivedEventHandler.HandleEvent(externalTransferReceivedEvent);

            handleEventAction.ShouldThrow<OverflowException>();
        }

        private Account AddReceiverAccount(decimal accountBalance)
        {
            var createdAccount = CreateAccount(accountBalance);
            MockDataSource.Add(createdAccount);
            return createdAccount;
        }

        private Account CreateAccount(decimal balance) => new Account
        {
            Balance = balance,
            Number = _receiverAccountNumber,
        };

        private ExternalTransferReceivedEvent CreateEvent(decimal transferAmount) => new ExternalTransferReceivedEvent(_senderAccountNumber, _receiverAccountNumber, transferAmount, "FromExternalBank");
    }
}