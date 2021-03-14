using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemoveIngredientsController : ControllerBase
    {
        private readonly ILogger<RemoveIngredientsController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public RemoveIngredientsController(ILogger<RemoveIngredientsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IngredientModel[] Post([FromBody] IngredientsAddRemoveModel model)
        {
            var result = _ingredientsManagement.RemoveIngredients(model.PantryId, model.Ingredients);
            return _ingredientsManagement.GetPantry(model.PantryId);
        }
    }
}
