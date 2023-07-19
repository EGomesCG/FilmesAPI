using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;
public class ReadFilmeDto
{
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
    public string Diretor { get; set; }
    //Podemos add uma propriedade importante para essa rotina, como exemplo add o horario da consulta
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;    
}
