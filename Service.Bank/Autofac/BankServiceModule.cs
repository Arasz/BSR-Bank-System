using Autofac;
using Core.Common.AccountNumber.Parser;
using Core.Common.ChecksumCalculator;
using Core.CQRS.Autofac;
using Data.Core;
using FluentValidation;
using Service.Bank.Decorators;
using Service.Bank.Implementation;
using Service.Bank.Operations;
using Service.Bank.Proxy;
using Service.Bank.Proxy.ServiceHttpClient;
using Service.Bank.Proxy.ServicesRegister;
using Service.Bank.Router;
using Service.Bank.Validation;
using Service.Contracts;
using System;
using System.Net.Http;
using System.Reflection;
using System.Resources;
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
                .WithParameter(new TypedParameter(typeof(string), @"InterbankTransferConfiguration.json"))
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

            builder.Register<Func<Type, IValidator>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return type => (IValidator)componentContext.Resolve(typeof(IValidator<>).MakeGenericType(type));
            });

            builder.RegisterType<BankService>()
                .Named<IBankService>(nameof(BankService));

            builder.RegisterDecorator<IBankService>(CreateBankServiceValidationDecorator, nameof(BankService), nameof(BankServiceValidationDecorator));

            builder.RegisterDecorator<IBankService>(CreateBankServiceExceptionDecorator, nameof(BankServiceValidationDecorator));
        }

        private IBankService CreateBankServiceExceptionDecorator(IComponentContext context, IBankService service) =>
            new BankServiceExceptionDecorator(service);

        private IBankService CreateBankServiceValidationDecorator(IComponentContext context, IBankService service) =>
            new BankServiceValidationDecorator(service, context.Resolve<Func<Type, IValidator>>());
    }
}