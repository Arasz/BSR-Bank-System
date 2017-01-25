using Autofac;
using Autofac.Features.AttributeFilters;
using FluentValidation;
using Service.Bank.Autofac;
using Service.Contracts;
using Service.Dto;
using Service.InterbankTransfer.Decorators;
using Service.InterbankTransfer.Implementation;
using Service.InterbankTransfer.Validation;

namespace Service.InterbankTransfer.Autofac
{
    public class InterbankTransactionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<InterbankTransferDescriptionValidator>()
                .As<IValidator<InterbankTransferDescription>>()
                .SingleInstance();

            builder.RegisterModule<BankServiceModule>();

            builder.RegisterType<InterbankTransferService>()
                .WithAttributeFiltering()
                .Named<IInterbankTransferService>(nameof(InterbankTransferService));

            builder.RegisterDecorator<IInterbankTransferService>(CreateValidationDecorator, nameof(InterbankTransferService), nameof(InterbankTransferServiceValidationDecorator));

            builder.RegisterDecorator<IInterbankTransferService>(CreateExceptionDecorator, nameof(InterbankTransferServiceValidationDecorator));
        }

        private IInterbankTransferService CreateExceptionDecorator(IComponentContext context, IInterbankTransferService service) =>
            new InterbankTransferServiceExceptionDecorator(service);

        private IInterbankTransferService CreateValidationDecorator(IComponentContext context, IInterbankTransferService service)
            => new InterbankTransferServiceValidationDecorator(service, context.Resolve<IValidator<InterbankTransferDescription>>());
    }
}