using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;
public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    //O endereço pode existe e não ter cinema
    public virtual Cinema Cinema { get; set; }//Relação 1:1 | na minha classe cinema indico o endereço
}
