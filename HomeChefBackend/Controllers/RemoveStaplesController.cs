using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemoveStaplesController : ControllerBase
    {
        private readonly ILogger<RemoveStaplesController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public RemoveStaplesController(ILogger<RemoveStaplesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IngredientModel[] Post([FromBody] IngredientsAddRemoveModel model)
        {
            var result = _ingredientsManagement.RemoveStaples(model.PantryId, model.Ingredients);
            return _ingredientsManagement.GetStaples(model.PantryId);
        }
    }
}
