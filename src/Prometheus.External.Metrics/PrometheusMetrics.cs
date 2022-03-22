using Prometheus.HttpClientMetrics;
using System;
using System.Threading.Tasks;

namespace Prometheus.External.Metrics
{
    public class PrometheusMetrics : IPrometheusMetrics
    {
        private readonly Histogram _handlerDuration = Prometheus.Metrics.CreateHistogram(
            "httpclient_request_duration_seconds",
            "Duration histogram of HTTP requests performed by an HttpClient.",
            new HistogramConfiguration
            {
                // 1 ms to 32K ms buckets
                Buckets = Histogram.ExponentialBuckets(0.001, 2, 16),
                LabelNames = HttpClientRequestLabelNames.All
            });

        private readonly Counter _handlerCount = Prometheus.Metrics.CreateCounter(
            "httpclient_requests_received_total",
            "Count of HTTP requests that have been completed by an HttpClient.",
            new CounterConfiguration
            {
                LabelNames = HttpClientRequestLabelNames.All
            });

        public async Task<T> AddMetrics<T>(Task<T> task, string method, string host, string client)
        {
            var success = true;
            try
            {
                using (_handlerDuration.WithLabels(method, host, client, "200").NewTimer())
                {
                    return await task.ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                success = false;
                throw;
            }
            finally
            {
                _handlerCount.WithLabels(method, host, client, success ? "200" : "500").Inc();
            }
        }
    }
}
