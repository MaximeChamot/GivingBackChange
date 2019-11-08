﻿using System;
using System.Threading.Tasks;
using GivingBackChange.Dal.Contexts;
using GivingBackChange.Dal.Repositories;
using GivingBackChange.Dal.Repositories.Implementations;

namespace GivingBackChange.Dal.Managers.Implementations
{
    public class StoreManager : IStoreManager
    {
        private readonly GivingBackChangeContext _context;

        public StoreManager()
        {
            this._context = new GivingBackChangeContext();
        }

        #region Dispose

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
            {
                this._context.Dispose();
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region CoinBoxRepository

        private ICoinBoxRepository _coinBoxRepository;

        public ICoinBoxRepository CoinBoxRepository => this._coinBoxRepository ?? (this._coinBoxRepository = new CoinBoxRepository(this._context));

        #endregion

        public async Task Save()
        {
            await this._context.SaveChangesAsync();
        }
    }
}