using Domain.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ProjectSem3Entities _dbContext;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(ProjectSem3Entities dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void CreateOnlyData(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual void Delete(int id)
        {
            var data = _dbSet.Find(id);
            if (data != null) _dbSet.Remove(data);
        }

        public virtual void Edit(TEntity entity)
        {
            _dbContext.Set<TEntity>().AddOrUpdate(entity);
        }
    }
}