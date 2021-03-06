using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetUserPreferencesController : ControllerBase
    {
        private readonly ILogger<GetUserPreferencesController> _logger;
        private readonly PreferenceManagement _preferenceManagement = new PreferenceManagement();
        
        public GetUserPreferencesController(ILogger<GetUserPreferencesController> logger)
        {
            _logger = logger;
        }
        //TODO:Check all loggers types
        [HttpGet]
        public string Get(string preferencesid)
        {
            var result = _preferenceManagement.GetUserPreferences(preferencesid);
            return JsonConvert.SerializeObject(result);
        }
    }
}
