using System.Threading.Tasks;

namespace Client.UniversalLightClient.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}