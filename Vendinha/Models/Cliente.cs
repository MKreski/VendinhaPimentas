using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendinha.Models;

// modelo que representa o cliente da caderneta de fiado
public class Cliente
{
    // chave primaria gerada pelo banco
    [Key]
    public int Id { get; set; }

    // nome completo do cliente, obrigatorio
    [Required(ErrorMessage = "O nome completo e obrigatorio.")]
    [MaxLength(150)]
    public string NomeCompleto { get; set; } = string.Empty;

    // aceita cpf so com numeros ou formatado com pontos e traco, unico no sistema
    [Required(ErrorMessage = "O CPF e obrigatorio.")]
    [MaxLength(14)]
    public string Cpf { get; set; } = string.Empty;

    // usando dateonly pq nao precisamos da hora pro nascimento
    [Required(ErrorMessage = "A data de nascimento e obrigatoria.")]
    public DateOnly DataNascimento { get; set; }

    // email fica opcional caso o cliente nao tenha
    [MaxLength(150)]
    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    public string? Email { get; set; }

    // calculado em memoria pra nao criar coluna fixa de idade no banco
    [NotMapped]
    public int Idade
    {
        get
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
            var idade = hoje.Year - DataNascimento.Year;

            // ajusta se o cliente ainda nao fez aniversario este ano
            if (DataNascimento > hoje.AddYears(-idade))
            {
                idade--;
            }

            return idade < 0 ? 0 : idade;
        }
    }

    // lista de compras penduradas do cliente
    public ICollection<Divida> Dividas { get; set; } = new List<Divida>();

    // soma so as dividas que ainda estao em aberto na caderneta
    [NotMapped]
    public decimal TotalDividas => Dividas?
        .Where(d => !d.SituacaoPaga)
        .Sum(d => d.Valor) ?? 0m;
}
