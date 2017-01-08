using AutoMapper;
using Service.Dto;
using Shared.Transfer;

namespace Service.InterbankTransaction.Mapping
{
    public class ConfiguredMapperProvider : IMapperProvider
    {
        public MapperConfiguration Configuration { get; private set; }

        public IMapper Mapper => Configuration.CreateMapper();

        public ConfiguredMapperProvider()
        {
            ConfigureMapper();
        }

        private void ConfigureMapper()
        {
            Configuration =
                new MapperConfiguration(cfg => cfg.CreateMap<InterbankTransferDescription, TransferDescription>()
                    .ForMember(transferDescription => transferDescription.Amount,
                        memberConfigurationExpression => memberConfigurationExpression.ResolveUsing<AmountResolver>()));
        }
    }
}