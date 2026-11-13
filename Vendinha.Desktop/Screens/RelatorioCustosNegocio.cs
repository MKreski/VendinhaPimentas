using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens;

// tela com o painel gerencial de custos, margens e valores de dividas abertas vs quitadas
public partial class RelatorioCustosNegocio : Form
{
    private readonly MolhoService _molhoService;
    private readonly CultureInfo _culturaBr = new("pt-BR");

    public RelatorioCustosNegocio(MolhoService molhoService)
    {
        _molhoService = molhoService ?? throw new ArgumentNullException(nameof(molhoService));
        InitializeComponent();
    }

    private void RelatorioCustosNegocio_Load(object? sender, EventArgs e)
    {
        ConfigurarGrid();
        CarregarRelatorio();
    }

    // define colunas do demonstrativo de molhos
    private void ConfigurarGrid()
    {
        dgvMolhosResumo.Columns.Clear();
        dgvMolhosResumo.AutoGenerateColumns = false;

        dgvMolhosResumo.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Nome",
            HeaderText = "Molho",
            FillWeight = 28
        });

        dgvMolhosResumo.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colTipo",
            HeaderText = "Tipo da Receita",
            FillWeight = 20
        });

        dgvMolhosResumo.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPimenta",
            HeaderText = "Pimenta Base",
            FillWeight = 20
        });

        var colCusto = new DataGridViewTextBoxColumn
        {
            Name = "colCusto",
            HeaderText = "Custo Producao",
            FillWeight = 16
        };
        colCusto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhosResumo.Columns.Add(colCusto);

        var colVenda = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "PrecoVenda",
            HeaderText = "Preco Venda",
            FillWeight = 16
        };
        colVenda.DefaultCellStyle.Format = "C2";
        colVenda.DefaultCellStyle.FormatProvider = _culturaBr;
        colVenda.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhosResumo.Columns.Add(colVenda);

        var colLucro = new DataGridViewTextBoxColumn
        {
            Name = "colLucro",
            HeaderText = "Lucro / Frasco",
            FillWeight = 16
        };
        colLucro.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colLucro.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        dgvMolhosResumo.Columns.Add(colLucro);

        var colMargem = new DataGridViewTextBoxColumn
        {
            Name = "colMargem",
            HeaderText = "Margem (%)",
            FillWeight = 14
        };
        colMargem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhosResumo.Columns.Add(colMargem);

        dgvMolhosResumo.CellFormatting += DgvMolhosResumo_CellFormatting;
    }

    // preenche e estiliza as colunas calculadas
    private void DgvMolhosResumo_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvMolhosResumo.Rows.Count)
        {
            return;
        }

        if (dgvMolhosResumo.Rows[e.RowIndex].DataBoundItem is Molho m)
        {
            var coluna = dgvMolhosResumo.Columns[e.ColumnIndex].Name;

            switch (coluna)
            {
                case "colTipo":
                    e.Value = m is MolhoEspecialReserva res
                        ? $"Reserva Especial ({res.TempoMaturacaoDias}d)"
                        : "Tradicional";
                    e.FormattingApplied = true;
                    break;

                case "colPimenta":
                    e.Value = m.PimentaBase?.Nome ?? "-";
                    e.FormattingApplied = true;
                    break;

                case "colCusto":
                    e.Value = m.CustoTotalProducao.ToString("C2", _culturaBr);
                    e.FormattingApplied = true;
                    break;

                case "colLucro":
                    e.Value = m.LucroUnitario.ToString("C2", _culturaBr);
                    e.CellStyle.ForeColor = m.LucroUnitario >= 0m
                        ? Color.FromArgb(22, 101, 52)
                        : Color.FromArgb(185, 28, 28);
                    e.FormattingApplied = true;
                    break;

                case "colMargem":
                    e.Value = $"{m.MargemLucroPercentual.ToString("N1", _culturaBr)}%";
                    e.FormattingApplied = true;
                    break;
            }
        }
    }

    // calcula as metricas consolidadas da vendinha e atualiza os cards
    private void CarregarRelatorio()
    {
        try
        {
            var rel = _molhoService.GerarRelatorioGerencial();

            lblCardCustoValor.Text = rel.MediaCustoPorFrasco.ToString("C2", _culturaBr);
            lblCardPrecoValor.Text = rel.MediaPrecoVenda.ToString("C2", _culturaBr);
            lblCardMargemValor.Text = $"{rel.MargemMediaPercentual.ToString("N1", _culturaBr)} %";

            lblCardAbertoValor.Text = rel.TotalAReceberDividasAbertas.ToString("C2", _culturaBr);
            lblCardAbertoTitulo.Text = $"FIADO A RECEBER ({rel.QuantidadeDividasAbertas} CONTA{(rel.QuantidadeDividasAbertas == 1 ? "" : "S")})";

            lblCardRecebidoValor.Text = rel.TotalJaRecebidoDividasPagas.ToString("C2", _culturaBr);
            lblCardRecebidoTitulo.Text = $"JA RECEBIDO ({rel.QuantidadeDividasPagas} CONTA{(rel.QuantidadeDividasPagas == 1 ? "" : "S")})";

            lblCardReceitasValor.Text = $"{rel.TotalMolhosCatalogo} receita(s) ({rel.TotalTradicionais} trad., {rel.TotalEspeciaisReserva} res.)";

            var catalogo = _molhoService.ListarCatalogo();
            dgvMolhosResumo.DataSource = null;
            dgvMolhosResumo.DataSource = catalogo;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao gerar relatorio gerencial: {ex.Message}",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnAtualizar_Click(object? sender, EventArgs e)
    {
        CarregarRelatorio();
    }

    private void btnFechar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
