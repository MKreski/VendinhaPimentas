using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendinha.Models;

// molho especial envelhecido em barril, herda de molho e soma o custo do barril
public class MolhoEspecialReserva : Molho
{
    // dias de maturacao do molho no barril
    [Range(1, int.MaxValue, ErrorMessage = "O tempo de maturacao precisa ser maior que zero.")]
    public int TempoMaturacaoDias { get; set; }

    // custo extra por frasco referente ao uso e preparo do barril
    [Column(TypeName = "numeric(10,2)")]
    [Range(0, 99999999.99, ErrorMessage = "O custo adicional do barril nao pode ser negativo.")]
    public decimal CustoAdicionalBarril { get; set; }

    // sobrescreve o calculo da classe mae somando o custo do barril
    [NotMapped]
    public override decimal CustoTotalProducao => base.CustoTotalProducao + CustoAdicionalBarril;
}
