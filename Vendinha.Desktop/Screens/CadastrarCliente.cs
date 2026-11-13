using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens;

// formulario para cadastrar ou editar cliente da vendinha
public partial class CadastrarCliente : Form
{
    private readonly ClienteService _clienteService;
    private readonly Cliente? _clienteEdicao;

    public CadastrarCliente(ClienteService clienteService, Cliente? clienteEdicao = null)
    {
        _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        _clienteEdicao = clienteEdicao;
        InitializeComponent();

        if (_clienteEdicao is not null)
        {
            lblTitulo.Text = "Editar Cliente";
            btnSalvar.Text = "Salvar Alteracoes";
            Text = "Editar Cliente - Vendinha do Avo Bob";
            PreencherCampos(_clienteEdicao);
        }
        else
        {
            dtpNascimento.Value = DateTime.Today.AddYears(-25);
            dtpNascimento.MaxDate = DateTime.Today;
        }
    }

    // preenche o formulario quando estiver no modo de edicao
    private void PreencherCampos(Cliente c)
    {
        txtNome.Text = c.NomeCompleto;
        txtCpf.Text = c.Cpf;
        dtpNascimento.MaxDate = DateTime.Today;
        dtpNascimento.Value = c.DataNascimento.ToDateTime(TimeOnly.MinValue);
        txtEmail.Text = c.Email ?? string.Empty;
    }

    // salva ou atualiza o cliente executando as validacoes completas
    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        try
        {
            var dataNascimento = DateOnly.FromDateTime(dtpNascimento.Value);

            if (_clienteEdicao is null)
            {
                var novoCliente = new Cliente
                {
                    NomeCompleto = txtNome.Text,
                    Cpf = txtCpf.Text,
                    DataNascimento = dataNascimento,
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text
                };

                var cadastrado = _clienteService.CadastrarCliente(novoCliente);
                MessageBox.Show(
                    $"Cliente '{cadastrado.NomeCompleto}' cadastrado com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                _clienteEdicao.NomeCompleto = txtNome.Text;
                _clienteEdicao.Cpf = txtCpf.Text;
                _clienteEdicao.DataNascimento = dataNascimento;
                _clienteEdicao.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;

                var atualizado = _clienteService.AtualizarCliente(_clienteEdicao);
                MessageBox.Show(
                    $"Cliente '{atualizado.NomeCompleto}' atualizado com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Validacao de Cliente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void btnCancelar_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
