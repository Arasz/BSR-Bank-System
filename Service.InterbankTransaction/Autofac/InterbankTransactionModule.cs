using Autofac;
using FluentValidation;
using Service.Bank.Autofac;
using Service.Contracts;
using Service.Dto;
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
                .Named<IInterbankTransferService>(nameof(InterbankTransferService));

            builder.RegisterDecorator<IInterbankTransferService>(
                (context, service) => new InterbankTransferServiceDecorator(service), nameof(InterbankTransferService));
        }
    }
}