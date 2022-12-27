using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace GoogleCast.SampleApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    static App()
    {
        var services = new ServiceCollection();
        services.AddGoogleCast();
        services.AddScoped<IDeviceLocator, DeviceLocator>();
        services.AddScoped<ISender, Sender>();
        services.AddScoped<MainViewModel>();
        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }
}
