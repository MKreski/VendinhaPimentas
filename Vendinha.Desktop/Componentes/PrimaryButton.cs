namespace Vendinha.Desktop.Componentes;

// botao customizado no estilo vermelho pimenta da vendinha do avo bob
public partial class PrimaryButton : Button
{
    public PrimaryButton()
    {
        InitializeComponent();
        AplicarTemaPimenta();
    }

    // aplica as cores, fontes e bordas limpas do tema artesanal
    private void AplicarTemaPimenta()
    {
        BackColor = Color.FromArgb(185, 28, 28);
        ForeColor = Color.White;
        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);
        FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 27, 27);
        Cursor = Cursors.Hand;
        Height = 40;
        UseVisualStyleBackColor = false;
    }
}
