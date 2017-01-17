using AutoMapper;

namespace Service.InterbankTransfer.Mapping
{
    public interface IMapperProvider
    {
        MapperConfiguration Configuration { get; }

        IMapper Mapper { get; }
    }
}