using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddIngredientsController : ControllerBase
    {
        private readonly ILogger<AddIngredientsController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public AddIngredientsController(ILogger<AddIngredientsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IngredientModel[] Post([FromBody] IngredientsAddRemoveModel model)
        {
            var id = Guid.NewGuid();
            var result = _ingredientsManagement.AddIngredients(model.PantryId, model.Ingredients);
            return _ingredientsManagement.GetPantry(model.PantryId);
            //TODO; Implement some kind of error handling 
        }
    }
}
