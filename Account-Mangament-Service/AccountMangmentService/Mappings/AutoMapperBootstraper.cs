using System;
using AutoMapper;
using Data.Core;
using Service.Bank.Commands;

namespace Service.Bank.Mappings
{
    public class AutoMapperBootstraper
    {
        private readonly Func<Type, object> _dependencyResolver;

        public AutoMapperBootstraper(Func<Type, object> dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public IMapper Bootstrap()
        {
            var configuration = new MapperConfiguration(Configure);

            return configuration.CreateMapper(_dependencyResolver);
        }

        private void Configure(IMapperConfigurationExpression configuration)
        {
            configuration.ConstructServicesUsing(_dependencyResolver);

            configuration.CreateMap<RegisterOperationCommand<WithdrawCommand>, Operation>()
                .ConvertUsing<WithdrawCommandConverter>();
        }
    }
}