﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly PreferenceManagement _preferencesManagement = new PreferenceManagement();
        
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get(string email, string password)
        {
            string[] result = new string[2];
            result[0] = _userManagement.Login(email, password);
          
            if(result[0] != "Failed")
            {
                result[1]= _preferencesManagement.GetPreferencesId(result[0]);
            }
            
            return JsonConvert.SerializeObject(result);
        }
    }
}
