using FilmesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;
public class Sessao
{
    //Como essa classe se tornou a classe de relacionamento de tabela
    [Key]// Não precisaremos mais do ID, pois, usaremos uma chave composta(entre o id do Filme e do id do cinema)
    [Required]
    public int Id { get; set; }

    //[Required]//Quando criamos a Sessao, o FilmeId não podia ser nulo e o CinemaId podia ser nulo, mas,
    //para essa classe se torna uma tabela de relacionamento precisamos retiar essa obrigatóriedade
    //assim, evitamos qualquer conflito a nível de nulidade com nosso índice
    public int? FilmeId { get; set; }//Tornando nulos esse campo null

    public virtual Filme Filme { get; set; }

   // [Required]//Tirando a obrigatóriedade
    public int? CinemaId { get; set; }//Estamos permitindo que este campo seja null na tabela Sessões no BD

    public virtual Cinema Cinema { get; set; }

    //////////////////////////////////
    //Construindo a chave composta:
    //  1ª precisamos na classe de contexto (FilmeContext) - Vamos escrever esse relacionamento como código.

}
