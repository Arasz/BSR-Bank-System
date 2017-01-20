using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using Service.Bank.Autofac;
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
            ContainerBuilder builder = new ContainerBuilder();

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
                Console.WriteLine($"The host {nameof(bankServiceHost)} has been opened.");

                interbankTransferServiceHost.Open();
                Console.WriteLine($"The host {nameof(interbankTransferServiceHost)} has been opened.");

                Console.ReadLine();

                bankServiceHost.Close();
                interbankTransferServiceHost.Close();
            }
        }
    }
}