using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using PrintTrack.Models;
using PrintTrack.ViewModels;

namespace PrintTrack.Views;

public class MainView : UserControl
{
    private readonly MainViewModel? mainViewModel = MainViewModel.Instance;
    private readonly StackPanel _stackPanel;
    private readonly StackPanel _buttonPanel;
    private readonly Button _buttonChargerCMDB;
    private readonly Button _refreshAll;
    private readonly TableView _tableView;
    private readonly TextBlock _infosPrintersChargedCMDB;
    private readonly ComboBox _comboBox;

    public MainView()
    {
        DataContext = mainViewModel;
        SettingsService.Load();

        var s = SettingsService.Load();

        _infosPrintersChargedCMDB = new TextBlock();

        _infosPrintersChargedCMDB.Bind(TextBlock.TextProperty, new Binding(nameof(MainViewModel.InfoList)));

        _stackPanel = new StackPanel()
        {
            Spacing = 5,
            //Background = Brushes.GhostWhite,
            Margin = new Thickness(10),
        };

        _buttonPanel = new StackPanel()
        {
            Spacing = 5,
            Orientation = Orientation.Horizontal,
        };



        _buttonChargerCMDB = new Button()
        {
            Content = "ChargerCMDB",
        };

        _buttonChargerCMDB.Click += LoadCMDBOnClick!;

        _refreshAll = new Button()
        {
            Content = "Rafraîchir tout"
        };

        _refreshAll.Click += RefreshAllOnClick!;

        _comboBox = new ComboBox();

        _comboBox.Bind(ItemsControl.ItemsSourceProperty, new Binding(nameof(MainViewModel.GrandSites)));

        // Lier l'élément sélectionné (SelectedItem -> SelectedOption)
        _comboBox.Bind(SelectingItemsControl.SelectedItemProperty, new Binding(nameof(MainViewModel.Filter)));





        _tableView = new TableView()
        {
            SelectionMode = SelectionMode.Single,
            CanUserResizeColumns = false
        };


        _tableView.Bind(ItemsControl.ItemsSourceProperty, new Binding(nameof(MainViewModel.FilteredPrinters)));




        var cellTemplate = new FuncDataTemplate<Printer>((item, namescope) =>
{
    // Container principal : rangée horizontale de 4 cubes
    var row = new StackPanel
    {
        Orientation = Orientation.Horizontal,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Width = 200,
        Spacing = 7,
    };

    // Couleurs demandées pour les 4 cubes
    var colors = new[] { Colors.Black, Colors.Cyan, Colors.Magenta, Colors.Yellow };

    if (item.Consommables?.Any() == true)
        // Exemple : supposons que `item.Pourcentages` soit un tableau/liste de 4 double (ex: [50, 20, 80, 10])
        // Tu peux adapter la source de données selon tes besoins.
        for (int i = 0; i < 4; i++)
        {

            Avalonia.Media.Color couleurFond = colors[i];

            // Un cube (Border) contenant le texte du pourcentage
            var cube = new Border
            {
                Width = 40,
                Height = 20,
                Background = new SolidColorBrush(couleurFond),
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4),
                Child = new TextBlock
                {
                    Text = item.Consommables[i].Percentage.ToString() + "%",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 11,
                    FontWeight = FontWeight.Bold,
                    // Inverse la couleur du texte si le fond est noir pour qu'il reste lisible
                    Foreground = couleurFond == Colors.Black ? Brushes.White : Brushes.Black
                }
            };


            row.Children.Add(cube);
        }



    return row;
});








        _tableView.Columns.Clear();
        _tableView.Columns.AddRange([
          new TableViewColumn { Header = "Site", Binding = new Binding(nameof(Printer.Site)), Width = new GridLength(200, GridUnitType.Pixel) },
          new TableViewColumn { Header = "AdresseIP", Binding = new Binding(nameof(Printer.AdresseIP)), Width = new GridLength(150, GridUnitType.Pixel) },
          new TableViewColumn { Header = "Numeroetiquette", Binding = new Binding(nameof(Printer.Numeroetiquette)), Width = new GridLength(100, GridUnitType.Pixel) },
          new TableViewColumn { Header = "Statut", Binding = new Binding(nameof(Printer.Status)), Width = new GridLength(100, GridUnitType.Pixel) },
          new TableViewColumn { Header = "Toner", CellTemplate = cellTemplate, Width = new GridLength(200, GridUnitType.Pixel) },
          new TableViewColumn { Header = "Modele", Binding = new Binding(nameof(Printer.Modele)), Width = new GridLength(100, GridUnitType.Pixel) },
          new TableViewColumn { Header = "Numserie", Binding = new Binding(nameof(Printer.Numserie)), Width = new GridLength(150, GridUnitType.Pixel) },
          new TableViewColumn { Header = "AdresseMAC", Binding = new Binding(nameof(Printer.AdresseMAC)), Width = new GridLength(150, GridUnitType.Pixel) },
          new TableViewColumn { Header = "ConcatenerLocation", Binding = new Binding(nameof(Printer.ConcatenerLocation)), Width = new GridLength(700, GridUnitType.Pixel) }
        ]);


        _tableView.Bind(TableView.SelectedItemProperty, new Binding(nameof(MainViewModel.SelectedPrinter)));









        SizeChanged += (sender, e) =>
        {
            // Fixe la taille de l'élément à 60% de la largeur et 40% de la hauteur de la fenêtre
            _tableView.Width = e.NewSize.Width * 0.99;
            _tableView.Height = e.NewSize.Height * 0.8;
            //_scrollViewer2.Width = e.NewSize.Width * 0.99;
            //_scrollViewer2.Height = e.NewSize.Height * 0.4;
        };


        _buttonPanel.Children.AddRange([_buttonChargerCMDB, _refreshAll]);

        _stackPanel.Children.AddRange([_buttonPanel, _comboBox, _tableView, new DetailView(), _infosPrintersChargedCMDB]);

        Content = _stackPanel;
    }



    private async void LoadCMDBOnClick(object sender, RoutedEventArgs args)
    {
        var s = SettingsService.Load();

        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        var filePath = await StorageService.GetFilePathCMDB(topLevel!);


        // VÉRIFICATION : Si aucun fichier n'a été sélectionné, on stoppe l'exécution
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            s.PrintersXlsxPath = filePath!;

            SettingsService.Save(s);

            var printersRead = await ExcelService.ReadCmdbSheet(s.PrintersXlsxPath);
            MainViewModel.Instance.Printers.Clear();


            foreach (var printer in printersRead)
            {
                MainViewModel.Instance.Printers.Add(printer);
                //Console.WriteLine(printer.Numeroetiquette);
            }

            MainViewModel.Instance.UpdateFilteredPrinters();


            mainViewModel?.InfoList = $"{printersRead.Count} imprimantes chargée depuis {filePath}";

    }

    private async void RefreshAllOnClick(object sender, RoutedEventArgs args)
    {
        await RefreshService.RefreshAll();
    }


}
