using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrintTrack;
using PrintTrack.ViewModels;

public class RefreshService
{
    
    public  void RefreshAll()
    {
        var printers = MainViewModel.Instance?.Printers;

        if (!printers!.Any())
        {
            
        var tasks = new List<Task>();


        foreach (var value in printers!)
        {
            tasks.Add(
                SnmpService.walkOperation(value.AdresseIP)
            );
        }
        
        Task t = Task.WhenAll(tasks);
      
        t.Wait();

        }
     

    }
}