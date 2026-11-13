#nullable disable

namespace Vendinha.Desktop.Screens;

partial class GerenciarPimentas
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private GroupBox grpDados;
    private Label lblNome;
    private TextBox txtNome;
    private Label lblEspecie;
    private TextBox txtEspecie;
    private Label lblScoville;
    private NumericUpDown numScoville;
    private Label lblCusto;
    private NumericUpDown numCusto;
    private Componentes.PrimaryButton btnSalvar;
    private Button btnNovo;
    private Button btnExcluir;
    private GroupBox grpLista;
    private DataGridView dgvPimentas;

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
        grpDados = new GroupBox();
        lblNome = new Label();
        txtNome = new TextBox();
        lblEspecie = new Label();
        txtEspecie = new TextBox();
        lblScoville = new Label();
        numScoville = new NumericUpDown();
        lblCusto = new Label();
        numCusto = new NumericUpDown();
        btnSalvar = new Componentes.PrimaryButton();
        btnNovo = new Button();
        btnExcluir = new Button();
        grpLista = new GroupBox();
        dgvPimentas = new DataGridView();
        pnlHeader.SuspendLayout();
        grpDados.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numScoville).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numCusto).BeginInit();
        grpLista.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPimentas).BeginInit();
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
        pnlHeader.Size = new Size(884, 65);
        pnlHeader.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(16, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(276, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Tipagens de Pimentas";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 9F);
        lblSubtitulo.ForeColor = Color.FromArgb(254, 226, 226);
        lblSubtitulo.Location = new Point(18, 38);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(357, 15);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Cadastre as variedades usadas nos molhos artesanais do Avo Bob";
        // 
        // grpDados
        // 
        grpDados.Controls.Add(btnExcluir);
        grpDados.Controls.Add(btnNovo);
        grpDados.Controls.Add(btnSalvar);
        grpDados.Controls.Add(numCusto);
        grpDados.Controls.Add(lblCusto);
        grpDados.Controls.Add(numScoville);
        grpDados.Controls.Add(lblScoville);
        grpDados.Controls.Add(txtEspecie);
        grpDados.Controls.Add(lblEspecie);
        grpDados.Controls.Add(txtNome);
        grpDados.Controls.Add(lblNome);
        grpDados.Font = new Font("Segoe UI", 9.5F);
        grpDados.Location = new Point(16, 75);
        grpDados.Name = "grpDados";
        grpDados.Size = new Size(320, 460);
        grpDados.TabIndex = 1;
        grpDados.TabStop = false;
        grpDados.Text = "Dados da Pimenta";
        // 
        // lblNome
        // 
        lblNome.AutoSize = true;
        lblNome.Location = new Point(15, 25);
        lblNome.Name = "lblNome";
        lblNome.Size = new Size(119, 17);
        lblNome.TabIndex = 0;
        lblNome.Text = "Nome Popular (*):";
        // 
        // txtNome
        // 
        txtNome.Location = new Point(15, 45);
        txtNome.MaxLength = 100;
        txtNome.Name = "txtNome";
        txtNome.PlaceholderText = "ex: Carolina Reaper";
        txtNome.Size = new Size(285, 24);
        txtNome.TabIndex = 1;
        // 
        // lblEspecie
        // 
        lblEspecie.AutoSize = true;
        lblEspecie.Location = new Point(15, 80);
        lblEspecie.Name = "lblEspecie";
        lblEspecie.Size = new Size(128, 17);
        lblEspecie.TabIndex = 2;
        lblEspecie.Text = "Especie / Origem (*):";
        // 
        // txtEspecie
        // 
        txtEspecie.Location = new Point(15, 100);
        txtEspecie.MaxLength = 150;
        txtEspecie.Name = "txtEspecie";
        txtEspecie.PlaceholderText = "ex: Capsicum chinense";
        txtEspecie.Size = new Size(285, 24);
        txtEspecie.TabIndex = 3;
        // 
        // lblScoville
        // 
        lblScoville.AutoSize = true;
        lblScoville.Location = new Point(15, 135);
        lblScoville.Name = "lblScoville";
        lblScoville.Size = new Size(157, 17);
        lblScoville.TabIndex = 4;
        lblScoville.Text = "Nivel Scoville (SHU) (*):";
        // 
        // numScoville
        // 
        numScoville.Location = new Point(15, 155);
        numScoville.Maximum = new decimal(new int[] { 15000000, 0, 0, 0 });
        numScoville.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numScoville.Name = "numScoville";
        numScoville.Size = new Size(285, 24);
        numScoville.TabIndex = 5;
        numScoville.ThousandsSeparator = true;
        numScoville.Value = new decimal(new int[] { 50000, 0, 0, 0 });
        // 
        // lblCusto
        // 
        lblCusto.AutoSize = true;
        lblCusto.Location = new Point(15, 190);
        lblCusto.Name = "lblCusto";
        lblCusto.Size = new Size(158, 17);
        lblCusto.TabIndex = 6;
        lblCusto.Text = "Custo por Grama (R$) (*):";
        // 
        // numCusto
        // 
        numCusto.DecimalPlaces = 4;
        numCusto.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        numCusto.Location = new Point(15, 210);
        numCusto.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numCusto.Minimum = new decimal(new int[] { 1, 0, 0, 262144 });
        numCusto.Name = "numCusto";
        numCusto.Size = new Size(285, 24);
        numCusto.TabIndex = 7;
        numCusto.Value = new decimal(new int[] { 1500, 0, 0, 262144 });
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(15, 260);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(285, 40);
        btnSalvar.TabIndex = 8;
        btnSalvar.Text = "Salvar Pimenta";
        btnSalvar.Click += btnSalvar_Click;
        // 
        // btnNovo
        // 
        btnNovo.BackColor = Color.FromArgb(243, 244, 246);
        btnNovo.FlatStyle = FlatStyle.Flat;
        btnNovo.Location = new Point(15, 310);
        btnNovo.Name = "btnNovo";
        btnNovo.Size = new Size(135, 35);
        btnNovo.TabIndex = 9;
        btnNovo.Text = "Limpar / Novo";
        btnNovo.UseVisualStyleBackColor = false;
        btnNovo.Click += btnNovo_Click;
        // 
        // btnExcluir
        // 
        btnExcluir.BackColor = Color.FromArgb(254, 242, 242);
        btnExcluir.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
        btnExcluir.FlatStyle = FlatStyle.Flat;
        btnExcluir.ForeColor = Color.FromArgb(185, 28, 28);
        btnExcluir.Location = new Point(165, 310);
        btnExcluir.Name = "btnExcluir";
        btnExcluir.Size = new Size(135, 35);
        btnExcluir.TabIndex = 10;
        btnExcluir.Text = "Remover";
        btnExcluir.UseVisualStyleBackColor = false;
        btnExcluir.Click += btnExcluir_Click;
        // 
        // grpLista
        // 
        grpLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpLista.Controls.Add(dgvPimentas);
        grpLista.Font = new Font("Segoe UI", 9.5F);
        grpLista.Location = new Point(345, 75);
        grpLista.Name = "grpLista";
        grpLista.Size = new Size(525, 460);
        grpLista.TabIndex = 2;
        grpLista.TabStop = false;
        grpLista.Text = "Pimentas Cadastradas";
        // 
        // dgvPimentas
        // 
        dgvPimentas.AllowUserToAddRows = false;
        dgvPimentas.AllowUserToDeleteRows = false;
        dgvPimentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPimentas.BackgroundColor = Color.White;
        dgvPimentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPimentas.Dock = DockStyle.Fill;
        dgvPimentas.Location = new Point(3, 20);
        dgvPimentas.MultiSelect = false;
        dgvPimentas.Name = "dgvPimentas";
        dgvPimentas.ReadOnly = true;
        dgvPimentas.RowHeadersVisible = false;
        dgvPimentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPimentas.Size = new Size(519, 437);
        dgvPimentas.TabIndex = 0;
        dgvPimentas.SelectionChanged += dgvPimentas_SelectionChanged;
        // 
        // GerenciarPimentas
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(884, 551);
        Controls.Add(grpLista);
        Controls.Add(grpDados);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(800, 500);
        Name = "GerenciarPimentas";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Gerenciar Tipagens de Pimentas - Vendinha do Avo Bob";
        Load += GerenciarPimentas_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        grpDados.ResumeLayout(false);
        grpDados.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numScoville).EndInit();
        ((System.ComponentModel.ISupportInitialize)numCusto).EndInit();
        grpLista.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvPimentas).EndInit();
        ResumeLayout(false);
    }
}
