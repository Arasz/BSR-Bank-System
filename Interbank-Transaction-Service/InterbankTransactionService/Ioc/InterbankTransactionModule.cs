using Autofac;
using FluentValidation;
using Service.Bank.Contract;
using Service.Bank.Implementation;
using Service.InterbankTransaction.Dto;
using Service.InterbankTransaction.Mapping;
using Service.InterbankTransaction.Validation;

namespace Service.InterbankTransaction.Ioc
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