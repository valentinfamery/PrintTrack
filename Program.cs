using Avalonia;
using Avalonia.Controls;
using Avalonia.Themes.Fluent;
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
        app.Run(window);
    }
}
