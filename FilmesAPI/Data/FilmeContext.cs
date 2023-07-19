using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace FilmesAPI.Data;
public class FilmeContext : DbContext //Estou informando que essa classe vai estender de DbContext
{
    //Definindo como será o construtor
    //Que receberá as configurações, ou seja, as opções de acesso ao BD
    //: base(options) = quem receberá as configurações recebidas
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
    {
        
    }
    //Criar as propriedades de acesso
    //Conjunto de dados, que será de um filme
    public DbSet<Filme> Filmes { get; set; }
}
