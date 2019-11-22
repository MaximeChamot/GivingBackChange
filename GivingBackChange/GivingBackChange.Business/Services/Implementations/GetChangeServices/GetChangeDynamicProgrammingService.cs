using GivingBackChange.Business.BusinessObjects;
using GivingBackChange.Business.Services.Implementations.GetChangeServices.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services.Implementations.GetChangeServices
{
    public class GetChangeDynamicProgrammingService : GetChangeBaseService
    {
        public GetChangeDynamicProgrammingService(ICoinBoxService coinBoxService) : base(coinBoxService)
        {
        }

        public override async Task<List<CoinBo>> GetChange(decimal remaningAmount)
        {
            var coinReferential = (await this._coinBoxService.GetCoins()).OrderBy(c => c.Value).ToList();
            var remaningAmountInCents = this.GetRemainingAmountInCents(remaningAmount);

            var computedChanges = new List<List<CoinBo>>
            {
                new List<CoinBo>()
            };


            var i = 1;

            while (i <= remaningAmountInCents)
            {
                computedChanges.Add(new List<CoinBo>());

                var j = 0;

                while (j < coinReferential.Count)
                {
                    var tempIndex = i - coinReferential[j].ValueInCent;

                    if (tempIndex >= 0 && (computedChanges[i].Count == 0 || computedChanges[i].Sum(c => c.Quantity) > computedChanges[tempIndex].Sum(c => c.Quantity) + 1))
                    {
                        computedChanges[i] = computedChanges[tempIndex].ToList();

                        var tmp = computedChanges[i].FirstOrDefault(c => c.Id == coinReferential[j].Id);

                        if (tmp != null)
                        {
                            tmp.Quantity += 1;
                        }
                        else
                        {
                            computedChanges[i].Add(new CoinBo(coinReferential[j].Id, coinReferential[j].Label, coinReferential[j].Value, 1));
                        }
                    }

                    j++;
                }

                i++;
            }

            return computedChanges[computedChanges.Count - 1];
        }
    }
}
