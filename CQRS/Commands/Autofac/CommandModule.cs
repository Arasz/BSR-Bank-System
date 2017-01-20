using System;
using System.Reflection;
using Autofac;
using Core.CQRS.Autofac;

namespace Core.CQRS.Commands.Autofac
{
    public sealed class CommandModule : ModuleTempalte<ICommandHandler>
    {
        public CommandModule(Assembly commandHandlersAssembly) : base(commandHandlersAssembly)
        {
        }

        public CommandModule()
        {
        }

        protected override Type GenericHandlerType() => typeof(ICommandHandler<>);

        protected override void RegisterBus(ContainerBuilder builder) => builder
            .RegisterType<CommandBus>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}