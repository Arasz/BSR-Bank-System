using System.Reflection;
using Autofac;
using CQRS.Bus;
using CQRS.Commands.Autofac;
using CQRS.Events.Autofac;
using CQRS.Queries.Autofac;
using CQRS.Validation.Autofac;
using Module = Autofac.Module;

namespace CQRS.Autofac
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
            builder.RegisterModule(new EventsModule(AssemblyWithHandlers));
            builder.RegisterModule(new ValidationModule(AssemblyWithHandlers));
        }
    }
}