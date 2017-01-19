using Autofac;
using Service.InterbankTransfer.Autofac;

namespace Host.WcfService.Bootstrap
{
    public static class ContainerBootstraper
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<InterbankTransactionModule>();

            return builder.Build();
        }
    }
}