using Microsoft.EntityFrameworkCore;
using RouletteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.DataAccess
{
    public class RouleteApiContext : DbContext
    {
        public RouleteApiContext(DbContextOptions options) : base(options)
        {
                
        }
        public DbSet<Roulette> Roulettes { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>()
                .HasOne(b => b.Roulette)
                .WithMany(r => r.Bets)
                .HasForeignKey(b => b.RouletteId);
            modelBuilder.Entity<Bet>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId);
        }
    }
}
