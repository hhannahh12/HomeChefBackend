using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get(string email, string password)
        {
            var result = _userManagement.Login(email, password);
            return JsonConvert.SerializeObject(result);
        }
    }
}
