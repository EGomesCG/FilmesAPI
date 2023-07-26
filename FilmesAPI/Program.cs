using FilmesApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Conectando ao servidor de BD
//Iremos precisar baixar o pacote do MySQL => Pomelo.EntityFrameworkCore.MySql
var connectioString = builder.Configuration.GetConnectionString("FilmeConnection");

//Além da configuração do BD precisamos passar mais um parametro, a autenticação - que o próprio MySql gerencia
//Esse .UseLazyLoadingProxies() está indicando que quendo for instânciar uma classe e dentro dela existe outra
//instância os dados da mesma também deve serem carregados
builder.Services.AddDbContext<FilmeContext>(opts =>
    opts.UseLazyLoadingProxies()
    .UseMySql(connectioString, ServerVersion.AutoDetect(connectioString)));

//A ideia é que o AutoMapper faça mapeamento automático de um DTO 
//Atravez do builder podemos add um serviço de map em todo contexto da aplicação
//Precisamos informar para o AutoMapper o que ele deve fazer
//Vamos criar uma nova classe para informar isso para o AutoMapper = > Profiles -> FilmeProfile.cs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
//****************************
//Depois de ADD o newtonsoft, precisamos informar que iremos usar o add newtonSoftJson,
//com esse recurso podemos tratar o JSON de atualização parcial dos dados
builder.Services.AddControllers().AddNewtonsoftJson();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //Estamos definindo qual é a informação da API que estamos documentando. O título é "FilmesAPI", a versão é "v1",
    //estamos usando libs internas para pegar o contexto atual e gerar um arquivo XML e temos um caminho baseado no
    //arquivo que estamos gerando e um BaseDirectory baseado no contexto da aplicação que estamos executando.
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
