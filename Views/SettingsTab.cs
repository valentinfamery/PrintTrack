using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

class SettingsTab : UserControl
{
    public SettingsTab()
    {
        Content = new TextBlock
            {
                Margin = new Thickness(5),
                Text = "This is tab 2 content"
            };
    }
}