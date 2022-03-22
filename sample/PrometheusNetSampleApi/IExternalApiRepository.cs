using System.Threading.Tasks;

namespace PrometheusNetSampleApi
{
    public interface IExternalApiRepository
    {
        Task<object> Get();
    }
}
