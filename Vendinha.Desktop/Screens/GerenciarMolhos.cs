using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens;

// tela para cadastrar receitas de molhos tradicionais ou especiais de reserva
public partial class GerenciarMolhos : Form
{
    private readonly MolhoService _molhoService;
    private readonly PimentaService _pimentaService;
    private readonly CultureInfo _culturaBr = new("pt-BR");
    private List<Pimenta> _pimentasDisponiveis = [];

    public GerenciarMolhos(MolhoService molhoService, PimentaService pimentaService)
    {
        _molhoService = molhoService ?? throw new ArgumentNullException(nameof(molhoService));
        _pimentaService = pimentaService ?? throw new ArgumentNullException(nameof(pimentaService));
        InitializeComponent();
    }

    private void GerenciarMolhos_Load(object? sender, EventArgs e)
    {
        ConfigurarGrid();
        CarregarPimentasNoCombo();
        CarregarCatalogo();
    }

    // configura a apresentacao das colunas na tabela de catalogo
    private void ConfigurarGrid()
    {
        dgvMolhos.Columns.Clear();
        dgvMolhos.AutoGenerateColumns = false;

        dgvMolhos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            FillWeight = 10
        });

        dgvMolhos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Nome",
            HeaderText = "Nome do Molho",
            FillWeight = 30
        });

        dgvMolhos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colTipo",
            HeaderText = "Tipo",
            FillWeight = 20
        });

        dgvMolhos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPimenta",
            HeaderText = "Pimenta Base",
            FillWeight = 22
        });

        var colScoville = new DataGridViewTextBoxColumn
        {
            Name = "colScoville",
            HeaderText = "Ardencia",
            FillWeight = 18
        };
        colScoville.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhos.Columns.Add(colScoville);

        var colCusto = new DataGridViewTextBoxColumn
        {
            Name = "colCusto",
            HeaderText = "Custo Prod.",
            FillWeight = 18
        };
        colCusto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhos.Columns.Add(colCusto);

        var colVenda = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "PrecoVenda",
            HeaderText = "Preco Venda",
            FillWeight = 18
        };
        colVenda.DefaultCellStyle.Format = "C2";
        colVenda.DefaultCellStyle.FormatProvider = _culturaBr;
        colVenda.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhos.Columns.Add(colVenda);

        var colLucro = new DataGridViewTextBoxColumn
        {
            Name = "colLucro",
            HeaderText = "Lucro / Frasco",
            FillWeight = 18
        };
        colLucro.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhos.Columns.Add(colLucro);

        var colMargem = new DataGridViewTextBoxColumn
        {
            Name = "colMargem",
            HeaderText = "Margem (%)",
            FillWeight = 16
        };
        colMargem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvMolhos.Columns.Add(colMargem);

        dgvMolhos.CellFormatting += DgvMolhos_CellFormatting;
    }

    // preenche colunas calculadas e polimorficas de acordo com o tipo do molho
    private void DgvMolhos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvMolhos.Rows.Count)
        {
            return;
        }

        if (dgvMolhos.Rows[e.RowIndex].DataBoundItem is Molho m)
        {
            var coluna = dgvMolhos.Columns[e.ColumnIndex].Name;

            switch (coluna)
            {
                case "colTipo":
                    e.Value = m is MolhoEspecialReserva res
                        ? $"Reserva ({res.TempoMaturacaoDias}d)"
                        : "Tradicional";
                    e.FormattingApplied = true;
                    break;

                case "colPimenta":
                    e.Value = m.PimentaBase?.Nome ?? "-";
                    e.FormattingApplied = true;
                    break;

                case "colScoville":
                    var shu = m.PimentaBase?.NivelScoville ?? 0;
                    e.Value = $"{shu.ToString("N0", _culturaBr)} SHU";
                    e.FormattingApplied = true;
                    break;

                case "colCusto":
                    e.Value = m.CustoTotalProducao.ToString("C2", _culturaBr);
                    e.FormattingApplied = true;
                    break;

                case "colLucro":
                    e.Value = m.LucroUnitario.ToString("C2", _culturaBr);
                    e.FormattingApplied = true;
                    break;

                case "colMargem":
                    e.Value = $"{m.MargemLucroPercentual.ToString("N1", _culturaBr)}%";
                    e.FormattingApplied = true;
                    break;
            }
        }
    }

    // popula o combobox de pimentas cadastradas
    private void CarregarPimentasNoCombo()
    {
        try
        {
            _pimentasDisponiveis = _pimentaService.ListarPimentas();
            cmbPimenta.DataSource = null;
            cmbPimenta.DataSource = _pimentasDisponiveis;
            cmbPimenta.DisplayMember = "Nome";
            cmbPimenta.ValueMember = "Id";

            if (_pimentasDisponiveis.Count == 0)
            {
                MessageBox.Show(
                    "Nenhuma pimenta cadastrada no sistema. Cadastre pelo menos uma pimenta antes de criar molhos.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                btnSalvar.Enabled = false;
            }
            else
            {
                btnSalvar.Enabled = true;
            }

            CalcularPreview(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao carregar pimentas: {ex.Message}",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // lista os molhos no catalogo lateral
    private void CarregarCatalogo()
    {
        try
        {
            var molhos = _molhoService.ListarCatalogo();
            dgvMolhos.DataSource = null;
            dgvMolhos.DataSource = molhos;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao listar catalogo de molhos: {ex.Message}",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // salva um molho tradicional ou reserva especial
    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cmbPimenta.SelectedItem is not Pimenta pimenta)
            {
                throw new ArgumentException("Selecione uma pimenta base para a receita.");
            }

            Molho molho;

            if (chkReserva.Checked)
            {
                molho = new MolhoEspecialReserva
                {
                    Nome = txtNome.Text,
                    PimentaId = pimenta.Id,
                    GramasPimentaPorFrasco = numGramas.Value,
                    CustoEmbalagemIngredientes = numEmbalagem.Value,
                    PrecoVenda = numPrecoVenda.Value,
                    TempoMaturacaoDias = (int)numDiasMaturacao.Value,
                    CustoAdicionalBarril = numCustoBarril.Value
                };
            }
            else
            {
                molho = new Molho
                {
                    Nome = txtNome.Text,
                    PimentaId = pimenta.Id,
                    GramasPimentaPorFrasco = numGramas.Value,
                    CustoEmbalagemIngredientes = numEmbalagem.Value,
                    PrecoVenda = numPrecoVenda.Value
                };
            }

            var criado = _molhoService.CadastrarMolho(molho);
            MessageBox.Show(
                $"Molho '{criado.Nome}' criado com sucesso!\nCusto: {criado.CustoTotalProducao.ToString("C2", _culturaBr)} | Venda: {criado.PrecoVenda.ToString("C2", _culturaBr)} | Lucro: {criado.LucroUnitario.ToString("C2", _culturaBr)}",
                "Receita Salva",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            CarregarCatalogo();
            btnLimpar_Click(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Validacao da Receita",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    // calcula em tempo real o custo e lucro estimados na tela
    private void CalcularPreview(object? sender, EventArgs e)
    {
        if (cmbPimenta.SelectedItem is not Pimenta pimenta)
        {
            lblPreviewCusto.Text = "Custo Total: -";
            lblPreviewLucro.Text = "Lucro / Frasco: -";
            return;
        }

        var custoPimenta = pimenta.CustoPorGrama * numGramas.Value;
        var custoTotal = custoPimenta + numEmbalagem.Value;

        if (chkReserva.Checked)
        {
            custoTotal += numCustoBarril.Value;
        }

        var lucro = numPrecoVenda.Value - custoTotal;
        var margem = numPrecoVenda.Value > 0
            ? Math.Round((lucro / numPrecoVenda.Value) * 100m, 1)
            : 0m;

        lblPreviewCusto.Text = $"Custo Total: {custoTotal.ToString("C2", _culturaBr)} / frasco";
        lblPreviewLucro.Text = $"Lucro / Frasco: {lucro.ToString("C2", _culturaBr)} ({margem.ToString("N1", _culturaBr)}%)";
    }

    // habilita ou desabilita os controles de reserva especial
    private void chkReserva_CheckedChanged(object? sender, EventArgs e)
    {
        pnlReserva.Enabled = chkReserva.Checked;
        CalcularPreview(sender, e);
    }

    private void cmbPimenta_SelectedIndexChanged(object? sender, EventArgs e)
    {
        CalcularPreview(sender, e);
    }

    // limpa os campos para nova receita
    private void btnLimpar_Click(object? sender, EventArgs e)
    {
        txtNome.Clear();
        numGramas.Value = 25m;
        numEmbalagem.Value = 4.50m;
        numPrecoVenda.Value = 22.00m;
        chkReserva.Checked = false;
        numDiasMaturacao.Value = 90;
        numCustoBarril.Value = 3.50m;
        if (_pimentasDisponiveis.Count > 0)
        {
            cmbPimenta.SelectedIndex = 0;
        }
        CalcularPreview(this, EventArgs.Empty);
    }
}
