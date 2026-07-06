using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Markup.Declarative;
using PrintTrack.Views;

namespace PrintTrack.Views;

public partial class MainWindow : Window
{
    public MainWindow()
        {
            // Configuration de la fenêtre
            Title = "Test Onglets C#";
            //Width = 400;
            //Height = 300;

        


            this.Content = new TabControl()
    .Margin(10)
    .Items(
        new TabItem { Header = "Printers", Content = new PrintersView() },
        new TabItem { Header = "Settings", Content = new SettingsView() }
    );
        }
    
}