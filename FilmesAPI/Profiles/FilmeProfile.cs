using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

//Teremos um perfil específico do AutoMapper que chamaremos de "profile". 
//Essa classe hedar de profile
public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        //Aqui temos um mapeamento de um filmeDto para um filme (Luiz)
        CreateMap<CreateFilmeDto, Filme>(); 
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}
