using CommunityToolkit.Mvvm.ComponentModel;

public partial class ConsumableInfo() : ObservableObject
{
    [ObservableProperty]
    public partial string? Name { get; set; }

    [ObservableProperty]
    public partial int CurrentLevel { get; set; } = -1;

    [ObservableProperty]
    public partial int MaxCapacity { get; set; } = -1;

    [ObservableProperty]
    public partial int Percentage { get; set; } = -1;
}
