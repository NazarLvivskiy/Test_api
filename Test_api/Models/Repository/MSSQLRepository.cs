using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Test_api.Models.Repository
{
    public class MSSQLRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ApplicationContext db;

        private DbSet<TEntity> _entities;

        public MSSQLRepository(ApplicationContext context)
        {
            db = context;
            _entities = db.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public Task Delete(string id)
        {
            _entities.Remove(Get(id).Result);
            return null;
        }

        public async Task<TEntity> Get(string id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public Task Update(TEntity newEntity, string id)
        {
            _entities.Update(newEntity);
            return null;
        }
    }
}
