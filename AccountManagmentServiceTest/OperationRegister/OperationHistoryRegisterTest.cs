using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Autofac;
using Data.Core;
using FluentAssertions;
using Service.Bank.Autofac;
using Service.Bank.Commands;
using Service.Bank.OperationRegister;
using Service.Dto;
using Test.Common;
using Xunit;

namespace Test.Service.Bank.OperationRegister
{
    public class OperationHistoryRegisterTest : HandlerTestBase<OperationHistoryRegister, Operation>
    {
        private decimal _amount = 10;
        private decimal _credit = 10;
        private decimal _debit = 0;
        private decimal _newBalance = 20;
        private string _sourceAccount = "1";
        private string _targetAccount = "2";
        private string _title = "Title";

        protected override Expression<Func<BankDataContext, DbSet<Operation>>> SelectDataSetFromDataContextExpression
            => context => context.Operations;

        [Theory]
        [InlineData(nameof(WithdrawCommand))]
        [InlineData(nameof(DepositCommand))]
        [InlineData(nameof(ExternalTransferCommand))]
        [InlineData(nameof(InternalTransferCommand))]
        [InlineData(nameof(ExternalTransferChargeCommand))]
        [InlineData(nameof(BookExternalTransferCommand))]
        public void HandleRegisterOperationCommand_ForDiffrenCommandNames_ShouldCreateNewOperation(string commandName)
        {
            var command = CreateRegisterBankOperationCommand(commandName);

            var operationHistoryRegister = Handler;

            operationHistoryRegister.HandleCommand(command);

            OperationAssertions();
        }

        protected override void RegisterComponents(ContainerBuilder builder)
        {
            base.RegisterComponents(builder);

            builder.RegisterType<CommandOperationConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterModule<BankServiceModule>();
        }

        private RegisterBankOperationCommand CreateRegisterBankOperationCommand(string commandName)
        {
            var transferDescription = new TransferDescription
            {
                Amount = _amount,
                From = _sourceAccount,
                To = _targetAccount,
                Title = _title
            };

            return new RegisterBankOperationCommand(transferDescription, _newBalance, commandName, _credit, _debit);
        }

        private void OperationAssertions()
        {
            MockDataSource.Count.Should()
                .Be(1);

            var operation = MockDataSource.Single();

            operation.Amount.Should()
                .Be(_amount);
            operation.AccountNumber.Should()
                .Be(_sourceAccount);
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