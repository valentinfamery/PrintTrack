using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Platform.Storage;

public static class StorageService
{
    public static async Task<string> GetFilePathCMDB(TopLevel topLevel)
    {
        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false
        });

        if (files.Count >= 1)
        {
            // 1. Récupérer le fichier sélectionné
        var selectedFile = files[0];

        // 2. Récupérer le chemin local (ex: C:\Dossier\fichier.txt)
        string filePath = selectedFile.TryGetLocalPath()!;

        // Alternative si vous avez besoin de l'URI complète (ex: file:///...)
        // Uri fileUri = selectedFile.Path;

        // Pour tester, on l'affiche dans la console
        Console.WriteLine($"Chemin du fichier : {filePath}");




            // Open reading stream from the first file.
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            // Reads all the content of file as a text.
            var fileContent = await streamReader.ReadToEndAsync();



        return filePath;
        }
        return "";

    }
}
