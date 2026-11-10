using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using PrintTrack.Views;
using Avalonia.Platform;
using System;
using PrintTrack.ViewModels;
using Avalonia.Data;

namespace PrintTrack.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        Title = "PrintTrack";
        Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://PrintTrack/Assets/avalonia-logo.ico")));
        MinHeight = 850;
        MinWidth = 1500;
        WindowState = WindowState.Maximized;
        Content = new MainView();
    }

}
