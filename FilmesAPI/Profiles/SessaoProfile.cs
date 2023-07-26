using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
namespace FilmesApi.Profiles;

public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDto, Sessao>();//Estou ensinando minha aplicação a converter CreateSessaoDto em Sessao, no momento da criação
        CreateMap<Sessao, ReadSessaoDto>();
    }
}
