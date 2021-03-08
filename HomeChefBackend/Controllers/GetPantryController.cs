using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPantryController : ControllerBase
    {
        private readonly ILogger<GetPantryController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public GetPantryController(ILogger<GetPantryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get(string pantryid)
        {
            var id = Guid.NewGuid();
            var result = _ingredientsManagement.GetPantry(pantryid);
            
            return result;
        }
    }
}
