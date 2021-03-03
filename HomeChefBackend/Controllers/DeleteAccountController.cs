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
        
        public DeleteAccountController(ILogger<DeleteAccountController> logger)
        {
            _logger = logger;
        }

        [HttpDelete]
        public bool Delete(string id)
        {
            //TODO: DELETE USER FROM OTHER TABLE
            var result = _userManagement.DeleteAccount(id);
            return result;
        }
    }
}
