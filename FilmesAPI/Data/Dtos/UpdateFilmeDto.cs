using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;
public class UpdateFilmeDto
{
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O genero do filme é obrigatório")]
    [StringLength(50, ErrorMessage = "O tamanho do genero não pode exceder 50 caracteres")]
    public string Genero { get; set; }

    [Required]
    [Range(70, 600, ErrorMessage = "A duração deve ter 70 e 600 minutos")]
    public int Duracao { get; set; }

    public string Diretor { get; set; }
}
