using System;
using System.Linq;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using Service.Bank.Implementation;
using Service.Contracts;
using Service.InterbankTransfer.Autofac;
using Service.InterbankTransfer.Implementation;

namespace Host.SelfHosting
{
    internal class Program
    {
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<InterbankTransactionModule>();

            return builder.Build();
        }

        private static void Main(string[] args)
        {
            using (var container = BuildContainer())
            using (var bankServiceHost = new ServiceHost(typeof(BankService)))
            using (var interbankTransferServiceHost = new ServiceHost(typeof(InterbankTransferService)))
            {
                bankServiceHost.AddDependencyInjectionBehavior<IBankService>(container);
                interbankTransferServiceHost.AddDependencyInjectionBehavior<IInterbankTransferService>(container);

                bankServiceHost.Open();
                Console.WriteLine(
                    $"The host {nameof(bankServiceHost)} has been opened with base address: {bankServiceHost.BaseAddresses.FirstOrDefault()}.");

                interbankTransferServiceHost.Open();
                Console.WriteLine(
                    $"The host {nameof(interbankTransferServiceHost)} has been opened with base address {interbankTransferServiceHost.BaseAddresses.FirstOrDefault()}.");

                Console.ReadLine();

                bankServiceHost.Close();
                interbankTransferServiceHost.Close();
            }
        }
    }
}