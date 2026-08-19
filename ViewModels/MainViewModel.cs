using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PrintTrack.Models;
using Avalonia.Threading;

namespace PrintTrack.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public static MainViewModel Instance { get; } = new MainViewModel();    
    public ObservableCollection<Printer> Printers { get; set; } = [];

   
    public MainViewModel()
    {
        
    }

    public void UpdateList(List<Printer> printers)
    {
        Dispatcher.UIThread.Invoke(() =>
    {
        Printers.Clear(); // Facultatif : pour vider la liste avant d'ajouter si besoin
        foreach (var printer in printers)
        {
            Printers.Add(printer);
        }
    });
    }

    public void ModifierAdresseIp(int index, string nouvelleIp)
    {
        // La vue Avalonia se mettra à jour automatiquement !
        if (index >= 0 && index < Printers.Count)
        {
            Printers[index].AdresseIP = nouvelleIp;
        }
    }
}