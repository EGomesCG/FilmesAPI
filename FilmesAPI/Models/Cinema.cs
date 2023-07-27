using System.ComponentModel.DataAnnotations;
namespace FilmesApi.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage ="Campo de nome é obrigatório.")]
    public string Nome { get; set; }
    
    public int EnderecoId { get; set; }//Todo cinema só pode existir se tiver um endereço associado
                                        //Como o C# conseguiu deduzir que este campo é chave estrageira?
                                        //Tenho Endereco que é o nome da tabela endereço e
                                        //o sufixo Id que é a chave primaria da tabela endereço

    //Para que o cinema exista o endereço deve existir. O cinema e o endereço tem o relacionamento 1:1
    //Para isto, iremos criar um propriedade virtual de endereço, e precisamos fazer a mesma coisa na classe Endereco
    //Essa propriedade além de definir que é 1:1, podemos através dela criar uma instância e exibir os dados que ela possui 
    //No momento que instânciar um cinema, também quero carregar os dados do endereço.
    //Para, isto, precisamos ter o proxies instalado e configurado.
    public virtual Endereco Endereco { get; set; }//Relacionamento 1:1 => 1 - Cinema : 1 - endereço
                                                  //| Na classe endereço indico cinema
    public virtual ICollection<Sessao> Sessoes { get; set; }//Relacionamento 1:n => 1 - Cinema : n - Sessões
}
