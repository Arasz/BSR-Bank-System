using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using CQRS.Autofac;
using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;
using CQRS.Validation;
using CQRS.Validation.Adapters;
using CQRS.Validation.Decorators;
using FluentValidation;
using Module = Autofac.Module;

namespace CQRS.Validation.Autofac
{
    public class ValidationModule : ModuleTempalte<IValidation>
    {
        public ValidationModule(Assembly validatorsAssembly) : base(validatorsAssembly)
        {
        }

        public ValidationModule()
        {
        }

        protected override Type GenericHandlerType() => typeof(IValidation<>);

        protected override void RegisterBus(ContainerBuilder builder)
        {
            builder.RegisterDecorator<ICommandBus>(
                (context, bus) => new CommandValidationBus(bus, context.Resolve<Func<Type, IValidation>>()),
                nameof(CommandBus));

            builder.RegisterDecorator<IQueryBus>(
                (context, bus) => new QueryValidationBus(bus, context.Resolve<Func<Type, IValidation>>()),
                nameof(QueryBus));
        }

        protected override void RegisterFactoryFunctionForBus(ContainerBuilder builder)
        {
            builder.Register<Func<Type, IValidation>>(componentContext =>
            {
                var context = ResolveComponentContext(componentContext);

                return type =>
                {
                    var fluentValidatorType = typeof(IValidator<>).MakeGenericType(type);

                    if (context.IsRegistered(fluentValidatorType))
                    {
                        var adapterType = typeof(FluentValidatorAdapter<>).MakeGenericType(type);
                        var adapterParameter = CreateFluentValidatorAsAdapterParameter(fluentValidatorType);

                        return (IValidation)context.Resolve(adapterType, adapterParameter);
                    }

                    var validatorType = typeof(IValidation<>).MakeGenericType(type);

                    if (context.IsRegistered(validatorType))
                        return (IValidation)context.Resolve(validatorType);

                    return EmptyValidation.Empty;
                };
            });
        }

        protected override void RegisterHandlersFromAssembly(ContainerBuilder builder)
        {
            base.RegisterHandlersFromAssembly(builder);

            RegisterFluentValidators(builder);

            RegisterFluentValidatorAdapter(builder);
        }

        private static ResolvedParameter CreateFluentValidatorAsAdapterParameter(Type fluentValidatorType) => new ResolvedParameter(
            (info, ctx) => info.ParameterType == fluentValidatorType,
            (info, ctx) => ctx.Resolve(fluentValidatorType));

        private static void RegisterFluentValidatorAdapter(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(FluentValidatorAdapter<>))
                .AsSelf();
        }

        private void RegisterFluentValidators(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AssemblyWithHandlers)
                .Where(type => type.IsAssignableTo<IValidator>())
                .AsImplementedInterfaces();
        }
    }
}