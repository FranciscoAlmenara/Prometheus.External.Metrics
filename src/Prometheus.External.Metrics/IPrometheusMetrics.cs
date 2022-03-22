using System;
using System.Threading.Tasks;

namespace Prometheus.External.Metrics
{
    public interface IPrometheusMetrics
    {
        //public Task<T> AddMetrics<T>(Func<Task<T>> func);
        public Task<T> AddMetrics<T>(Task<T> func, string method, string host, string client);
    }
}
