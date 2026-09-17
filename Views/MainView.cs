using System;

using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
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
    private readonly ScrollViewer _scrollViewer;
    private readonly TextBlock _infosPrintersChargedCMDB;
    private readonly ComboBox _comboBox;

    public MainView()
    {
        DataContext = mainViewModel;
        SettingsService.Load();

        var s = SettingsService.Load();



        

      

        _infosPrintersChargedCMDB = new TextBlock()
        {
            Text=""
        };


        _stackPanel = new StackPanel()
        {
            Spacing = 5,
            Background = Brushes.GhostWhite
        };

        _buttonPanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            Spacing = 5, 
            Margin = new Thickness(10),
        };

        

        _buttonChargerCMDB = new Button()
        {
            Content = "ChargerCMDB",
        };

        _buttonChargerCMDB.Click +=  OpenFileButton_Clicked!;

        _refreshAll = new Button()
        {
            Content = "Rafraîchir tout"
        };

        _refreshAll.Click += RefreshAllClick!;

        _comboBox = new ComboBox();

        _comboBox.Bind(ItemsControl.ItemsSourceProperty, new Binding(nameof(MainViewModel.GrandSites)));

        // Lier l'élément sélectionné (SelectedItem -> SelectedOption)
        _comboBox.Bind(SelectingItemsControl.SelectedItemProperty, new Binding(nameof(MainViewModel.Filter)));

       
       


        _tableView = new TableView().CanUserResizeColumns(false).ItemsSource(mainViewModel.FilteredPrinters)
        .VerticalAlignment(VerticalAlignment.Stretch)
        ;


        var cellTemplate = new FuncDataTemplate<Printer>((item, namescope) =>
{
    // Container principal : rangée horizontale de 4 cubes
    var row = new StackPanel
    {
        Orientation = Orientation.Horizontal,
        //Spacing = 6,
        //Margin = new Thickness(4)
    };

    // Couleurs demandées pour les 4 cubes
    var colors = new[] { Colors.Black, Colors.Cyan, Colors.Magenta, Colors.Yellow };
    
    // Exemple : supposons que `item.Pourcentages` soit un tableau/liste de 4 double (ex: [50, 20, 80, 10])
    // Tu peux adapter la source de données selon tes besoins.
    for (int i = 0; i < 4; i++)
    {
       
        Color couleurFond = colors[i];
        
        // Un cube (Border) contenant le texte du pourcentage
        var cube = new Border
        {
            Width = 40,
            Height = 40,
            Background = new SolidColorBrush(couleurFond),
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4),
            Child = new TextBlock
            {
                Text = $"10%",
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

        _tableView.Columns([
            new TableViewColumn().Header("Site").Binding(new Binding(nameof(Printer.Site))),
            new TableViewColumn().Header("AdresseIP").Binding(new Binding(nameof(Printer.AdresseIP))),
            new TableViewColumn().Header("Numeroetiquette").Binding(new Binding(nameof(Printer.Numeroetiquette))),
            new TableViewColumn().Header("Statut").Binding(new Binding(nameof(Printer.Status))),
            new TableViewColumn().Header("Consommables").CellTemplate(cellTemplate),
            

            //new TableViewColumn().Header("Categorie").Binding(new Binding(nameof(Printer.Categorie))),
            
            //new TableViewColumn().Header("Marque").Binding(new Binding(nameof(Printer.Marque))),
            new TableViewColumn().Header("Modele").Binding(new Binding(nameof(Printer.Modele))),
            new TableViewColumn().Header("Numserie").Binding(new Binding(nameof(Printer.Numserie))),
            new TableViewColumn().Header("AdresseMAC").Binding(new Binding(nameof(Printer.AdresseMAC))),
            new TableViewColumn().Header("ConcatenerLocation").Width(new GridLength(500)).Binding(new Binding(nameof(Printer.ConcatenerLocation))),
        ]
        );

        
        

       

        _scrollViewer = new ScrollViewer()
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
            VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
            Width = 1500* 0.99,
            Height = 850* 0.4,
            Content = _tableView
        };

SizeChanged += (sender, e) =>
{
    // Fixe la taille de l'élément à 60% de la largeur et 40% de la hauteur de la fenêtre
    _scrollViewer.Width = e.NewSize.Width * 0.99;
    _scrollViewer.Height = e.NewSize.Height * 0.4;
};


        _buttonPanel.Children(_buttonChargerCMDB,_refreshAll);

        _stackPanel.Children(_buttonPanel,_comboBox,_scrollViewer,_infosPrintersChargedCMDB);
        
        Content = _stackPanel;
    }

   

    private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        var s = SettingsService.Load();

        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        var filePath = await StorageService.GetFilePathCMDB(topLevel!);

        s.PrintersXlsxPath = filePath;

        SettingsService.Save(s);

        var printersRead = await ExcelService.ReadCmdbSheet(s.PrintersXlsxPath);
        MainViewModel.Instance.Printers.Clear();
        
foreach (var printer in printersRead)
{
    MainViewModel.Instance.Printers.Add(printer);
    //Console.WriteLine(printer.Numeroetiquette);
}

MainViewModel.Instance.UpdateFilteredPrinters();


        _infosPrintersChargedCMDB.Text = $"{printersRead.Count} imprimantes chargée depuis {filePath}";
    }

    private async void RefreshAllClick(object sender,RoutedEventArgs args)
    {
        await RefreshService.RefreshAll();
        //var printers = MainViewModel.Instance.Printers;

        //foreach(var printer in printers)
        //{
            //printer.Status = "Modified";
        //}
    }
}



