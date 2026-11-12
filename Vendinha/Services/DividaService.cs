using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Models;

namespace Vendinha.Services;

// servico que controla as dividas penduradas e quitacoes na caderneta do avo bob
public class DividaService
{
    private readonly VendinhaDbContext _context;

    public DividaService(VendinhaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // registra uma nova compra pendurada pro cliente (aceita molho do catalogo ou lancamento avulso)
    public Divida CadastrarDivida(Divida divida)
    {
        ArgumentNullException.ThrowIfNull(divida);

        var clienteExiste = _context.Clientes.Any(c => c.Id == divida.ClienteId);
        if (!clienteExiste)
        {
            throw new KeyNotFoundException($"Cliente com id {divida.ClienteId} nao encontrado.");
        }

        // regra critica: nao pode pendurar outra enquanto tiver conta em aberto
        var possuiDividaEmAberto = _context.Dividas
            .Any(d => d.ClienteId == divida.ClienteId && !d.SituacaoPaga);

        if (possuiDividaEmAberto)
        {
            throw new InvalidOperationException(
                "Este cliente ja possui uma divida em aberto e precisa acertar a conta atual de molhos do Avo Bob antes de pendurar outra.");
        }

        if (divida.MolhoId.HasValue)
        {
            if (divida.QuantidadeFrascos <= 0)
            {
                throw new ArgumentException("A quantidade de frascos precisa ser maior que zero.", nameof(divida));
            }

            var molho = _context.Molhos
                .Include(m => m.PimentaBase)
                .FirstOrDefault(m => m.Id == divida.MolhoId.Value);

            if (molho is null)
            {
                throw new KeyNotFoundException($"Molho com id {divida.MolhoId.Value} nao encontrado no catalogo.");
            }

            // calcula o valor da divida direto pelo preco de venda do molho x frascos
            divida.Valor = Math.Round(molho.PrecoVenda * divida.QuantidadeFrascos, 2);

            if (string.IsNullOrWhiteSpace(divida.DescricaoMolhos) ||
                divida.DescricaoMolhos == "Molhos de pimenta artesanais do Avô Bob")
            {
                var sufixoReserva = molho is MolhoEspecialReserva reserva
                    ? $" (Reserva {reserva.TempoMaturacaoDias}d)"
                    : string.Empty;

                divida.DescricaoMolhos = $"{divida.QuantidadeFrascos}x {molho.Nome}{sufixoReserva}";
            }
            else
            {
                divida.DescricaoMolhos = divida.DescricaoMolhos.Trim();
            }
        }
        else
        {
            if (divida.QuantidadeFrascos <= 0)
            {
                divida.QuantidadeFrascos = 1;
            }

            if (divida.Valor <= 0)
            {
                throw new ArgumentException("O valor da divida precisa ser maior que zero.", nameof(divida));
            }

            if (string.IsNullOrWhiteSpace(divida.DescricaoMolhos))
            {
                divida.DescricaoMolhos = "Molhos de pimenta artesanais do Avô Bob";
            }
            else
            {
                divida.DescricaoMolhos = divida.DescricaoMolhos.Trim();
            }
        }

        if (divida.DescricaoMolhos.Length > 300)
        {
            throw new ArgumentException("A descricao dos molhos pode ter no maximo 300 caracteres.", nameof(divida));
        }

        divida.DataCriacao = DateTime.Now;
        divida.SituacaoPaga = false;
        divida.DataPagamento = null;

        _context.Dividas.Add(divida);
        _context.SaveChanges();

        return divida;
    }

    // atalho pratico pra cadastrar direto passando cliente, valor e descricao opcional
    public Divida CadastrarDivida(int clienteId, decimal valor, string? descricaoMolhos = null)
    {
        var divida = new Divida
        {
            ClienteId = clienteId,
            Valor = valor,
            QuantidadeFrascos = 1,
            DescricaoMolhos = string.IsNullOrWhiteSpace(descricaoMolhos)
                ? "Molhos de pimenta artesanais do Avô Bob"
                : descricaoMolhos
        };

        return CadastrarDivida(divida);
    }

    // registra a divida escolhendo um molho do catalogo e a quantidade de frascos
    public Divida CadastrarDividaPorMolho(int clienteId, int molhoId, int quantidadeFrascos, string? observacao = null)
    {
        var divida = new Divida
        {
            ClienteId = clienteId,
            MolhoId = molhoId,
            QuantidadeFrascos = quantidadeFrascos,
            DescricaoMolhos = string.IsNullOrWhiteSpace(observacao) ? string.Empty : observacao
        };

        return CadastrarDivida(divida);
    }

    // retorna o historico de dividas do cliente com a conta em aberto primeiro e as mais novas no topo
    public List<Divida> ListarDividasPorCliente(int clienteId)
    {
        var clienteExiste = _context.Clientes.Any(c => c.Id == clienteId);
        if (!clienteExiste)
        {
            throw new KeyNotFoundException($"Cliente com id {clienteId} nao encontrado.");
        }

        return _context.Dividas
            .AsNoTracking()
            .Include(d => d.Cliente)
            .Include(d => d.Molho)
            .Where(d => d.ClienteId == clienteId)
            .OrderBy(d => d.SituacaoPaga)
            .ThenByDescending(d => d.DataCriacao)
            .ToList();
    }

    // marca a divida como paga e grava a data/hora do acerto
    public Divida QuitarDivida(int dividaId)
    {
        var divida = _context.Dividas.FirstOrDefault(d => d.Id == dividaId);

        if (divida is null)
        {
            throw new KeyNotFoundException($"Divida com id {dividaId} nao encontrada.");
        }

        if (divida.SituacaoPaga)
        {
            throw new InvalidOperationException("Esta divida ja foi quitada anteriormente.");
        }

        divida.SituacaoPaga = true;
        divida.DataPagamento = DateTime.Now;

        _context.SaveChanges();

        return divida;
    }

    // soma o total e quantidade de dividas ainda nao pagas pro relatorio gerencial
    public (decimal Total, int Quantidade) ObterResumoDividasEmAberto()
    {
        var query = _context.Dividas.Where(d => !d.SituacaoPaga);
        var total = query.Sum(d => (decimal?)d.Valor) ?? 0m;
        var quantidade = query.Count();

        return (total, quantidade);
    }

    // soma o total e quantidade de dividas ja recebidas pro relatorio gerencial
    public (decimal Total, int Quantidade) ObterResumoDividasPagas()
    {
        var query = _context.Dividas.Where(d => d.SituacaoPaga);
        var total = query.Sum(d => (decimal?)d.Valor) ?? 0m;
        var quantidade = query.Count();

        return (total, quantidade);
    }
}
