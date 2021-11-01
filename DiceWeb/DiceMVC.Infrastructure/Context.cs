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
    public class Context: IdentityDbContext
    {
        public DbSet<PlayerValue> PlayerValues { get; set; }
        public DbSet<Player> Players { get; set; }
        public  Context(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Player>()
                .HasOne(a => a.PlayerValue).WithOne(b => b.Player)
                .HasForeignKey<PlayerValue>(e => e.PlayerRef);

            builder.Entity<Player>()
                .HasOne(a => a.PlayerFreeValue).WithOne(b => b.Player)
                .HasForeignKey<PlayerFreeValue>(e => e.PlayerRef);
        }
    }
}
