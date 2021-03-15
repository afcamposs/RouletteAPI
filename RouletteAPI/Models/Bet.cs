using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models
{
    public class Bet
    {
        public int BetId { get; set; }
        [Range(0, 36)]
        public int Number { get; set; }
        public int Color { get; set; }
        public float MoneyBet { get; set; }
        public float MoneyEarned { get; set; }
        public int RouletteId { get; set; }
        public Roulette Roulette { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
