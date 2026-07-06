using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;

namespace PrintTrack.Views;

public class SettingsView : UserControl
{
    public SettingsView()
    {
        // On définit le contenu de cet onglet ici
        Content = new TextBlock()
            .Text("Contenu 2 - Paramètres")
            .Margin(10);
    }
}