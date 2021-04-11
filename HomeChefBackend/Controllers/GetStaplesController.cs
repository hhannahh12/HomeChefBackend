using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetStaplesController : ControllerBase
    {
        private readonly ILogger<GetStaplesController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public GetStaplesController(ILogger<GetStaplesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IngredientModel[] Get(string pantryid)
        {
            var result = _ingredientsManagement.GetStaples(pantryid);
            
            return result;
        }
    }
}
