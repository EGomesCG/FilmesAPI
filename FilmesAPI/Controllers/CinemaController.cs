using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }
    
    [HttpPost] //Adicionar dados de um cinema
    public IActionResult AdicionarCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemaPorId), new { Id = cinema.Id}, cinemaDto);
    }

    //Consulta todos os cinemas - Se o usuário quere buscar o cinema por endereço, ele pode passar o id do endereço por parametro
    [HttpGet]
    public  IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null)
    {
        if(enderecoId == null)
        {
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        }
        //O context nos permite realizar alguns tipos de operações no banco, entre elas a execução de uma query SQL
        //Precisamos também mapear a busca para um <ReadCinemaDto> assim como estávamos fazendo antes, já que o nosso
        //retorno será um Enumerable de <ReadCinemaDto> e não somente um Cinema.
        //o _context.cinemas retorna um queryable, temos que convertê-lo para uma lista, adicionando à direita dos parênteses da query um .ToList().
        return _mapper.Map<List<ReadCinemaDto>>
            (_context.Cinemas.FromSqlRaw(
                $"SELECT  Id, Nome, EnderecoId FROM cinemas WHERE cinemas.EnderecoId = {enderecoId}").ToList());
    }

    [HttpGet("{id}")] //Consulta apenas um cinema por Id
    public IActionResult RecuperaCinemaPorId(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema != null)
        {
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema == null)
        {
            return NotFound();
        }
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(C => C.Id == id);
        if (cinema == null) return NotFound();
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}
