using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;
public class ReadFilmeDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
    //public string Diretor { get; set; }
    //Podemos add uma propriedade importante para essa rotina, como exemplo add o horario da consulta
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    public ICollection<ReadSessaoDto> Sessoes { get; set; } //Um filme pode se exibido em várias sessões
}
