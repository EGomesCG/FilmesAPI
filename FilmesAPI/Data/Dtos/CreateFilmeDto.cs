using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;
public class CreateFilmeDto
{
    //A propriedade de id não precisa está aqui porque o usuário não ira enviar id.
    //public int Id { get; set; }
    //Posso colocar em cima dos campos as validações que quero que aconteça
    [Required(ErrorMessage = "O título do filme é obrigatório")]//Obrigatório - Add mensagem entre parentes
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O genero do filme é obrigatório")]
    //StringLength é uma anotação de dados que será usada para validação da entrada do usuário 
    //MaxLength é usado para o Entity Framework para decidir o tamanho de um campo de valor de string ao criar o banco de dados.
    //Do ponto de vista da validação, também notei que o MaxLength é validado apenas no lado do servidor,
    //enquanto o StringLength também é validado no lado do cliente
    [StringLength(50, ErrorMessage = "O tamanho do genero não pode exceder 50 caracteres")]
    public string Genero { get; set; }

    [Required]
    [Range(70, 600, ErrorMessage = "A duração deve ter 70 e 600 minutos")]//Estipulando minimo e máximo 
    public int Duracao { get; set; }

    //public string Diretor { get; set; }
}
