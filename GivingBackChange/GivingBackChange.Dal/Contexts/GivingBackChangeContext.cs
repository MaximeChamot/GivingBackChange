using System.Data.Common;
using GivingBackChange.Entity;
using Microsoft.EntityFrameworkCore;

namespace GivingBackChange.Dal.Contexts
{
    public class GivingBackChangeContext : DbContext
    {
        private const string ConnectionString = "Data Source=./givingBackChange.db";

        public GivingBackChangeContext()
        {
            
        }

        public DbSet<Coin> Coins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(ConnectionString);
        }
    }
}
