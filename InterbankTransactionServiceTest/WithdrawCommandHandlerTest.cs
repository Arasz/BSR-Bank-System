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

            var account = AddAccountMock();

            withdrawHandler.HandleCommand(mockedCommand);

            account.Balance.Should()
                .Be(AccountBalance - withdrawAmount);
        }

        private Account AddAccountMock()
        {
            var accountMock = new Mock<Account>();

            accountMock.SetupProperty(account => account.Balance);

            var mockedAccount = accountMock.Object;

            mockedAccount.Balance = AccountBalance;

            MockDataSource.Add(mockedAccount);

            return mockedAccount;
        }

        private WithdrawCommand CreateWithdrawCommandMock(decimal withdrawAmount)
        {
            var mock = new Mock<WithdrawCommand>();

            mock.Setup(command => command.Amount)
                .Returns(withdrawAmount);

            return mock.Object;
        }

        private void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<WithdrawCommandHandler>()
                .AsSelf()
                .AsImplementedInterfaces();

            builder.Register(componentContext => MockDataContext(context => context.Accounts));
        }
    }
}