﻿/*
*Author: Xonaki
*Website: http://xonaki.com
*Copyright (c) xonaki.com
*License: BSD (3 Clause)
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NetCoreCMS.Framework.Core.Data;
using NetCoreCMS.Framework.Core.Mvc.Models;
using System.Linq;

namespace NetCoreCMS.Framework.Core.Mvc.Repository
{
    public class BaseRepository<EntityT, IdT> : IBaseRepository<EntityT, IdT> where EntityT : class, IBaseModel<IdT>
    {
        public BaseRepository(NccDbContext context)
        {
            Context = context;
            DbSet = Context.Set<EntityT>();
        }

        protected DbContext Context { get; }

        protected DbSet<EntityT> DbSet { get; }

        public EntityT Add(EntityT entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public EntityT Eidt(EntityT entity)
        {
            entity.Status = EntityStatus.Modified;
            Context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void SaveChange()
        {
            Context.SaveChanges();
        }

        public IQueryable<EntityT> Query()
        {
            return DbSet;
        }

        public void Remove(EntityT entity)
        {
            DbSet.Remove(entity);
        }

        public void DeletePermanently(EntityT entity)
        {
            DbSet.Remove(entity);
        }
    }
}