using Avalonia.Controls;
using Avalonia.Data;
using PrintTrack.ViewModels;

namespace PrintTrack.Views;

public class DetailView : UserControl {

    private readonly StackPanel _stackPanel;
    private readonly TextBox _textBox;

    public DetailView() {
        DataContext = MainViewModel.Instance;
        Content = _stackPanel;

        _stackPanel = new StackPanel();

        _textBox = new TextBox(){Text = "DSP"};

        _stackPanel.Children.AddRange([_textBox]);

        _textBox.Bind(TextBox.TextProperty,new Binding(nameof(MainViewModel.SelectedPrinter.Numeroetiquette)));
    }





}
