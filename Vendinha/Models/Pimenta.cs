using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendinha.Models;

// representa cada tipo de pimenta usada nas receitas do avo bob
public class Pimenta
{
    // chave primaria da pimenta
    [Key]
    public int Id { get; set; }

    // nome popular da pimenta (ex: malagueta, habanero, carolina reaper)
    [Required(ErrorMessage = "O nome da pimenta e obrigatorio.")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    // especie botanica ou origem (ex: capsicum chinense)
    [Required(ErrorMessage = "A especie de origem e obrigatoria.")]
    [MaxLength(150)]
    public string EspecieOrigem { get; set; } = string.Empty;

    // grau de ardencia na escala scoville (shu)
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O nivel scoville precisa ser maior que zero.")]
    public int NivelScoville { get; set; }

    // custo de compra ou cultivo por grama de pimenta
    [Required]
    [Column(TypeName = "numeric(10,4)")]
    [Range(0.0001, 999999.9999, ErrorMessage = "O custo por grama precisa ser maior que zero.")]
    public decimal CustoPorGrama { get; set; }

    // molhos que usam essa pimenta como base
    public ICollection<Molho> Molhos { get; set; } = new List<Molho>();
}
