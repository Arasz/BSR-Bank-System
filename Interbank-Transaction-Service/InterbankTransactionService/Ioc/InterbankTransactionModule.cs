using Autofac;
using FluentValidation;
using Service.Bank.Implementation;
using Service.Contracts;
using Service.Dto;
using Service.InterbankTransfer.Mapping;
using Service.InterbankTransfer.Validation;

namespace Service.InterbankTransfer.Ioc
{
    public class InterbankTransactionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ConfiguredMapperProvider>()
                .As<IMapperProvider>()
                .SingleInstance();

            builder.RegisterType<InterbankTransferDescriptionValidator>()
                .As<IValidator<InterbankTransferDescription>>()
                .SingleInstance();

            builder.RegisterType<IBankService>()
                .As<BankService>();
        }
    }
}