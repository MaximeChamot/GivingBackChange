using System.Threading.Tasks;
using GivingBackChange.Dal.Managers;

namespace GivingBackChange.Business.Services.Implementations
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDatabaseManager _databaseManager;

        public DatabaseService(IDatabaseManager databaseManager)
        {
            this._databaseManager = databaseManager;
        }

        public async Task DeleteAndCreateNewDatabase()
        {
            await this._databaseManager.EnsureDeleted();
            await this._databaseManager.EnsureCreated();
        }

        public async Task SeedData()
        {
            await this._databaseManager.Seed();
        }
    }
}
