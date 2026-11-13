#nullable disable

namespace Vendinha.Desktop.Screens;

partial class GerenciarDividas
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private GroupBox grpCliente;
    private Label lblClienteNome;
    private Label lblClienteCpf;
    private Label lblClienteSaldo;
    private GroupBox grpPendurar;
    private Label lblMolho;
    private ComboBox cmbMolhos;
    private Label lblQtd;
    private NumericUpDown numQuantidade;
    private Label lblPrecoUnitario;
    private Label lblValorTotal;
    private Label lblObs;
    private TextBox txtObs;
    private Componentes.PrimaryButton btnPendurar;
    private GroupBox grpHistorico;
    private DataGridView dgvDividas;
    private Panel pnlAcoesHistorico;
    private Componentes.PrimaryButton btnMarcarPaga;

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
        grpCliente = new GroupBox();
        lblClienteSaldo = new Label();
        lblClienteCpf = new Label();
        lblClienteNome = new Label();
        grpPendurar = new GroupBox();
        btnPendurar = new Componentes.PrimaryButton();
        txtObs = new TextBox();
        lblObs = new Label();
        lblValorTotal = new Label();
        lblPrecoUnitario = new Label();
        numQuantidade = new NumericUpDown();
        lblQtd = new Label();
        cmbMolhos = new ComboBox();
        lblMolho = new Label();
        grpHistorico = new GroupBox();
        dgvDividas = new DataGridView();
        pnlAcoesHistorico = new Panel();
        btnMarcarPaga = new Componentes.PrimaryButton();
        pnlHeader.SuspendLayout();
        grpCliente.SuspendLayout();
        grpPendurar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantidade).BeginInit();
        grpHistorico.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDividas).BeginInit();
        pnlAcoesHistorico.SuspendLayout();
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
        pnlHeader.Size = new Size(1004, 65);
        pnlHeader.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(16, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(335, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Caderneta de Dividas e Pagamentos";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 9F);
        lblSubtitulo.ForeColor = Color.FromArgb(254, 226, 226);
        lblSubtitulo.Location = new Point(18, 38);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(428, 15);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Pendure compras de molhos com calculo automatico e registre os acertos de contas";
        // 
        // grpCliente
        // 
        grpCliente.BackColor = Color.White;
        grpCliente.Controls.Add(lblClienteSaldo);
        grpCliente.Controls.Add(lblClienteCpf);
        grpCliente.Controls.Add(lblClienteNome);
        grpCliente.Dock = DockStyle.Top;
        grpCliente.Font = new Font("Segoe UI", 9.5F);
        grpCliente.Location = new Point(0, 65);
        grpCliente.Name = "grpCliente";
        grpCliente.Size = new Size(1004, 75);
        grpCliente.TabIndex = 1;
        grpCliente.TabStop = false;
        grpCliente.Text = "Fregues Selecionado";
        // 
        // lblClienteSaldo
        // 
        lblClienteSaldo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblClienteSaldo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblClienteSaldo.ForeColor = Color.FromArgb(185, 28, 28);
        lblClienteSaldo.Location = new Point(620, 25);
        lblClienteSaldo.Name = "lblClienteSaldo";
        lblClienteSaldo.Size = new Size(365, 30);
        lblClienteSaldo.TabIndex = 2;
        lblClienteSaldo.Text = "Divida em Aberto: R$ 0,00";
        lblClienteSaldo.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblClienteCpf
        // 
        lblClienteCpf.AutoSize = true;
        lblClienteCpf.ForeColor = Color.FromArgb(75, 85, 99);
        lblClienteCpf.Location = new Point(16, 45);
        lblClienteCpf.Name = "lblClienteCpf";
        lblClienteCpf.Size = new Size(198, 17);
        lblClienteCpf.TabIndex = 1;
        lblClienteCpf.Text = "CPF: 000.000.000-00 | Idade: 0 anos";
        // 
        // lblClienteNome
        // 
        lblClienteNome.AutoSize = true;
        lblClienteNome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblClienteNome.Location = new Point(16, 22);
        lblClienteNome.Name = "lblClienteNome";
        lblClienteNome.Size = new Size(125, 20);
        lblClienteNome.TabIndex = 0;
        lblClienteNome.Text = "Nome do Cliente";
        // 
        // grpPendurar
        // 
        grpPendurar.BackColor = Color.FromArgb(254, 252, 232);
        grpPendurar.Controls.Add(btnPendurar);
        grpPendurar.Controls.Add(txtObs);
        grpPendurar.Controls.Add(lblObs);
        grpPendurar.Controls.Add(lblValorTotal);
        grpPendurar.Controls.Add(lblPrecoUnitario);
        grpPendurar.Controls.Add(numQuantidade);
        grpPendurar.Controls.Add(lblQtd);
        grpPendurar.Controls.Add(cmbMolhos);
        grpPendurar.Controls.Add(lblMolho);
        grpPendurar.Dock = DockStyle.Top;
        grpPendurar.Font = new Font("Segoe UI", 9.5F);
        grpPendurar.Location = new Point(0, 140);
        grpPendurar.Name = "grpPendurar";
        grpPendurar.Size = new Size(1004, 115);
        grpPendurar.TabIndex = 2;
        grpPendurar.TabStop = false;
        grpPendurar.Text = "Pendurar Nova Compra de Molho (Trava: Maximo 1 divida aberta por vez)";
        // 
        // btnPendurar
        // 
        btnPendurar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnPendurar.Location = new Point(780, 58);
        btnPendurar.Name = "btnPendurar";
        btnPendurar.Size = new Size(205, 42);
        btnPendurar.TabIndex = 8;
        btnPendurar.Text = "Pendurar na Caderneta";
        btnPendurar.Click += btnPendurar_Click;
        // 
        // txtObs
        // 
        txtObs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtObs.Location = new Point(16, 75);
        txtObs.Name = "txtObs";
        txtObs.PlaceholderText = "observacao opcional (se vazio, usa o nome do molho automaticamente)...";
        txtObs.Size = new Size(505, 24);
        txtObs.TabIndex = 7;
        // 
        // lblObs
        // 
        lblObs.AutoSize = true;
        lblObs.Font = new Font("Segoe UI", 8.5F);
        lblObs.Location = new Point(16, 55);
        lblObs.Name = "lblObs";
        lblObs.Size = new Size(122, 15);
        lblObs.TabIndex = 6;
        lblObs.Text = "Descricao / Anotacao:";
        // 
        // lblValorTotal
        // 
        lblValorTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblValorTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblValorTotal.ForeColor = Color.FromArgb(185, 28, 28);
        lblValorTotal.Location = new Point(540, 60);
        lblValorTotal.Name = "lblValorTotal";
        lblValorTotal.Size = new Size(225, 38);
        lblValorTotal.TabIndex = 5;
        lblValorTotal.Text = "Total: R$ 0,00";
        lblValorTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblPrecoUnitario
        // 
        lblPrecoUnitario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPrecoUnitario.AutoSize = true;
        lblPrecoUnitario.Font = new Font("Segoe UI", 9F);
        lblPrecoUnitario.ForeColor = Color.FromArgb(75, 85, 99);
        lblPrecoUnitario.Location = new Point(545, 30);
        lblPrecoUnitario.Name = "lblPrecoUnitario";
        lblPrecoUnitario.Size = new Size(116, 15);
        lblPrecoUnitario.TabIndex = 4;
        lblPrecoUnitario.Text = "Preco Unit.: R$ 0,00";
        // 
        // numQuantidade
        // 
        numQuantidade.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        numQuantidade.Location = new Point(435, 26);
        numQuantidade.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        numQuantidade.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numQuantidade.Name = "numQuantidade";
        numQuantidade.Size = new Size(85, 24);
        numQuantidade.TabIndex = 3;
        numQuantidade.Value = new decimal(new int[] { 1, 0, 0, 0 });
        numQuantidade.ValueChanged += numQuantidade_ValueChanged;
        // 
        // lblQtd
        // 
        lblQtd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblQtd.AutoSize = true;
        lblQtd.Location = new Point(345, 28);
        lblQtd.Name = "lblQtd";
        lblQtd.Size = new Size(82, 17);
        lblQtd.TabIndex = 2;
        lblQtd.Text = "Qtd Frascos:";
        // 
        // cmbMolhos
        // 
        cmbMolhos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbMolhos.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMolhos.FormattingEnabled = true;
        cmbMolhos.Location = new Point(125, 25);
        cmbMolhos.Name = "cmbMolhos";
        cmbMolhos.Size = new Size(205, 25);
        cmbMolhos.TabIndex = 1;
        cmbMolhos.SelectedIndexChanged += cmbMolhos_SelectedIndexChanged;
        // 
        // lblMolho
        // 
        lblMolho.AutoSize = true;
        lblMolho.Location = new Point(16, 28);
        lblMolho.Name = "lblMolho";
        lblMolho.Size = new Size(100, 17);
        lblMolho.TabIndex = 0;
        lblMolho.Text = "Escolher Molho:";
        // 
        // grpHistorico
        // 
        grpHistorico.Controls.Add(dgvDividas);
        grpHistorico.Controls.Add(pnlAcoesHistorico);
        grpHistorico.Dock = DockStyle.Fill;
        grpHistorico.Font = new Font("Segoe UI", 9.5F);
        grpHistorico.Location = new Point(0, 255);
        grpHistorico.Name = "grpHistorico";
        grpHistorico.Size = new Size(1004, 396);
        grpHistorico.TabIndex = 3;
        grpHistorico.TabStop = false;
        grpHistorico.Text = "Historico de Compras Penduradas";
        // 
        // dgvDividas
        // 
        dgvDividas.AllowUserToAddRows = false;
        dgvDividas.AllowUserToDeleteRows = false;
        dgvDividas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDividas.BackgroundColor = Color.White;
        dgvDividas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDividas.Dock = DockStyle.Fill;
        dgvDividas.Location = new Point(3, 20);
        dgvDividas.MultiSelect = false;
        dgvDividas.Name = "dgvDividas";
        dgvDividas.ReadOnly = true;
        dgvDividas.RowHeadersVisible = false;
        dgvDividas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDividas.Size = new Size(998, 318);
        dgvDividas.TabIndex = 0;
        dgvDividas.SelectionChanged += dgvDividas_SelectionChanged;
        // 
        // pnlAcoesHistorico
        // 
        pnlAcoesHistorico.BackColor = Color.White;
        pnlAcoesHistorico.Controls.Add(btnMarcarPaga);
        pnlAcoesHistorico.Dock = DockStyle.Bottom;
        pnlAcoesHistorico.Location = new Point(3, 338);
        pnlAcoesHistorico.Name = "pnlAcoesHistorico";
        pnlAcoesHistorico.Size = new Size(998, 55);
        pnlAcoesHistorico.TabIndex = 1;
        // 
        // btnMarcarPaga
        // 
        btnMarcarPaga.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnMarcarPaga.Location = new Point(745, 8);
        btnMarcarPaga.Name = "btnMarcarPaga";
        btnMarcarPaga.Size = new Size(240, 40);
        btnMarcarPaga.TabIndex = 0;
        btnMarcarPaga.Text = "Marcar Divida Aberta como Paga";
        btnMarcarPaga.Click += btnMarcarPaga_Click;
        // 
        // GerenciarDividas
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(1004, 651);
        Controls.Add(grpHistorico);
        Controls.Add(grpPendurar);
        Controls.Add(grpCliente);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(900, 550);
        Name = "GerenciarDividas";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Gerenciar Dividas do Cliente - Vendinha do Avo Bob";
        Load += GerenciarDividas_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        grpCliente.ResumeLayout(false);
        grpCliente.PerformLayout();
        grpPendurar.ResumeLayout(false);
        grpPendurar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantidade).EndInit();
        grpHistorico.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDividas).EndInit();
        pnlAcoesHistorico.ResumeLayout(false);
        ResumeLayout(false);
    }
}
