using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Models;

namespace Vendinha.Services;

// servico pra cadastrar, listar, atualizar e remover os tipos de pimenta
public class PimentaService
{
    private readonly VendinhaDbContext _context;

    public PimentaService(VendinhaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // cadastra uma nova pimenta validando nome, especie, scoville e custo por grama
    public Pimenta CadastrarPimenta(Pimenta pimenta)
    {
        ArgumentNullException.ThrowIfNull(pimenta);

        ValidarENormalizar(pimenta);

        var nomeLower = pimenta.Nome.ToLower();
        var nomeJaExiste = _context.Pimentas.Any(p => p.Nome.ToLower() == nomeLower);
        if (nomeJaExiste)
        {
            throw new InvalidOperationException("Ja existe uma pimenta cadastrada com esse nome.");
        }

        _context.Pimentas.Add(pimenta);
        _context.SaveChanges();

        return pimenta;
    }

    // lista todas as pimentas cadastradas ordenadas por nome
    public List<Pimenta> ListarPimentas()
    {
        return _context.Pimentas
            .AsNoTracking()
            .OrderBy(p => p.Nome)
            .ToList();
    }

    // busca uma pimenta pelo id
    public Pimenta? BuscarPorId(int id)
    {
        return _context.Pimentas.FirstOrDefault(p => p.Id == id);
    }

    // atualiza os dados de uma pimenta existente
    public Pimenta AtualizarPimenta(Pimenta pimenta)
    {
        ArgumentNullException.ThrowIfNull(pimenta);

        var existente = _context.Pimentas.FirstOrDefault(p => p.Id == pimenta.Id);
        if (existente is null)
        {
            throw new KeyNotFoundException($"Pimenta com id {pimenta.Id} nao encontrada.");
        }

        ValidarENormalizar(pimenta);

        var nomeLower = pimenta.Nome.ToLower();
        var nomeDuplicado = _context.Pimentas.Any(p => p.Id != pimenta.Id && p.Nome.ToLower() == nomeLower);
        if (nomeDuplicado)
        {
            throw new InvalidOperationException("Ja existe outra pimenta cadastrada com esse nome.");
        }

        existente.Nome = pimenta.Nome;
        existente.EspecieOrigem = pimenta.EspecieOrigem;
        existente.NivelScoville = pimenta.NivelScoville;
        existente.CustoPorGrama = pimenta.CustoPorGrama;

        _context.SaveChanges();

        return existente;
    }

    // remove a pimenta so se nenhum molho estiver usando ela
    public void RemoverPimenta(int id)
    {
        var existente = _context.Pimentas.FirstOrDefault(p => p.Id == id);
        if (existente is null)
        {
            throw new KeyNotFoundException($"Pimenta com id {id} nao encontrada.");
        }

        var emUso = _context.Molhos.Any(m => m.PimentaId == id);
        if (emUso)
        {
            throw new InvalidOperationException("Nao e possivel remover esta pimenta porque ela esta vinculada a receitas de molhos.");
        }

        _context.Pimentas.Remove(existente);
        _context.SaveChanges();
    }

    // valida obrigatoriedade e valores positivos de ardencia e custo
    private static void ValidarENormalizar(Pimenta pimenta)
    {
        if (string.IsNullOrWhiteSpace(pimenta.Nome))
        {
            throw new ArgumentException("O nome da pimenta e obrigatorio.", nameof(pimenta));
        }

        pimenta.Nome = pimenta.Nome.Trim();
        if (pimenta.Nome.Length > 100)
        {
            throw new ArgumentException("O nome da pimenta pode ter no maximo 100 caracteres.", nameof(pimenta));
        }

        if (string.IsNullOrWhiteSpace(pimenta.EspecieOrigem))
        {
            throw new ArgumentException("A especie de origem da pimenta e obrigatoria.", nameof(pimenta));
        }

        pimenta.EspecieOrigem = pimenta.EspecieOrigem.Trim();
        if (pimenta.EspecieOrigem.Length > 150)
        {
            throw new ArgumentException("A especie de origem pode ter no maximo 150 caracteres.", nameof(pimenta));
        }

        if (pimenta.NivelScoville <= 0)
        {
            throw new ArgumentException("O nivel de ardencia Scoville (SHU) deve ser maior que zero.", nameof(pimenta));
        }

        if (pimenta.CustoPorGrama <= 0m)
        {
            throw new ArgumentException("O custo por grama da pimenta deve ser maior que zero.", nameof(pimenta));
        }
    }
}
