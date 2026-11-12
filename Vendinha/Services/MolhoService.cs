using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Models;

namespace Vendinha.Services;

// resumo gerencial de custos, margens e recebimentos da vendinha
public record RelatorioGerencialVendinha(
    int TotalMolhosCatalogo,
    int TotalTradicionais,
    int TotalEspeciaisReserva,
    decimal MediaCustoPorFrasco,
    decimal MediaPrecoVenda,
    decimal MediaLucroUnitario,
    decimal MargemMediaPercentual,
    decimal TotalAReceberDividasAbertas,
    int QuantidadeDividasAbertas,
    decimal TotalJaRecebidoDividasPagas,
    int QuantidadeDividasPagas);

// servico pra gerenciar o catalogo de molhos (tradicionais e reserva) e relatorios de custo
public class MolhoService
{
    private readonly VendinhaDbContext _context;
    private readonly DividaService _dividaService;

    public MolhoService(VendinhaDbContext context, DividaService? dividaService = null)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dividaService = dividaService ?? new DividaService(context);
    }

    // cadastra um molho tradicional ou especial de reserva vinculando a uma pimenta existente
    public Molho CadastrarMolho(Molho molho)
    {
        ArgumentNullException.ThrowIfNull(molho);

        if (string.IsNullOrWhiteSpace(molho.Nome))
        {
            throw new ArgumentException("O nome do molho e obrigatorio.", nameof(molho));
        }

        molho.Nome = molho.Nome.Trim();
        if (molho.Nome.Length > 150)
        {
            throw new ArgumentException("O nome do molho pode ter no maximo 150 caracteres.", nameof(molho));
        }

        var pimenta = _context.Pimentas.FirstOrDefault(p => p.Id == molho.PimentaId);
        if (pimenta is null)
        {
            throw new KeyNotFoundException($"Pimenta com id {molho.PimentaId} nao encontrada.");
        }

        if (molho.GramasPimentaPorFrasco <= 0m)
        {
            throw new ArgumentException("A quantidade de pimenta por frasco deve ser maior que zero.", nameof(molho));
        }

        if (molho.CustoEmbalagemIngredientes < 0m)
        {
            throw new ArgumentException("O custo de embalagem e ingredientes nao pode ser negativo.", nameof(molho));
        }

        if (molho.PrecoVenda <= 0m)
        {
            throw new ArgumentException("O preco de venda do molho deve ser maior que zero.", nameof(molho));
        }

        if (molho is MolhoEspecialReserva reserva)
        {
            if (reserva.TempoMaturacaoDias <= 0)
            {
                throw new ArgumentException("O tempo de maturacao em barril deve ser maior que zero.", nameof(molho));
            }

            if (reserva.CustoAdicionalBarril < 0m)
            {
                throw new ArgumentException("O custo adicional do barril nao pode ser negativo.", nameof(molho));
            }
        }

        // ja vincula a pimenta carregada pra propriedades calculadas funcionarem na hora
        molho.PimentaBase = pimenta;

        _context.Molhos.Add(molho);
        _context.SaveChanges();

        return molho;
    }

    // lista todos os molhos do catalogo com a pimenta base carregada pra exibir ardencia e custos
    public List<Molho> ListarCatalogo()
    {
        return _context.Molhos
            .AsNoTracking()
            .Include(m => m.PimentaBase)
            .OrderBy(m => m.Nome)
            .ToList();
    }

    // busca um molho especifico pelo id com a pimenta base carregada
    public Molho? BuscarPorId(int id)
    {
        return _context.Molhos
            .Include(m => m.PimentaBase)
            .FirstOrDefault(m => m.Id == id);
    }

    // gera o relatorio gerencial cruzando custos do catalogo com os totais do dividaservice
    public RelatorioGerencialVendinha GerarRelatorioGerencial()
    {
        var catalogo = ListarCatalogo();

        var totalTradicionais = catalogo.Count(m => m is not MolhoEspecialReserva);
        var totalReservas = catalogo.Count(m => m is MolhoEspecialReserva);

        var mediaCusto = catalogo.Count > 0
            ? Math.Round(catalogo.Average(m => m.CustoTotalProducao), 2)
            : 0m;

        var mediaPrecoVenda = catalogo.Count > 0
            ? Math.Round(catalogo.Average(m => m.PrecoVenda), 2)
            : 0m;

        var mediaLucro = catalogo.Count > 0
            ? Math.Round(catalogo.Average(m => m.LucroUnitario), 2)
            : 0m;

        var margemMedia = catalogo.Count > 0
            ? Math.Round(catalogo.Average(m => m.MargemLucroPercentual), 2)
            : 0m;

        var (totalAberto, qtdAberta) = _dividaService.ObterResumoDividasEmAberto();
        var (totalPago, qtdPaga) = _dividaService.ObterResumoDividasPagas();

        return new RelatorioGerencialVendinha(
            TotalMolhosCatalogo: catalogo.Count,
            TotalTradicionais: totalTradicionais,
            TotalEspeciaisReserva: totalReservas,
            MediaCustoPorFrasco: mediaCusto,
            MediaPrecoVenda: mediaPrecoVenda,
            MediaLucroUnitario: mediaLucro,
            MargemMediaPercentual: margemMedia,
            TotalAReceberDividasAbertas: totalAberto,
            QuantidadeDividasAbertas: qtdAberta,
            TotalJaRecebidoDividasPagas: totalPago,
            QuantidadeDividasPagas: qtdPaga);
    }
}
