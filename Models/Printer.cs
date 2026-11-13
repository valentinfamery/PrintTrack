using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PrintTrack.Models;

public partial class Printer(
    string grandSite,
    string categorie,
    string adresseIP,
    string numeroetiquette,
    string marque,
    string modele,
    string numserie,
    string adresseMAC,
    string concatenerLocation,
    string status) : ObservableObject
{
    [ObservableProperty]
    public partial string Site { get; set; } = grandSite;

    [ObservableProperty]
    public partial string Categorie { get; set; } = categorie;

    [ObservableProperty]
    public partial string AdresseIP { get; set; } = adresseIP;

    [ObservableProperty]
    public partial string Numeroetiquette { get; set; } = numeroetiquette;

    [ObservableProperty]
    public partial string Marque { get; set; } = marque;

    [ObservableProperty]
    public partial string Modele { get; set; } = modele;

    [ObservableProperty]
    public partial string Numserie { get; set; } = numserie;

    [ObservableProperty]
    public partial string AdresseMAC { get; set; } = adresseMAC;

    [ObservableProperty]
    public partial string ConcatenerLocation { get; set; } = concatenerLocation;

    [ObservableProperty]
    public partial string Status { get; set; } = status;

    [ObservableProperty]
    public partial ObservableCollection<ConsumableInfo> Consommables { get; set; } = [];
}
