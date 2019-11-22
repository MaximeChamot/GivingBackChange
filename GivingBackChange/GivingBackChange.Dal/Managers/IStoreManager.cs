using System;
using System.Threading.Tasks;
using GivingBackChange.Dal.Repositories;

namespace GivingBackChange.Dal.Managers
{
    public interface IStoreManager : IDisposable
    {
        ICoinBoxRepository CoinBoxRepository { get; }

        Task SaveChanges();
    }
}
