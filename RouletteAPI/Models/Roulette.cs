using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models
{
    public class Roulette
    {
        public int RouletteId { get; set; }
        public bool Status { get; set; }
        public string Winningcolor { get; set; }
        public int Winingnumber { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
