using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteAccountController : ControllerBase
    {
        private readonly ILogger<DeleteAccountController> _logger;
        private readonly UserManagement _userManagement = new UserManagement();
        private readonly PreferenceManagement _preferencseManagement = new PreferenceManagement();
        private readonly FavoritesManagement _favoritesManagement = new FavoritesManagement();
        private readonly PantryManagement _pantryManagement = new PantryManagement();
        
        public DeleteAccountController(ILogger<DeleteAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Post([FromBody] UserIdsModel userIds)
        {
            var result = _preferencseManagement.DeletePreferences(userIds.PreferencesId); 
            if(result){
                _pantryManagement.DeletePantry(userIds.UserId);
                if (result)
                {
                    result = _favoritesManagement.DeleteFavorites(userIds.UserId);
                    if (result)
                    {
                        _userManagement.DeleteAccount(userIds.UserId);
                    }
                }
            }
            return result;
        }
    }
}
