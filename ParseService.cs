using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using Lextm.SharpSnmpLib;
using PrintTrack.Models;
// Supposons l'utilisation de Lextm.SharpSnmpLib ou équivalent
// using Lextm.SharpSnmpLib; 

public class ParseService{

public static void ParsePrinterSnmpData(Printer printer, IList<Variable> results)
{
    // ==========================================
    // 1. TRAITEMENT DES SCALAIRES (Indices 0 à 4)
    // ==========================================
    
    // prtGeneralStatus (1.3.6.1.2.1.43.5.1.1.1.1)
    var prtStatus = results.ElementAtOrDefault(2)?.Data;
    printer.Status = TranslatePrinterStatus(Convert.ToInt32(prtStatus?.ToString()));

    // ==========================================
    // 2. TRAITEMENT DES TABLES DE CONSOMMABLES
    // ==========================================
    // Les index après les 5 scalaires contiennent les répétitions (jusqu'à 10 par OID de table).
    // Structure standard Printer-MIB (prtMarkerSupplies) :
    // - Capacités max (OIDs de base + .8.Instance)
    // - Niveaux actuels (OIDs de base + .9.Instance)

    // Exemple de dictionnaire temporaire pour stocker les consommables par index/instance
    var supplies = new Dictionary<string, ConsumableInfo>();

    // Parcourir les résultats à partir de l'index 5 pour extraire les tables
    // (Cette boucle dépend de la façon dont votre bibliothèque retourne l'ordre des variables GetBulk)
    foreach (var variable in results.Skip(5))
    {
        string oidString = variable.Id.ToString();
        
        // Exemple pour le Printer-MIB Standard :
        // Détection de prtMarkerSuppliesMaxCapacity (.8) et prtMarkerSuppliesLevel (.9)
        if (oidString.StartsWith("1.3.6.1.2.1.43.11.1.1.8."))
        {
            string instance = oidString.Split('.').Last();
            if (!supplies.ContainsKey(instance)) supplies[instance] = new ConsumableInfo();
            supplies[instance].MaxCapacity = Convert.ToInt32(variable.Data.ToString());
        }
        else if (oidString.StartsWith("1.3.6.1.2.1.43.11.1.1.9."))
        {
            string instance = oidString.Split('.').Last();
            if (!supplies.ContainsKey(instance)) supplies[instance] = new ConsumableInfo();
            supplies[instance].CurrentLevel = Convert.ToInt32(variable.Data.ToString());
        }
        // Vous pouvez ajouter ici la logique pour Lexmark, Epson, Brother de la même manière...
    }

    // ==========================================
    // 3. CALCUL DES POURCENTAGES
    // ==========================================
    foreach (var supply in supplies.Values)
    {
        if (supply.MaxCapacity > 0 && supply.CurrentLevel >= 0)
        {
            // Calcul du pourcentage
            supply.Percentage = (int)Math.Round((double)supply.CurrentLevel / supply.MaxCapacity * 100);
        }
        else if (supply.CurrentLevel == -3) // -3 correspond souvent à "inconnu" ou "capacity indéterminée" (ex: -2 ou -3 selon MIB)
        {
            supply.Percentage = -1; // Valeur spéciale pour indiquer un niveau indéterminé (cartouche tiers, etc.)
        }
    }

    printer.Consommables.Clear();
    foreach(var supplie in supplies.Values)
        {
            printer.Consommables.Add(supplie);
        }

}

private static string TranslatePrinterStatus(int? status) => status switch
{
    3 => "En ligne / Prêt",
    4 => "En impression",
    5 => "En erreur / Attention requise",
    _ => "Inconnu"
};

}