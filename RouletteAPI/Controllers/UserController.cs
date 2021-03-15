using Microsoft.AspNetCore.Mvc;
using RouletteAPI.DataAccess;
using RouletteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private RouleteApiContext rouletteApiContext;

        public UserController(RouleteApiContext context)
        {
            rouletteApiContext = context;
        }

        [HttpGet]
        [Route("api/Users")]
        public ActionResult<List<User>> GetUsers()
        {
            var UsersResponse = rouletteApiContext.Users.ToList();
            return Ok(UsersResponse);
        }

        ~UserController()
        {
            rouletteApiContext.Dispose();
        }
    }
}
