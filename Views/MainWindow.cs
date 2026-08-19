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
            Title = "PrintTrack";
            Icon = new WindowIcon("Assets/avalonia-logo.ico");
            MinHeight = 850;
            MinWidth = 1500;
            Content = new MainView();
        }
    
}