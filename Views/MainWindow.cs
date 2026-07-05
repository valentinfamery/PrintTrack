using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Markup.Declarative;

namespace PrintTrack.Views;

public partial class MainWindow : Window
{
    public MainWindow()
        {
            // Configuration de la fenêtre
            Title = "Test Onglets C#";
            Width = 400;
            Height = 300;

            // 1. Le contrôle d'onglets
            var tabControl = new TabControl { Margin = new Thickness(10) };

            // 2. Onglet 1
            var tab1 = new TabItem 
            { 
                Header = "Onglet 1", 
                Content = new TextBlock { Text = "Contenu 1", Margin = new Thickness(10) } 
            };

            // 3. Onglet 2
            var tab2 = new TabItem 
            { 
                Header = "Onglet 2", 
                Content = new TextBlock { Text = "Contenu 2", Margin = new Thickness(10) } 
            };

            // Assemblage
            tabControl.Items.Add(tab1);
            tabControl.Items.Add(tab2);

            // On injecte le tout directement dans la fenêtre
            this.Content = tabControl;
        }
    
}