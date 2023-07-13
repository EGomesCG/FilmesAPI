using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace FilmesAPI.Controllers;

//As [] são anotações:
[ApiController] //API de acesso
[Route("[controller]")] //Rota de acesso do usuário a classe, tenho que passa o nome da classe dentro dos cochetes
public class FilmeController : ControllerBase
{
    //Criando um objeto de lista de filme estático
    private static List<Filme> filmes = new List<Filme>();

    //Como estamos add informação a aplicação então a funcionalidade utilizada será Post
    [HttpPost]//Verbo - Salvar
    //Metodo pronto para cadastrar filme no nosso sistema,
    //Se estamos cadastrando então o usuário precisa passar as informações para a API
    //[FromBody] -> as informções vem do corpo da requisição
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filmes.Add(filme);
        Console.WriteLine(filme.Titulo);
        Console.WriteLine(filme.Duracao);
    }
    [HttpGet]//Verbo - Consultar
    //Quando precisar consultar os dados, crio o metodo de busca do tipo LISTA DE FILME
    //Como lista possui várias dependências uma delas é a IEnumerable, então,
    //o ideal é alterar pois se a lista futuramente sofre alguma alteração na implementação, ou seja,
    //deixa de se lista e se torne outra classe que tbém utilize o IEnumerable, então, não iremos precisar trocar o cabeçalho(tipo) do método
    //Quanto menos dependemos de classe concreta no cabeçalho melhor será na implementação
    public IEnumerable<Filme> RecuperarFilmes()
    {
        return filmes;
    }
}
