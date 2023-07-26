using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
namespace FilmesApi.Profiles;
public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();
        CreateMap<Cinema, UpdateCinemaDto>();
        //Depois de instalar e configurar o Proxie -> serve para exibir os dados das classes que estã dentro da aula
        CreateMap<Cinema, ReadCinemaDto>()//Quando for carregar os dados do cinema (BD) para ReadCinemaDto (Aplicação)
            .ForMember(cinemaDto => cinemaDto.Endereco, //A propriedade ReadEnderecoDto(tipo) => Endereco
            opt => opt.MapFrom(cinema => cinema.Endereco));//Também precisa ser mapeada e carregada com seus dados, assim teremos o endereço do cinema
            //.ForMember(cinemaDto => cinemaDto.Sessoes, 
            //opt => opt.MapFrom(cinema => cinema.Sessoes));
    }
}
