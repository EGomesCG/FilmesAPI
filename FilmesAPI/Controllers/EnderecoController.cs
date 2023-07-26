using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using FilmesApi.Data;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost] //Adicionar dados de um endereco
    public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { Id = endereco.Id }, enderecoDto);
    }

    [HttpGet] //Consulta todos os endereços
    public IEnumerable<ReadEnderecoDto> RecuperaEnderecos()
    {
        return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.ToList());
    }

    [HttpGet("{id}")] //Consulta apenas um endereco por Id
    public IActionResult RecuperaEnderecoPorId(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
        if (endereco != null)
        {
            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(enderecoDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEndereco(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();
    }
}
