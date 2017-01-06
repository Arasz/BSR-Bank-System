using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace CQRS.Autofac
{
    /// <summary>
    /// Template class for CQRS autofac module 
    /// </summary>
    public abstract class ModuleTempalte<THandler> : Module
    {
        protected Assembly AssemblyWithHandlers { get; }

        protected ModuleTempalte(Assembly assemblyWithHandlers)
        {
            AssemblyWithHandlers = assemblyWithHandlers;
        }

        protected ModuleTempalte()
        {
            AssemblyWithHandlers = ThisAssembly;
        }

        protected static IComponentContext ResolveComponentContext(IComponentContext context)
                            => context.Resolve<IComponentContext>();

        protected abstract Type GenericHandlerType();

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterHandlersFromAssembly(builder);

            RegisterFactoryFunctionForBus(builder);

            RegisterBus(builder);
        }

        protected abstract void RegisterBus(ContainerBuilder builder);

        protected virtual void RegisterFactoryFunctionForBus(ContainerBuilder builder)
        {
            var genericHandlerType = GenericHandlerType();

            builder.Register<Func<Type, THandler>>(componentContext =>
            {
                var context = ResolveComponentContext(componentContext);

                return (type =>
                {
                    var handlerType = genericHandlerType.MakeGenericType(type);
                    return (THandler)context.Resolve(handlerType);
                });
            });
        }

        protected virtual void RegisterHandlersFromAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AssemblyWithHandlers)
                .Where(type => type.IsAssignableTo<THandler>())
                .AsImplementedInterfaces();
        }
    }
}