using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private readonly IMyService _service;
        private ILogger<MyController> _logger;

        public MyController(ILogger<MyController> logger, IMyService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public int Get()
        {
            return _service.Get();
        }
    }
}