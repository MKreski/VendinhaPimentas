#nullable disable

namespace Vendinha.Desktop.Screens;

partial class CadastrarCliente
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlHeader;
    private Label lblTitulo;
    private Label lblSubtitulo;
    private GroupBox grpDados;
    private Label lblNome;
    private TextBox txtNome;
    private Label lblCpf;
    private TextBox txtCpf;
    private Label lblNascimento;
    private DateTimePicker dtpNascimento;
    private Label lblEmail;
    private TextBox txtEmail;
    private Componentes.PrimaryButton btnSalvar;
    private Button btnCancelar;

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
        btnCancelar = new Button();
        btnSalvar = new Componentes.PrimaryButton();
        txtEmail = new TextBox();
        lblEmail = new Label();
        dtpNascimento = new DateTimePicker();
        lblNascimento = new Label();
        txtCpf = new TextBox();
        lblCpf = new Label();
        txtNome = new TextBox();
        lblNome = new Label();
        pnlHeader.SuspendLayout();
        grpDados.SuspendLayout();
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
        pnlHeader.Size = new Size(484, 65);
        pnlHeader.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(16, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(207, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Cadastro de Clientes";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.AutoSize = true;
        lblSubtitulo.Font = new Font("Segoe UI", 9F);
        lblSubtitulo.ForeColor = Color.FromArgb(254, 226, 226);
        lblSubtitulo.Location = new Point(18, 38);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(335, 15);
        lblSubtitulo.TabIndex = 1;
        lblSubtitulo.Text = "Controle de fregueses da caderneta de fiado do Avo Bob";
        // 
        // grpDados
        // 
        grpDados.Controls.Add(btnCancelar);
        grpDados.Controls.Add(btnSalvar);
        grpDados.Controls.Add(txtEmail);
        grpDados.Controls.Add(lblEmail);
        grpDados.Controls.Add(dtpNascimento);
        grpDados.Controls.Add(lblNascimento);
        grpDados.Controls.Add(txtCpf);
        grpDados.Controls.Add(lblCpf);
        grpDados.Controls.Add(txtNome);
        grpDados.Controls.Add(lblNome);
        grpDados.Font = new Font("Segoe UI", 9.5F);
        grpDados.Location = new Point(16, 75);
        grpDados.Name = "grpDados";
        grpDados.Size = new Size(452, 335);
        grpDados.TabIndex = 1;
        grpDados.TabStop = false;
        grpDados.Text = "Informacoes Pessoais";
        // 
        // btnCancelar
        // 
        btnCancelar.BackColor = Color.FromArgb(243, 244, 246);
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.FlatStyle = FlatStyle.Flat;
        btnCancelar.Location = new Point(230, 275);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(200, 40);
        btnCancelar.TabIndex = 9;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = false;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(20, 275);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(200, 40);
        btnSalvar.TabIndex = 8;
        btnSalvar.Text = "Salvar Cliente";
        btnSalvar.Click += btnSalvar_Click;
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(20, 225);
        txtEmail.MaxLength = 150;
        txtEmail.Name = "txtEmail";
        txtEmail.PlaceholderText = "ex: cliente@email.com (opcional)";
        txtEmail.Size = new Size(410, 24);
        txtEmail.TabIndex = 7;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(20, 205);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(111, 17);
        lblEmail.TabIndex = 6;
        lblEmail.Text = "E-mail (opcional):";
        // 
        // dtpNascimento
        // 
        dtpNascimento.CustomFormat = "dd/MM/yyyy";
        dtpNascimento.Format = DateTimePickerFormat.Custom;
        dtpNascimento.Location = new Point(20, 165);
        dtpNascimento.Name = "dtpNascimento";
        dtpNascimento.Size = new Size(410, 24);
        dtpNascimento.TabIndex = 5;
        // 
        // lblNascimento
        // 
        lblNascimento.AutoSize = true;
        lblNascimento.Location = new Point(20, 145);
        lblNascimento.Name = "lblNascimento";
        lblNascimento.Size = new Size(155, 17);
        lblNascimento.TabIndex = 4;
        lblNascimento.Text = "Data de Nascimento (*):";
        // 
        // txtCpf
        // 
        txtCpf.Location = new Point(20, 105);
        txtCpf.MaxLength = 14;
        txtCpf.Name = "txtCpf";
        txtCpf.PlaceholderText = "000.000.000-00 ou apenas digitos";
        txtCpf.Size = new Size(410, 24);
        txtCpf.TabIndex = 3;
        // 
        // lblCpf
        // 
        lblCpf.AutoSize = true;
        lblCpf.Location = new Point(20, 85);
        lblCpf.Name = "lblCpf";
        lblCpf.Size = new Size(174, 17);
        lblCpf.TabIndex = 2;
        lblCpf.Text = "CPF (validado e unico) (*):";
        // 
        // txtNome
        // 
        txtNome.Location = new Point(20, 45);
        txtNome.MaxLength = 150;
        txtNome.Name = "txtNome";
        txtNome.PlaceholderText = "ex: Maria Aparecida da Silva";
        txtNome.Size = new Size(410, 24);
        txtNome.TabIndex = 1;
        // 
        // lblNome
        // 
        lblNome.AutoSize = true;
        lblNome.Location = new Point(20, 25);
        lblNome.Name = "lblNome";
        lblNome.Size = new Size(130, 17);
        lblNome.TabIndex = 0;
        lblNome.Text = "Nome Completo (*):";
        // 
        // CadastrarCliente
        // 
        AcceptButton = btnSalvar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(250, 250, 250);
        CancelButton = btnCancelar;
        ClientSize = new Size(484, 426);
        Controls.Add(grpDados);
        Controls.Add(pnlHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CadastrarCliente";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cadastrar Cliente - Vendinha do Avo Bob";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        grpDados.ResumeLayout(false);
        grpDados.PerformLayout();
        ResumeLayout(false);
    }
}
