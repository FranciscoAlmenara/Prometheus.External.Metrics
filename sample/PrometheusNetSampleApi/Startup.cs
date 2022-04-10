using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Prometheus.External.Metrics;
using Prometheus.HttpClientMetrics;
using PrometheusNetSampleApi.Repository;
using PrometheusNetSampleApi.Repository.Metrics;

namespace PrometheusNetSampleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddExternalMetrics();

            services.AddSingleton<IExternalServiceRepository>(serviceProvider =>
                new ExternalServiceMetricsRepository(serviceProvider.GetService<IPrometheusMetrics>(), new ExternalServiceRepository()));

            services
                .AddHttpClient<IExternalApiRepository, ExternalApiRepository>()
                .UseHttpClientMetrics();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMetricServer();
            app.UseHttpMetrics();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
