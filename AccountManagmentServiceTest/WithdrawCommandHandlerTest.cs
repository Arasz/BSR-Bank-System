using Autofac;
using Data.Core;
using FluentAssertions;
using Moq;
using Service.Bank.CommandHandlers;
using Service.Bank.Commands;
using Test.Common;
using Xunit;

namespace AccountManagmentServiceTest
{
    public class WithdrawCommandHandlerTest : DataContextAccessTest<Account>, IDependencyInjectionTest
    {
        private const decimal AccountBalance = 500;

        private readonly string AccountNumber = "1234";

        public IContainer Container { get; set; }

        public WithdrawCommandHandlerTest()
        {
            Container = BuildContainer();
        }

        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            RegisterComponents(builder);

            return builder.Build();
        }

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

        private WithdrawCommand CreateWithdrawCommandMock(decimal withdrawAmount) => new WithdrawCommand(AccountNumber, withdrawAmount);

        private void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<WithdrawCommandHandler>()
                .AsSelf()
                .AsImplementedInterfaces();

            builder.Register(componentContext => MockDataContext(context => context.Accounts));
        }
    }
}