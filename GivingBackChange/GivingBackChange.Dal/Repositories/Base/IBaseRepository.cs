using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GivingBackChange.Dal.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        #region Create

        Task Create(T entity);

        Task CreateAll(IList<T> entities);

        #endregion

        #region Read

        Task<List<T>> GetAll();

        Task<List<T>> GetMany(Expression<Func<T, bool>> where);

        Task<T> GetFirst(Expression<Func<T, bool>> where);

        #endregion

        #region Update

        Task Update(T entity);

        Task UpdateAll(IList<T> entity);

        #endregion

        #region Delete

        Task Delete(T entity);

        Task DeleteAll(IList<T> entities);

        Task DeleteMany(Expression<Func<T, bool>> where);

        #endregion
    }
}
