﻿using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public abstract class BaseRepository<TEntry> : Notifiable, IBaseRepository<TEntry> where TEntry : class
    {
        protected readonly CloudMeToDeTaxiContext Context;

        protected BaseRepository(CloudMeToDeTaxiContext context)
        {
            Context = context;
        }

        public async Task<int> CountAsync()
        {
            return await this.Context.Set<TEntry>().CountAsync();
        }

        public async Task<TEntry> FindByIdAsync(params object[] keyComposition)
        {
            return await this.Context.FindAsync<TEntry>(keyComposition);
        }

        public async Task<TEntry> FindByIdAsync(object key)
        {
            return await this.Context.FindAsync<TEntry>(key);
        }

        public async Task<TEntry> FindByIdAsync(object key, string[] paths)
        {
            var model = await this.Context.Set<TEntry>().FindAsync(key);
            foreach (var path in paths)
            {
                if (path.EndsWith("s") && !path.Contains(".")) //se for uma coleção
                {
                    this.Context.Entry(model).Collection(path).Load();
                }
                else
                {
                    this.Context.Entry(model).Reference(path).Load();
                }
            }
            return model;
        }

        public IEnumerable<TEntry> FindAll()
        {
            return this.Context.Set<TEntry>();
        }

        public async Task<IEnumerable<TEntry>> FindAllAsync()
        {
            return await this.Context.Set<TEntry>().ToListAsync();
        }

        public async Task<IEnumerable<TEntry>> FindAllAsync(string[] paths)
        {
            var qry = this.Context.Set<TEntry>() as IQueryable<TEntry>;
            if (paths != null && paths.Any())
            {
                foreach (var path in paths)
                {
                    qry = qry.Include(path);
                }
            }

            return await qry.ToListAsync();
        }

        public async Task<bool> SaveAsync(TEntry entry)
        {
            this.Context.Entry(entry).State = EntityState.Added;
            return await Task.FromResult(true);
        }

        public async Task<bool> ModifyAsync(TEntry entry)
        {
            this.Context.Entry(entry).State = EntityState.Modified;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(TEntry entry)
        {
            this.Context.Entry(entry).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }


        public IEnumerable<TEntry> Search(Expression<Func<TEntry, bool>> where, string[] paths = null)
        {
            IQueryable<TEntry> qry = this.Context.Set<TEntry>();

            if (paths != null)
            {
                foreach (var path in paths)
                {
                    qry = qry.Include(path);
                }
            }

            return qry.Where(where);
        }

        public void Detach<TEntity>(TEntity entry) where TEntity : class
        {
            Context.Entry(entry).State = EntityState.Detached;
        }

    }
}
