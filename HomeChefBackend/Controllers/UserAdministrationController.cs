using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAdministrationController : ControllerBase
    {
        private readonly ILogger<UserAdministrationController> _logger;
        public enum Result
        {
            Success,
            NoAccount,
            IncorrectDetails,
            InternalErr
        }
        public UserAdministrationController(ILogger<UserAdministrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Result Get()
        {
            return Result.Success;// UserManagement.GetUsers();
        }
    }
}
