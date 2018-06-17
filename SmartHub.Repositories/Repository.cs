using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace SmartHub.Repositories
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        public Repository(DbContext context)
        {
            Context = context as TDbContext;
            DbSet = Context.Set<TEntity>();
        }

        protected TDbContext Context { get; private set; }

        protected DbSet<TEntity> DbSet { get; private set; }

        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.AddOrUpdate(entity);
        }

        public void UpdateRange(TEntity[] entities)
        {
            DbSet.AddOrUpdate(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void RemoveAll(string tableName)
        {
            Context.Database.ExecuteSqlCommand("DELETE FROM " + tableName);
        }
    }
}
