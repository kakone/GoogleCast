using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace GoogleCast.SampleApp
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var simpleIoc = SimpleIoc.Default;
            simpleIoc.Reset();
            ServiceLocator.SetLocatorProvider(() => simpleIoc);
            simpleIoc.Register<IDeviceLocator, DeviceLocator>();
            simpleIoc.Register<ISender>(() => new Sender());
            simpleIoc.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }
    }
}
