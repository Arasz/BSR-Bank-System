using Autofac;
using Service.InterbankTransaction.Ioc;

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