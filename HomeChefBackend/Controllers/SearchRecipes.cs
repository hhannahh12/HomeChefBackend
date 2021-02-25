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
    public class SearchRecipesController: ControllerBase
    {
        private readonly ILogger<SearchRecipesController> _logger;
        
        public SearchRecipesController(ILogger<SearchRecipesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get(string preferences)
        {
            var id = Guid.NewGuid();
            return ;

        }
    }
}
