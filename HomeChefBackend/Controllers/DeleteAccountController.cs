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
        
        public DeleteAccountController(ILogger<DeleteAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Post([FromBody] UserIdsModel userIds)
        {
            //TODO: DELETE USER FROM OTHER TABLE
            var result = _preferencseManagement.DeletePreferences(userIds.PreferencesId); 
            if(result){
                _userManagement.DeleteAccount(userIds.UserId);
            }
            return result;
        }
    }
}
