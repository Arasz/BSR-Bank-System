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

namespace Test.Service.Bank.CommandHandlers
{
    public sealed class WithdrawCommandHandlerTest : HandlerTestBase<WithdrawCommandHandler, Account>
    {
        private readonly string AccountNumber = "1234";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

        [Theory]
        [InlineData(500, 400)]
        [InlineData(500, 500)]
        public void WithdrawFromAccount_CheckBalanceAfterWithdraw_BlanceShouldBeReducedByAmount(decimal accountBalance, decimal transferAmount)
        {
            var withdrawCommandHandler = Handler;

            var mockedCommand = CreateWithdrawCommandMock(transferAmount);

            var account = CreateAndInitializeAccount(accountBalance);

            withdrawCommandHandler.HandleCommand(mockedCommand);

            account.Balance.Should().Be(accountBalance - transferAmount);
        }

        [Theory]
        [InlineData(0, 400)]
        [InlineData(200, 500)]
        public void WithdrawToMuchFromAccount_CheckAmountValidation_ShouldThrowException(decimal accountBalance, decimal transferAmount)
        {
            var withdrawCommandHandler = Handler;

            var mockedCommand = CreateWithdrawCommandMock(transferAmount);

            var account = CreateAndInitializeAccount(accountBalance);

            Action handlerAction = () => withdrawCommandHandler.HandleCommand(mockedCommand);

            handlerAction.ShouldThrow<AccountBalanceToLowException>();

            account.Balance.Should().Be(accountBalance);
        }

        private Account CreateAndInitializeAccount(decimal balance)
        {
            var accountMock = new Account
            {
                Number = AccountNumber,
                Balance = balance
            };

            MockDataSource.Add(accountMock);

            return accountMock;
        }

        private WithdrawCommand CreateWithdrawCommandMock(decimal withdrawAmount)
            => new WithdrawCommand(AccountNumber, withdrawAmount);
    }
}