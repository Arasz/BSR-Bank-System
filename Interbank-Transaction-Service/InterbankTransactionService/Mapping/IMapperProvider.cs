using AutoMapper;

namespace Service.InterbankTransaction.Mapping
{
    public interface IMapperProvider
    {
        MapperConfiguration Configuration { get; }

        IMapper Mapper { get; }
    }
}