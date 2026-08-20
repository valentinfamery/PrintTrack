using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Markup.Declarative;
using PrintTrack.Views;
using Avalonia.Platform;
using System;

namespace PrintTrack.Views;

public partial class MainWindow : Window
{
    public MainWindow()
        {
            // Configuration de la fenêtre
            Title = "PrintTrack";
            Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://PrintTrack/Assets/avalonia-logo.ico")));
            MinHeight = 850;
            MinWidth = 1500;
            Content = new MainView();
        }
    
}