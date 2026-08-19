using System;

using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Media;
using PrintTrack.Models;
using PrintTrack.ViewModels;



namespace PrintTrack.Views;
    

public class MainView : TabItem
{
    private readonly MainViewModel? mainViewModel = MainViewModel.Instance;
    private readonly StackPanel _stackPanel;
    private readonly StackPanel _buttonPanel;

    private readonly Button _buttonChargerCMDB;
    private readonly Button _refreshAll;

    private readonly TableView _tableView;

    private readonly SettingsService settingsService = new();

    private readonly TextBlock _textBlock;

    private readonly ScrollViewer _scrollViewer;

    private readonly TextBlock _textBlockResultSnmp;

    private readonly TextBlock _infosPrintersChargedCMDB;

 
    public MainView()
    {
        //DataContext = mainViewModel;
        settingsService.Load();

        var s = settingsService.Load();



        _textBlock = new TextBlock()
        {
            Text=""
        };

        _textBlockResultSnmp = new TextBlock()
        {
            Text=""
        };

        _infosPrintersChargedCMDB = new TextBlock()
        {
            Text=""
        };

        _textBlock.Text = s.PrintersXlsxPath;

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

    

      

        Console.WriteLine(mainViewModel?.Printers.ToString());

        _tableView = new TableView().CanUserResizeColumns(false).ItemsSource(mainViewModel?.Printers).HorizontalAlignment(HorizontalAlignment.Stretch).VerticalAlignment(VerticalAlignment.Stretch);

        _tableView.Columns([
            new TableViewColumn().Header("Site").Binding(new Binding(nameof(Printer.Site))),
            new TableViewColumn().Header("Categorie").Binding(new Binding(nameof(Printer.Categorie))),
            new TableViewColumn().Header("AdresseIP").Binding(new Binding(nameof(Printer.AdresseIP))),
            new TableViewColumn().Header("Numeroetiquette").Binding(new Binding(nameof(Printer.Numeroetiquette))),
            new TableViewColumn().Header("Marque").Binding(new Binding(nameof(Printer.Marque))),
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
            Content = _tableView
        };

SizeChanged += (sender, e) =>
{
    // Fixe la taille de l'élément à 60% de la largeur et 40% de la hauteur de la fenêtre
    _scrollViewer.Width = e.NewSize.Width * 0.99;
    _scrollViewer.Height = e.NewSize.Height * 0.4;
};
      

        _buttonPanel.Children(_buttonChargerCMDB);

        _stackPanel.Children(_buttonPanel,_scrollViewer,_textBlock,_textBlockResultSnmp,_infosPrintersChargedCMDB);
        
        Content = _stackPanel;
    }

   

    private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        var s = settingsService.Load();

        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        var filePath = await StorageService.GetFilePathCMDB(topLevel!);

        s.PrintersXlsxPath = filePath;

        _textBlock.Text = $"Chemin du fichier : {filePath}";

        settingsService.Save(s);

        var printersRead = await ExcelService.ReadCmdbSheet(s.PrintersXlsxPath);

        _infosPrintersChargedCMDB.Text = $"{printersRead.Count} imprimantes chargée depuis {filePath}";

        mainViewModel?.UpdateList(printersRead);

        
        

        
    }
}