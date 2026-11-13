#nullable disable

namespace Vendinha.Desktop.Componentes;

partial class PrimaryButton
{
    // container de componentes do designer
    private System.ComponentModel.IContainer components = null;

    // libera os recursos gerenciados
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    // inicializacao basica do componente no designer
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
    }
}
