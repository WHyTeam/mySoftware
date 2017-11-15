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

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using WHyProject.View;
using System;

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

            SimpleIoc.Default.Register<loginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DebugViewModel>();
            SimpleIoc.Default.Register<SystemFaultViewModel>();
            SimpleIoc.Default.Register<GridFaultViewModel>();
            SimpleIoc.Default.Register<GenFaultViewModel>();
            SimpleIoc.Default.Register<SystemSettingViewModel>();
            SimpleIoc.Default.Register<ParamViewModel>();
            SimpleIoc.Default.Register<SystemMaintainViewModel>();


            var navigationService = this.InitNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }

        //public INavigationService NavigationService => ServiceLocator.Current.GetInstance<INavigationService>();
        public INavigationService InitNavigationService()
        {
            NavigationService service = new NavigationService();
            
            service.Configure(typeof(MainViewModel).FullName, new Uri("/WHyProject;component/View/MainView.xaml", UriKind.Relative));
            service.Configure(typeof(loginViewModel).FullName, new Uri("/WHyProject;component/View/LoginView.xaml", UriKind.Relative));
            return service;
        }

        public loginViewModel LoginVM 
        {
            get
            {
                return ServiceLocator.Current.GetInstance<loginViewModel>();
            }
        }
        public MainViewModel MainVM
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

        public SystemSettingViewModel systemSetting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SystemSettingViewModel>();
            }
        }

        public ParamViewModel paramViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParamViewModel>();
            }
        }
        

        public SystemMaintainViewModel systemMaintainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SystemMaintainViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}