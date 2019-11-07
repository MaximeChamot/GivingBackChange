using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GivingBackChange.Dal.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GivingBackChange.Dal.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private GivingBackChangeContext _context;

        public BaseRepository(GivingBackChangeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Create

        public virtual async Task Create(T entity)
        {
            await this._context.Set<T>().AddAsync(entity);
            //await this._context.SaveChangesAsync();
        }

        public virtual async Task CreateAll(IEnumerable<T> entities)
        {
            await this._context.Set<T>().AddRangeAsync(entities);
            //await this._context.SaveChangesAsync();
        }

        #endregion

        #region Read

        public virtual async Task<List<T>> GetAll()
        {
            return await this._context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> GetMany(Expression<Func<T, bool>> where)
        {
            return await this._context.Set<T>().Where(where).ToListAsync();
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> where)
        {
            return await this._context.Set<T>().Where(where).FirstOrDefaultAsync();
        }

        #endregion

        #region Update

        public virtual async Task Update(T entity)
        {
            //this._dbContext.Entry(entity).State = EntityState.Modified;

            await Task.Run(() =>
            {
                this._context.Set<T>().Attach(entity);
                this._context.Entry(entity).State = EntityState.Modified;
            });

            //await Task.Run(() => this._context.Set<T>().Update(entity));
            //await this._context.SaveChangesAsync();
        }

        public virtual async Task UpdateAll(IEnumerable<T> entities)
        {
            await Task.Run(() => this._context.Set<T>().UpdateRange(entities));
            //await this._context.SaveChangesAsync();
        }

        #endregion

        #region Delete

        public virtual async Task Delete(T entity)
        {
            //if (this._context.Entry(entity).State == EntityState.Detached)
            //{
            //    this._context.Set<T>().Attach(entity);
            //}

            var searchedEntry = await this._context.Set<T>().FindAsync(entity);

            if (searchedEntry == null)
            {
                return;
            }

            this._context.Set<T>().Remove(searchedEntry);
        }

        public virtual async Task DeleteAll(IEnumerable<T> entities)
        {
            await Task.Run(() => this._context.Set<T>().RemoveRange(entities));
            //await this._context.SaveChangesAsync();
        }

        public virtual async Task DeleteMany(Expression<Func<T, bool>> where)
        {
            var items = await this.GetMany(where);

            foreach (var item in items.Where(item => this._context.Entry(item).State == EntityState.Detached))
            {
                this._context.Set<T>().Attach(item);
            }

            await this.DeleteAll(items);
        }

        #endregion
    }
}
