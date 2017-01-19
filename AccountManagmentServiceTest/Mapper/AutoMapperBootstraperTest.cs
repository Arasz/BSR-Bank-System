using System;
using Autofac;
using FluentAssertions;
using Service.Bank.Mappings;
using Service.Bank.Mappings.Autofac;
using Xunit;

namespace Test.Service.Bank.Mapper
{
    public class AutoMapperBootstraperTest
    {
        private IContainer _container;

        public AutoMapperBootstraperTest()
        {
            ConfigureConatainer();
        }

        [Fact]
        public void MapperCreation_CreatedMapper_MapperShouldBeCreated()
        {
            var bootstraper = _container.Resolve<AutoMapperBootstraper>();

            Action bootstrapMapperAction = () => bootstraper.Bootstrap();

            bootstrapMapperAction.ShouldNotThrow();
        }

        private void ConfigureConatainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<OperationHistoryMappingsModule>();

            _container = containerBuilder.Build();
        }
    }
}