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
    public class SaveUserPreferencesController : ControllerBase
    {
        private readonly ILogger<SaveUserPreferencesController> _logger;
        private readonly PreferenceManagement _preferenceManagement = new PreferenceManagement();
        
        public SaveUserPreferencesController(ILogger<SaveUserPreferencesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Post(string userid,string dietryrequirements,string intollerances, string measurementunits,string portion)
        {
            //todo: put all these in to an object instead.
            //todo: should be adding this by preferences id
            return _preferenceManagement.SavePreferences(userid,dietryrequirements,intollerances,measurementunits,portion);

        }
    }
}
