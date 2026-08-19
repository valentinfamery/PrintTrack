using CommunityToolkit.Mvvm.ComponentModel;

namespace PrintTrack.Models;

public partial class Printer(
    string site,
    string categorie,
    string adresseIP,
    string numeroetiquette,
    string marque,
    string modele,
    string numserie,
    string adresseMAC,
    string concatenerLocation) : ObservableObject
{
    [ObservableProperty] private string  _site = site;
    [ObservableProperty] private string _categorie = categorie;
    [ObservableProperty] private string _adresseIP = adresseIP;
    [ObservableProperty] private string _numeroetiquette = numeroetiquette;
    [ObservableProperty] private string _marque = marque;
    [ObservableProperty] private string _modele = modele;
    [ObservableProperty] private string _numserie = numserie;
    [ObservableProperty] private string _adresseMAC = adresseMAC;
    [ObservableProperty] private string _concatenerLocation = concatenerLocation;
}