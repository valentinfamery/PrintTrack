using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Markup.Declarative;

namespace PrintTrack.Views;
public class Printer
{
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";
    public string IpAddress { get; set; } = "";
}


public class PrintersView : UserControl
{
    private readonly StackPanel _detailPanel;

    public PrintersView()
    {
        var printers = new List<Printer>
        {
            new() { Name = "Imprimante Bureau A", Status = "En ligne", IpAddress = "192.168.1.50" },
            new() { Name = "Imprimante Étage 1", Status = "Hors ligne", IpAddress = "192.168.1.51" },
            new() { Name = "Traceur Grand Format", Status = "Toner Bas", IpAddress = "192.168.1.52" }
        };

        // 2. Grille principale (Gauche: colonne fixe, Droite: tout le reste)
        var mainGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("220, *")
        };

        // --- COLONNE DE GAUCHE (Menu) ---
        var menuPanel = new StackPanel 
        { 
            Spacing = 5, 
            Margin = new Thickness(10),
            Background = Brushes.GhostWhite
        };

        // --- COLONNE DE DROITE (Détails) ---
        _detailPanel = new StackPanel 
        { 
            Spacing = 15, 
            Margin = new Thickness(25) 
        };

        // 3. Génération des boutons dynamiquement
        foreach (var printer in printers)
        {
            var btn = new Button
            {
                Content = printer.Name,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Padding = new Thickness(10, 8),
                // Astuce : On stocke l'objet printer directement dans le bouton pour le retrouver au clic
                Tag = printer 
            };

            // L'événement au clic
            btn.Click += (sender, e) =>
            {
                if (sender is Button clickedButton && clickedButton.Tag is Printer selectedPrinter)
                {
                    AfficherDetails(selectedPrinter);
                }
            };

            menuPanel.Children.Add(btn);
        }

        // Positionnement dans la grille
        Grid.SetColumn(menuPanel, 0);
        Grid.SetColumn(_detailPanel, 1);

        mainGrid.Children.Add(menuPanel);
        mainGrid.Children.Add(_detailPanel);

        // Afficher la première imprimante par défaut au démarrage
        if (printers.Count > 0)
        {
            AfficherDetails(printers[0]);
        }

        this.Content = mainGrid;
    }


      

        private void AfficherDetails(Printer printer)
    {
        // On nettoie l'ancien écran de détail
        _detailPanel.Children.Clear();

        // On recrée les éléments textuels avec les données de l'imprimante cliquée
        var titleLabel = new TextBlock { FontSize = 22, FontWeight = FontWeight.Bold, Text = printer.Name };
        var ipLabel = new TextBlock { FontSize = 14, Text = $"Adresse IP : {printer.IpAddress}" };
        var statusLabel = new TextBlock { FontSize = 14, Text = $"Statut de l'appareil : {printer.Status}" };

        // On les ajoute au panneau de droite
        _detailPanel.Children.Add(titleLabel);
        _detailPanel.Children.Add(ipLabel);
        _detailPanel.Children.Add(statusLabel);
    }
    
}