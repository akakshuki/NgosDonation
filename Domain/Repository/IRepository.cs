using System.Collections.Generic;

namespace Domain.Repository
{
    /// <summary>
    /// Defines the generic interface for a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>An enumerable collection of entities.</returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified identifier.</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Creates a new entity without saving it.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        object CreateOnlyData(TEntity entity);

        /// <summary>
        /// Creates and saves a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        void Delete(int id);

        /// <summary>
        /// Edits an existing entity.
        /// </summary>
        /// <param name="entity">The entity to edit.</param>
        void Edit(TEntity entity);
    }
}