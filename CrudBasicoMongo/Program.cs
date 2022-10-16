using CrudBasicoMongo.Dados;
using CrudBasicoMongo.Dados.Interface;
using CrudBasicoMongo.Modelo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

Console.WriteLine("Exemplo usando Mongo DB");

//Lendo informações de configuração.
IConfiguration configuration = new ConfigurationBuilder()
                                               .SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile("appSettings.json")
                                               .Build();

//Montando a configuração.
MongoDbConfiguration config = new MongoDbConfiguration();
config.Host = configuration["Host"];
config.Database = configuration["Database"];
config.Port = Convert.ToInt16(configuration["Port"]);
config.User = configuration["User"];
config.Password = configuration["Password"];

//Criando um contexto
var context = new MongoDbContext(config);

//Criando repositório.

var repository = new MongoDbRepository(context);

CadastrarPessoa(context);

void CadastrarPessoa(MongoDbContext context)
{
    var pessoa = new Pessoa
    {
        Id = new Random().Next(0, 100),
        CPF="10843252766",
        Email="andre@email.com",
        Nome="André Silva"

    }; 
     repository.Create(pessoa);
}

Console.WriteLine("Iniciando Aplicação.");
