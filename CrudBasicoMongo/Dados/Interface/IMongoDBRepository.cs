using CrudBasicoMongo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudBasicoMongo.Dados.Interface;

public interface IMongoDBRepository
{
    IEnumerable<Pessoa> GetAll();
    Pessoa GetOne(long id);
    void Create(Pessoa p);
    bool Update(Pessoa p);
    bool Delete(long id);
    long GetNextId();
}
