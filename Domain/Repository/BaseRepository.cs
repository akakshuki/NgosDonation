using Domain.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ProjectSem3Entities _dbContext;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(ProjectSem3Entities dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void CreateOnlyData(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var data = _dbSet.Find(id);
            if (data != null) _dbSet.Remove(data);
        }

        public void Edit(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}