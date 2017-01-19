using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using Service.Bank.Implementation;
using Service.Contracts;
using Service.InterbankTransfer.Autofac;

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
            {
                //Uri address = new Uri("http://localhost:8080/Service1");
                //ServiceHost host = new ServiceHost(typeof(BankService), address);
                //host.AddServiceEndpoint(typeof(IEchoService), new BasicHttpBinding(), string.Empty);

                // Here's the important part - attaching the DI behavior to the service host and
                // passing in the container.
                bankServiceHost.AddDependencyInjectionBehavior<IBankService>(container);

                //host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = address });
                bankServiceHost.Open();

                Console.WriteLine("The host has been opened.");
                Console.ReadLine();

                bankServiceHost.Close();
            }
        }
    }
}