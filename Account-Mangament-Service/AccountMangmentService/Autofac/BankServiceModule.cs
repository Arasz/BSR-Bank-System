using System.Reflection;
using System.Resources;
using Autofac;
using Module = Autofac.Module;

namespace Service.Bank.Autofac
{
    public class BankServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //builder.RegisterAssemblyTypes(ThisAssembly);

            builder.RegisterType<ResourceManager>()
                .WithParameters(new[]
                {
                    new TypedParameter(typeof(string), "Service.Bank.OperationNames"),
                    new TypedParameter(typeof(Assembly), ThisAssembly)
                });
        }
    }
}