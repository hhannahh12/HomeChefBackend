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
    public class AddFavoriteController : ControllerBase
    {
        private readonly ILogger<AddFavoriteController> _logger;
        private readonly FavoritesManagement _favoritesManagement = new FavoritesManagement();
        
        public AddFavoriteController(ILogger<AddFavoriteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Post(FavoriteRecipeModel favorites)
        {
            return _favoritesManagement.AddFavorite(favorites);
        }
    }
}
