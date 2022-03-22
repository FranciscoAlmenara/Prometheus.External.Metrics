using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PrometheusNetSampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalServiceController : ControllerBase
    {
        private readonly IExternalApiRepository _externalApiRepository;
        private readonly IExternalServiceRepository _externalServiceRepository;

        public ExternalServiceController(IExternalApiRepository externalApiRepository, IExternalServiceRepository externalServiceRepository)
        {
            _externalApiRepository = externalApiRepository ?? throw new ArgumentException();
            _externalServiceRepository = externalServiceRepository ?? throw new ArgumentException();
        }

        [HttpGet("api")]
        public async Task<ActionResult<object>> GetExternalApi()
        {
            await Task.Delay(1000);
            return Ok(await _externalApiRepository.Get());
        }

        [HttpGet("other")]
        public async Task<ActionResult<object>> GetExternalService()
        {
            await Task.Delay(1000);
            return Ok(await _externalServiceRepository.Get());
        }
    }
}
