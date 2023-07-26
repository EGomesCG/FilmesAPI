namespace FilmesApi.Data.Dtos;
public class CreateSessaoDto
{
    //Qdo for realizar o cadastro de uma sessão o usuário precisa informa o ID do filme
    public int FilmeId { get; set; }
    public int CinemaId { get; set; }
}
