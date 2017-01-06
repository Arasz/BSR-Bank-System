using System;
using System.Reflection;
using Autofac;
using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;
using CQRS.Validation.Decorators;
using Module = Autofac.Module;

namespace CQRS.Validation.Autofac
{
    public class ValidationDecoratorsModule : Module
    {
        public Assembly ValidatorsAssembly { get; set; }

        public ValidationDecoratorsModule(Assembly validatorsAssembly)
        {
            ValidatorsAssembly = validatorsAssembly;
        }

        public ValidationDecoratorsModule()
        {
            ValidatorsAssembly = ThisAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ValidatorsAssembly)
                .Where(type => type.IsAssignableTo<IValidation>())
                .AsImplementedInterfaces();

            builder.Register<Func<Type, IValidation>>(componentContext =>
            {
                var context = componentContext.Resolve<IComponentContext>();
                return type =>
                {
                    var validatorType = typeof(IValidation<>).MakeGenericType(type);

                    if (context.IsRegistered(validatorType))
                        return (IValidation)context.Resolve(validatorType);

                    return EmptyValidation.Empty;
                };
            });

            builder.RegisterDecorator<ICommandBus>((context, bus) => new CommandValidationBus(bus, context.Resolve<Func<Type, IValidation>>()),
                nameof(CommandBus));

            builder.RegisterDecorator<IQueryBus>((context, bus) => new QueryValidationBus(bus, context.Resolve<Func<Type, IValidation>>()),
                nameof(QueryBus));
        }
    }
}