using Autofac;
using Service.Bank.Autofac;
using Service.Contracts;
using Test.Common;
using Xunit;

namespace Test.Service.Bank
{
    public class BankServiceTest : IDependencyInjectionTest
    {
        public IContainer Container { get; set; }

        public BankServiceTest()
        {
            Container = BuildContainer();
        }

        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BankServiceModule>();

            return builder.Build();
        }

        [Fact]
        public void TestWithdrawOperation_WithCorrectAccountNumberAndBalance_ShouldDecreaseAccountBalance()
        {
            var bankService = Container.Resolve<IBankService>();
        }
    }
}