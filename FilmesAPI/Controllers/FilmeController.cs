using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }
    
    [HttpPost] //Adicionar dados de um FILME
    public IActionResult AdicionarfILME([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme= _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = filme.Id}, filmeDto);
    }

    //Consulta todos os filmes
    [HttpGet]
    public  IEnumerable<ReadFilmeDto> RecuperaFilmes()
    {
           return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.ToList());
    }

    [HttpGet("{id}")] //Consulta apenas um filme por Id
    public IActionResult RecuperaFilmePorId(int id)
    {
        Filme filme = _context.Filmes.FirstOrDefault(c => c.Id == id);
        if (filme != null)
        {
            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        Filme filme = _context.Filmes.FirstOrDefault(c => c.Id == id);
        if (filme == null)
        {
            return NotFound();
        }
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id)
    {
        Filme filme = _context.Filmes.FirstOrDefault(C => C.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
