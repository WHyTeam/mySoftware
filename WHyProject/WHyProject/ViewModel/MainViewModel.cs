using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Threading;
using WHyProject.Model.Conductor;
using WHyProject.Server;
using WHyProject.Protocol;

namespace WHyProject.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        private MyServer listener;

        private TransMsg tranMsg = TransMsg.Instance;

        private MasterConductor objMasterConductor = MasterConductor.Master;
            /// <summary>
            /// Initializes a new instance of the MainViewModel class.
            /// </summary>
            public MainViewModel()
            {
               // listener = new MyServer();
                listener = new MyServer(65535);
                listener.Start();
            }

            private RelayCommand<string> _openWindowcommand;

            public RelayCommand<string> OpenWindowCommand
            {
                get
                {
                    if (_openWindowcommand == null)
                    _openWindowcommand = new RelayCommand<string>(para => ExcuteGridConnected(para));
                    return _openWindowcommand;
                }
         }

        private static Dictionary<string, Window> _windows = new Dictionary<string, Window>();

        public void ExcuteGridConnected(string para)
       {
                    Window window = null;
                    //if (_windows.ContainsKey(para))
                    //{
                    //    window = _windows[para];
                    //}
                    //else
                    //{
                        window = App.Current.GetType().Assembly.CreateInstance(para) as Window;
                    //    _windows.Add(para, window);
                    //}
                    window.Owner = App.Current.MainWindow;
                    if (window != null)
                    {
                        window.Show();
                    }
              }
  
    }
}