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
    public class GetFavoriteByIdController : ControllerBase
    {
        private readonly ILogger<GetFavoriteByIdController> _logger;
        private readonly FavoritesManagement _favoritesManagement = new FavoritesManagement();
        
        public GetFavoriteByIdController(ILogger<GetFavoriteByIdController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public FavoriteRecipeModel Get(string favoritesId,string recipeId)
        {
            return _favoritesManagement.GetFavoriteById(favoritesId,recipeId);
        }
    }
}
