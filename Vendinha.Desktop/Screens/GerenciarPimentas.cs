using System.Globalization;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Desktop.Screens;

// tela para cadastrar, editar e listar variedades de pimentas
public partial class GerenciarPimentas : Form
{
    private readonly PimentaService _pimentaService;
    private readonly CultureInfo _culturaBr = new("pt-BR");
    private Pimenta? _pimentaSelecionada;

    public GerenciarPimentas(PimentaService pimentaService)
    {
        _pimentaService = pimentaService ?? throw new ArgumentNullException(nameof(pimentaService));
        InitializeComponent();
    }

    private void GerenciarPimentas_Load(object? sender, EventArgs e)
    {
        ConfigurarGrid();
        CarregarPimentas();
    }

    // define colunas amigaveis e formatacao da tabela
    private void ConfigurarGrid()
    {
        dgvPimentas.Columns.Clear();
        dgvPimentas.AutoGenerateColumns = false;

        dgvPimentas.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            FillWeight = 15
        });

        dgvPimentas.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Nome",
            HeaderText = "Nome",
            FillWeight = 35
        });

        dgvPimentas.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "EspecieOrigem",
            HeaderText = "Especie / Origem",
            FillWeight = 30
        });

        var colScoville = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "NivelScoville",
            HeaderText = "Ardencia (SHU)",
            FillWeight = 25
        };
        colScoville.DefaultCellStyle.Format = "N0";
        colScoville.DefaultCellStyle.FormatProvider = _culturaBr;
        colScoville.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvPimentas.Columns.Add(colScoville);

        var colCusto = new DataGridViewTextBoxColumn
        {
            DataPropertyName = "CustoPorGrama",
            HeaderText = "Custo / grama",
            FillWeight = 25
        };
        colCusto.DefaultCellStyle.Format = "C4";
        colCusto.DefaultCellStyle.FormatProvider = _culturaBr;
        colCusto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvPimentas.Columns.Add(colCusto);
    }

    // recarrega os dados do banco de dados na tabela
    private void CarregarPimentas()
    {
        try
        {
            var pimentas = _pimentaService.ListarPimentas();
            dgvPimentas.DataSource = null;
            dgvPimentas.DataSource = pimentas;
            LimparCampos();
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

    // salva uma nova pimenta ou atualiza a existente
    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_pimentaSelecionada is null)
            {
                var novaPimenta = new Pimenta
                {
                    Nome = txtNome.Text,
                    EspecieOrigem = txtEspecie.Text,
                    NivelScoville = (int)numScoville.Value,
                    CustoPorGrama = numCusto.Value
                };

                _pimentaService.CadastrarPimenta(novaPimenta);
                MessageBox.Show(
                    "Pimenta cadastrada com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                _pimentaSelecionada.Nome = txtNome.Text;
                _pimentaSelecionada.EspecieOrigem = txtEspecie.Text;
                _pimentaSelecionada.NivelScoville = (int)numScoville.Value;
                _pimentaSelecionada.CustoPorGrama = numCusto.Value;

                _pimentaService.AtualizarPimenta(_pimentaSelecionada);
                MessageBox.Show(
                    "Pimenta atualizada com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            CarregarPimentas();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Validacao de Pimenta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    // remove a pimenta selecionada caso nao esteja vinculada a molhos
    private void btnExcluir_Click(object? sender, EventArgs e)
    {
        if (_pimentaSelecionada is null)
        {
            MessageBox.Show(
                "Selecione uma pimenta na tabela para remover.",
                "Atencao",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var confirmacao = MessageBox.Show(
            $"Deseja realmente remover a pimenta '{_pimentaSelecionada.Nome}'?",
            "Confirmar Remocao",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmacao == DialogResult.Yes)
        {
            try
            {
                _pimentaService.RemoverPimenta(_pimentaSelecionada.Id);
                MessageBox.Show(
                    "Pimenta removida com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                CarregarPimentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Impossivel Remover",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }

    // limpa o formulario para permitir novo cadastro
    private void btnNovo_Click(object? sender, EventArgs e)
    {
        LimparCampos();
    }

    // preenche os campos com a pimenta clicada na grade
    private void dgvPimentas_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvPimentas.CurrentRow?.DataBoundItem is Pimenta p)
        {
            _pimentaSelecionada = p;
            txtNome.Text = p.Nome;
            txtEspecie.Text = p.EspecieOrigem;
            numScoville.Value = Math.Clamp(p.NivelScoville, numScoville.Minimum, numScoville.Maximum);
            numCusto.Value = Math.Clamp(p.CustoPorGrama, numCusto.Minimum, numCusto.Maximum);
            btnSalvar.Text = "Atualizar Pimenta";
        }
    }

    // reseta o estado do formulario
    private void LimparCampos()
    {
        _pimentaSelecionada = null;
        txtNome.Clear();
        txtEspecie.Clear();
        numScoville.Value = 50000;
        numCusto.Value = 0.1500m;
        btnSalvar.Text = "Salvar Pimenta";
        dgvPimentas.ClearSelection();
    }
}
