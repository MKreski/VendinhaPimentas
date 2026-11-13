#nullable disable

namespace Vendinha.Desktop.Screens;

partial class ListarClientes
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private Panel pnlFiltro;
    private Label lblBusca;
    private TextBox txtBusca;
    private Button btnBuscar;
    private Button btnLimpar;
    private DataGridView dgvClientes;
    private Panel pnlRodape;
    private Button btnAnterior;
    private Label lblPagina;
    private Button btnProxima;
    private Componentes.PrimaryButton btnGerenciarDividas;
    private Button btnEditar;
    private Button btnNovo;

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
        pnlFiltro = new Panel();
        btnLimpar = new Button();
        btnBuscar = new Button();
        txtBusca = new TextBox();
        lblBusca = new Label();
        dgvClientes = new DataGridView();
        pnlRodape = new Panel();
        btnEditar = new Button();
        btnNovo = new Button();
        btnGerenciarDividas = new Componentes.PrimaryButton();
        btnProxima = new Button();
        lblPagina = new Label();
        btnAnterior = new Button();
        pnlHeader.SuspendLayout();
        pnlFiltro.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
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
        lblTitulo.Size = new Size(204, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Caderneta de Clientes";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 9F);
        lblSubtitulo.ForeColor = Color.FromArgb(254, 226, 226);
        lblSubtitulo.Location = new Point(18, 38);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(411, 15);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Ordenados do maior para o menor devedor com paginacao de 10 em 10 itens";
        // 
        // pnlFiltro
        // 
        pnlFiltro.BackColor = Color.White;
        pnlFiltro.Controls.Add(btnLimpar);
        pnlFiltro.Controls.Add(btnBuscar);
        pnlFiltro.Controls.Add(txtBusca);
        pnlFiltro.Controls.Add(lblBusca);
        pnlFiltro.Dock = DockStyle.Top;
        pnlFiltro.Location = new Point(0, 65);
        pnlFiltro.Name = "pnlFiltro";
        pnlFiltro.Size = new Size(984, 55);
        pnlFiltro.TabIndex = 1;
        // 
        // btnLimpar
        // 
        btnLimpar.BackColor = Color.FromArgb(243, 244, 246);
        btnLimpar.FlatStyle = FlatStyle.Flat;
        btnLimpar.Location = new Point(480, 12);
        btnLimpar.Name = "btnLimpar";
        btnLimpar.Size = new Size(100, 30);
        btnLimpar.TabIndex = 3;
        btnLimpar.Text = "Limpar";
        btnLimpar.UseVisualStyleBackColor = false;
        btnLimpar.Click += btnLimpar_Click;
        // 
        // btnBuscar
        // 
        btnBuscar.BackColor = Color.FromArgb(243, 244, 246);
        btnBuscar.FlatStyle = FlatStyle.Flat;
        btnBuscar.Location = new Point(370, 12);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(100, 30);
        btnBuscar.TabIndex = 2;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = false;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // txtBusca
        // 
        txtBusca.Location = new Point(135, 16);
        txtBusca.Name = "txtBusca";
        txtBusca.PlaceholderText = "digite parte do nome do cliente...";
        txtBusca.Size = new Size(225, 23);
        txtBusca.TabIndex = 1;
        txtBusca.KeyDown += txtBusca_KeyDown;
        // 
        // lblBusca
        // 
        lblBusca.AutoSize = true;
        lblBusca.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBusca.Location = new Point(16, 18);
        lblBusca.Name = "lblBusca";
        lblBusca.Size = new Size(113, 17);
        lblBusca.TabIndex = 0;
        lblBusca.Text = "Filtrar por Nome:";
        // 
        // dgvClientes
        // 
        dgvClientes.AllowUserToAddRows = false;
        dgvClientes.AllowUserToDeleteRows = false;
        dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvClientes.BackgroundColor = Color.White;
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvClientes.Dock = DockStyle.Fill;
        dgvClientes.Location = new Point(0, 120);
        dgvClientes.MultiSelect = false;
        dgvClientes.Name = "dgvClientes";
        dgvClientes.ReadOnly = true;
        dgvClientes.RowHeadersVisible = false;
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClientes.Size = new Size(984, 381);
        dgvClientes.TabIndex = 2;
        dgvClientes.CellDoubleClick += dgvClientes_CellDoubleClick;
        // 
        // pnlRodape
        // 
        pnlRodape.BackColor = Color.White;
        pnlRodape.Controls.Add(btnEditar);
        pnlRodape.Controls.Add(btnNovo);
        pnlRodape.Controls.Add(btnGerenciarDividas);
        pnlRodape.Controls.Add(btnProxima);
        pnlRodape.Controls.Add(lblPagina);
        pnlRodape.Controls.Add(btnAnterior);
        pnlRodape.Dock = DockStyle.Bottom;
        pnlRodape.Location = new Point(0, 501);
        pnlRodape.Name = "pnlRodape";
        pnlRodape.Size = new Size(984, 60);
        pnlRodape.TabIndex = 3;
        // 
        // btnEditar
        // 
        btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEditar.BackColor = Color.FromArgb(243, 244, 246);
        btnEditar.FlatStyle = FlatStyle.Flat;
        btnEditar.Location = new Point(620, 12);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(110, 36);
        btnEditar.TabIndex = 5;
        btnEditar.Text = "Editar Cliente";
        btnEditar.UseVisualStyleBackColor = false;
        btnEditar.Click += btnEditar_Click;
        // 
        // btnNovo
        // 
        btnNovo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNovo.BackColor = Color.FromArgb(243, 244, 246);
        btnNovo.FlatStyle = FlatStyle.Flat;
        btnNovo.Location = new Point(500, 12);
        btnNovo.Name = "btnNovo";
        btnNovo.Size = new Size(110, 36);
        btnNovo.TabIndex = 4;
        btnNovo.Text = "+ Novo Cliente";
        btnNovo.UseVisualStyleBackColor = false;
        btnNovo.Click += btnNovo_Click;
        // 
        // btnGerenciarDividas
        // 
        btnGerenciarDividas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnGerenciarDividas.Location = new Point(740, 10);
        btnGerenciarDividas.Name = "btnGerenciarDividas";
        btnGerenciarDividas.Size = new Size(230, 40);
        btnGerenciarDividas.TabIndex = 3;
        btnGerenciarDividas.Text = "Gerenciar Dividas do Cliente";
        btnGerenciarDividas.Click += btnGerenciarDividas_Click;
        // 
        // btnProxima
        // 
        btnProxima.BackColor = Color.FromArgb(243, 244, 246);
        btnProxima.FlatStyle = FlatStyle.Flat;
        btnProxima.Location = new Point(220, 14);
        btnProxima.Name = "btnProxima";
        btnProxima.Size = new Size(95, 32);
        btnProxima.TabIndex = 2;
        btnProxima.Text = "Proxima ▶";
        btnProxima.UseVisualStyleBackColor = false;
        btnProxima.Click += btnProxima_Click;
        // 
        // lblPagina
        // 
        lblPagina.AutoSize = true;
        lblPagina.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPagina.Location = new Point(130, 21);
        lblPagina.Name = "lblPagina";
        lblPagina.Size = new Size(61, 17);
        lblPagina.TabIndex = 1;
        lblPagina.Text = "Pagina 1";
        // 
        // btnAnterior
        // 
        btnAnterior.BackColor = Color.FromArgb(243, 244, 246);
        btnAnterior.FlatStyle = FlatStyle.Flat;
        btnAnterior.Location = new Point(16, 14);
        btnAnterior.Name = "btnAnterior";
        btnAnterior.Size = new Size(95, 32);
        btnAnterior.TabIndex = 0;
        btnAnterior.Text = "◀ Anterior";
        btnAnterior.UseVisualStyleBackColor = false;
        btnAnterior.Click += btnAnterior_Click;
        // 
        // ListarClientes
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(984, 561);
        Controls.Add(dgvClientes);
        Controls.Add(pnlRodape);
        Controls.Add(pnlFiltro);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(900, 500);
        Name = "ListarClientes";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Caderneta de Clientes - Vendinha do Avo Bob";
        Load += ListarClientes_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlFiltro.ResumeLayout(false);
        pnlFiltro.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
        pnlRodape.ResumeLayout(false);
        pnlRodape.PerformLayout();
        ResumeLayout(false);
    }
}
