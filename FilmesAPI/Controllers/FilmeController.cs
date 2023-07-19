using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace FilmesAPI.Controllers;
//*************************************************************************
//Aqui estamos na camada de apresentação, onde o usuário acessa os dados
//*************************************************************************
//As [] são anotações:
[ApiController] //API de acesso de controle
[Route("[controller]")] //Rota de acesso do usuário a classe, tenho que passa o nome da classe dentro dos cochetes
public class FilmeController : ControllerBase
{
    //Com a conexão do banco estabelecida, não iremos precisar das propriedades: lista e id, e,
    //pequenos ajustes teremos que realizar no decorre da aplicação
    ////Criando um objeto de lista de filme estático
    //private static List<Filme> filmes = new List<Filme>();
    ////Para diferenciar uma inserção da outra podemos usar o incremento, para isto de forma elegante podemos fazer?
    //private static int id = 0;

    //***************************************************************
    //Ajuste depois que fizemos a conexão do banco estabelecida
    //Propriedade que define a injeção de dependência - Da classe FilmeController com o BD
    private FilmeContext _context;

    //Depois de configurar o Mapper -> tenhos que declara aqui sua propriedade
    //Alt+Enter - para add este parametro ao construtor.
    private IMapper _mapper;

    //Construtor desta propriedade
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        //uma instância do contexto que vamos utilizar
        _context = context;
        _mapper = mapper;//Depois do paramentro no construtor -> podemos instanciar na aplicação
    }
    //////////////////////////////////////////////////////////////
    ///O que está aqui irá aparecer na API
    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    ///////////////////////////////////////////////////////////////
    
    //Como estamos add informação a aplicação então a funcionalidade utilizada será Post
    [HttpPost]//Verbo - Salvar
    [ProducesResponseType(StatusCodes.Status201Created)]//Produz uma resposta do tipo: Status 201 create
    //Metodo pronto para cadastrar filme no nosso sistema,
    //Se estamos cadastrando então o usuário precisa passar as informações para a API
    //[FromBody] -> as informções vem do corpo da requisição
    //Quando o usuário add um item precisamos dar uma resposta, para isso o método passará a ser do tipo IActionResult - interface
    //Este método está recebendo como parametro um objeto do tipo Filme => porém,
    //Precisamos alterar, pq? não é boa prática deixar nosso dados assim, disponivel na camada de controlador, pois,
    //temos dados que precimos proteje. Por ex.: o modelo do BD.
    //Então, a boa prática é: iremos criar classe que irá trafegar dados em diferentes camadas,
    //Camada de persistência => é onde vamos gravar o dado no banco - DTO (Data Transfer Object).
    //Agora não iremos receber um Filme => mas, CreateFilmeDto, e algumas mudança precisam serem feitas
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        //Agora que temos o BD, não precisaremos destas linhas
        //filme.Id = id++;
        //filmes.Add(filme);
        //************************
        //Ajuste depois criado BD
        ///Uma forma de resolver a add, é:
        //Filme filme = new Filme
        //{ Titulo = filmeDta.Titulo}
        //***************************************************************************
        //Outra forma, que iremos usar, é: lib - AutoMapper
        //Depois de add o AutoMapper na aplicação - posso mapear um filme Dto e obter um filme
        Filme filme = _mapper.Map<Filme>(filmeDto); //Tornamos o nosso código um pouco mais bem escrito, pois não estamos mais expondo informações desnecessárias
        _context.Filmes.Add(filme);
        _context.SaveChanges();

        //Por padrão depois que o usuário add um item devemos retornar algo, para resolver isto iremos usar o
        //CreateAtAction -> resposta
        //      1º Recebe por parametro a função que consulta filme por ID, essa função recebe por parametro o ID
        //      2º Passo o ID que acabe de criar para este filme
        //      3º Passo os dados do filme que acabou de se add na lista
        return CreatedAtAction(nameof(BuscarFilmeId), new { id = filme.Id }, filme); //Retono 201 - Algo foi criado e a localização de onde o recurso pode ser acessado no nosso sistema
    }
    
    [HttpGet]//Verbo - Consultar
    //Quando precisar consultar os dados, crio o metodo de busca do tipo LISTA DE FILME
    //Como lista possui várias dependências uma delas é a IEnumerable, então,
    //o ideal é alterar pois se a lista futuramente sofre alguma alteração na implementação, ou seja,
    //deixa de ser lista e se torne outra classe que tbém utilize o IEnumerable, então, não iremos precisar trocar o cabeçalho(tipo) do método
    //Quanto menos dependemos de classe concreta no cabeçalho melhor será na implementação
    //Podemos definir paginação principalmente quando temos uma base de dados muito grande,
    //então, o usuário ou a aplicação pode informar a partir de qual id deve se carregado os dados e quantos devem serem carregados
    //Assim podemos recebe COMO parametro o Skip(pula para) e o Take(quantos carregar). Lembrado que precisamos especificar o que esses parametro serão passados na consulta - usamos o [FromQuery]
    //Agora que estamos recebendo parametro na consulta. Exemplo da query de consulta:(https://localhost:7106/filme?skip=10&take=5), mas,
    //Caso o usuário não passe nada podemos deixar definido valores padrões
    public IEnumerable<ReadFilmeDto> BuscarFilmes([FromQuery] int skip = 0, int take = 50)
    {
        //************************
        //Ajuste depois criado BD - só add o _context.Filmes
        //***********************
        //Para a consulta não ser direta a classe com as regras de negocio, termos que realizar algumas mudanças
        //Vamos ass o _mapper-DTO => O método não é mais de filme mais sim de ReadFilmeDto
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));//Se não encontrar nada retorna uma lista vázia
    }

    //Como diferenciar um get do outro? Para resolver isto devemos informa que este get recebe como parametro o ID
    //Ou seja, se na chamada tiver o id este get será executado, senão, o outro será executado 
    [HttpGet("{id}")]//Critério de busca por ID
    
    //O usuário passa como parametro o id do filme que deseja
    //Este método retorna o tipo Filme, a exclamação na frente significa que o retorno pode se null
    //Para padronizar o retorno da aplicação usaremos NotFound() ou OK() no retorno, porém, teremos que usa a IActionResult
    public IActionResult BuscarFilmeId(int id)
    {
        //Aqui estamos buscando na lista de filmes o filme que tenha o id igual passado pelo usuário
        //************************
        //Ajuste depois criado BD - só add o _context.Filmes
        var filmeId = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if(filmeId == null) return NotFound();//Error 404 - Não encontrou o item
        //Podemos ler o filme pelo Dto para o usuário, sem explor as regras de negócio.
        //Posso o filme encontrado para o mapper_DTO
        var filmeDto = _mapper.Map<ReadFilmeDto>(filmeId);
        return Ok(filmeId);//Retorno 200 - Ok e passa o filme encontrado na busca
    }

    [HttpPut("{id}")]//Atualizar filme por id - todos os dados
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);//Estou resgatando o filme do BD - PEGANDO O FILME
        if (filme == null) return NotFound();//Se não existi no BD retorna no encontrado
        //Os campos sejam mapeados para meu filme
        _mapper.Map(filmeDto, filme);//PASSADO OS NOVOS DADOS DO FILME DTO PARA O FILME - Temos que especificar um map para o Update
        _context.SaveChanges();
        return NoContent();//Usamos o NoContent como retorno quando fazemos a alteração de um filme
    }
    [HttpPatch("{id}")] //Atualiza as informações, porém, parcialmente
    public IActionResult AtualizaParcialFilme(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        //*****************************
        //Como tenhos os dados parciais, então, precisamos pegar esses dados tranformar em filmeDto e depois em Filme ante de enviar a atualização ao BD
        //Estou pegando um filme e tranformando em filmeDto
        var filmeAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        //ModelState = modelo de estado valido
        patch.ApplyTo(filmeAtualizar, ModelState);

        //Se os dados recebidos não forem validos
        if (!TryValidateModel(filmeAtualizar)) 
        {
            //Irá exibir este erro - no modelState
            return ValidationProblem(ModelState);
        };
        //Senão, irá complilar os dados - transformando de filmeDto em filme e salvando no BD
        //Para consegui realizar este manuseio de JSON iremos usar: lib newtonSoft
        _mapper.Map(filmeAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(int id) 
    { 
        var Filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (Filme == null) return NotFound();
        _context.Remove(Filme);
        _context.SaveChanges();
        return NoContent();
    }
}
