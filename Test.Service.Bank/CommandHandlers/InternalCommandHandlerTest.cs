using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using Autofac;
using Data.Core;
using FluentAssertions;
using Service.Bank.Autofac;
using Service.Bank.CommandHandlers.Internal;
using Service.Bank.Commands;
using Service.Bank.Exceptions;
using Service.Dto;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.CommandHandlers
{
    public class InternalCommandHandlerTest : HandlerTestBase<InternalTransferCommandHandler, Account>
    {
        private const string ReceiverAccountNumber = "5678";
        private const string SenderAccountNumber = "1234";
        private const string TransferTitle = "Title";
        private readonly List<Operation> OperationTableMock = new List<Operation>();

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(500, 500, 400)]
        [InlineData(500, 0, 400)]
        [InlineData(500, 500, 500)]
        public void TransferFoundsBetweenLocalAccounts_CheckReceiverAndSenderBalance_ShouldChangeBalanceByCorrectAmount(
            decimal senderBalance, decimal receiverBalance, decimal transferAmount)
        {
            var internalTransferCommandHandler = Handler;

            var internalTransferCommand = CreateCommand(transferAmount);

            var senderAccount = CreateAndInitializeAccount(SenderAccountNumber, senderBalance);
            var receiverAccount = CreateAndInitializeAccount(ReceiverAccountNumber, receiverBalance);

            internalTransferCommandHandler.HandleCommand(internalTransferCommand);

            senderAccount.Balance.Should().Be(senderBalance - transferAmount);
            receiverAccount.Balance.Should().Be(receiverBalance + transferAmount);
        }

        [Theory]
        [InlineData(500, 500, 800)]
        [InlineData(100, 0, 400)]
        [InlineData(0, 500, 10)]
        [InlineData(-10, 500, 5)]
        public void TransferIncorrectAmountBetweenLocalAccounts_CheckBalanceValidation_ShouldThrowAccountBalanceToLow(
            decimal senderBalance, decimal receiverBalance, decimal transferAmount)
        {
            var internalTransferCommandHandler = Handler;

            var internalTransferCommand = CreateCommand(transferAmount);

            var senderAccount = CreateAndInitializeAccount(SenderAccountNumber, senderBalance);
            var receiverAccount = CreateAndInitializeAccount(ReceiverAccountNumber, receiverBalance);

            Action handleAction = () => internalTransferCommandHandler.HandleCommand(internalTransferCommand);

            handleAction.ShouldThrow<AccountBalanceToLowException>();

            senderAccount.Balance.Should().Be(senderBalance);

            receiverAccount.Balance.Should().Be(receiverBalance);
        }

        protected override void RegisterComponents(ContainerBuilder builder)
        {
            base.RegisterComponents(builder);

            builder.RegisterModule<BankServiceModule>();
        }

        private Account CreateAndInitializeAccount(string accountNumber, decimal balance)
        {
            var accountMock = new Account
            {
                Number = accountNumber,
                Balance = balance,
                Operations = OperationTableMock,
                Id = int.Parse(accountNumber)
            };

            MockDataSource.Add(accountMock);

            return accountMock;
        }

        private InternalTransferCommand CreateCommand(decimal withdrawAmount) => new InternalTransferCommand
        (
            new TransferDescription
            {
                Amount = withdrawAmount,
                From = SenderAccountNumber,
                To = ReceiverAccountNumber,
                Title = "TransferToExternalBank"
            }
        );
    }
}