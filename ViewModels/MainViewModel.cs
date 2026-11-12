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
    public partial ObservableCollection<Printer> FilteredPrinters { get; set; } = [];
    [ObservableProperty]
    public partial ObservableCollection<string> GrandSites { get; set; } = [];


    [ObservableProperty]
    public partial string? Filter { get; set; } = "Tous les sites";
    [ObservableProperty]
    public partial string InfoList { get; set; } = "";
    [ObservableProperty]
    public partial Printer? SelectedPrinter { get; set; }

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
}
