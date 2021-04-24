using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoesUserExistController : ControllerBase
    {
        private readonly ILogger<DoesUserExistController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        
        public DoesUserExistController(ILogger<DoesUserExistController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get(string email)
        {
           return _userManagement.doesAccountExist(email); 
        }
    }
}
