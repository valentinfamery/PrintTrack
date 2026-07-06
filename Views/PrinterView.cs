using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;

namespace PrintTrack.Views;

public class PrintersView : UserControl
{
    public PrintersView()
    {
        // On définit le contenu de cet onglet ici
        Content = new TextBlock()
            .Text("Contenu 1 - Liste des imprimantes")
            .Margin(10);
    }
}