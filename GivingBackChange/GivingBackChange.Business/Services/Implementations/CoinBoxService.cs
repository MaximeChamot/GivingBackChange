using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Dal.Managers;
using GivingBackChange.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services.Implementations
{
    public class CoinBoxService : ICoinBoxService
    {
        private readonly IStoreManager _unitOfWork;

        public CoinBoxService(IStoreManager storeManager)
        {
            this._unitOfWork = storeManager ?? throw new ArgumentNullException(nameof(storeManager));
        }

        public async Task<IList<CoinBo>> GetCoins()
        {
            var coins = await this._unitOfWork.CoinBoxRepository.GetAll();

            return coins == null
                ? new List<CoinBo>()
                : coins.Select(c => new CoinBo(c.Id, c.Label, c.Value, c.Quantity)).ToList();
        }

        public async Task UpdateCoins(IList<CoinBo> updatedCoins)
        {
            var coins = updatedCoins.Select(c => new Coin(c.Id, c.Label, c.Value, c.Quantity)).ToList();
            await this._unitOfWork.CoinBoxRepository.UpdateAll(coins);
            await this._unitOfWork.SaveChanges();
        }
    }
}
