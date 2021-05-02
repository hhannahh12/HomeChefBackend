using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddStaplesController : ControllerBase
    {
        private readonly ILogger<AddStaplesController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public AddStaplesController(ILogger<AddStaplesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IngredientModel[] Post([FromBody] IngredientsAddRemoveModel model)
        {
            var result = _ingredientsManagement.AddStaples(model.PantryId, model.Ingredients);
            return _ingredientsManagement.GetStaples(model.PantryId);
        }
    }
}
