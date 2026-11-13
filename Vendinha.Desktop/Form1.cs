using Vendinha.Contexts;
using Vendinha.Desktop.Screens;
using Vendinha.Services;

namespace Vendinha.Desktop;

// painel principal da vendinha do avo bob conectando todas as telas do sistema
public partial class Form1 : Form
{
    private readonly VendinhaDbContext _context;
    private readonly ClienteService _clienteService;
    private readonly DividaService _dividaService;
    private readonly PimentaService _pimentaService;
    private readonly MolhoService _molhoService;

    public Form1()
    {
        InitializeComponent();

        _context = new VendinhaDbContext();
        _clienteService = new ClienteService(_context);
        _dividaService = new DividaService(_context);
        _pimentaService = new PimentaService(_context);
        _molhoService = new MolhoService(_context, _dividaService);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        try
        {
            // garante criacao do banco e das tabelas caso ainda nao existam
            _context.Database.EnsureCreated();
            lblStatusDb.Text = "Banco de Dados: Conectado e tabelas verificadas com sucesso";
            lblStatusDb.ForeColor = Color.FromArgb(22, 101, 52);
        }
        catch (Exception ex)
        {
            lblStatusDb.Text = "Banco de Dados: Nao foi possivel conectar ao PostgreSQL local";
            lblStatusDb.ForeColor = Color.FromArgb(185, 28, 28);

            MessageBox.Show(
                $"Nao foi possivel conectar ao banco PostgreSQL.\n\nDetalhes: {ex.Message}\n\nVerifique se o servico do PostgreSQL esta em execucao ou ajuste a variavel VENDINHA_CONNECTION_STRING.",
                "Aviso de Conexao",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    // abre a tela de listagem de clientes com paginacao e filtro
    private void btnListarClientes_Click(object? sender, EventArgs e)
    {
        using var tela = new ListarClientes(_clienteService, _dividaService, _molhoService);
        tela.ShowDialog(this);
    }

    // abre a tela de cadastro rapido de novo cliente
    private void btnNovoCliente_Click(object? sender, EventArgs e)
    {
        using var tela = new CadastrarCliente(_clienteService);
        tela.ShowDialog(this);
    }

    // abre a lista de clientes para escolher qual tera a compra pendurada ou quitada
    private void btnDividas_Click(object? sender, EventArgs e)
    {
        using var tela = new ListarClientes(_clienteService, _dividaService, _molhoService);
        tela.ShowDialog(this);
    }

    // abre a tela de variedades e custos de pimentas
    private void btnPimentas_Click(object? sender, EventArgs e)
    {
        using var tela = new GerenciarPimentas(_pimentaService);
        tela.ShowDialog(this);
    }

    // abre a tela de receitas e catalogo de molhos
    private void btnMolhos_Click(object? sender, EventArgs e)
    {
        using var tela = new GerenciarMolhos(_molhoService, _pimentaService);
        tela.ShowDialog(this);
    }

    // abre o relatorio gerencial de custos e lucro
    private void btnRelatorio_Click(object? sender, EventArgs e)
    {
        using var tela = new RelatorioCustosNegocio(_molhoService);
        tela.ShowDialog(this);
    }

    private void btnSair_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
    {
        // libera a conexao do dbcontext ao fechar a aplicacao
        _context.Dispose();
    }
}
