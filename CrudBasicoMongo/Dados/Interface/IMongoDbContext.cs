using CrudBasicoMongo.Modelo;
using MongoDB.Driver;

namespace CrudBasicoMongo.Dados.Interface;

public interface IMongoDbContext
{
    IMongoCollection<Pessoa> Todos { get; }
}
