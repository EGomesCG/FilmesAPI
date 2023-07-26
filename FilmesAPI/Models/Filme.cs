using System.ComponentModel.DataAnnotations;
namespace FilmesApi.Models;
public class Filme
{
    [Key]//Estou informando que este campo ID é uma chave para o BD
    [Required] //Obrigatória
    public int Id { get; set; }
    //Posso colocar em cima dos campos as validações que quero que aconteça

    [Required(ErrorMessage = "O título do filme é obrigatório")]//Obrigatório - Add mensagem entre parentes
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O genero do filme é obrigatório")]
    [MaxLength(50, ErrorMessage ="O tamanho do genero não pode exceder 50 caracteres")]//Definido limite de caracteres
    public string Genero { get; set; }

    [Required]
    [Range(70, 600, ErrorMessage ="A duração deve ter 70 e 600 minutos")]//Estipulando minimo e máximo 
    public int Duracao { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; } //Um filme pode ter uma ou muitas sessões => 1 - sessao : n - filme

    // public string Diretor { get; set; }
}
//A anotação[Required] torna obrigatório passar um parâmetro determinado.
//A anotação[StringLength] limita o tamanho de caracteres de uma string.
//A anotação [Range] limita o intervalo inferior e superior para valores numéricos.
