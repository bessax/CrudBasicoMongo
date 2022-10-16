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

        public Pessoa()
        {
            this.Id = new Random().Next(0, 1000);
        }

        public override string ToString()
        {
            return $"::: Dados Pessoa ::: \n" +
                   $"Id = {this.Id}\n" +
                   $"CPF = {this.CPF}\n" +
                   $"E-mail = {this.Email}\n" +
                   $"Nome = {this.Nome}";
        }
    }
}
