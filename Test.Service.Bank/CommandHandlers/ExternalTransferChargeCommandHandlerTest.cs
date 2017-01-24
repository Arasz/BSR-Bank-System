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
    public class ExternalTransferChargeCommandHandlerTest :
        HandlerTestBase<ExternalTransferChargeCommandHandler, Account>
    {
        private readonly string _receiverAccountNumber = "2";
        private readonly string _senderAccountNumber = "1";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => context => context.Accounts;

        [Theory]
        [InlineData(1000, 0.05, 100)]
        [InlineData(1000, 0.25, 100)]
        [InlineData(1000, 0.1, 100)]
        [InlineData(1000, 0.5, 100)]
        [InlineData(1000, 0, 100)]
        public void ChargeForExternalTransfer_WithCorrectTransferPercent_ShouldChargeSenderAccount(decimal balance,
            decimal chargePercent, decimal transferAmount)
        {
            var senderAccount = CreateSenderAccount(balance);

            var chargeHandler = Handler;

            chargeHandler.HandleCommand(CreateChargeCommand(transferAmount, chargePercent));

            senderAccount.Balance
                .Should().Be(balance - chargePercent * transferAmount);
        }

        [Theory]
        [InlineData(1000, 100)]
        [InlineData(1000, 100)]
        public void ChargeForExternalTransfer_WithDefaultChargePercent_ShouldChargeSenderAccount(decimal balance,
            decimal transferAmount)
        {
            var senderAccount = CreateSenderAccount(balance);

            var chargeHandler = Handler;

            var chargeCommand = CreateChargeCommand(transferAmount);

            chargeHandler.HandleCommand(chargeCommand);

            senderAccount.Balance
                .Should().Be(balance - chargeCommand.ChargePercent * transferAmount);
        }

        [Theory]
        [InlineData(0, 0.05, 100)]
        [InlineData(0, 0.25, 100)]
        public void ChargeForExternalTransfer_WithZeroAccountBalance_BalanceShouldBeNegative(decimal balance,
            decimal chargePercent, decimal transferAmount)
        {
            var senderAccount = CreateSenderAccount(balance);

            var chargeHandler = Handler;

            chargeHandler.HandleCommand(CreateChargeCommand(transferAmount, chargePercent));

            senderAccount.Balance
                .Should().Be(balance - chargePercent * transferAmount)
                .And.BeNegative();
        }

        private ExternalTransferChargeCommand CreateChargeCommand(decimal amount, decimal chargePercent)
            => new ExternalTransferChargeCommand(new TransferDescription
            {
                Amount = amount,
                SourceAccountNumber = _senderAccountNumber,
                TargetAccountNumber = _receiverAccountNumber
            }, chargePercent);

        private ExternalTransferChargeCommand CreateChargeCommand(decimal amount)
            => new ExternalTransferChargeCommand(new TransferDescription
            {
                Amount = amount,
                SourceAccountNumber = _senderAccountNumber,
                TargetAccountNumber = _receiverAccountNumber
            });

        private Account CreateSenderAccount(decimal balance)
        {
            var account = new Account
            {
                Balance = balance,
                Number = _senderAccountNumber
            };
            MockDataSource.Add(account);
            return account;
        }
    }
}