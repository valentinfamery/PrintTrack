using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;


class PrintersTab : TabItem
{
   

    public PrintersTab()
    {
        Header = "Printer";
        Content = new TextBlock
            {
                Margin = new Thickness(5),
                Text = "This is tab 1 content"
            };
    }

}