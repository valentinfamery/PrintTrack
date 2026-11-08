using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;
using DocumentFormat.OpenXml.Spreadsheet;
using PrintTrack.Models;
using PrintTrack.ViewModels;

namespace PrintTrack.Views;


public class DetailView : UserControl
{
    private readonly StackPanel _detailPanel;
    private readonly TextBlock _textDetail;
    public DetailView()
    {
        _detailPanel = new StackPanel()
        {
            Spacing = 5,
            Margin = new Thickness(10),
            Orientation = Orientation.Vertical,
        };

        _textDetail = new TextBlock();

        _textDetail.Bind(TextBlock.TextProperty,new Binding(nameof(MainViewModel.SelectedPrinter.Numeroetiquette)));

        _detailPanel.Children.Add(_textDetail);
        Content = _detailPanel;
        DataContext = MainViewModel.Instance;
    }
}
