using GivingBackChange.Business.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivingBackChange.Business.Services
{
    public interface IGetChangeService
    {
        Task<List<CoinBo>> GetChange(decimal remaningAmount);
    }
}
