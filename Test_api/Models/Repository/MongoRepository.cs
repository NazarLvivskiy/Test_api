using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_api.Models.Repository
{
    public class MongoRepository<Entity>: IRepository<Entity> where Entity : class
    {
        IGridFSBucket gridFS;   // файловое хранилище
        IMongoCollection<Entity> Collection;
        public MongoRepository()
        {
            string connectionString = "mongodb://localhost:27017/test_2";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
            // обращаемся к коллекции Products
            Collection = database.GetCollection<Entity>("Cars");
        }

        public async Task Add(Entity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task Delete(string id)
        {
            await Collection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            return await Collection.Find(new FilterDefinitionBuilder<Entity>().Empty).ToListAsync();
        }

        public async Task<Entity> Get(string id)
        {
            return await Collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }


        public async Task Update(Entity newEntity, string id)
        {
            await Collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), newEntity);
        }

        public async Task AddRange(IEnumerable<Entity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }
    }
}
