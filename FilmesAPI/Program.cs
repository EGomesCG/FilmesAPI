using FilmesApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Conectando ao servidor de BD
//Iremos precisar baixar o pacote do MySQL => Pomelo.EntityFrameworkCore.MySql
var connectioString = builder.Configuration.GetConnectionString("FilmeConnection");

//Al�m da configura��o do BD precisamos passar mais um parametro, a autentica��o - que o pr�prio MySql gerencia
//Esse .UseLazyLoadingProxies() est� indicando que quendo for inst�nciar uma classe e dentro dela existe outra
//inst�ncia os dados da mesma tamb�m deve serem carregados
builder.Services.AddDbContext<FilmeContext>(opts =>
    opts.UseLazyLoadingProxies()
    .UseMySql(connectioString, ServerVersion.AutoDetect(connectioString)));

//A ideia � que o AutoMapper fa�a mapeamento autom�tico de um DTO 
//Atravez do builder podemos add um servi�o de map em todo contexto da aplica��o
//Precisamos informar para o AutoMapper o que ele deve fazer
//Vamos criar uma nova classe para informar isso para o AutoMapper = > Profiles -> FilmeProfile.cs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
//****************************
//Depois de ADD o newtonsoft, precisamos informar que iremos usar o add newtonSoftJson,
//com esse recurso podemos tratar o JSON de atualiza��o parcial dos dados
builder.Services.AddControllers().AddNewtonsoftJson();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //Estamos definindo qual � a informa��o da API que estamos documentando. O t�tulo � "FilmesAPI", a vers�o � "v1",
    //estamos usando libs internas para pegar o contexto atual e gerar um arquivo XML e temos um caminho baseado no
    //arquivo que estamos gerando e um BaseDirectory baseado no contexto da aplica��o que estamos executando.
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
