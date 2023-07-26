using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
namespace FilmesApi.Profiles;
public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDto, Endereco>();//Quando estamos criando - passamos do create para o endereço
        CreateMap<Endereco, ReadEnderecoDto>();//Qdo estamos consultando - pegamos do endereço para a leitura
        CreateMap<UpdateEnderecoDto, Endereco>();//Qdo é atualização - 
        CreateMap<Endereco, UpdateEnderecoDto>();
    }
}
