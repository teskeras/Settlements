using System;
using System.Collections.Generic;
using System.Linq;

namespace Settlements.Data.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable
       where TEntity : class, IEntity
    {
        TEntity GetOne(int entityId);
        TEntity GetOneOrNone(int entityId);
        TEntity GetFirstOrNone(int entityId);

        TEntity GetOne(long entityId);
        TEntity GetOneOrNone(long entityId);
        TEntity GetFirstOrNone(long entityId);

        IQueryable<TEntity> Query();

        //TEntity Create();
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Attach(TEntity entity);
        void Update(TEntity entity);
        void Modify(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Refresh(TEntity entity);
        void Commit();
    }
}