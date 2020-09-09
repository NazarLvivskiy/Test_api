using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_api.Models.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(string id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task Delete(string id);

        Task Update(TEntity newEntity, string id);
    }
}
