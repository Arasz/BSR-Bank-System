﻿using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Data.Core;
using Data.Core.Entities;
using FluentAssertions;
using Service.Bank.CommandHandlers.Internal;
using Service.Bank.Commands;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.CommandHandlers
{
    public sealed class DepositCommandHandlerTest : HandlerTestBase<DepositCommandHandler, Account>
    {
        private const decimal AccountBalance = 500;

        private readonly string AccountNumber = "1234";

        protected override Expression<Func<BankDataContext, DbSet<Account>>> SelectDataSetFromDataContextExpression
            => bankDataContext => bankDataContext.Accounts;

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

        [Fact]
        public void AddFoundsToAccount_CheckAccountBlance_ShouldBeIncreasedByDepositAmount()
        {
            var depositCommandHandler = Handler;
            const int depositAmount = 30;

            var mockedCommand = CreateDepositCommand(depositAmount);

            var account = CreateAndInitializeAccount();

            depositCommandHandler.HandleCommand(mockedCommand);

            account.Balance.Should().Be(AccountBalance + depositAmount);
        }
    }
}