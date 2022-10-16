using CrudBasicoMongo.Dados.Interface;
using CrudBasicoMongo.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrudBasicoMongo.Dados
{
    public class MongoDbRepository : IMongoDBRepository
    {
        private readonly IMongoDbContext _context;
        public MongoDbRepository(IMongoDbContext context)
        {
            _context = context;
        }
        public void Create(Pessoa p)
        {
           _context.Todos.InsertOne(p);
        }

        public bool Delete(long id)
        {
            var filter = this.FindById(id);
            DeleteResult deleteResult =  _context
                                                .Todos
                                              .DeleteOne(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        private FilterDefinition<Pessoa> FindById(long id)
        {
            return Builders<Pessoa>.Filter.Eq(m => m.Id, id);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return  _context
                       .Todos
                       .Find(_ => true)
                       .ToList();
        }

        public long GetNextId()
        {
            return  _context.Todos.CountDocuments(new BsonDocument()) + 1;
        }

        public Pessoa GetOne(long id)
        {
            var filter = this.FindById(id);
            return _context
                    .Todos
                    .Find(filter)
                    .FirstOrDefault();
        }

        public bool Update(Pessoa p)
        {
            ReplaceOneResult updateResult =
             _context
                    .Todos
                    .ReplaceOne(
                        filter: g => g.Id == p.Id,
                        replacement: p);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
