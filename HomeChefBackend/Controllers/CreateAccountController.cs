using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateAccountController : ControllerBase
    {
        private readonly ILogger<CreateAccountController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        
        public CreateAccountController(ILogger<CreateAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Post(string email, string password)
        {
            var id = Guid.NewGuid();
            var result = _userManagement.CreateAccount(id.ToString(), email, password);
            return result;
        }
    }
}
