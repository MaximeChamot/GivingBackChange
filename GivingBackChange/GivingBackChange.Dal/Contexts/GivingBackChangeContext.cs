using GivingBackChange.Entity;
using Microsoft.EntityFrameworkCore;

namespace GivingBackChange.Dal.Contexts
{
    public class GivingBackChangeContext : DbContext
    {
        private static GivingBackChangeContext _instance;
        private const string ConnectionString = "Data Source='C:\\Program Files\\dotnet\\givingBackChange.db'";

        private GivingBackChangeContext()
        {
        }

        public static GivingBackChangeContext GetInstance()
        {
            return _instance ?? (_instance = new GivingBackChangeContext());
        }

        public DbSet<Coin> Coins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(ConnectionString);
        }
    }
}
