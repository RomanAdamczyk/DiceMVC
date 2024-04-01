using DiceMVC.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Infrastructure
{
    public class WorkContext: IdentityDbContext
    {
        public DbSet<PlayerValue> PlayerValues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Dices> Dices { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayer> GamePlayer { get; set; }
        public DbSet<PlayersTurn> PlayersTurns { get; set; }

        public  WorkContext(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Player>()
               .HasOne(a => a.PlayerStatistic).WithOne(b => b.Player)
               .HasForeignKey<PlayerStatistic>(e => e.PlayerRef);

            builder.Entity<GamePlayer>()
                .HasKey(gp => new { gp.GameId, gp.PlayerId });

            builder.Entity<GamePlayer>()
                .HasOne<Game>(gp => gp.Game)
                .WithMany(g => g.GamePlayers)
                .HasForeignKey(gp => gp.GameId);

            builder.Entity<GamePlayer>()
                .HasOne<Player>(gp => gp.Player)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(gp => gp.PlayerId);

        }
    }
}
