using Autofac;
using Autofac.Integration.Wcf;
using Service.Bank.Implementation;
using Service.Contracts;
using Service.InterbankTransfer.Autofac;
using Service.InterbankTransfer.Implementation;
using System;
using System.ServiceModel;

namespace Host.SelfHosting
{
    internal class Program
    {
        private static void AddContainer<TContract>(ServiceHost host, IContainer container)
        {
            host.AddDependencyInjectionBehavior<TContract>(container);
        }

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
                AddContainer<IBankService>(bankServiceHost, container);
                AddContainer<IInterbankTransferService>(interbankTransferServiceHost, container);

                SubscribeEvents(bankServiceHost);
                SubscribeEvents(interbankTransferServiceHost);

                bankServiceHost.Open();
                interbankTransferServiceHost.Open();

                Console.ReadLine();

                bankServiceHost.Close();
                interbankTransferServiceHost.Close();
            }
        }

        private static void SoapServiceHostOnClosed(object sender, EventArgs eventArgs)
        {
            var host = (ServiceHost)sender;
            Console.WriteLine($"Service {host.Description.Name} closed.\n");
        }

        private static void SoapServiceHostOnFaulted(object sender, EventArgs eventArgs)
        {
            var host = (ServiceHost)sender;
            Console.WriteLine($"Service {host.Description.Name} faulted.\n" +
                              $"State: {host.State}");
        }

        private static void SoapServiceHostOnOpened(object sender, EventArgs eventArgs)
        {
            var host = (ServiceHost)sender;
            var serviceDescription = new HostedServiceDescription(host);
            Console.WriteLine($"Service {host.Description.Name} opened.\n" + serviceDescription.Description);
        }

        private static void SoapServiceHostOnUnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs unknownMessageReceivedEventArgs)
        {
            var host = (ServiceHost)sender;
            Console.WriteLine($"Service {host.Description.Name} received unknown message : {unknownMessageReceivedEventArgs.Message}");
        }

        private static void SubscribeEvents(ServiceHost soapServiceHost)
        {
            soapServiceHost.UnknownMessageReceived += SoapServiceHostOnUnknownMessageReceived;

            soapServiceHost.Faulted += SoapServiceHostOnFaulted;

            soapServiceHost.Opened += SoapServiceHostOnOpened;

            soapServiceHost.Closed += SoapServiceHostOnClosed;
        }
    }
}