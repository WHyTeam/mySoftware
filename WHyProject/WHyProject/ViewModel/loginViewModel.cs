using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WHyProject.Model.User;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using WHyProject.Server;



namespace WHyProject.ViewModel
{

    
    public class loginViewModel:ViewModelBase
    {
        private UserInfo user = new UserInfo();

        private readonly INavigationService _frameNavigationService;
        private LogFile userLog = new LogFile();
        public RelayCommand LoginCommand
        {
            get;private set;

        }
        public string UserName
        {
            get { return user.Name; }
            set { user.Name = value; }
        }
        private void ExecuteLogin()
        {

            if(UserName == "admin" && PassWord == "admin")
            {

                _frameNavigationService.NavigateTo(typeof(MainViewModel).FullName);
                string logText = UserName + "--Login";
                userLog.WriteToFile("\\login.txt", logText);
            }
        }
        public string PassWord
        {
            get { return user.Password; }
            set { user.Password = value; }
        }

        public loginViewModel(INavigationService frameNavigationService)
        {
            _frameNavigationService = frameNavigationService;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }
    }
}
