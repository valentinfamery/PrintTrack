using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PrintTrack.Models;
using Avalonia.Threading;
using System.Linq;
using System;

namespace PrintTrack.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public static MainViewModel Instance { get; } = new MainViewModel(); 

    
    public List<Printer> Printers {get; set; } = [];


    [ObservableProperty]
    private ObservableCollection<Printer> _filteredPrinters = [];

    [ObservableProperty]
    private ObservableCollection<string> _grandSites = [];

    

    [ObservableProperty]
    private string? _filter = "Tous les sites"; 

    [ObservableProperty]
    private string _infoList ="";

    [ObservableProperty]
    private Printer? _selectedPrinter; 

    partial void OnFilterChanged(string? value)
    {
        Console.WriteLine(value);
        Dispatcher.UIThread.Post(() =>
        {
        Console.WriteLine(value);
        
            FilteredPrinters.Clear();

            IEnumerable<Printer> printersToDisplay = string.IsNullOrEmpty(value) || value == "Tous les sites"
                ? Printers
                : Printers.Where(i => i.Site?.Contains(value, StringComparison.OrdinalIgnoreCase) == true);

            foreach (var printer in printersToDisplay)
            {
                FilteredPrinters.Add(printer);
            }

            InfoList = $"{FilteredPrinters.Count} imprimantes selectionnées ";

        });
    }

    partial void OnSelectedPrinterChanged(Printer? value)
    {
        Console.WriteLine(value?.Numeroetiquette);
    }

    public void UpdateFilteredPrinters()
    {
        Dispatcher.UIThread.Post(() =>
        {
            FilteredPrinters.Clear();

            foreach (var printer in Printers)
            {
                FilteredPrinters.Add(printer);
            }
        });
    }

    public MainViewModel()
    {
        
    }
}