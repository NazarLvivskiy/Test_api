using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_api.Models.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get_async(string id);

        TEntity Get(string id);

        Task<IEnumerable<TEntity>> GetAll_async();

        IEnumerable<TEntity> GetAll();

        Task Add_async(TEntity entity);

        void Add(TEntity entity);

        Task Delete_async(string id);

        void Delete(string id);

        Task Update_async(TEntity newEntity, string id);

        void Update(TEntity newEntity, string id);
    }
}
