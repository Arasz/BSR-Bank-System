using System.Collections.Generic;
using System.Linq;
using Autofac;
using Data.Core;
using FluentAssertions;
using Service.Bank.Autofac;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Dto;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.OperationRegister
{
    public class OperationRegisterTest : DataContextAccessTest<Operation>, IDependencyInjectionTest
    {
        private decimal _amount = 10;
        private decimal _credit = 0;
        private decimal _debit = 10;
        private decimal _newBalance = 20;
        private string _sourceAccount = "1";
        private string _targetAccount = "2";
        private string _title = "Title";

        public IContainer Container { get; set; }

        public OperationRegisterTest()
        {
            Container = BuildContainer();
            MockDataContext(context => context.Operations);
        }

        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BankServiceModule>();

            builder.RegisterType<global::Service.Bank.Operations.OperationRegister>()
                .AsImplementedInterfaces();

            return builder.Build();
        }

        [Fact]
        public void RegisterOperation_GivenCorrectData_OperationShouldBySaved()
        {
            var operationsRegister = Container.Resolve<IOperationRegister>();
            var account = CreateAccount();
            var transferDescription = CreateTransferDescription();

            operationsRegister.RegisterOperation<WithdrawCommand>(account, transferDescription);

            account.Operations.Count
                .Should().Be(1);

            account.Operations
                .Single()
                .Type
                .Should()
                .NotBeNullOrWhiteSpace();

            OperationAssertions();
        }

        private Account CreateAccount()
        {
            var account = new Account
            {
                Balance = _newBalance,
                Number = _sourceAccount,
                Operations = MockDataSource
            };
            return account;
        }

        private TransferDescription CreateTransferDescription()
        {
            var transferDescription = new TransferDescription
            {
                Amount = _amount,
                From = _sourceAccount,
                To = _targetAccount,
                Title = _title
            };
            return transferDescription;
        }

        private void OperationAssertions()
        {
            MockDataSource.Count.Should()
                .Be(1);

            var operation = MockDataSource.Single();

            operation.Amount.Should()
                .Be(_amount);
            operation.Balance.Should()
                .Be(_newBalance);
            operation.Credit.Should()
                .Be(_credit);
            operation.Debit.Should()
                .Be(_debit);
            operation.Title.Should()
                .Be(_title);
            operation.Type.Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}