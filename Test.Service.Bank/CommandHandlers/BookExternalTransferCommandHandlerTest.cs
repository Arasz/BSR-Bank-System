using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Data.Core;
using Data.Core.Entities;
using FluentAssertions;
using Service.Bank.CommandHandlers.External;
using Service.Bank.Commands;
using Service.Dto;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.CommandHandlers
{
    public class BookExternalTransferCommandHandlerTest : HandlerTestBase<RegisterExternalTransferCommandHandler, Account>
    {
        private readonly string _receiverAccountNumber = "2";
        private readonly string _senderAccountNumber = "1";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(10, 100)]
        [InlineData(1, 0)]
        [InlineData(2322223323232, 100000000233232)]
        public void HandleBookExternalTransferCommand_CheckTargetAccountBalance_ShouldIncreaseTargetAccountBalance(
            decimal transferAmount, decimal accountBalance)
        {
            var bookExternalTransferCommandHandler = Handler;

            var externalTransferReceivedEvent = CreateCommand(transferAmount);

            var receiverAccount = AddReceiverAccount(accountBalance);

            bookExternalTransferCommandHandler.HandleCommand(externalTransferReceivedEvent);

            receiverAccount.Balance.Should()
                .Be(transferAmount + accountBalance);
        }

        [Theory]
        [InlineData(10, "79228162514264337593543950335")]
        [InlineData(1, "79228162514264337593543950335")]
        public void HandleBookExternalTransferCommand_TooBigAmount_ShouldThrowOverflowException(decimal transferAmount,
            decimal accountBalance)
        {
            var bookExternalTransferCommandHandler = Handler;

            var externalTransferReceivedEvent = CreateCommand(transferAmount);

            var receiverAccount = AddReceiverAccount(accountBalance);

            Action handleCommandAction =
                () => bookExternalTransferCommandHandler.HandleCommand(externalTransferReceivedEvent);

            handleCommandAction.ShouldThrow<OverflowException>();
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
            Number = _receiverAccountNumber
        };

        private RegisterExternalTransferCommand CreateCommand(decimal transferAmount) => new RegisterExternalTransferCommand
        (
            new TransferDescription
            {
                Amount = transferAmount,
                SourceAccountNumber = _senderAccountNumber,
                TargetAccountNumber = _receiverAccountNumber,
                Title = "FromExternalBank"
            }
        );
    }
}