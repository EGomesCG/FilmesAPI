
using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;
namespace FilmesApi.Data;
public class FilmeContext : DbContext //Estou informando que essa classe vai estender de DbContext
{
    //Definindo como será o construtor
    //Que receberá as configurações, ou seja, as opções de acesso ao BD
    //: base(options) = quem receberá as configurações recebidas
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
    {
        
    }
    //Criar as propriedades de acesso
    //Conjunto de dados
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Vamos definir o método OnModelCreating de DbContext
        //Agora queremos que o id do filme e o if do cinema se torne a chave da tabela de relacionamento da sessão
        modelBuilder.Entity<Sessao>()//A entidade Sessao - tabela
            .HasKey(sessao => new {sessao.FilmeId, sessao.CinemaId}); //Terá como chave = FilmeId, CinemaId

        //Precisamos também estabelecer o relacionamento entre as tabelas ou classes
        modelBuilder.Entity<Sessao>()//Uma sessão
            .HasOne(sessao => sessao.Cinema)//Vai ter um cinema
            .WithMany(cinema => cinema.Sessoes)//Esse cinema vai ter uma ou várias sessões
            .HasForeignKey(sessao => sessao.CinemaId);//Essa sessão vai ter como chaave estrangeira o id do cinema

        modelBuilder.Entity<Sessao>()//Uma sessão
            .HasOne(sessao => sessao.Filme)//Vai ter um filme
            .WithMany(filme => filme.Sessoes)//Esse filme pode está em uma ou várias sessões
            .HasForeignKey(sessao => sessao.FilmeId);//essa sessão vai receber como chave estrangeira o id do filme.

    }
}
