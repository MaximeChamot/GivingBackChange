using GivingBackChange.Dal.Contexts;
using GivingBackChange.Referential;
using System.Threading.Tasks;

namespace GivingBackChange.Dal.Managers.Implementations
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly GivingBackChangeContext _context;

        public DatabaseManager()
        {
            this._context = GivingBackChangeContext.GetInstance();
        }

        public async Task EnsureCreated()
        {
            await this._context.Database.EnsureCreatedAsync();
        }

        public async Task EnsureDeleted()
        {
            await this._context.Database.EnsureDeletedAsync();
        }

        public async Task Seed()
        {
            var coinReferentials = SwissFrancCoinReferential.GetAll();

            await this._context.Coins.AddRangeAsync(coinReferentials);
            await this._context.SaveChangesAsync();
        }
    }
}
