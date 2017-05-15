/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WHyProject"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;


namespace WHyProject.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DebugViewModel>();
            SimpleIoc.Default.Register<SystemFaultViewModel>();
            SimpleIoc.Default.Register<GridFaultViewModel>();
            SimpleIoc.Default.Register<GenFaultViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DebugViewModel rs485debug
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DebugViewModel>();
            }
        }

        public SystemFaultViewModel SystemFault
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SystemFaultViewModel>();
            }
        }

        public GridFaultViewModel gridFault
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GridFaultViewModel>();
            }
        }

        public GenFaultViewModel genFault
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GenFaultViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}