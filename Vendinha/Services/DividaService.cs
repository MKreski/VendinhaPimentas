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

    // registra uma nova compra pendurada pro cliente respeitando a trava de 1 divida aberta por vez
    public Divida CadastrarDivida(Divida divida)
    {
        ArgumentNullException.ThrowIfNull(divida);

        var clienteExiste = _context.Clientes.Any(c => c.Id == divida.ClienteId);
        if (!clienteExiste)
        {
            throw new KeyNotFoundException($"Cliente com id {divida.ClienteId} nao encontrado.");
        }

        if (divida.Valor <= 0)
        {
            throw new ArgumentException("O valor da divida precisa ser maior que zero.", nameof(divida));
        }

        // regra critica: nao pode pendurar outra enquanto tiver conta em aberto
        var possuiDividaEmAberto = _context.Dividas
            .Any(d => d.ClienteId == divida.ClienteId && !d.SituacaoPaga);

        if (possuiDividaEmAberto)
        {
            throw new InvalidOperationException(
                "Este cliente ja possui uma divida em aberto e precisa acertar a conta atual de molhos do Avo Bob antes de pendurar outra.");
        }

        if (string.IsNullOrWhiteSpace(divida.DescricaoMolhos))
        {
            divida.DescricaoMolhos = "Molhos de pimenta artesanais do Avô Bob";
        }
        else
        {
            divida.DescricaoMolhos = divida.DescricaoMolhos.Trim();
            if (divida.DescricaoMolhos.Length > 300)
            {
                throw new ArgumentException("A descricao dos molhos pode ter no maximo 300 caracteres.", nameof(divida));
            }
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
            DescricaoMolhos = string.IsNullOrWhiteSpace(descricaoMolhos)
                ? "Molhos de pimenta artesanais do Avô Bob"
                : descricaoMolhos
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
}
