using CrudBasicoMongo.Dados;
using CrudBasicoMongo.Modelo;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Exemplo usando Mongo DB e C#");

MongoDbConfiguration StartConfiguration()
{
  
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

    return config;
}

StartApplication();
void StartApplication()
{
    char op = 'x';
    while (op!='6')
    {
        Console.Clear();
        Console.WriteLine("::: Cadastro de Pessoas :::");
        Console.WriteLine("1 - Cadastrar");
        Console.WriteLine("2 - Listar");
        Console.WriteLine("3 - Deletar");
        Console.WriteLine("4 - Pesquisar");
        Console.WriteLine("5 - Atualizar");
        Console.WriteLine("6 - Sair");
        Console.Write("Digite sua opção: ");
        op = Console.ReadLine()[0];
        switch (op)
        {
            case '1': Cadastrar();
                break;
            case '2':
                Listar();
                break;
            case '3':
                Deletar();
                break;
            case '4':
                Pesquisar();
                break;
            case '5':
                Atualizar();
                break;
            case '6':
                Console.WriteLine("... Encerrando a aplicação ...");
                op = '6';
                Console.ReadKey();
                break;
            default:
                break;
        }


    }
}

void Atualizar()
{
    Console.Clear();
    Console.WriteLine("::: Atualizar :::");
    Pessoa p = new Pessoa();
    Console.WriteLine("Infome o ID para atualização:");
    int id = int.Parse(Console.ReadLine());
    p.Id = id;
    Console.Write("Informe Nome: ");
    p.Nome = Console.ReadLine();
    Console.Write("Infome E-mail: ");
    p.Email = Console.ReadLine();
    Console.Write("Informe CPF: ");
    p.CPF = Console.ReadLine();
       

    //Criando um contexto
    var context = new MongoDbContext(StartConfiguration());

    //Criando repositório.
    var repository = new MongoDbRepository(context);

    var pessoaFromDB = repository.GetOne(id);
    p.Id = pessoaFromDB.Id;
    p.InternalId = pessoaFromDB.InternalId; 

    if (repository.Update(p))
    {
        Console.WriteLine("... Dados atualizados com sucesso! ...");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("... Dados não atualizados. Verifique! ...");
        Console.ReadKey();
    }
}

void Pesquisar()
{
    Console.Clear();
    Console.WriteLine("::: PESQUISAR :::");
    //Criando um contexto
    var context = new MongoDbContext(StartConfiguration());

    //Criando repositório.
    var repository = new MongoDbRepository(context);

    Console.Write("Informe o ID para pesquisa: ");
    int idPesquisa = int.Parse(Console.ReadLine());

    var p = repository.GetOne(idPesquisa);

    if (p==null)
    {
        Console.WriteLine("Pessoa não encontrada.");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine(p.ToString());
        Console.ReadKey();
    }
  

}

void Deletar()
{
    Console.Clear();
    Console.WriteLine("::: DELETAR :::");
    //Criando um contexto
    var context = new MongoDbContext(StartConfiguration());

    //Criando repositório.
    var repository = new MongoDbRepository(context);

    Console.Write("Informe o ID para remoção: ");
    int idRemocao = int.Parse(Console.ReadLine());

    if (repository.Delete(idRemocao))
    {
        Console.WriteLine($"ID removido {idRemocao}");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("ID não removido!");
        Console.ReadKey();
    }
    
}

void Listar()
{
    Console.Clear();
    Console.WriteLine("::: LISTAR :::");

    //Criando um contexto
    var context = new MongoDbContext(StartConfiguration());

    //Criando repositório.
    var repository = new MongoDbRepository(context);

    var lista = repository.GetAll();

    if (lista==null)
    {
        Console.WriteLine("... Não a dados a serem retornados ...");
        Console.ReadKey();
    }
    else
    {
        foreach (var item in lista)
        {
            Console.WriteLine("ID = " + item.Id);
            Console.WriteLine("Nome = " + item.Nome);
            Console.WriteLine("Email = " + item.Email);
            Console.WriteLine("CPF = " + item.CPF);
            Console.WriteLine(":::::::::::::::::::::::::::");
        }
        Console.ReadKey();
    }
}

void Cadastrar()
{
    Console.Clear();
    Console.WriteLine("::: CADASTRAR :::");
    Pessoa p = new Pessoa();
    Console.Write("Informe Nome: ");
    p.Nome = Console.ReadLine();
    Console.Write("Infome E-mail: ");
    p.Email = Console.ReadLine();
    Console.Write("Informe CPF: ");
    p.CPF = Console.ReadLine();

    //Criando um contexto
    var context = new MongoDbContext(StartConfiguration());

    //Criando repositório.
    var repository = new MongoDbRepository(context);
    repository.Create(p);

    Console.WriteLine("... Dados cadastrados com sucesso! ...");
    Console.ReadKey();
}