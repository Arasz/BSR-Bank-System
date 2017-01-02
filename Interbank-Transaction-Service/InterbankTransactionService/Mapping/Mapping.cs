using AutoMapper;
using InterbankTransactionService.Dto;
using Shared.Transfer;

namespace InterbankTransactionService.Mapping
{
    public interface IMapping
    {
        MapperConfiguration Configuration { get; }

        IMapper Mapper { get; }
    }

    public class Mapping : IMapping
    {
        public MapperConfiguration Configuration { get; private set; }

        public IMapper Mapper => Configuration.CreateMapper();

        public Mapping()
        {
            ConfigureMapper();
        }

        private void ConfigureMapper()
        {
            Configuration = new MapperConfiguration(cfg => cfg.CreateMap<InterbankTransferDescription, TransferDescription>()
                .ForMember(transferDescription => transferDescription.Amount, memberConfigurationExpression => memberConfigurationExpression.ResolveUsing<AmountResolver>()));
        }
    }
}