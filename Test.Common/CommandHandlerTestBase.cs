using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Data.Core;

namespace Test.Common
{
    public abstract class CommandHandlerTestBase<TCommandHandler, TData> : DataContextAccessTest<TData>, IDependencyInjectionTest
        where TData : class
    {
        public IContainer Container { get; set; }

        protected abstract Expression<Func<BankDataContext, DbSet<TData>>> SelectDataSetFromDataContext { get; }

        protected CommandHandlerTestBase()
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
            builder.RegisterType<TCommandHandler>()
                .AsSelf()
                .AsImplementedInterfaces();

            RegisterDataContextMock(builder);
        }

        protected virtual void RegisterDataContextMock(ContainerBuilder builder)
            => builder.Register(componentContext => MockDataContext(SelectDataSetFromDataContext));
    }
}