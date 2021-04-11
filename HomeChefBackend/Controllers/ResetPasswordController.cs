using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResetPasswordController : ControllerBase
    {
        private readonly ILogger<ResetPasswordController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        
        public ResetPasswordController(ILogger<ResetPasswordController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get(string email, string password)
        {
            var result = _userManagement.resetPassword(email,password);
            return result;
        }
    }
}
