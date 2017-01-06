using System;
using System.Reflection;
using Autofac;
using CQRS.Autofac;
using Module = Autofac.Module;

namespace CQRS.Commands.Autofac
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
            .Named<ICommandBus>(nameof(CommandBus));
    }
}