using Avalonia.Controls;
using Avalonia.Data;
using PrintTrack.ViewModels;

namespace PrintTrack.Views;

public class DetailView : UserControl {

    private readonly StackPanel _stackPanel;
    private readonly TextBlock _textBlock;

    public DetailView() {
        DataContext = MainViewModel.Instance;


        _stackPanel = new StackPanel();

        _textBlock = new TextBlock(){Text = "DSP"};

        _stackPanel.Children.AddRange([_textBlock]);

        _textBlock.Bind(TextBlock.TextProperty, new Binding("SelectedPrinter.Numeroetiquette"));
        Content = _stackPanel;
    }





}
