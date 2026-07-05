using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

class PrinterTab : UserControl
{
   

    public PrinterTab()
    {
        Content = new TextBlock
            {
                Margin = new Thickness(5),
                Text = "This is tab 1 content"
            };
    }

}