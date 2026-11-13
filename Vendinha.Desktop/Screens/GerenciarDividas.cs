using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens;

// tela que controla as dividas penduradas de um cliente especifico e o recebimento de contas
public partial class GerenciarDividas : Form
{
    private readonly Cliente _cliente;
    private readonly DividaService _dividaService;
    private readonly MolhoService _molhoService;
    private readonly ClienteService _clienteService;
    private readonly CultureInfo _culturaBr = new("pt-BR");

    private List<Molho> _molhosCatalogo = [];
    private List<Divida> _dividasCliente = [];

    public GerenciarDividas(
        Cliente cliente,
        DividaService dividaService,
        MolhoService molhoService,
        ClienteService clienteService)
    {
        _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
        _dividaService = dividaService ?? throw new ArgumentNullException(nameof(dividaService));
        _molhoService = molhoService ?? throw new ArgumentNullException(nameof(molhoService));
        _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        InitializeComponent();
    }

    private void GerenciarDividas_Load(object? sender, EventArgs e)
    {
        AtualizarCardCliente();
        ConfigurarGrid();
        CarregarCatalogoMolhos();
        CarregarHistoricoDividas();
    }

    // exibe o resumo atual do cliente com saldo devedor
    private void AtualizarCardCliente()
    {
        var cAtualizado = _clienteService.BuscarPorId(_cliente.Id) ?? _cliente;

        lblClienteNome.Text = $"{cAtualizado.NomeCompleto} (ID: {cAtualizado.Id})";
        lblClienteCpf.Text = $"CPF: {FormatarCpf(cAtualizado.Cpf)} | Idade: {cAtualizado.Idade} anos | E-mail: {cAtualizado.Email ?? "nao informado"}";

        if (cAtualizado.TotalDividas > 0m)
        {
            lblClienteSaldo.ForeColor = Color.FromArgb(185, 28, 28);
            lblClienteSaldo.Text = $"Conta em Aberto: {cAtualizado.TotalDividas.ToString("C2", _culturaBr)}";
        }
        else
        {
            lblClienteSaldo.ForeColor = Color.FromArgb(22, 101, 52);
            lblClienteSaldo.Text = "Conta em Dia (R$ 0,00 pendurado)";
        }
    }

