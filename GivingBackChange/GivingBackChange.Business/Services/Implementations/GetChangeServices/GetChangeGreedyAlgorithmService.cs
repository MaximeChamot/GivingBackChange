using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Business.Services.Implementations.GetChangeServices.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services.Implementations.GetChangeServices
{
    public class GetChangeGreedyAlgorithmService : GetChangeBaseService
    {
        public GetChangeGreedyAlgorithmService(ICoinBoxService coinBoxService) : base(coinBoxService)
        {
        }

        public override async Task<List<CoinBo>> GetChange(float remaningAmount)
        {
            var coinReferential = await this._coinBoxService.GetCoinsOrderedByDescendingOrderValue();
            var change = new List<CoinBo>();
            var remaningAmountInCents = this.GetRemainingAmountInCents(remaningAmount);
            var i = 0;

            while (remaningAmountInCents > 0 && i < coinReferential.Count)
            {
                if (remaningAmountInCents >= coinReferential[i].ValueInCent)
                {
                    var time = remaningAmountInCents / coinReferential[i].ValueInCent;
                    remaningAmountInCents -= (time * coinReferential[i].ValueInCent);
                    change.Add(new CoinBo(coinReferential[i].Id, coinReferential[i].Label, coinReferential[i].Value, time));
                }

                i++;
            }

            return change;
        }
    }
}
