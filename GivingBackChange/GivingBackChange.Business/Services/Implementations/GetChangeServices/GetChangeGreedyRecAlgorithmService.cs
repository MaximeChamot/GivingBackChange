using System.Collections.Generic;
using System.Threading.Tasks;
using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Business.Services.Implementations.GetChangeServices.Base;

namespace GivingBackChange.Business.Services.Implementations.GetChangeServices
{
    public class GetChangeGreedyRecAlgorithmService : GetChangeBaseService
    {
        public GetChangeGreedyRecAlgorithmService(ICoinBoxService coinBoxService) : base(coinBoxService)
        {
        }

        public override async Task<List<CoinBo>> GetChange(float remaningAmount)
        {
            var coinReferential = await this._coinBoxService.GetCoinsOrderedByDescendingOrderValue();
            var change = new List<CoinBo>();

            this.GetChangeRec(this.GetRemainingAmountInCents(remaningAmount), 0, coinReferential, change);

            return change;
        }

        private void GetChangeRec(float remaningAmountInCents, int i, IList<CoinBo> coinReferential, IList<CoinBo> change)
        {
            if (i == coinReferential.Count)
            {
                return;
            }

            if (remaningAmountInCents >= coinReferential[i].ValueInCent)
            {
                var time = (int)(remaningAmountInCents / coinReferential[i].ValueInCent);
                remaningAmountInCents = remaningAmountInCents - (time * coinReferential[i].ValueInCent);
                change.Add(new CoinBo(coinReferential[i].Id, coinReferential[i].Label, coinReferential[i].Value, time));
            }

            this.GetChangeRec(remaningAmountInCents, i + 1, coinReferential, change);
        }
    }
}