    // monta as colunas da grade de dividas
    private void ConfigurarGrid()
    {
        dgvDividas.Columns.Clear();
        dgvDividas.AutoGenerateColumns = false;

        dgvDividas.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            FillWeight = 8
        });

        dgvDividas.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSituacao",
            HeaderText = "Situacao",
            FillWeight = 14
        });

        var colValor = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Valor",
            HeaderText = "Valor da Compra",
            FillWeight = 16
        };
        colValor.DefaultCellStyle.Format = "C2";
        colValor.DefaultCellStyle.FormatProvider = _culturaBr;
        colValor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colValor.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        dgvDividas.Columns.Add(colValor);

        var colQtd = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "QuantidadeFrascos",
            HeaderText = "Frascos",
            FillWeight = 10
        };
        colQtd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvDividas.Columns.Add(colQtd);

        dgvDividas.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DescricaoMolhos",
            HeaderText = "Molhos Levados / Descricao",
            FillWeight = 32
        });

        var colDataCompra = new DataGridViewTextBoxColumn
        {
            Name = "colDataCompra",
            HeaderText = "Data da Compra",
            FillWeight = 18
        };
        colDataCompra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvDividas.Columns.Add(colDataCompra);

        var colDataPagto = new DataGridViewTextBoxColumn
        {
            Name = "colDataPagto",
            HeaderText = "Data do Pagamento",
            FillWeight = 18
        };
        colDataPagto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvDividas.Columns.Add(colDataPagto);

        dgvDividas.CellFormatting += DgvDividas_CellFormatting;
    }

    // pinta situacao e formata datas
    private void DgvDividas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvDividas.Rows.Count)
        {
            return;
        }

        if (dgvDividas.Rows[e.RowIndex].DataBoundItem is Divida d)
        {
            var coluna = dgvDividas.Columns[e.ColumnIndex].Name;

            switch (coluna)
            {
                case "colSituacao":
                    if (!d.SituacaoPaga)
                    {
                        e.Value = "EM ABERTO";
                        e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                        e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    }
                    else
                    {
                        e.Value = "PAGA";
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    }
                    e.FormattingApplied = true;
                    break;

                case "colDataCompra":
                    e.Value = d.DataCriacao.ToString("dd/MM/yyyy HH:mm");
                    e.FormattingApplied = true;
                    break;

                case "colDataPagto":
                    e.Value = d.DataPagamento.HasValue
                        ? d.DataPagamento.Value.ToString("dd/MM/yyyy HH:mm")
                        : "-";
                    e.FormattingApplied = true;
                    break;
            }
        }
    }

    // carrega os molhos do catalogo no combobox
    private void CarregarCatalogoMolhos()
    {
        try
        {
            _molhosCatalogo = _molhoService.ListarCatalogo();
            cmbMolhos.DataSource = null;
            cmbMolhos.DataSource = _molhosCatalogo;
            cmbMolhos.DisplayMember = "Nome";
            cmbMolhos.ValueMember = "Id";

            if (_molhosCatalogo.Count == 0)
            {
                lblPrecoUnitario.Text = "Preco Unit.: -";
                lblValorTotal.Text = "Total: R$ 0,00";
                btnPendurar.Enabled = false;
            }
            else
            {
                btnPendurar.Enabled = true;
                CalcularTotalCompra();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao carregar catalogo de molhos: {ex.Message}",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // busca o historico de dividas do cliente
    private void CarregarHistoricoDividas()
    {
        try
        {
            _dividasCliente = _dividaService.ListarDividasPorCliente(_cliente.Id);
            dgvDividas.DataSource = null;
            dgvDividas.DataSource = _dividasCliente;
            AtualizarCardCliente();
            AtualizarBotaoQuitar();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao listar dividas: {ex.Message}",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // recalcula o valor total da compra na interface
    private void CalcularTotalCompra()
    {
        if (cmbMolhos.SelectedItem is Molho m)
        {
            var qtd = (int)numQuantidade.Value;
            var unitario = m.PrecoVenda;
            var total = unitario * qtd;

            lblPrecoUnitario.Text = $"Preco Unit.: {unitario.ToString("C2", _culturaBr)}";
            lblValorTotal.Text = $"Total: {total.ToString("C2", _culturaBr)}";
        }
        else
        {
            lblPrecoUnitario.Text = "Preco Unit.: R$ 0,00";
            lblValorTotal.Text = "Total: R$ 0,00";
        }
    }

    private void cmbMolhos_SelectedIndexChanged(object? sender, EventArgs e)
    {
        CalcularTotalCompra();
    }

    private void numQuantidade_ValueChanged(object? sender, EventArgs e)
    {
        CalcularTotalCompra();
    }

    // registra a compra pendurada com a trava de apenas 1 divida aberta
    private void btnPendurar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cmbMolhos.SelectedItem is not Molho molhoEscolhido)
            {
                throw new ArgumentException("Selecione um molho do catalogo.");
            }

            var qtd = (int)numQuantidade.Value;
            var obs = string.IsNullOrWhiteSpace(txtObs.Text) ? null : txtObs.Text.Trim();

            var divida = _dividaService.CadastrarDividaPorMolho(_cliente.Id, molhoEscolhido.Id, qtd, obs);

            MessageBox.Show(
                $"Compra pendurada com sucesso na caderneta do Avo Bob!\nMolho: {divida.DescricaoMolhos}\nValor Total: {divida.Valor.ToString("C2", _culturaBr)}",
                "Compra Pendurada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtObs.Clear();
            numQuantidade.Value = 1;
            CarregarHistoricoDividas();
        }
        catch (InvalidOperationException ex)
        {
            // regra critica do pequeno negocio: trava de apenas 1 divida aberta por vez
            MessageBox.Show(
                ex.Message,
                "Trava da Caderneta do Avo Bob",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Erro ao Pendurar Compra",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // quita a divida selecionada
    private void btnMarcarPaga_Click(object? sender, EventArgs e)
    {
        var divida = ObterDividaSelecionada();
        if (divida is null)
        {
            MessageBox.Show("Selecione uma divida na tabela para marcar como paga.", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (divida.SituacaoPaga)
        {
            MessageBox.Show("Esta divida ja foi quitada anteriormente!", "Informacao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirmacao = MessageBox.Show(
            $"Confirma o recebimento e quitacao da divida ID {divida.Id} no valor de {divida.Valor.ToString("C2", _culturaBr)}?",
            "Acerto de Contas",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmacao == DialogResult.Yes)
        {
            try
            {
                var quitada = _dividaService.QuitarDivida(divida.Id);
                MessageBox.Show(
                    $"Conta acertada com sucesso!\nQuitada em {quitada.DataPagamento:dd/MM/yyyy HH:mm}.",
                    "Divida Quitada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CarregarHistoricoDividas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao quitar divida: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void dgvDividas_SelectionChanged(object? sender, EventArgs e)
    {
        AtualizarBotaoQuitar();
    }

    // ajusta a ativacao do botao de quitação conforme a selecao
    private void AtualizarBotaoQuitar()
    {
        var divida = ObterDividaSelecionada();
        if (divida is null)
        {
            btnMarcarPaga.Enabled = false;
            btnMarcarPaga.Text = "Marcar Divida como Paga";
        }
        else if (divida.SituacaoPaga)
        {
            btnMarcarPaga.Enabled = false;
            btnMarcarPaga.Text = "Divida ja Quitada";
        }
        else
        {
            btnMarcarPaga.Enabled = true;
            btnMarcarPaga.Text = $"Quitar Divida ({divida.Valor.ToString("C2", _culturaBr)})";
        }
    }

    private Divida? ObterDividaSelecionada()
    {
        return dgvDividas.CurrentRow?.DataBoundItem as Divida;
    }

    // formata cpf com pontos e traco
    private static string FormatarCpf(string cpf)
    {
        var digitos = new string(cpf.Where(char.IsDigit).ToArray());
        if (digitos.Length != 11)
        {
            return cpf;
        }

        return $"{digitos[..3]}.{digitos[3..6]}.{digitos[6..9]}-{digitos[9..]}";
    }
}
