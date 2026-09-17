using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PrintTrack;
using PrintTrack.Models;
using PrintTrack.ViewModels;

public static class RefreshService
{
    
    public static async Task RefreshAll()
    {
        ThreadPool.SetMinThreads(250, 250);

        var printers = MainViewModel.Instance?.FilteredPrinters;

        Console.WriteLine("Fonction RefreshAll Démarre");

        if (printers?.Any() == true)
        {
            Console.WriteLine("Passe le if");
            var tasks = new List<Task>();

            foreach (var printer in printers)
            {
                tasks.Add(Task.Run(()=>{ScanAndUpdateStatusAsync(printer);}));
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur globale lors du Scan : {ex.Message}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Temps écoulé : {stopwatch.ElapsedMilliseconds} ms");
        }
        else
        {
            Console.WriteLine("Passe pas le if");
        }
    }

    public static void ScanAndUpdateStatusAsync(Printer printer)
    {
        try
        {
           
            var result = SnmpService.GetOperation(printer.AdresseIP);

            if (result?.FirstOrDefault() != null)
            {
                var statusValue = result[0].ToString();
                if (!string.IsNullOrWhiteSpace(statusValue))
                {
                    printer.Status = statusValue;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur sur l'IP {printer.AdresseIP} : {e.Message}");
            printer.Status = "Erreur SNMP";
        }
    }
}