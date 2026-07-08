using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using Avalonia.Platform.Storage;

namespace PrintTrack.Views;

public class SettingsView : UserControl
{
    private readonly TextBlock _textBlock;
    private readonly Button _button;
    private readonly StackPanel _stackPanel;

    public SettingsView()
    {
        _textBlock = new TextBlock()
        {
            Text=""
        };

        _button = new Button()
        {
            Content = "Select File",
            HorizontalAlignment = HorizontalAlignment.Center,
        };


        // On définit le contenu de cet onglet ici
        

        _button.Click +=  OpenFileButton_Clicked;

        _stackPanel = new StackPanel();
        
        _stackPanel.Children.Add(_textBlock);
        _stackPanel.Children.Add(_button);

        Content = _stackPanel;
    }

    private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
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

        _textBlock.Text = $"Chemin du fichier : {filePath}";


            // Open reading stream from the first file.
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            // Reads all the content of file as a text.
            var fileContent = await streamReader.ReadToEndAsync();

           


            
        }

        
    }
}