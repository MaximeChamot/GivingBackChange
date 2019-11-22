using GivingBackChange.Business.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services.Implementations.GetChangeServices.Base
{
    public abstract class GetChangeBaseService : IGetChangeService
    {
        protected ICoinBoxService _coinBoxService;

        protected GetChangeBaseService(ICoinBoxService coinBoxService)
        {
            this._coinBoxService = coinBoxService ?? throw new ArgumentNullException(nameof(coinBoxService));
        }

        public abstract Task<List<CoinBo>> GetChange(decimal remaningAmount);

        protected virtual int GetRemainingAmountInCents(decimal remaningAmount)
        {
            return (int)(remaningAmount * 100);
        }
    }
}
