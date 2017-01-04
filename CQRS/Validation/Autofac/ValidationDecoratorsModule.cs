using Autofac;
using CQRS.Commands;
using CQRS.Events;
using CQRS.Queries;
using CQRS.Validation.Decorators;

namespace CQRS.Validation.Autofac
{
    public class ValidationDecoratorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterDecorator<ICommandBus>((context, bus) => new CommandValidationBus(bus, commandType =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                var validatorType = typeof(IValidation<>).MakeGenericType(commandType);

                return (IValidation)componentContext.Resolve(validatorType);
            }), nameof(CommandBus));

            builder.RegisterDecorator<IQueryBus>((context, bus) => new QueryValidationBus(bus, queryBus =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                var validatorType = typeof(IValidation<>).MakeGenericType(queryBus);

                return (IValidation)componentContext.Resolve(validatorType);
            }), nameof(QueryBus));
        }
    }
}