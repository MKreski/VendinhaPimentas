#nullable disable

namespace Vendinha.Desktop.Screens;

partial class RelatorioCustosNegocio
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private TableLayoutPanel tblCards;
    private Panel pnlCardCusto;
    private Label lblCardCustoValor;
    private Label lblCardCustoTitulo;
    private Panel pnlCardPreco;
    private Label lblCardPrecoValor;
    private Label lblCardPrecoTitulo;
    private Panel pnlCardMargem;
    private Label lblCardMargemValor;
    private Label lblCardMargemTitulo;
    private Panel pnlCardAberto;
    private Label lblCardAbertoValor;
    private Label lblCardAbertoTitulo;
    private Panel pnlCardRecebido;
    private Label lblCardRecebidoValor;
    private Label lblCardRecebidoTitulo;
    private Panel pnlCardReceitas;
    private Label lblCardReceitasValor;
    private Label lblCardReceitasTitulo;
    private GroupBox grpTabela;
    private DataGridView dgvMolhosResumo;
    private Panel pnlRodape;
    private Componentes.PrimaryButton btnAtualizar;
    private Button btnFechar;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlHeader = new Panel();
        lblTitulo = new Label();
        lblSubtitulo = new Label();
        tblCards = new TableLayoutPanel();
        pnlCardReceitas = new Panel();
        lblCardReceitasValor = new Label();
        lblCardReceitasTitulo = new Label();
        pnlCardRecebido = new Panel();
        lblCardRecebidoValor = new Label();
        lblCardRecebidoTitulo = new Label();
        pnlCardAberto = new Panel();
        lblCardAbertoValor = new Label();
        lblCardAbertoTitulo = new Label();
        pnlCardMargem = new Panel();
        lblCardMargemValor = new Label();
        lblCardMargemTitulo = new Label();
        pnlCardPreco = new Panel();
        lblCardPrecoValor = new Label();
        lblCardPrecoTitulo = new Label();
        pnlCardCusto = new Panel();
        lblCardCustoValor = new Label();
        lblCardCustoTitulo = new Label();
        grpTabela = new GroupBox();
        dgvMolhosResumo = new DataGridView();
        pnlRodape = new Panel();
        btnFechar = new Button();
        btnAtualizar = new Componentes.PrimaryButton();
        pnlHeader.SuspendLayout();
        tblCards.SuspendLayout();
        pnlCardReceitas.SuspendLayout();
        pnlCardRecebido.SuspendLayout();
        pnlCardAberto.SuspendLayout();
        pnlCardMargem.SuspendLayout();
        pnlCardPreco.SuspendLayout();
        pnlCardCusto.SuspendLayout();
        grpTabela.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMolhosResumo).BeginInit();
        pnlRodape.SuspendLayout();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(185, 28, 28);
        pnlHeader.Controls.Add(lblSubtitulo);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(984, 65);
        pnlHeader.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(16, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(330, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Relatorio de Custos e Desempenho";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 9F);
        lblSubtitulo.ForeColor = Color.FromArgb(254, 226, 226);
        lblSubtitulo.Location = new Point(18, 38);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(393, 15);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Metricas financeiras, margens de lucro e status dos recebimentos fiado";
        // 
        // tblCards
        // 
        tblCards.ColumnCount = 3;
        tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tblCards.Controls.Add(pnlCardReceitas, 2, 1);
        tblCards.Controls.Add(pnlCardRecebido, 1, 1);
        tblCards.Controls.Add(pnlCardAberto, 0, 1);
        tblCards.Controls.Add(pnlCardMargem, 2, 0);
        tblCards.Controls.Add(pnlCardPreco, 1, 0);
        tblCards.Controls.Add(pnlCardCusto, 0, 0);
        tblCards.Dock = DockStyle.Top;
        tblCards.Location = new Point(0, 65);
        tblCards.Name = "tblCards";
        tblCards.Padding = new Padding(12, 10, 12, 10);
        tblCards.RowCount = 2;
        tblCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblCards.Size = new Size(984, 185);
        tblCards.TabIndex = 1;
        // 
        // pnlCardReceitas
        // 
        pnlCardReceitas.BackColor = Color.White;
        pnlCardReceitas.BorderStyle = BorderStyle.FixedSingle;
        pnlCardReceitas.Controls.Add(lblCardReceitasValor);
        pnlCardReceitas.Controls.Add(lblCardReceitasTitulo);
        pnlCardReceitas.Dock = DockStyle.Fill;
        pnlCardReceitas.Location = new Point(655, 95);
        pnlCardReceitas.Margin = new Padding(4);
        pnlCardReceitas.Name = "pnlCardReceitas";
        pnlCardReceitas.Padding = new Padding(10);
        pnlCardReceitas.Size = new Size(313, 76);
        pnlCardReceitas.TabIndex = 5;
        // 
        // lblCardReceitasValor
        // 
        lblCardReceitasValor.AutoSize = true;
        lblCardReceitasValor.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblCardReceitasValor.ForeColor = Color.FromArgb(31, 41, 55);
        lblCardReceitasValor.Location = new Point(10, 35);
        lblCardReceitasValor.Name = "lblCardReceitasValor";
        lblCardReceitasValor.Size = new Size(187, 25);
        lblCardReceitasValor.TabIndex = 1;
        lblCardReceitasValor.Text = "0 receitas no catalogo";
        // 
        // lblCardReceitasTitulo
        // 
        lblCardReceitasTitulo.AutoSize = true;
        lblCardReceitasTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblCardReceitasTitulo.ForeColor = Color.FromArgb(107, 114, 128);
        lblCardReceitasTitulo.Location = new Point(10, 12);
        lblCardReceitasTitulo.Name = "lblCardReceitasTitulo";
        lblCardReceitasTitulo.Size = new Size(147, 15);
        lblCardReceitasTitulo.TabIndex = 0;
        lblCardReceitasTitulo.Text = "VARIEDADES DE MOLHOS";
        // 
        // pnlCardRecebido
        // 
        pnlCardRecebido.BackColor = Color.White;
        pnlCardRecebido.BorderStyle = BorderStyle.FixedSingle;
        pnlCardRecebido.Controls.Add(lblCardRecebidoValor);
        pnlCardRecebido.Controls.Add(lblCardRecebidoTitulo);
        pnlCardRecebido.Dock = DockStyle.Fill;
        pnlCardRecebido.Location = new Point(335, 95);
        pnlCardRecebido.Margin = new Padding(4);
        pnlCardRecebido.Name = "pnlCardRecebido";
        pnlCardRecebido.Padding = new Padding(10);
        pnlCardRecebido.Size = new Size(312, 76);
        pnlCardRecebido.TabIndex = 4;
        // 
        // lblCardRecebidoValor
        // 
        lblCardRecebidoValor.AutoSize = true;
        lblCardRecebidoValor.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblCardRecebidoValor.ForeColor = Color.FromArgb(22, 101, 52);
        lblCardRecebidoValor.Location = new Point(10, 32);
        lblCardRecebidoValor.Name = "lblCardRecebidoValor";
        lblCardRecebidoValor.Size = new Size(82, 28);
        lblCardRecebidoValor.TabIndex = 1;
        lblCardRecebidoValor.Text = "R$ 0,00";
        // 
        // lblCardRecebidoTitulo
        // 
        lblCardRecebidoTitulo.AutoSize = true;
        lblCardRecebidoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblCardRecebidoTitulo.ForeColor = Color.FromArgb(107, 114, 128);
        lblCardRecebidoTitulo.Location = new Point(10, 12);
        lblCardRecebidoTitulo.Name = "lblCardRecebidoTitulo";
        lblCardRecebidoTitulo.Size = new Size(160, 15);
        lblCardRecebidoTitulo.TabIndex = 0;
        lblCardRecebidoTitulo.Text = "TOTAL JA RECEBIDO (PAGO)";
        // 
        // pnlCardAberto
        // 
        pnlCardAberto.BackColor = Color.White;
        pnlCardAberto.BorderStyle = BorderStyle.FixedSingle;
        pnlCardAberto.Controls.Add(lblCardAbertoValor);
        pnlCardAberto.Controls.Add(lblCardAbertoTitulo);
        pnlCardAberto.Dock = DockStyle.Fill;
        pnlCardAberto.Location = new Point(16, 95);
        pnlCardAberto.Margin = new Padding(4);
        pnlCardAberto.Name = "pnlCardAberto";
        pnlCardAberto.Padding = new Padding(10);
        pnlCardAberto.Size = new Size(311, 76);
        pnlCardAberto.TabIndex = 3;
        // 
        // lblCardAbertoValor
        // 
        lblCardAbertoValor.AutoSize = true;
        lblCardAbertoValor.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblCardAbertoValor.ForeColor = Color.FromArgb(185, 28, 28);
        lblCardAbertoValor.Location = new Point(10, 32);
        lblCardAbertoValor.Name = "lblCardAbertoValor";
        lblCardAbertoValor.Size = new Size(82, 28);
        lblCardAbertoValor.TabIndex = 1;
        lblCardAbertoValor.Text = "R$ 0,00";
        // 
        // lblCardAbertoTitulo
        // 
        lblCardAbertoTitulo.AutoSize = true;
        lblCardAbertoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblCardAbertoTitulo.ForeColor = Color.FromArgb(107, 114, 128);
        lblCardAbertoTitulo.Location = new Point(10, 12);
        lblCardAbertoTitulo.Name = "lblCardAbertoTitulo";
        lblCardAbertoTitulo.Size = new Size(181, 15);
        lblCardAbertoTitulo.TabIndex = 0;
        lblCardAbertoTitulo.Text = "TOTAL FIADO (A RECEBER HOJE)";
        // 
        // pnlCardMargem
        // 
        pnlCardMargem.BackColor = Color.White;
        pnlCardMargem.BorderStyle = BorderStyle.FixedSingle;
        pnlCardMargem.Controls.Add(lblCardMargemValor);
        pnlCardMargem.Controls.Add(lblCardMargemTitulo);
        pnlCardMargem.Dock = DockStyle.Fill;
        pnlCardMargem.Location = new Point(655, 14);
        pnlCardMargem.Margin = new Padding(4);
        pnlCardMargem.Name = "pnlCardMargem";
        pnlCardMargem.Padding = new Padding(10);
        pnlCardMargem.Size = new Size(313, 73);
        pnlCardMargem.TabIndex = 2;
        // 
        // lblCardMargemValor
        // 
        lblCardMargemValor.AutoSize = true;
        lblCardMargemValor.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblCardMargemValor.ForeColor = Color.FromArgb(3, 105, 161);
        lblCardMargemValor.Location = new Point(10, 32);
        lblCardMargemValor.Name = "lblCardMargemValor";
        lblCardMargemValor.Size = new Size(62, 28);
        lblCardMargemValor.TabIndex = 1;
        lblCardMargemValor.Text = "0,0 %";
        // 
        // lblCardMargemTitulo
        // 
        lblCardMargemTitulo.AutoSize = true;
        lblCardMargemTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblCardMargemTitulo.ForeColor = Color.FromArgb(107, 114, 128);
        lblCardMargemTitulo.Location = new Point(10, 12);
        lblCardMargemTitulo.Name = "lblCardMargemTitulo";
        lblCardMargemTitulo.Size = new Size(157, 15);
        lblCardMargemTitulo.TabIndex = 0;
        lblCardMargemTitulo.Text = "MARGEM MEDIA DE LUCRO";
        // 
        // pnlCardPreco
        // 
        pnlCardPreco.BackColor = Color.White;
        pnlCardPreco.BorderStyle = BorderStyle.FixedSingle;
        pnlCardPreco.Controls.Add(lblCardPrecoValor);
        pnlCardPreco.Controls.Add(lblCardPrecoTitulo);
        pnlCardPreco.Dock = DockStyle.Fill;
        pnlCardPreco.Location = new Point(335, 14);
        pnlCardPreco.Margin = new Padding(4);
        pnlCardPreco.Name = "pnlCardPreco";
        pnlCardPreco.Padding = new Padding(10);
        pnlCardPreco.Size = new Size(312, 73);
        pnlCardPreco.TabIndex = 1;
        // 
        // lblCardPrecoValor
        // 
        lblCardPrecoValor.AutoSize = true;
        lblCardPrecoValor.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblCardPrecoValor.ForeColor = Color.FromArgb(146, 64, 14);
        lblCardPrecoValor.Location = new Point(10, 32);
        lblCardPrecoValor.Name = "lblCardPrecoValor";
        lblCardPrecoValor.Size = new Size(82, 28);
        lblCardPrecoValor.TabIndex = 1;
        lblCardPrecoValor.Text = "R$ 0,00";
        // 
        // lblCardPrecoTitulo
        // 
        lblCardPrecoTitulo.AutoSize = true;
        lblCardPrecoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblCardPrecoTitulo.ForeColor = Color.FromArgb(107, 114, 128);
        lblCardPrecoTitulo.Location = new Point(10, 12);
        lblCardPrecoTitulo.Name = "lblCardPrecoTitulo";
        lblCardPrecoTitulo.Size = new Size(156, 15);
        lblCardPrecoTitulo.TabIndex = 0;
        lblCardPrecoTitulo.Text = "PRECO MEDIO DE BALCAO";
        // 
        // pnlCardCusto
        // 
        pnlCardCusto.BackColor = Color.White;
        pnlCardCusto.BorderStyle = BorderStyle.FixedSingle;
        pnlCardCusto.Controls.Add(lblCardCustoValor);
        pnlCardCusto.Controls.Add(lblCardCustoTitulo);
        pnlCardCusto.Dock = DockStyle.Fill;
        pnlCardCusto.Location = new Point(16, 14);
        pnlCardCusto.Margin = new Padding(4);
        pnlCardCusto.Name = "pnlCardCusto";
        pnlCardCusto.Padding = new Padding(10);
        pnlCardCusto.Size = new Size(311, 73);
        pnlCardCusto.TabIndex = 0;
        // 
        // lblCardCustoValor
        // 
        lblCardCustoValor.AutoSize = true;
        lblCardCustoValor.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblCardCustoValor.ForeColor = Color.FromArgb(31, 41, 55);
        lblCardCustoValor.Location = new Point(10, 32);
        lblCardCustoValor.Name = "lblCardCustoValor";
        lblCardCustoValor.Size = new Size(82, 28);
        lblCardCustoValor.TabIndex = 1;
        lblCardCustoValor.Text = "R$ 0,00";
        // 
        // lblCardCustoTitulo
        // 
        lblCardCustoTitulo.AutoSize = true;
        lblCardCustoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblCardCustoTitulo.ForeColor = Color.FromArgb(107, 114, 128);
        lblCardCustoTitulo.Location = new Point(10, 12);
        lblCardCustoTitulo.Name = "lblCardCustoTitulo";
        lblCardCustoTitulo.Size = new Size(163, 15);
        lblCardCustoTitulo.TabIndex = 0;
        lblCardCustoTitulo.Text = "CUSTO MEDIO POR FRASCO";
        // 
        // grpTabela
        // 
        grpTabela.Controls.Add(dgvMolhosResumo);
        grpTabela.Dock = DockStyle.Fill;
        grpTabela.Font = new Font("Segoe UI", 9.5F);
        grpTabela.Location = new Point(0, 250);
        grpTabela.Name = "grpTabela";
        grpTabela.Padding = new Padding(12, 10, 12, 10);
        grpTabela.Size = new Size(984, 256);
        grpTabela.TabIndex = 2;
        grpTabela.TabStop = false;
        grpTabela.Text = "Margens Detalhadas por Molho do Catalogo";
        // 
        // dgvMolhosResumo
        // 
        dgvMolhosResumo.AllowUserToAddRows = false;
        dgvMolhosResumo.AllowUserToDeleteRows = false;
        dgvMolhosResumo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMolhosResumo.BackgroundColor = Color.White;
        dgvMolhosResumo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMolhosResumo.Dock = DockStyle.Fill;
        dgvMolhosResumo.Location = new Point(12, 27);
        dgvMolhosResumo.MultiSelect = false;
        dgvMolhosResumo.Name = "dgvMolhosResumo";
        dgvMolhosResumo.ReadOnly = true;
        dgvMolhosResumo.RowHeadersVisible = false;
        dgvMolhosResumo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMolhosResumo.Size = new Size(960, 219);
        dgvMolhosResumo.TabIndex = 0;
        // 
        // pnlRodape
        // 
        pnlRodape.BackColor = Color.White;
        pnlRodape.Controls.Add(btnFechar);
        pnlRodape.Controls.Add(btnAtualizar);
        pnlRodape.Dock = DockStyle.Bottom;
        pnlRodape.Location = new Point(0, 506);
        pnlRodape.Name = "pnlRodape";
        pnlRodape.Size = new Size(984, 55);
        pnlRodape.TabIndex = 3;
        // 
        // btnFechar
        // 
        btnFechar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnFechar.BackColor = Color.FromArgb(243, 244, 246);
        btnFechar.FlatStyle = FlatStyle.Flat;
        btnFechar.Location = new Point(870, 10);
        btnFechar.Name = "btnFechar";
        btnFechar.Size = new Size(100, 36);
        btnFechar.TabIndex = 1;
        btnFechar.Text = "Fechar";
        btnFechar.UseVisualStyleBackColor = false;
        btnFechar.Click += btnFechar_Click;
        // 
        // btnAtualizar
        // 
        btnAtualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAtualizar.Location = new Point(660, 8);
        btnAtualizar.Name = "btnAtualizar";
        btnAtualizar.Size = new Size(200, 40);
        btnAtualizar.TabIndex = 0;
        btnAtualizar.Text = "Atualizar Indicadores";
        btnAtualizar.Click += btnAtualizar_Click;
        // 
        // RelatorioCustosNegocio
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(984, 561);
        Controls.Add(grpTabela);
        Controls.Add(pnlRodape);
        Controls.Add(tblCards);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(900, 550);
        Name = "RelatorioCustosNegocio";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Relatorio de Custos e Desempenho - Vendinha do Avo Bob";
        Load += RelatorioCustosNegocio_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        tblCards.ResumeLayout(false);
        pnlCardReceitas.ResumeLayout(false);
        pnlCardReceitas.PerformLayout();
        pnlCardRecebido.ResumeLayout(false);
        pnlCardRecebido.PerformLayout();
        pnlCardAberto.ResumeLayout(false);
        pnlCardAberto.PerformLayout();
        pnlCardMargem.ResumeLayout(false);
        pnlCardMargem.PerformLayout();
        pnlCardPreco.ResumeLayout(false);
        pnlCardPreco.PerformLayout();
        pnlCardCusto.ResumeLayout(false);
        pnlCardCusto.PerformLayout();
        grpTabela.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvMolhosResumo).EndInit();
        pnlRodape.ResumeLayout(false);
        ResumeLayout(false);
    }
}
