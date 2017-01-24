using Autofac;
using Data.Core.Entities;
using FluentAssertions;
using Service.Bank.Autofac;
using Service.Bank.Commands;
using Service.Bank.Operations;
using Service.Dto;
using System.Linq;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.OperationRegister
{
    public class OperationRegisterTest : DataContextAccessTest<Operation>, IDependencyInjectionTest
    {
        private readonly decimal _amount = 10;

        private readonly decimal _newBalance = 20;

        private readonly string _sourceAccount = "1";

        private readonly string _targetAccount = "2";

        private readonly string _title = "Title";

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
        public void RegisterBookExternalTransferOperation_GivenCorrectData_OperationShouldBySaved()
        {
            var operationsRegister = Container.Resolve<IOperationRegister>();
            var account = CreateAccount(_targetAccount);
            var transferDescription = CreateTransferDescription(_sourceAccount, _targetAccount);

            operationsRegister.RegisterOperation<BookExternalTransferCommand>(account, transferDescription);

            account.Operations.Count
                .Should().Be(1);

            account.Operations
                .Single()
                .Type
                .Should()
                .NotBeNullOrWhiteSpace();

            OperationAssertions(_newBalance, _amount, 0, _amount);
        }

        [Fact]
        public void RegisterDepositOperation_GivenCorrectData_OperationShouldBySaved()
        {
            var operationsRegister = Container.Resolve<IOperationRegister>();
            var account = CreateAccount(_sourceAccount);
            var transferDescription = CreateTransferDescription("", _sourceAccount);

            operationsRegister.RegisterOperation<DepositCommand>(account, transferDescription);

            account.Operations.Count
                .Should().Be(1);

            account.Operations
                .Single()
                .Type
                .Should()
                .NotBeNullOrWhiteSpace();

            OperationAssertions(_newBalance, _amount, 0, _amount);
        }

        [Fact]
        public void RegisterInternalTransferOperation_GivenCorrectData_OperationShouldBySaved()
        {
            var operationsRegister = Container.Resolve<IOperationRegister>();
            var account = CreateAccount(_sourceAccount);
            var transferDescription = CreateTransferDescription(_sourceAccount, _targetAccount);

            operationsRegister.RegisterOperation<InternalTransferCommand>(account, transferDescription);

            account.Operations.Count
                .Should().Be(1);

            account.Operations
                .Single()
                .Type
                .Should()
                .NotBeNullOrWhiteSpace();

            OperationAssertions(_newBalance, _amount, _amount, 0);
        }

        [Fact]
        public void RegisterWithdrawOperation_GivenCorrectData_OperationShouldBySaved()
        {
            var operationsRegister = Container.Resolve<IOperationRegister>();
            var account = CreateAccount(_sourceAccount);
            var transferDescription = CreateTransferDescription(_sourceAccount, "");

            operationsRegister.RegisterOperation<WithdrawCommand>(account, transferDescription);

            account.Operations.Count
                .Should().Be(1);

            account.Operations
                .Single()
                .Type
                .Should()
                .NotBeNullOrWhiteSpace();

            OperationAssertions(_newBalance, _amount, _amount, 0);
        }

        private Account CreateAccount(string accountNumber)
        {
            var account = new Account
            {
                Balance = _newBalance,
                Number = accountNumber,
                Operations = MockDataSource
            };
            return account;
        }

        private TransferDescription CreateTransferDescription(string source, string target)
        {
            var transferDescription = new TransferDescription
            {
                Amount = _amount,
                SourceAccountNumber = source,
                TargetAccountNumber = target,
                Title = _title
            };
            return transferDescription;
        }

        private void OperationAssertions(decimal newBalance, decimal amount, decimal debit, decimal credit)
        {
            MockDataSource.Count.Should()
                .Be(1);

            var operation = MockDataSource.Single();

            operation.Credit.Should()
                .Be(credit);

            operation.Debit.Should()
                .Be(debit);

            operation.Amount.Should()
                .Be(amount);

            operation.Balance.Should()
                .Be(newBalance);

            operation.Title.Should()
                .Be(_title);

            operation.Type.Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}