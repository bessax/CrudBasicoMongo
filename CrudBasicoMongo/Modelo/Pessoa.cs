using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudBasicoMongo.Modelo
{
    public class Pessoa
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
