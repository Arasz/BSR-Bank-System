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
    public sealed class WithdrawCommandHandlerTest : CommandHandlerTestBase<WithdrawCommandHandler, Account>
    {
        private const decimal AccountBalance = 500;

        private readonly string AccountNumber = "1234";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Fact]
        public void HandleWithdrawCommand_WidthdrawMoney_AccountBalanceIsLoweredByWithdrawAmount()
        {
            var withdrawHandler = Container.Resolve<WithdrawCommandHandler>();
            const int withdrawAmount = 30;

            var mockedCommand = CreateWithdrawCommandMock(withdrawAmount);

            var account = CreateAndInitializeAccount();

            withdrawHandler.HandleCommand(mockedCommand);

            account.Balance.Should()
                .Be(AccountBalance - withdrawAmount);
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

        private WithdrawCommand CreateWithdrawCommandMock(decimal withdrawAmount)
            => new WithdrawCommand(AccountNumber, withdrawAmount);
    }
}