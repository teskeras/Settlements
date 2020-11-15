using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Settlements.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : class, IEntity
    {
        protected DbContext DbContext { get; }

        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity GetOne(int entityId)
        {
            return Query().Single(entity => entity.Id == entityId);
        }

        public TEntity GetOneOrNone(int entityId)
        {
            return Query().SingleOrDefault(entity => entity.Id == entityId);
        }

        public TEntity GetFirstOrNone(int entityId)
        {
            return Query().FirstOrDefault(entity => entity.Id == entityId);
        }

        public TEntity GetOne(long entityId)
        {
            return Query().Single(entity => entity.Id == entityId);
        }

        public TEntity GetOneOrNone(long entityId)
        {
            return Query().SingleOrDefault(entity => entity.Id == entityId);
        }

        public TEntity GetFirstOrNone(long entityId)
        {
            return Query().FirstOrDefault(entity => entity.Id == entityId);
        }

        public virtual IQueryable<TEntity> Query()
        {
            return DbContext.Set<TEntity>();
        }

        //public TEntity Create()
        //{
        //    return DbContext.Set<TEntity>().
        //}

        public void Insert(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public void Attach(TEntity entity)
        {
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Modify(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Refresh(TEntity entity)
        {
            DbContext.Entry<TEntity>(entity).Reload();
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}