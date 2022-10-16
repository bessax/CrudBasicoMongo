using CrudBasicoMongo.Dados.Interface;
using CrudBasicoMongo.Modelo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudBasicoMongo.Dados;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _db;

    public MongoDbContext(MongoDbConfiguration config)
    {
        var client = new MongoClient(config.ConnectionString);
        _db = client.GetDatabase(config.Database);
    }

    //public IMongoCollection<Pessoa> Todos => _db.GetCollection<Pessoa>("PessoasDB");

    public IMongoCollection<Pessoa> Todos
    {
        get
        {
            IMongoCollection<Pessoa> pessoas = null;
            try
            {
                pessoas = _db.GetCollection<Pessoa>("PessoasDB");
            }
            catch (Exception ex)
            {

                Console.WriteLine("ferrou" + ex.Message); ;
            }

            return pessoas;
        }
    }

}
