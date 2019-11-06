using GivingBackChange.Entity;
using GivingBackChange.Referential;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GivingBackChange.Dal.Repositories.Implementations.Fakes
{
    public class FakeCoinBoxRepository : ICoinBoxRepository
    {
        public Task Create(Coin entity)
        {
            throw new NotImplementedException();
        }

        public Task CreateAll(IEnumerable<Coin> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Coin>> GetAll()
        {
            var coinsReferential = await Task.Run(() => SwissFrancCoinReferential.GetAll());
            var coins = new List<Coin>();

            coinsReferential.ForEach(c => coins.Add(new Coin(c.Id, c.Label, c.Value, 10)));

            return coins;
        }

        public Task<List<Coin>> GetMany(Expression<Func<Coin, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public Task<Coin> GetFirst(Expression<Func<Coin, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public Task Update(Coin entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAll(IEnumerable<Coin> entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Coin entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAll(IEnumerable<Coin> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMany(Expression<Func<Coin, bool>> @where)
        {
            throw new NotImplementedException();
        }
    }
}
