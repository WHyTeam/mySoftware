using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;
using System.Collections.ObjectModel;
using System;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using WHyProject.ModelForShow;


namespace WHyProject.ViewModel
{
    public class TimeCheckBox : MasterModel
    {
        private bool _isChecked;
        public bool IsCheck
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }
    }
    public class VersionClass: ViewModelBase
    {
        private string _projectNum;
        private string _hardware;
        private string _software;
        private string _engNum;
        public string Descrip { get; set; }

        public string ProjectNum
        {
            get { return _projectNum; }
            set
            {
                if (_projectNum == value) return;
                _projectNum = value;
                RaisePropertyChanged(() => ProjectNum);
            }
        }
        public string HardWareNum
        {
            get { return _hardware; }
            set
            {
                if (_hardware == value) return;
                _hardware = value;
                RaisePropertyChanged(() => HardWareNum);
            }
        }
        public string SoftWareNum
        {
            get { return _software; }
            set
            {
                if (_software == value) return;
                _software = value;
                RaisePropertyChanged(() => SoftWareNum);
            }
        }
        public string EngNum
        {
            get { return _engNum; }
            set
            {
                if (_engNum == value) return;
                _engNum = value;
                RaisePropertyChanged(() => EngNum);
            }
        }

    }


    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SystemMaintainViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;

        private readonly string[] _descrip = { "监控版本控制", "网侧程序版本", "机侧程序编号" };
        private readonly string[] _projectNum = new string[3];
        private readonly string[] _hardwareNum = new string[3];
        private readonly string[] _softwareNum = new string[3];
        private readonly string[] _engNum = new string[3];

        private readonly string[] _info = { "总并网时间", "总发电量","#2风扇时间", "#3风扇时间", "#4风扇时间", "#5风扇时间" };
        private readonly string[] _time = {"GEN_MC", "GRD_MCB", "EE_MCB"};
        private readonly string[] _unit = {"h", "KW", "h", "h", "h", "h"};

        private readonly string[] _checkboxStrings =
        {
            "总并网时间", "总发电量", "#2风扇时间", "#3风扇时间", "#4风扇时间", "#5风扇时间", "GEN_MC", "GRD_MCB", "EE_MCB", "TBD1", "TBD2",
            "TBD3"
        };

       

        public ObservableCollection<VersionClass> Version { get; set; }
        public ObservableCollection<MasterModel> TextBlock1 { get; set; }
        public ObservableCollection<MasterModel> TextBlock2 { get; set; }
        public ObservableCollection<TimeCheckBox> CheckBox1 { get; set; }

   
        private void _initVersion(ObservableCollection<VersionClass> Version, string[] Text)
        {
            for (var ii = 0; ii < Text.Length; ii ++)
            {
                Version.Add(new VersionClass()
                {
                    Descrip = _descrip[ii],
                    ProjectNum = "项目编号:" + _projectNum[ii],
                    HardWareNum ="硬件版本:" + _hardwareNum[ii],
                    SoftWareNum = "软件版本:" + _softwareNum[ii],
                    EngNum = _engNum[ii]
                });
            }
        }

        private void _initTextBlock1(ObservableCollection<MasterModel> block, string[] text,string[] unit)
        {
            for (var i = 0; i < text.Length; i++)
            {
                block.Add(new MasterModel()
                {
                    Data = 0,
                    Info = text[i],
                    Unit = unit[i],
                });
            }
        }

        private void _initTextBlock2(ObservableCollection<MasterModel>block, string[] Text)
        {
            for (var i = 0; i < Text.Length; i++)
            {
                block.Add(new MasterModel()
                {
                    Data = 0,
                    Info = Text[i],
                });
            }
        }

        private void _initCheckBox1(ObservableCollection<TimeCheckBox> box, string[] Text)
        {
            for (var i = 0; i < Text.Length; i++)
            {
                box.Add(new TimeCheckBox()
                {
                      Info = Text[i],
                      IsCheck = false,
                      
                });
            }
        }
        /// <summary>
        /// Initializes a new instance of the SystemMaintainViewModel class.
        /// </summary>
        public SystemMaintainViewModel()
        {


            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Sender is MasterConductor)
                {
                    ShowModelData(message.Notification);
                }
            });

            Version = new ObservableCollection<VersionClass>();
            _initVersion(Version, _descrip);

            TextBlock1 = new ObservableCollection<MasterModel>();
            _initTextBlock1(TextBlock1,_info,_unit);

            TextBlock2 = new ObservableCollection<MasterModel>();
            _initTextBlock2(TextBlock2, _time);

            CheckBox1 = new ObservableCollection<TimeCheckBox>();
            _initCheckBox1(CheckBox1, _checkboxStrings);
        }

        private void ShowModelData(string propertyName)
        {
            if(propertyName == "RecvMaintainStr")
            {
                Type type = objMasterConductor.GetType();
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
                var RecStr = (string)propertyInfo.GetValue(objMasterConductor);
                var i = 0;
                for (var j = 0; j < 3; j ++)
                {
                    Version[j].ProjectNum = "项目编号: " + RecStr.Substring(i, 13);
                    Version[j].HardWareNum = "硬件版本: " + RecStr.Substring(i + 13, 3);
                    Version[j].SoftWareNum = "软件版本: "+ RecStr.Substring(i + 16, 3);
                    Version[j].EngNum = RecStr.Substring(i + 19, 16);
                    i += 35;
                }
            }
        }
    }
}