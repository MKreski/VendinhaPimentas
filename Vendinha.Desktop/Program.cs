namespace Vendinha.Desktop;

internal static class Program
{
    // ponto de entrada principal do aplicativo windows forms da vendinha
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
