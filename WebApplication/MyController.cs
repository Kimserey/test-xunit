using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private readonly IMyService _service;
        private ILogger<MyController> _logger;
        private readonly UserDbContext _db;

        public MyController(UserDbContext db, ILogger<MyController> logger, IMyService service)
        {
            _db = db;
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public int Get()
        {
            return _service.Get();
        }

        [HttpGet("user/{name}")]

        public async Task<ActionResult<object>> GetUser(string name)
        {
            var user =
                await _db.Users.Include(u => u.Profile).FirstAsync(u => u.Name == name);

            return new
            {
                id = user.Id,
                name = user.Name,
                profile = new
                {
                    address = user.Profile.Address
                }
            };
        }
    }
}