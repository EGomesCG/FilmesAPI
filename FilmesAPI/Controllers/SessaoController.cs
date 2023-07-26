using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Data;
using Microsoft.AspNetCore.Mvc;
namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{
    //Como essa a classe sessão se tornou uma tabela de relacionamento teremos que realizar alguns ajustes neste arquivo
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext contexto,IMapper mapper)
    {
        _context = contexto;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaSessao(CreateSessaoDto dto)
    {
        Sessao sessao = _mapper.Map<Sessao>(dto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        //Agora onde se passava o Id, receberemos o FilmeId e o CinemaId
        return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
    }
    [HttpGet]
    public IEnumerable<ReadSessaoDto> RecuperaSessoes()
    {
        return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
    }
    [HttpGet("{filmeId}/{cinemaId}")] // Ao invés de receber um {id} -> agora iremos receber: {filmeId} e {cinemaId}
    public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
    {
        Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
        if (sessao != null)
        {
            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

            return Ok(sessaoDto);
        }
        return NotFound();
    }
}
