using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPreferencesController : ControllerBase
    {
        private readonly ILogger<CreateAccountController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        
        public UserPreferencesController(ILogger<CreateAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Post(string userid)
        {
            var id = Guid.NewGuid();
           // return _userManagement.CreateAccount(id.ToString(), email, password);

        }
    }
}
