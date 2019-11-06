using GivingBackChange.Dal.Contexts;
using GivingBackChange.Dal.Repositories.Base;
using GivingBackChange.Entity;

namespace GivingBackChange.Dal.Repositories.Implementations
{
    public class CoinBoxRepository : BaseRepository<Coin>, ICoinBoxRepository
    {
        public CoinBoxRepository(GivingBackChangeContext context) : base(context)
        {

        }
    }
}
