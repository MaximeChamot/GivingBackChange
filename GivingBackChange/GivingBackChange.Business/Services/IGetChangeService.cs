using System.Collections.Generic;
using System.Threading.Tasks;
using GivingBackChange.Business.BusinessObjects;

namespace GivingBackChange.Business.Services
{
    public interface IGetChangeService
    {
        Task<List<CoinBo>> GetChange(float remaningAmount);
    }
}
