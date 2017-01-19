using System;
using System.Reflection;
using System.Resources;
using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace Service.Bank.Mappings.Autofac
{
    public class OperationHistoryMappingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<Func<Type, object>>(componentContext =>
            {
                var context = componentContext.Resolve<IComponentContext>();

                return (type) => context.Resolve(type);
            });

            builder.RegisterInstance(new ResourceManager("OperationNames", ThisAssembly))
                .AsSelf()
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.IsSubclassOf(typeof(ITypeConverter<,>)))
                .AsImplementedInterfaces();

            builder.RegisterType<AutoMapperBootstraper>()
                .AsSelf()
                .SingleInstance();
        }
    }
}