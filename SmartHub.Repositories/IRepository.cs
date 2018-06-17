using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Get(int id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(TEntity[] entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
