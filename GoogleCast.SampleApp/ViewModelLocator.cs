using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace GoogleCast.SampleApp;

class ViewModelLocator
{
    /// <summary>
    /// Gets the main view model
    /// </summary>
    public static MainViewModel Main => Ioc.Default.GetRequiredService<MainViewModel>();
}
