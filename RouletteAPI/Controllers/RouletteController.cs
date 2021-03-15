using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouletteAPI.DataAccess;
using RouletteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Controllers
{
    [ApiController]
    public class RouletteController : Controller
    {
        private RouleteApiContext rouletteApiContext;

        public RouletteController(RouleteApiContext context)
        {
            rouletteApiContext = context;
        }

        [HttpGet]
        [Route("api/CreateRoulette")]
        public ActionResult<int> CreateRoulette()
        {
            Roulette roulette = new Roulette();
            roulette.Status = false;
            var rouleteResponse = rouletteApiContext.Roulettes.Add(roulette);
            rouletteApiContext.SaveChanges();
            return Ok(rouleteResponse.Entity.RouletteId);
        }

        [HttpGet]
        [Route("api/OpenRoulette")]
        public ActionResult OpenRoulette(int id)
        {

            var roulette = rouletteApiContext.Roulettes.Where(r => r.RouletteId == id).FirstOrDefault();
            if (roulette == null)
                return BadRequest();
            roulette.Status = true;
            rouletteApiContext.Entry(roulette).State = EntityState.Modified;
            rouletteApiContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("api/CloseRoulette")]
        public ActionResult<Roulette> CloseRoulette(int id)
        {
            var roulette = rouletteApiContext.Roulettes.Where(r => r.RouletteId == id).FirstOrDefault();
            if (roulette == null)
                return BadRequest();
            if (roulette.Status == false)
                return BadRequest();
            roulette.Status = false;
            Random rd = new Random();
            roulette.Winingnumber = rd.Next(0, 36);
            roulette.Winningcolor = roulette.Winingnumber % 2 == 0 ? "red" : "black"; ;
            rouletteApiContext.Entry(roulette).State = EntityState.Modified;
            rouletteApiContext.SaveChanges();
            UpdateBetsByRoulette(roulette);
            var responseRoulette = rouletteApiContext.Roulettes
                .Where(r => r.RouletteId == id).Include(r => r.Bets)
                .FirstOrDefault();
            return Ok(responseRoulette);
        }

        [HttpGet]
        [Route("api/Roulettes")]
        public ActionResult<List<Roulette>> Roulettes()
        {

            var roulettes = rouletteApiContext.Roulettes.ToList();
            return Ok(roulettes);
        }

        private void UpdateBetsByRoulette(Roulette roulette)
        {
            var bets = rouletteApiContext.Bets.Where(r => r.RouletteId == roulette.RouletteId).ToList();
            foreach (var bet in bets)
            {
                if (bet.Color.Equals(roulette.Winningcolor))
                    bet.MoneyEarned = bet.MoneyEarned * 1.8f;
                else if (bet.Number == roulette.Winingnumber)
                    bet.MoneyEarned = bet.MoneyEarned * 5f;
                else
                    bet.MoneyEarned = 0;
                rouletteApiContext.Entry(bet).State = EntityState.Modified;
                rouletteApiContext.SaveChanges();
            }
        }

        ~RouletteController()
        {
            rouletteApiContext.Dispose();
        }
    }
}
