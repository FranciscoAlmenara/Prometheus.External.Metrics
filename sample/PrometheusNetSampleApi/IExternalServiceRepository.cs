using System.Threading.Tasks;

namespace PrometheusNetSampleApi
{
    public interface IExternalServiceRepository
    {
        Task<object> Get();
    }
}
