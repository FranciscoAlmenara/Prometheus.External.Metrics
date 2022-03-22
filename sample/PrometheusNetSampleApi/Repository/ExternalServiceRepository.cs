using Prometheus.External.Metrics;
using System;
using System.Threading.Tasks;

namespace PrometheusNetSampleApi.Repository
{
    public class ExternalServiceRepository : IExternalServiceRepository
    {
        public async Task<object> Get()
        {
            await Task.Delay(500);

            return await Task.FromResult(new { name = "Luke Skywalker", height = "172", mass = "77" });
        }
    }
}
