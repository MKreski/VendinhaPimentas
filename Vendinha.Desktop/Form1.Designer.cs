#nullable disable

namespace Vendinha.Desktop;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private Panel pnlConteudo;
    private GroupBox grpCaderneta;
    private Componentes.PrimaryButton btnListarClientes;
    private Componentes.PrimaryButton btnNovoCliente;
    private Componentes.PrimaryButton btnDividas;
    private GroupBox grpProducao;
    private Componentes.PrimaryButton btnPimentas;
    private Componentes.PrimaryButton btnMolhos;
    private Componentes.PrimaryButton btnRelatorio;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatusDb;
    private Button btnSair;

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
        pnlConteudo = new Panel();
        btnSair = new Button();
        grpProducao = new GroupBox();
        btnRelatorio = new Componentes.PrimaryButton();
        btnMolhos = new Componentes.PrimaryButton();
        btnPimentas = new Componentes.PrimaryButton();
        grpCaderneta = new GroupBox();
        btnDividas = new Componentes.PrimaryButton();
        btnNovoCliente = new Componentes.PrimaryButton();
        btnListarClientes = new Componentes.PrimaryButton();
        statusStrip = new StatusStrip();
        lblStatusDb = new ToolStripStatusLabel();
        pnlHeader.SuspendLayout();
        pnlConteudo.SuspendLayout();
        grpProducao.SuspendLayout();
        grpCaderneta.SuspendLayout();
        statusStrip.SuspendLayout();
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
        pnlHeader.Size = new Size(884, 85);
        pnlHeader.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(24, 15);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(504, 30);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Vendinha de Molhos de Pimenta do Avo Bob";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 10F);
        lblSubtitulo.ForeColor = Color.FromArgb(254, 226, 226);
        lblSubtitulo.Location = new Point(26, 48);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(475, 19);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Sistema de Vendas Fiado (Caderneta) & Modulo de Producao Artesanal de Molhos";
        // 
        // pnlConteudo
        // 
        pnlConteudo.Controls.Add(btnSair);
        pnlConteudo.Controls.Add(grpProducao);
        pnlConteudo.Controls.Add(grpCaderneta);
        pnlConteudo.Dock = DockStyle.Fill;
        pnlConteudo.Location = new Point(0, 85);
        pnlConteudo.Name = "pnlConteudo";
        pnlConteudo.Padding = new Padding(24);
        pnlConteudo.Size = new Size(884, 451);
        pnlConteudo.TabIndex = 1;
        // 
        // btnSair
        // 
        btnSair.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSair.BackColor = Color.FromArgb(243, 244, 246);
        btnSair.FlatStyle = FlatStyle.Flat;
        btnSair.Font = new Font("Segoe UI", 9.5F);
        btnSair.Location = new Point(740, 395);
        btnSair.Name = "btnSair";
        btnSair.Size = new Size(120, 35);
        btnSair.TabIndex = 2;
        btnSair.Text = "Encerrar";
        btnSair.UseVisualStyleBackColor = false;
        btnSair.Click += btnSair_Click;
        // 
        // grpProducao
        // 
        grpProducao.Controls.Add(btnRelatorio);
        grpProducao.Controls.Add(btnMolhos);
        grpProducao.Controls.Add(btnPimentas);
        grpProducao.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        grpProducao.ForeColor = Color.FromArgb(31, 41, 55);
        grpProducao.Location = new Point(455, 30);
        grpProducao.Name = "grpProducao";
        grpProducao.Padding = new Padding(16);
        grpProducao.Size = new Size(405, 345);
        grpProducao.TabIndex = 1;
        grpProducao.TabStop = false;
        grpProducao.Text = "Producao Artesanal & Financeiro";
        // 
        // btnRelatorio
        // 
        btnRelatorio.Location = new Point(20, 220);
        btnRelatorio.Name = "btnRelatorio";
        btnRelatorio.Size = new Size(365, 65);
        btnRelatorio.TabIndex = 2;
        btnRelatorio.Text = "Relatorio Financeiro e Custos do Negocio\r\n(Margens, contas a receber e ja recebidas)";
        btnRelatorio.Click += btnRelatorio_Click;
        // 
        // btnMolhos
        // 
        btnMolhos.Location = new Point(20, 130);
        btnMolhos.Name = "btnMolhos";
        btnMolhos.Size = new Size(365, 65);
        btnMolhos.TabIndex = 1;
        btnMolhos.Text = "Catalogo de Molhos do Avo Bob\r\n(Receitas Tradicionais e Reservas em Barril)";
        btnMolhos.Click += btnMolhos_Click;
        // 
        // btnPimentas
        // 
        btnPimentas.Location = new Point(20, 40);
        btnPimentas.Name = "btnPimentas";
        btnPimentas.Size = new Size(365, 65);
        btnPimentas.TabIndex = 0;
        btnPimentas.Text = "Gerenciar Tipagens de Pimentas\r\n(Variedades, Escala Scoville SHU e Custo/g)";
        btnPimentas.Click += btnPimentas_Click;
        // 
        // grpCaderneta
        // 
        grpCaderneta.Controls.Add(btnDividas);
        grpCaderneta.Controls.Add(btnNovoCliente);
        grpCaderneta.Controls.Add(btnListarClientes);
        grpCaderneta.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        grpCaderneta.ForeColor = Color.FromArgb(31, 41, 55);
        grpCaderneta.Location = new Point(24, 30);
        grpCaderneta.Name = "grpCaderneta";
        grpCaderneta.Padding = new Padding(16);
        grpCaderneta.Size = new Size(405, 345);
        grpCaderneta.TabIndex = 0;
        grpCaderneta.TabStop = false;
        grpCaderneta.Text = "Caderneta de Clientes (Fiado)";
        // 
        // btnDividas
        // 
        btnDividas.Location = new Point(20, 220);
        btnDividas.Name = "btnDividas";
        btnDividas.Size = new Size(365, 65);
        btnDividas.TabIndex = 2;
        btnDividas.Text = "Pendurar Compra ou Acertar Contas\r\n(Escolher molho do catalogo e quitar dividas)";
        btnDividas.Click += btnDividas_Click;
        // 
        // btnNovoCliente
        // 
        btnNovoCliente.Location = new Point(20, 130);
        btnNovoCliente.Name = "btnNovoCliente";
        btnNovoCliente.Size = new Size(365, 65);
        btnNovoCliente.TabIndex = 1;
        btnNovoCliente.Text = "Cadastrar Novo Fregues\r\n(Nome, CPF validado, nascimento e e-mail)";
        btnNovoCliente.Click += btnNovoCliente_Click;
        // 
        // btnListarClientes
        // 
        btnListarClientes.Location = new Point(20, 40);
        btnListarClientes.Name = "btnListarClientes";
        btnListarClientes.Size = new Size(365, 65);
        btnListarClientes.TabIndex = 0;
        btnListarClientes.Text = "Listar Clientes e Caderneta\r\n(Ordenados do maior devedor ao menor)";
        btnListarClientes.Click += btnListarClientes_Click;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { lblStatusDb });
        statusStrip.Location = new Point(0, 536);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(884, 25);
        statusStrip.TabIndex = 2;
        // 
        // lblStatusDb
        // 
        lblStatusDb.Font = new Font("Segoe UI", 9F);
        lblStatusDb.ForeColor = Color.FromArgb(75, 85, 99);
        lblStatusDb.Name = "lblStatusDb";
        lblStatusDb.Size = new Size(130, 20);
        lblStatusDb.Text = "Banco de Dados: Ativo";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        ClientSize = new Size(884, 561);
        Controls.Add(pnlConteudo);
        Controls.Add(statusStrip);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(850, 520);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Vendinha de Molhos de Pimenta do Avo Bob - Painel Principal";
        Load += Form1_Load;
        FormClosed += Form1_FormClosed;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlConteudo.ResumeLayout(false);
        grpProducao.ResumeLayout(false);
        grpCaderneta.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
