using System.ComponentModel;
using System.Windows;

namespace GoogleCast.SampleApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initializes a new instance of <see cref="MainWindow"/> class
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    private new MainViewModel DataContext => (MainViewModel)base.DataContext;

    private async void WindowLoadedAsync(object sender, RoutedEventArgs e)
    {
        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            await DataContext.RefreshAsync();
        }
    }
}
