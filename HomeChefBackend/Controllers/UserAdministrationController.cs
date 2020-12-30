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
    public class UserAdministrationController : ControllerBase
    {
        private readonly ILogger<UserAdministrationController> _logger;
        private readonly UserManagement _userManagement;
        
        public UserAdministrationController(ILogger<UserAdministrationController> logger, 
            UserManagement userManagement)
        {
            _logger = logger;
            _userManagement = userManagement;
        }

        [HttpGet]
        public Result Get(string email, string password)
        {
            var result = _userManagement.Login(email, password);
            return result;// UserManagement.GetUsers();
        }
    }
}
