using System.Threading.Tasks;
using GivingBackChange.Dal.Repositories;
using GivingBackChange.Dal.Repositories.Implementations.Fakes;

namespace GivingBackChange.Dal.Managers.Implementations.Fakes
{
    public class FakeStoreManager : IStoreManager
    {
        public void Dispose()
        {
        }

        private ICoinBoxRepository _coinBoxRepository;

        public ICoinBoxRepository CoinBoxRepository => this._coinBoxRepository ?? (this._coinBoxRepository = new FakeCoinBoxRepository());

        public Task Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
