using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateAccountController : ControllerBase
    {
        private readonly ILogger<CreateAccountController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        private readonly FavoritesManagement _favoritesManagement = new FavoritesManagement();
        
        public CreateAccountController(ILogger<CreateAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Post([FromBody] CreateAccountModel model)
        {
            var id = Guid.NewGuid();
            var result = _userManagement.CreateAccount(id.ToString(), model.Email, model.Password);
            if (result) {
                _userManagement.AddUserToPreferencesDB(id.ToString());
                _userManagement.AddUserToPantryDB(id.ToString());
                _favoritesManagement.AddUserToFavoritesDB(id.ToString());
            }
            return result;
        }
    }
}
