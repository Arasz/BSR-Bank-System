using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Data.Core;
using FluentAssertions;
using Service.Bank.CommandHandlers;
using Service.Bank.Commands;
using Service.Bank.Exceptions;
using Test.Common;
using Xunit;

namespace AccountManagmentServiceTest
{
    public class InternalCommandHandlerTest : CommandHandlerTestBase<InternalTransferCommandHandler, Account>
    {
        private const string ReciverAccountNumber = "5678";
        private const string SenderAccountNumber = "1234";
        private const string TransferTitle = "Title";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(500, 500, 400)]
        [InlineData(500, 0, 400)]
        [InlineData(500, 500, 500)]
        public void TransferFoundsBetweenLocalAccounts_CheckReciverAndSenderBalance_ShouldChangeBalnceByCorrectAmount(decimal senderBalance, decimal receiverBalance, decimal transferAmount)
        {
            var internalTransferCommandHandler = CommandHandler;

            var internalTransferCommand = CreateCommand(transferAmount);

            var senderAccount = CreateAndInitializeAccount(SenderAccountNumber, senderBalance);
            var receiverAccount = CreateAndInitializeAccount(ReciverAccountNumber, receiverBalance);

            internalTransferCommandHandler.HandleCommand(internalTransferCommand);

            senderAccount.Balance.Should().Be(senderBalance - transferAmount);
            receiverAccount.Balance.Should().Be(receiverBalance + transferAmount);
        }

        [Theory]
        [InlineData(500, 500, 800)]
        [InlineData(100, 0, 400)]
        [InlineData(0, 500, 10)]
        [InlineData(-10, 500, 5)]
        public void TransferIncorrectAmountBetweenLocalAccounts_CheckBalanceValidation_ShouldThrowAccountBalanceToLow(decimal senderBalance, decimal receiverBalance, decimal transferAmount)
        {
            var internalTransferCommandHandler = CommandHandler;

            var internalTransferCommand = CreateCommand(transferAmount);

            var senderAccount = CreateAndInitializeAccount(SenderAccountNumber, senderBalance);
            var receiverAccount = CreateAndInitializeAccount(ReciverAccountNumber, receiverBalance);

            Action handleAction = () => internalTransferCommandHandler.HandleCommand(internalTransferCommand);

            handleAction.ShouldThrow<AccountBalanceToLowException>();

            senderAccount.Balance.Should().Be(senderBalance);

            receiverAccount.Balance.Should().Be(receiverBalance);
        }

        private Account CreateAndInitializeAccount(string accountNumber, decimal balance)
        {
            var accountMock = new Account
            {
                Number = accountNumber,
                Balance = balance
            };

            MockDataSource.Add(accountMock);

            return accountMock;
        }

        private InternalTransferCommand CreateCommand(decimal withdrawAmount)
            => new InternalTransferCommand(SenderAccountNumber, ReciverAccountNumber, TransferTitle, withdrawAmount);
    }
}