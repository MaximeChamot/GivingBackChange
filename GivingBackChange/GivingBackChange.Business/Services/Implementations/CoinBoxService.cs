using GivingBackChange.Business.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GivingBackChange.Dal.Managers;
using GivingBackChange.Entity;

namespace GivingBackChange.Business.Services.Implementations
{
    public class CoinBoxService : ICoinBoxService
    {
        private readonly IStoreManager _unitOfWork;

        public CoinBoxService(IStoreManager storeManager)
        {
            this._unitOfWork = storeManager ?? throw new ArgumentNullException(nameof(storeManager));
        }

        public async Task<IList<CoinBo>> GetCoinsOrderedByAscendingOrderValue()
        {
            var coins = await this._unitOfWork.CoinBoxRepository.GetAll();
            var coinBos = coins.Select(c => new CoinBo(c.Id, c.Label, c.Value, c.Quantity)).ToList();

            return coinBos.OrderBy(c => c.Value).ToList();
        }

        public async Task<IList<CoinBo>> GetCoinsOrderedByDescendingOrderValue()
        {
            var coins = await this._unitOfWork.CoinBoxRepository.GetAll();
            var coinBos = coins.Select(c => new CoinBo(c.Id, c.Label, c.Value, c.Quantity)).ToList();

            return coinBos.OrderByDescending(c => c.Value).ToList();
        }

        public async Task SaveCoins(IList<CoinBo> updatedCoins)
        {
            var coins = updatedCoins.Select(c => new Coin(c.Id, c.Label, c.Value, c.Quantity)).ToList();

            await this._unitOfWork.CoinBoxRepository.UpdateAll(coins);
        }
    }
}
