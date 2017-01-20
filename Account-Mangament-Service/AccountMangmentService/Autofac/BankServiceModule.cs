using System.Net.Http;
using System.Reflection;
using System.Resources;
using Autofac;
using Core.Common.AccountNumber.Parser;
using Core.Common.ChecksumCalculator;
using Core.CQRS.Autofac;
using Data.Core;
using FluentValidation;
using Service.Bank.Implementation;
using Service.Bank.Operations;
using Service.Bank.Proxy;
using Service.Bank.Proxy.ServiceHttpClient;
using Service.Bank.Proxy.ServicesRegister;
using Service.Bank.Router;
using Service.Bank.Validation;
using Service.Contracts;
using Service.Dto;
using Module = Autofac.Module;

namespace Service.Bank.Autofac
{
    public class BankServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //builder.RegisterAssemblyTypes(ThisAssembly);

            builder.RegisterType<ResourceManager>()
                .WithParameters(new[]
                {
                    new TypedParameter(typeof(string), "Service.Bank.OperationNames"),
                    new TypedParameter(typeof(Assembly), ThisAssembly)
                })
                .SingleInstance();

            builder.RegisterModule(new CqrsModule(ThisAssembly));

            builder.RegisterType<NrbChecksumCalculator>()
                .AsImplementedInterfaces();

            builder.RegisterType<OperationRegister>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AccountNumberParser>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ExternalTransferRouter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<OperationRegister>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<HttpClient>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<TransferServiceHttpClient>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<InterbankTransferServiceProxy>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<TransferServicesRegister>()
                .WithParameter(new TypedParameter(typeof(string), @"..\..\..\Core\InterbankTransferConfiguration.json"))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<BankDataContext>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<TransferDescriptionValidator>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AccountHistoryQueryValidator>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<BankService>()
                .Named<IBankService>(nameof(BankService));

            builder.RegisterDecorator<IBankService>((context, service) => new BankServiceValidationDecorator(service, context.Resolve<IValidator<TransferDescription>>(), context.Resolve<IValidator<AccountHistoryQuery>>()),
                nameof(BankService), nameof(BankServiceValidationDecorator));

            builder.RegisterDecorator<IBankService>((context, service) => new BankServiceExceptionDecorator(service),
                nameof(BankServiceValidationDecorator));
        }
    }
}