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
    public class GetFavoritesController : ControllerBase
    {
        private readonly ILogger<GetFavoritesController> _logger;
        private readonly FavoritesManagement _favoritesManagement = new FavoritesManagement();
        
        public GetFavoritesController(ILogger<GetFavoritesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public FavoriteRecipeModel[] Get(string favoritesId)
        {
            return _favoritesManagement.GetFavorites(favoritesId);
        }
    }
}
