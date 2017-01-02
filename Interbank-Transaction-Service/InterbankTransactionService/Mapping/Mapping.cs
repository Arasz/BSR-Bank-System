using AccountMangmentService.Transfer.Dto;
using AutoMapper;
using InterbankTransactionService.Dto;

namespace InterbankTransactionService.Mapping
{
    public class Mapping
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