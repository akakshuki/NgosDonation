using Domain.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Domain.Repository
{
    /// <summary>
    /// Implements the generic repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ProjectSem3Entities _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public BaseRepository(ProjectSem3Entities dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>An enumerable collection of entities.</returns>
        public virtual IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified identifier.</returns>
        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Creates a new entity without saving it.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        public virtual object CreateOnlyData(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        /// <summary>
        /// Creates and saves a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        public virtual TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        public virtual void Delete(int id)
        {
            var data = _dbSet.Find(id);
            if (data != null) _dbSet.Remove(data);
        }

        /// <summary>
        /// Edits an existing entity.
        /// </summary>
        /// <param name="entity">The entity to edit.</param>
        public virtual void Edit(TEntity entity)
        {
            _dbContext.Set<TEntity>().AddOrUpdate(entity);
        }
    }
}