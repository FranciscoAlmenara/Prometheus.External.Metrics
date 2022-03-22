using Prometheus.External.Metrics;
using System;
using System.Threading.Tasks;

namespace PrometheusNetSampleApi.Repository.Metrics
{
    public class ExternalServiceMetricsRepository : IExternalServiceRepository
    {
        private readonly IPrometheusMetrics _prometheusMetrics;
        private readonly IExternalServiceRepository _externalServiceRepository;

        public ExternalServiceMetricsRepository(IPrometheusMetrics prometheusMetrics, IExternalServiceRepository externalServiceRepository)
        {
            _prometheusMetrics = prometheusMetrics ?? throw new ArgumentException();
            _externalServiceRepository = externalServiceRepository ?? throw new ArgumentException();
        }

        public Task<object> Get()
        {
            return _prometheusMetrics.AddMetrics(_externalServiceRepository.Get(), string.Empty, nameof(Get), nameof(IExternalServiceRepository));
        }
    }
}
