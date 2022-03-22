using Microsoft.Extensions.DependencyInjection;

namespace Prometheus.External.Metrics
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalMetrics(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPrometheusMetrics, PrometheusMetrics>();
        }
    }
}
