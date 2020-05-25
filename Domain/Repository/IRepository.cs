using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
       IEnumerable<TEntity> Get();

        TEntity GetById(int id);

        void CreateOnlyData(TEntity entity);

        TEntity Create(TEntity entity);

        void Delete(int id);

        void Edit(TEntity entity);
    }
}