using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_api.Tools;

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

        public void Add(Entity entity)
        {
            Collection.InsertOne(entity);
        }

        public async Task Add_async(Entity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task Delete_async(string id)
        {
            await Collection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<IEnumerable<Entity>> GetAll_async()
        {
            return await Collection.Find(new FilterDefinitionBuilder<Entity>().Empty).ToListAsync();
        }

        public async Task<Entity> Get_async(string id)
        {
            return await Collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public Entity Get(string id)
        {
            return  Collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefault();
        }

        public async Task Update_async(Entity newEntity, string id)
        {
            var oldEntity = Get_async(id);
            var model = PUT<Entity>.Up(newEntity, oldEntity.Result);
            Console.Out.WriteLine("aefce");
            await Collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), model);
        }

        public IEnumerable<Entity> GetAll()
        {
            return Collection.Find(new FilterDefinitionBuilder<Entity>().Empty).ToList();
        }

        public void Delete(string id)
        {
            Collection.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
        }

        public void Update(Entity newEntity, string id)
        {
            
            Collection.ReplaceOne(new BsonDocument("_id", new ObjectId(id)), newEntity);
        }
    }
}
