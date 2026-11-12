using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendinha.Models;

// classe base pros molhos artesanais vendidos na vendinha
public class Molho
{
    // chave primaria do molho
    [Key]
    public int Id { get; set; }

    // nome comercial do molho no catalogo
    [Required(ErrorMessage = "O nome do molho e obrigatorio.")]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    // fk obrigatoria pra pimenta principal da receita
    [Required]
    public int PimentaId { get; set; }

    // navegacao pra pimenta base usada no calculo do custo
    public Pimenta? PimentaBase { get; set; }

    // quantos gramas de pimenta vao em cada frasco
    [Required]
    [Column(TypeName = "numeric(10,2)")]
    [Range(0.01, 99999999.99, ErrorMessage = "A quantidade de pimenta por frasco precisa ser maior que zero.")]
    public decimal GramasPimentaPorFrasco { get; set; }

    // custo de vidro, rotulo, vinagre e outros temperos por frasco
    [Required]
    [Column(TypeName = "numeric(10,2)")]
    [Range(0, 99999999.99, ErrorMessage = "O custo de embalagem e ingredientes nao pode ser negativo.")]
    public decimal CustoEmbalagemIngredientes { get; set; }

    // valor cobrado por frasco na vendinha
    [Required]
    [Column(TypeName = "numeric(10,2)")]
    [Range(0.01, 99999999.99, ErrorMessage = "O preco de venda precisa ser maior que zero.")]
    public decimal PrecoVenda { get; set; }

    // calculo polimorfico do custo total de producao (classes filhas podem somar extras)
    [NotMapped]
    public virtual decimal CustoTotalProducao
    {
        get
        {
            var custoPimenta = (PimentaBase?.CustoPorGrama ?? 0m) * GramasPimentaPorFrasco;
            return custoPimenta + CustoEmbalagemIngredientes;
        }
    }

    // lucro por frasco descontando o custo total de producao
    [NotMapped]
    public virtual decimal LucroUnitario => PrecoVenda - CustoTotalProducao;

    // margem de lucro percentual sobre o preco de venda
    [NotMapped]
    public decimal MargemLucroPercentual => PrecoVenda > 0m
        ? Math.Round((LucroUnitario / PrecoVenda) * 100m, 2)
        : 0m;

    // dividas onde esse molho foi vendido
    public ICollection<Divida> Dividas { get; set; } = new List<Divida>();
}
