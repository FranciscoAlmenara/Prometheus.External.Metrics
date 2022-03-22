using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrometheusNetSampleApi.Repository
{
    public class ExternalApiRepository : IExternalApiRepository
    {
        private readonly HttpClient _httpClient;

        public ExternalApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException();
        }

        public async Task<object> Get()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("https://swapi.dev/api/people/1/");
            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
