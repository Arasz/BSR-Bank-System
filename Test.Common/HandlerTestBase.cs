using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Autofac;
using Data.Core;
using Service.Bank.Autofac;

namespace Test.Common
{
    public abstract class HandlerTestBase<THandler, TData> : DataContextAccessTest<TData>, IDependencyInjectionTest
        where TData : class
    {
        public IContainer Container { get; set; }

        public THandler Handler => Container.Resolve<THandler>();

        protected abstract Expression<Func<BankDataContext, DbSet<TData>>> SelectDataSetFromDataContextExpression
        {
            get;
        }

        protected HandlerTestBase()
        {
            Container = BuildContainer();
        }

        public IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterComponents(containerBuilder);

            return containerBuilder.Build();
        }

        protected virtual void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterModule<BankServiceModule>();

            builder.RegisterType<THandler>()
                .AsSelf()
                .AsImplementedInterfaces();

            RegisterDataContextMock(builder);
        }

        protected virtual void RegisterDataContextMock(ContainerBuilder builder)
            => builder.Register(componentContext => MockDataContext(SelectDataSetFromDataContextExpression));
    }
}