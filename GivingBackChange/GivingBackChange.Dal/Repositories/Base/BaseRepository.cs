using GivingBackChange.Dal.Contexts;
using GivingBackChange.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GivingBackChange.Dal.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
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
        }

        public virtual async Task CreateAll(IList<T> entities)
        {
            await this._context.Set<T>().AddRangeAsync(entities);
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
            var searchedEntity = await this._context.Set<T>().FindAsync(entity.Id);

            if (searchedEntity == null)
            {
                return;
            }

            await Task.Run(() =>
            {
                this._context.Entry(searchedEntity).State = EntityState.Detached;
                this._context.Entry(entity).State = EntityState.Modified;
            });

            await Task.Run(() => this._context.Set<T>().Update(entity));
        }

        public virtual async Task UpdateAll(IList<T> entities)
        {
            var i = 0;

            while (i < entities.Count)
            {
                await this.Update(entities[i]);
                i++;
            }

            //this._context.Set<T>().UpdateRange(entities);
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

        public virtual async Task DeleteAll(IList<T> entities)
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
