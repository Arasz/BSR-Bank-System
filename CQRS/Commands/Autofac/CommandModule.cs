using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace CQRS.Commands.Autofac
{
    public sealed class CommandModule : Module
    {
        public Assembly CommandHandlersAssembly { get; }

        public CommandModule(Assembly commandHandlersAssembly)
        {
            CommandHandlersAssembly = commandHandlersAssembly;
        }

        public CommandModule()
        {
            CommandHandlersAssembly = ThisAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(CommandHandlersAssembly)
                .Where(type => type.IsAssignableTo<ICommandHandler>())
                .AsImplementedInterfaces();

            builder.Register<Func<Type, ICommandHandler>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return (commandType =>
                {
                    var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
                    return (ICommandHandler)componentContext.Resolve(handlerType);
                });
            });

            builder.RegisterType<CommandBus>()
                .Named<ICommandBus>(nameof(CommandBus));
        }
    }
}