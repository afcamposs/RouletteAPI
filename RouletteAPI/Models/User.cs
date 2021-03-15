using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public float Credit { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
