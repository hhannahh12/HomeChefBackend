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
    public class RemoveFavoriteController : ControllerBase
    {
        private readonly ILogger<RemoveFavoriteController> _logger;
        private readonly FavoritesManagement _favoritesManagement;
        
        public RemoveFavoriteController(ILogger<RemoveFavoriteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Post([FromBody] AddFavoritesModel favorites)
        {
            return _favoritesManagement.RemoveFavorites(favorites.favoritesId, favorites.recipe);
        }
    }
}
