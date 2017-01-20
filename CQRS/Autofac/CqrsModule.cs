using System.Reflection;
using Autofac;
using Core.CQRS.Bus;
using Core.CQRS.Commands.Autofac;
using Core.CQRS.Queries.Autofac;
using Module = Autofac.Module;

namespace Core.CQRS.Autofac
{
    public class CqrsModule : Module
    {
        public Assembly AssemblyWithHandlers { get; }

        public CqrsModule(Assembly assemblyWithHandlers)
        {
            AssemblyWithHandlers = assemblyWithHandlers;
        }

        public CqrsModule()
        {
            AssemblyWithHandlers = ThisAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<GenericBus>()
                .AsImplementedInterfaces();

            builder.RegisterModule(new CommandModule(AssemblyWithHandlers));
            builder.RegisterModule(new QueryModule(AssemblyWithHandlers));
        }
    }
}