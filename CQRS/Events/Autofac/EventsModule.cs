using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace CQRS.Events.Autofac
{
    public sealed class EventsModule : Module
    {
        public Assembly EventsHandlersAssembly { get; }

        public EventsModule()
        {
            EventsHandlersAssembly = ThisAssembly;
        }

        public EventsModule(Assembly eventsHandlersAssembly)
        {
            EventsHandlersAssembly = eventsHandlersAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(EventsHandlersAssembly)
                .Where(type => type.IsAssignableTo<IEventHandler>())
                .AsImplementedInterfaces();

            builder.Register<Func<Type, IEnumerable<IEventHandler>>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return (type =>
                {
                    var handlerType = typeof(IEventHandler<>).MakeGenericType(type);
                    var handlersCollectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);
                    return (IEnumerable<IEventHandler>)componentContext.Resolve(handlersCollectionType);
                });
            });

            builder.RegisterType<EventBus>()
                .AsImplementedInterfaces();
        }
    }
}