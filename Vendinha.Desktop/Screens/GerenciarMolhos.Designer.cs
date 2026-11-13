#nullable disable

namespace Vendinha.Desktop.Screens;

partial class GerenciarMolhos
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private GroupBox grpReceita;
    private Label lblNome;
    private TextBox txtNome;
    private Label lblPimenta;
    private ComboBox cmbPimenta;
    private Label lblGramas;
    private NumericUpDown numGramas;
    private Label lblEmbalagem;
    private NumericUpDown numEmbalagem;
    private Label lblPrecoVenda;
    private NumericUpDown numPrecoVenda;
    private CheckBox chkReserva;
    private Panel pnlReserva;
    private Label lblDiasMaturacao;
    private NumericUpDown numDiasMaturacao;
    private Label lblCustoBarril;
    private NumericUpDown numCustoBarril;
    private Panel pnlPreview;
    private Label lblPreviewCusto;
    private Label lblPreviewLucro;
    private Componentes.PrimaryButton btnSalvar;
    private Button btnLimpar;
    private GroupBox grpCatalogo;
    private DataGridView dgvMolhos;

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
        grpReceita = new GroupBox();
        btnLimpar = new Button();
        btnSalvar = new Componentes.PrimaryButton();
        pnlPreview = new Panel();
        lblPreviewLucro = new Label();
        lblPreviewCusto = new Label();
        pnlReserva = new Panel();
        numCustoBarril = new NumericUpDown();
        lblCustoBarril = new Label();
        numDiasMaturacao = new NumericUpDown();
        lblDiasMaturacao = new Label();
        chkReserva = new CheckBox();
        numPrecoVenda = new NumericUpDown();
        lblPrecoVenda = new Label();
        numEmbalagem = new NumericUpDown();
        lblEmbalagem = new Label();
        numGramas = new NumericUpDown();
        lblGramas = new Label();
        cmbPimenta = new ComboBox();
        lblPimenta = new Label();
        txtNome = new TextBox();
        lblNome = new Label();
        grpCatalogo = new GroupBox();
        dgvMolhos = new DataGridView();
        pnlHeader.SuspendLayout();
        grpReceita.SuspendLayout();
        pnlPreview.SuspendLayout();
        pnlReserva.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numCustoBarril).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numDiasMaturacao).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numPrecoVenda).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numEmbalagem).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numGramas).BeginInit();
        grpCatalogo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMolhos).BeginInit();
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
        lblTitulo.Size = new Size(331, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Catalogo de Molhos do Avo Bob";
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
        lblSubtitulo.Text = "Crie receitas tradicionais ou reservas especiais com calculo automatico de custos";
        // 
        // grpReceita
        // 
        grpReceita.Controls.Add(btnLimpar);
        grpReceita.Controls.Add(btnSalvar);
        grpReceita.Controls.Add(pnlPreview);
        grpReceita.Controls.Add(pnlReserva);
        grpReceita.Controls.Add(chkReserva);
        grpReceita.Controls.Add(numPrecoVenda);
        grpReceita.Controls.Add(lblPrecoVenda);
        grpReceita.Controls.Add(numEmbalagem);
        grpReceita.Controls.Add(lblEmbalagem);
        grpReceita.Controls.Add(numGramas);
        grpReceita.Controls.Add(lblGramas);
        grpReceita.Controls.Add(cmbPimenta);
        grpReceita.Controls.Add(lblPimenta);
        grpReceita.Controls.Add(txtNome);
        grpReceita.Controls.Add(lblNome);
        grpReceita.Font = new Font("Segoe UI", 9.5F);
        grpReceita.Location = new Point(16, 75);
        grpReceita.Name = "grpReceita";
        grpReceita.Size = new Size(360, 560);
        grpReceita.TabIndex = 1;
        grpReceita.TabStop = false;
        grpReceita.Text = "Nova Receita de Molho";
        // 
        // btnLimpar
        // 
        btnLimpar.BackColor = Color.FromArgb(243, 244, 246);
        btnLimpar.FlatStyle = FlatStyle.Flat;
        btnLimpar.Location = new Point(15, 515);
        btnLimpar.Name = "btnLimpar";
        btnLimpar.Size = new Size(330, 32);
        btnLimpar.TabIndex = 14;
        btnLimpar.Text = "Limpar Campos";
        btnLimpar.UseVisualStyleBackColor = false;
        btnLimpar.Click += btnLimpar_Click;
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(15, 470);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(330, 40);
        btnSalvar.TabIndex = 13;
        btnSalvar.Text = "Criar Receita de Molho";
        btnSalvar.Click += btnSalvar_Click;
        // 
        // pnlPreview
        // 
        pnlPreview.BackColor = Color.FromArgb(254, 242, 242);
        pnlPreview.BorderStyle = BorderStyle.FixedSingle;
        pnlPreview.Controls.Add(lblPreviewLucro);
        pnlPreview.Controls.Add(lblPreviewCusto);
        pnlPreview.Location = new Point(15, 395);
        pnlPreview.Name = "pnlPreview";
        pnlPreview.Size = new Size(330, 65);
        pnlPreview.TabIndex = 12;
        // 
        // lblPreviewLucro
        // 
        lblPreviewLucro.AutoSize = true;
        lblPreviewLucro.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPreviewLucro.ForeColor = Color.FromArgb(22, 101, 52);
        lblPreviewLucro.Location = new Point(8, 35);
        lblPreviewLucro.Name = "lblPreviewLucro";
        lblPreviewLucro.Size = new Size(153, 15);
        lblPreviewLucro.TabIndex = 1;
        lblPreviewLucro.Text = "Lucro / Frasco: R$ 0,00 (0%)";
        // 
        // lblPreviewCusto
        // 
        lblPreviewCusto.AutoSize = true;
        lblPreviewCusto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPreviewCusto.ForeColor = Color.FromArgb(185, 28, 28);
        lblPreviewCusto.Location = new Point(8, 10);
        lblPreviewCusto.Name = "lblPreviewCusto";
        lblPreviewCusto.Size = new Size(151, 15);
        lblPreviewCusto.TabIndex = 0;
        lblPreviewCusto.Text = "Custo Total: R$ 0,00 / frasco";
        // 
        // pnlReserva
        // 
        pnlReserva.BackColor = Color.FromArgb(249, 250, 251);
        pnlReserva.BorderStyle = BorderStyle.FixedSingle;
        pnlReserva.Controls.Add(numCustoBarril);
        pnlReserva.Controls.Add(lblCustoBarril);
        pnlReserva.Controls.Add(numDiasMaturacao);
        pnlReserva.Controls.Add(lblDiasMaturacao);
        pnlReserva.Enabled = false;
        pnlReserva.Location = new Point(15, 310);
        pnlReserva.Name = "pnlReserva";
        pnlReserva.Size = new Size(330, 75);
        pnlReserva.TabIndex = 11;
        // 
        // numCustoBarril
        // 
        numCustoBarril.DecimalPlaces = 2;
        numCustoBarril.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        numCustoBarril.Location = new Point(170, 35);
        numCustoBarril.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numCustoBarril.Name = "numCustoBarril";
        numCustoBarril.Size = new Size(145, 24);
        numCustoBarril.TabIndex = 3;
        numCustoBarril.Value = new decimal(new int[] { 35, 0, 0, 65536 });
        numCustoBarril.ValueChanged += CalcularPreview;
        // 
        // lblCustoBarril
        // 
        lblCustoBarril.AutoSize = true;
        lblCustoBarril.Font = new Font("Segoe UI", 8.5F);
        lblCustoBarril.Location = new Point(170, 15);
        lblCustoBarril.Name = "lblCustoBarril";
        lblCustoBarril.Size = new Size(116, 15);
        lblCustoBarril.TabIndex = 2;
        lblCustoBarril.Text = "Custo do Barril (R$):";
        // 
        // numDiasMaturacao
        // 
        numDiasMaturacao.Location = new Point(10, 35);
        numDiasMaturacao.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        numDiasMaturacao.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numDiasMaturacao.Name = "numDiasMaturacao";
        numDiasMaturacao.Size = new Size(145, 24);
        numDiasMaturacao.TabIndex = 1;
        numDiasMaturacao.Value = new decimal(new int[] { 90, 0, 0, 0 });
        // 
        // lblDiasMaturacao
        // 
        lblDiasMaturacao.AutoSize = true;
        lblDiasMaturacao.Font = new Font("Segoe UI", 8.5F);
        lblDiasMaturacao.Location = new Point(10, 15);
        lblDiasMaturacao.Name = "lblDiasMaturacao";
        lblDiasMaturacao.Size = new Size(92, 15);
        lblDiasMaturacao.TabIndex = 0;
        lblDiasMaturacao.Text = "Maturacao (dias):";
        // 
        // chkReserva
        // 
        chkReserva.AutoSize = true;
        chkReserva.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        chkReserva.ForeColor = Color.FromArgb(146, 64, 14);
        chkReserva.Location = new Point(15, 285);
        chkReserva.Name = "chkReserva";
        chkReserva.Size = new Size(302, 19);
        chkReserva.TabIndex = 10;
        chkReserva.Text = "Molho Especial Reserva (envelhecido em barril)";
        chkReserva.UseVisualStyleBackColor = true;
        chkReserva.CheckedChanged += chkReserva_CheckedChanged;
        // 
        // numPrecoVenda
        // 
        numPrecoVenda.DecimalPlaces = 2;
        numPrecoVenda.Increment = new decimal(new int[] { 1, 0, 0, 0 });
        numPrecoVenda.Location = new Point(15, 250);
        numPrecoVenda.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numPrecoVenda.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numPrecoVenda.Name = "numPrecoVenda";
        numPrecoVenda.Size = new Size(330, 24);
        numPrecoVenda.TabIndex = 9;
        numPrecoVenda.Value = new decimal(new int[] { 2200, 0, 0, 131072 });
        numPrecoVenda.ValueChanged += CalcularPreview;
        // 
        // lblPrecoVenda
        // 
        lblPrecoVenda.AutoSize = true;
        lblPrecoVenda.Location = new Point(15, 230);
        lblPrecoVenda.Name = "lblPrecoVenda";
        lblPrecoVenda.Size = new Size(182, 17);
        lblPrecoVenda.TabIndex = 8;
        lblPrecoVenda.Text = "Preco de Venda no Balcao (*):";
        // 
        // numEmbalagem
        // 
        numEmbalagem.DecimalPlaces = 2;
        numEmbalagem.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        numEmbalagem.Location = new Point(15, 200);
        numEmbalagem.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numEmbalagem.Name = "numEmbalagem";
        numEmbalagem.Size = new Size(330, 24);
        numEmbalagem.TabIndex = 7;
        numEmbalagem.Value = new decimal(new int[] { 45, 0, 0, 65536 });
        numEmbalagem.ValueChanged += CalcularPreview;
        // 
        // lblEmbalagem
        // 
        lblEmbalagem.AutoSize = true;
        lblEmbalagem.Location = new Point(15, 180);
        lblEmbalagem.Name = "lblEmbalagem";
        lblEmbalagem.Size = new Size(254, 17);
        lblEmbalagem.TabIndex = 6;
        lblEmbalagem.Text = "Custo Embalagem / Outros Ingredientes (*):";
        // 
        // numGramas
        // 
        numGramas.DecimalPlaces = 2;
        numGramas.Increment = new decimal(new int[] { 1, 0, 0, 0 });
        numGramas.Location = new Point(15, 150);
        numGramas.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numGramas.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numGramas.Name = "numGramas";
        numGramas.Size = new Size(330, 24);
        numGramas.TabIndex = 5;
        numGramas.Value = new decimal(new int[] { 25, 0, 0, 0 });
        numGramas.ValueChanged += CalcularPreview;
        // 
        // lblGramas
        // 
        lblGramas.AutoSize = true;
        lblGramas.Location = new Point(15, 130);
        lblGramas.Name = "lblGramas";
        lblGramas.Size = new Size(211, 17);
        lblGramas.TabIndex = 4;
        lblGramas.Text = "Gramas de Pimenta por Frasco (*):";
        // 
        // cmbPimenta
        // 
        cmbPimenta.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPimenta.FormattingEnabled = true;
        cmbPimenta.Location = new Point(15, 95);
        cmbPimenta.Name = "cmbPimenta";
        cmbPimenta.Size = new Size(330, 25);
        cmbPimenta.TabIndex = 3;
        cmbPimenta.SelectedIndexChanged += cmbPimenta_SelectedIndexChanged;
        // 
        // lblPimenta
        // 
        lblPimenta.AutoSize = true;
        lblPimenta.Location = new Point(15, 75);
        lblPimenta.Name = "lblPimenta";
        lblPimenta.Size = new Size(160, 17);
        lblPimenta.TabIndex = 2;
        lblPimenta.Text = "Pimenta Base da Receita (*):";
        // 
        // txtNome
        // 
        txtNome.Location = new Point(15, 45);
        txtNome.MaxLength = 150;
        txtNome.Name = "txtNome";
        txtNome.PlaceholderText = "ex: Molho Defumado do Vo Bob";
        txtNome.Size = new Size(330, 24);
        txtNome.TabIndex = 1;
        // 
        // lblNome
        // 
        lblNome.AutoSize = true;
        lblNome.Location = new Point(15, 25);
        lblNome.Name = "lblNome";
        lblNome.Size = new Size(116, 17);
        lblNome.TabIndex = 0;
        lblNome.Text = "Nome do Molho (*):";
        // 
        // grpCatalogo
        // 
        grpCatalogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpCatalogo.Controls.Add(dgvMolhos);
        grpCatalogo.Font = new Font("Segoe UI", 9.5F);
        grpCatalogo.Location = new Point(385, 75);
        grpCatalogo.Name = "grpCatalogo";
        grpCatalogo.Size = new Size(605, 560);
        grpCatalogo.TabIndex = 2;
        grpCatalogo.TabStop = false;
        grpCatalogo.Text = "Catalogo de Molhos Disponiveis";
        // 
        // dgvMolhos
        // 
        dgvMolhos.AllowUserToAddRows = false;
        dgvMolhos.AllowUserToDeleteRows = false;
        dgvMolhos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMolhos.BackgroundColor = Color.White;
        dgvMolhos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvMolhos.Dock = DockStyle.Fill;
        dgvMolhos.Location = new Point(3, 20);
        dgvMolhos.MultiSelect = false;
        dgvMolhos.Name = "dgvMolhos";
        dgvMolhos.ReadOnly = true;
        dgvMolhos.RowHeadersVisible = false;
        dgvMolhos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMolhos.Size = new Size(599, 537);
        dgvMolhos.TabIndex = 0;
        // 
        // GerenciarMolhos
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(1004, 651);
        Controls.Add(grpCatalogo);
        Controls.Add(grpReceita);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(950, 600);
        Name = "GerenciarMolhos";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Catalogo de Molhos - Vendinha do Avo Bob";
        Load += GerenciarMolhos_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        grpReceita.ResumeLayout(false);
        grpReceita.PerformLayout();
        pnlPreview.ResumeLayout(false);
        pnlPreview.PerformLayout();
        pnlReserva.ResumeLayout(false);
        pnlReserva.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numCustoBarril).EndInit();
        ((System.ComponentModel.ISupportInitialize)numDiasMaturacao).EndInit();
        ((System.ComponentModel.ISupportInitialize)numPrecoVenda).EndInit();
        ((System.ComponentModel.ISupportInitialize)numEmbalagem).EndInit();
        ((System.ComponentModel.ISupportInitialize)numGramas).EndInit();
        grpCatalogo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvMolhos).EndInit();
        ResumeLayout(false);
    }
}
