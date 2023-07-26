namespace FilmesApi.Data.Dtos;

public class ReadCinemaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ReadEnderecoDto Endereco { get; set; } //Irá buscar o endereço cadastrado do cinema
    public ICollection<ReadSessaoDto> Sessoes { get; set; }//Quais são as sessoes que esse cinema possui?
    //Depois precisamos acrescentar o mapeamento dessas sessões no profile

}
