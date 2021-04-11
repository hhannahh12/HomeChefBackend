using System;
using HomeChefBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeChefBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetClearoutDateController : ControllerBase
    {
        private readonly ILogger<GetClearoutDateController> _logger;
        private readonly PantryManagement _ingredientsManagement = new PantryManagement();
        
        public GetClearoutDateController(ILogger<GetClearoutDateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get(string pantryId)
        {
            var lastupdated = _ingredientsManagement.GetClearoutDate(pantryId);
            //todo: new date time doesnt do now
            var currentDate = DateTime.Now;
            var numberOfDays = (currentDate - lastupdated).TotalDays;
            if(numberOfDays > 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
