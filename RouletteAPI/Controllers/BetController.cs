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
    public class BetController : Controller
    {
        private RouleteApiContext rouletteApiContext;

        public BetController(RouleteApiContext context)
        {
            rouletteApiContext = context;
        }

        [HttpPost]
        [Route("api/Bet")]
        public ActionResult<Bet> AddBet(int userId,int rouletteId, [FromBody]Bet bet)
        {
            var user = rouletteApiContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            bet.User = user;
            var roulete = rouletteApiContext.Roulettes.Where(x => x.RouletteId == rouletteId).FirstOrDefault();
            bet.Roulette = roulete;
            bet.Color = bet.Number % 2 == 0 ? "red":"black";
            var betResponse = rouletteApiContext.Bets.Add(bet);
            rouletteApiContext.SaveChanges();
            return Ok(betResponse.Entity);
        }
        ~BetController()
        {
            rouletteApiContext.Dispose();
        }
    }
}
