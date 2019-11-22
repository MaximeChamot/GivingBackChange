using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Business.Services.Implementations.GetChangeServices.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services.Implementations.GetChangeServices
{
    public class GetChangeGreedyRecAlgorithmService : GetChangeBaseService
    {
        public GetChangeGreedyRecAlgorithmService(ICoinBoxService coinBoxService) : base(coinBoxService)
        {
        }

        public override async Task<List<CoinBo>> GetChange(decimal remaningAmount)
        {
            var coinReferential = (await this._coinBoxService.GetCoins()).OrderByDescending(c => c.Value).ToList();
            var change = new List<CoinBo>();

            this.GetChangeRec(this.GetRemainingAmountInCents(remaningAmount), 0, coinReferential, change);
            await this._coinBoxService.UpdateCoins(coinReferential);

            return change;
        }

        private void GetChangeRec(int remaningAmountInCents, int i, IList<CoinBo> coinReferential, IList<CoinBo> change)
        {
            if (remaningAmountInCents >= coinReferential[i].ValueInCent && coinReferential[i].Quantity > 0)
            {
                // Calculate the number of time I can take the specific coin
                var time = remaningAmountInCents / coinReferential[i].ValueInCent;

                if (coinReferential[i].Quantity - time < 0)
                {
                    time = coinReferential[i].Quantity;
                }

                // Calculate the remaining amount in cents
                remaningAmountInCents -= (time * coinReferential[i].ValueInCent);

                // Update coin quantity 
                coinReferential[i].Quantity -= time;

                // Add in change the coin with the amount
                change.Add(new CoinBo(coinReferential[i].Id, coinReferential[i].Label, coinReferential[i].Value, time));
            }

            if (i + 1 < coinReferential.Count)
            {
                this.GetChangeRec(remaningAmountInCents, i + 1, coinReferential, change);
            }
        }
    }
}
