using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendinha.Models;

// representa uma compra de molhos de pimenta pendurada na caderneta
public class Divida
{
    // chave primaria da divida
    [Key]
    public int Id { get; set; }

    // fk pro cliente dono da divida
    [Required]
    public int ClienteId { get; set; }

    // navegacao pro cliente, pode ficar nulo ate carregar com include
    public Cliente? Cliente { get; set; }

    // descricao dos molhos levados, ja vem com texto padrao se ninguem preencher
    [Required]
    [MaxLength(300)]
    public string DescricaoMolhos { get; set; } = "Molhos de pimenta artesanais do Avô Bob";

    // valor da compra, precisa ser maior que zero
    [Required]
    [Column(TypeName = "numeric(10,2)")]
    [Range(0.01, 99999999.99, ErrorMessage = "O valor da divida precisa ser maior que zero.")]
    public decimal Valor { get; set; }

    // false = em aberto na caderneta, true = ja foi acertada
    public bool SituacaoPaga { get; set; } = false;

    // guarda em utc pq o npgsql exige timestamp with time zone em utc
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // fica nulo enquanto estiver devendo e preenche quando quitar
    public DateTime? DataPagamento { get; set; }
}
