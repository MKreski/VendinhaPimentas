using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens;

// tela que lista os clientes paginados e ordenados pelo maior devedor
public partial class ListarClientes : Form
{
    private readonly ClienteService _clienteService;
    private readonly DividaService _dividaService;
    private readonly MolhoService _molhoService;
    private readonly CultureInfo _culturaBr = new("pt-BR");

    private int _paginaAtual = 1;
    private const int TamanhoPagina = 10;
    private string? _filtroNomeAtual = null;

    public ListarClientes(
        ClienteService clienteService,
        DividaService dividaService,
        MolhoService molhoService)
    {
        _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        _dividaService = dividaService ?? throw new ArgumentNullException(nameof(dividaService));
        _molhoService = molhoService ?? throw new ArgumentNullException(nameof(molhoService));
        InitializeComponent();
    }

    private void ListarClientes_Load(object? sender, EventArgs e)
    {
        ConfigurarGrid();
        CarregarClientes();
    }

    // define colunas e formatacao de valores
    private void ConfigurarGrid()
    {
        dgvClientes.Columns.Clear();
        dgvClientes.AutoGenerateColumns = false;

        dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            FillWeight = 10
        });

        dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "NomeCompleto",
            HeaderText = "Nome Completo",
            FillWeight = 38
        });

        dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colCpf",
            HeaderText = "CPF",
            FillWeight = 20
        });

        var colIdade = new DataGridViewTextBoxColumn
        {
            Name = "colIdade",
            HeaderText = "Idade Atual",
            FillWeight = 14
        };
        colIdade.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvClientes.Columns.Add(colIdade);

        var colDivida = new DataGridViewTextBoxColumn
        {
            Name = "colDivida",
            HeaderText = "Soma de Dividas (Aberta)",
            FillWeight = 22
        };
        colDivida.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colDivida.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        dgvClientes.Columns.Add(colDivida);

        dgvClientes.CellFormatting += DgvClientes_CellFormatting;
    }

    // formata cpf com mascara, idade e valor em moeda brasileira
    private void DgvClientes_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvClientes.Rows.Count)
        {
            return;
        }

        if (dgvClientes.Rows[e.RowIndex].DataBoundItem is Cliente c)
        {
            var coluna = dgvClientes.Columns[e.ColumnIndex].Name;

            switch (coluna)
            {
                case "colCpf":
                    e.Value = FormatarCpf(c.Cpf);
                    e.FormattingApplied = true;
                    break;

                case "colIdade":
                    e.Value = $"{c.Idade} anos";
                    e.FormattingApplied = true;
                    break;

                case "colDivida":
                    e.Value = c.TotalDividas.ToString("C2", _culturaBr);
                    if (c.TotalDividas > 0m)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    }
                    e.FormattingApplied = true;
                    break;
            }
        }
    }

    // busca clientes no servico com paginacao e ordenacao do maior devedor
    private void CarregarClientes()
    {
        try
        {
            var clientes = _clienteService.ListarClientes(_paginaAtual, TamanhoPagina, _filtroNomeAtual);
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = clientes;

            lblPagina.Text = $"Pagina {_paginaAtual}";
            btnAnterior.Enabled = _paginaAtual > 1;

            // desabilita o botao proxima se veio menos que 10 registros
            btnProxima.Enabled = clientes.Count == TamanhoPagina;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao carregar clientes: {ex.Message}",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        _filtroNomeAtual = string.IsNullOrWhiteSpace(txtBusca.Text) ? null : txtBusca.Text.Trim();
        _paginaAtual = 1;
        CarregarClientes();
    }

    private void btnLimpar_Click(object? sender, EventArgs e)
    {
        txtBusca.Clear();
        _filtroNomeAtual = null;
        _paginaAtual = 1;
        CarregarClientes();
    }

    private void txtBusca_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnBuscar_Click(sender, e);
            e.SuppressKeyPress = true;
        }
    }

    private void btnAnterior_Click(object? sender, EventArgs e)
    {
        if (_paginaAtual > 1)
        {
            _paginaAtual--;
            CarregarClientes();
        }
    }

    private void btnProxima_Click(object? sender, EventArgs e)
    {
        _paginaAtual++;
        CarregarClientes();
    }

    // abre tela de cadastro de novo cliente
    private void btnNovo_Click(object? sender, EventArgs e)
    {
        using var tela = new CadastrarCliente(_clienteService);
        if (tela.ShowDialog(this) == DialogResult.OK)
        {
            CarregarClientes();
        }
    }

    // abre tela de edicao para o cliente selecionado
    private void btnEditar_Click(object? sender, EventArgs e)
    {
        var cliente = ObterClienteSelecionado();
        if (cliente is null)
        {
            MessageBox.Show("Selecione um cliente na tabela para editar.", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var tela = new CadastrarCliente(_clienteService, cliente);
        if (tela.ShowDialog(this) == DialogResult.OK)
        {
            CarregarClientes();
        }
    }

    // abre a tela de gerenciamento de dividas para o cliente selecionado
    private void btnGerenciarDividas_Click(object? sender, EventArgs e)
    {
        var cliente = ObterClienteSelecionado();
        if (cliente is null)
        {
            MessageBox.Show(
                "Selecione um cliente na tabela para ver suas dividas ou pendurar uma compra.",
                "Atencao",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        using var telaDividas = new GerenciarDividas(cliente, _dividaService, _molhoService, _clienteService);
        telaDividas.ShowDialog(this);

        // recarrega a pagina para refletir pagamentos ou novas dividas
        CarregarClientes();
    }

    private void dgvClientes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            btnGerenciarDividas_Click(sender, e);
        }
    }

    private Cliente? ObterClienteSelecionado()
    {
        return dgvClientes.CurrentRow?.DataBoundItem as Cliente;
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
