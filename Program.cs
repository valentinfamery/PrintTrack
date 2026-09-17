using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Themes.Fluent;
using DocumentFormat.OpenXml.Drawing.Charts;
using PrintTrack.ViewModels;
using PrintTrack.Views;
using System;

namespace PrintTrack;

class Program
{
    public static void Main(string[] args)
    {
        AppBuilder.Configure<Application>()
                  .UsePlatformDetect()
                  .Start(AppMain, args);
    }

    static void AppMain(Application app, string[] args)
    {
        // Apply the Fluent theme so controls look polished
        app.Styles.Add(new FluentTheme());

        var window = new MainWindow();



        window.Show();

        window.Bind(Window.HeightProperty,new Binding(nameof(MainViewModel.WindowHeight)));
        window.Bind(Window.WidthProperty,new Binding(nameof(MainViewModel.WindowWidth)));
        app.Run(window);
    }
}
