using GivingBackChange.Entity;
using Microsoft.EntityFrameworkCore;

namespace GivingBackChange.Dal.Contexts
{
    public class GivingBackChangeContext : DbContext
    {
        public GivingBackChangeContext()
        {
            
        }

        public DbSet<Coin> Coin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=./vendingMachine.db");
        }
    }
}
