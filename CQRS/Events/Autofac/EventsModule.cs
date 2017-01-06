using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using CQRS.Autofac;
using Module = Autofac.Module;

namespace CQRS.Events.Autofac
{
    public sealed class EventsModule : ModuleTempalte<IEventHandler>
    {
        public EventsModule()
        {
        }

        public EventsModule(Assembly eventsHandlersAssembly) : base(eventsHandlersAssembly)
        {
        }

        protected override Type GenericHandlerType() => typeof(IEventHandler<>);

        protected override void RegisterBus(ContainerBuilder builder) => builder
            .RegisterType<EventBus>()
            .AsImplementedInterfaces();

        protected override void RegisterFactoryFunctionForBus(ContainerBuilder builder)
        {
            builder.Register<Func<Type, IEnumerable<IEventHandler>>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return (eventType =>
                {
                    var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    var handlersCollectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);
                    return (IEnumerable<IEventHandler>)componentContext.Resolve(handlersCollectionType);
                });
            });
        }
    }
}