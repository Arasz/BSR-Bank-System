using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Autofac;
using Data.Core;
using FluentAssertions;
using Service.Bank.CommandHandlers;
using Service.Bank.Commands;
using Test.Common;
using Xunit;

namespace AccountManagmentServiceTest
{
    public sealed class DepositCommandHandlerTest : CommandHandlerTestBase<DepositCommandHandler, Account>
    {
        private const decimal AccountBalance = 500;

        private readonly string AccountNumber = "1234";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Fact]
        public void HandleDepositCommand_DepositMoney_AccountBalanceIsIncreasedByDepositAmount()
        {
            var depositCommandHandler = CommandHandler;
            const int depositAmount = 30;

            var mockedCommand = CreateDepositCommand(depositAmount);

            var account = CreateAndInitializeAccount();

            depositCommandHandler.HandleCommand(mockedCommand);

            account.Balance.Should()
                .Be(AccountBalance + depositAmount);
        }

        private Account CreateAndInitializeAccount()
        {
            var accountMock = new Account
            {
                Number = AccountNumber,
                Balance = AccountBalance
            };

            MockDataSource.Add(accountMock);

            return accountMock;
        }

        private DepositCommand CreateDepositCommand(decimal withdrawAmount)
            => new DepositCommand(AccountNumber, withdrawAmount);
    }
}