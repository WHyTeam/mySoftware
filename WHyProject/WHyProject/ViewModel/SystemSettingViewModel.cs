using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using WHyProject.ModelForShow;
using GalaSoft.MvvmLight.Command;

namespace WHyProject.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SystemSettingShow: MasterModel
    {
        private bool _isChecked;
        public bool IsCheck
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }
    }
    public class SystemSettingViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;
        private ObservableCollection<SystemSettingShow> _McuSystemSetting;
        private ObservableCollection<SystemSettingShow> _gridSystemSetting;
        private ObservableCollection<SystemSettingShow> _genSystemSetting;

        private RelayCommand<string> _setCommand;
        private RelayCommand<string> _readCommand;

        private readonly Dictionary<string, string> ButtonToPropertyForSet;
      
        public RelayCommand<string> SetCommand {
            get
            {
                if(_setCommand == null)
                {
                    _setCommand = new RelayCommand<string>(x => ExecuteSet(x));

                }
                return _setCommand;
            }
        }

        public RelayCommand<string> ReadCommand
        {
            get
            {
                if(_readCommand == null)
                {
                    _readCommand = new RelayCommand<string>(x => ExecuteRead(x));
                }
                return _readCommand;
            }
        }

        string[] _groupBoxHeader =
        {
            "监控模式",
            "网侧模式",
            "机侧模式"
        };

        string []_buttonText = { "设定", "读取" };

        string[] MCUSystemSettingText =
        {
            "CTRLCABTEMP","SWICABTEMP","WATERTEMP","WATERPRE","GRID_SPD","GEN_SPD","AUX_SPD","ALL_FAN",
            "POWER_FAN","EPO","SAFECHAIN","GRID_MCB","UPS_LOST","UPS_WARRING","主从不一致","TBD"
        };

        string[] GridSystemSettingText =
        {
            "大/小系统","制动电路","滤波电路","测试电路","PF/KVAR","不锁电网","负序控制","变电压PI",
            "TBD","TBD","TBD","TBD","TBD","TBD","无前馈","主/从"
        };

        string[] GenSystemSettingText =
        {
            "模式1","模式2","SPI并联","主/从","编码器","CAN并联","电励磁","大/小系统",
            "相位测试","转矩/转速","TBD","TBD","TBD","TBD","TBD","共模控制"
        };

        public string[] ButtonSet
        {
            get { return _buttonText; }
        }
        public string[] SystemTextBlock
        {
            get { return _groupBoxHeader; }
        }
        public ObservableCollection<SystemSettingShow> MCUSystemSetting
        {
            get { return _McuSystemSetting; }
            set {
                if (_McuSystemSetting == value) return;
                _McuSystemSetting = value; }
        }

        public ObservableCollection<SystemSettingShow> GridSystemSetting
        {
            get { return _gridSystemSetting; }
            set
            {
                if (_gridSystemSetting == value) return;
                _gridSystemSetting = value;
            }
        }

        public ObservableCollection<SystemSettingShow> GenSystemSetting
        {
            get { return _genSystemSetting; }
            set
            {
                if (_genSystemSetting == value) return;
                _genSystemSetting = value;
            }
        }

        private void _initSystem(ObservableCollection<SystemSettingShow> Setting, string[] Info)
        {
            for(int i = 0; i < Info.Length; i ++)
            {
                Setting.Add(new SystemSettingShow() { Info = Info[i], IsCheck = false ,Light = true});
            }
        }

        private void InitSystemSetting()
        {
            MCUSystemSetting = new ObservableCollection<SystemSettingShow>();
            _initSystem(MCUSystemSetting, MCUSystemSettingText);

            GridSystemSetting = new ObservableCollection<SystemSettingShow>();
            _initSystem(GridSystemSetting, GridSystemSettingText);

            GenSystemSetting = new ObservableCollection<SystemSettingShow>();
            _initSystem(GenSystemSetting, GenSystemSettingText);
        }
        
        private void ExecuteSet(string x)
        {
            ushort value = 0;
            Type type = objMasterConductor.GetType();
            System.Reflection.PropertyInfo objPropertyInfo = type.GetProperty(ButtonToPropertyForSet[x]);
            //  var value = (ushort)propertyInfo.GetValue(objMasterConductor);
            System.Reflection.PropertyInfo propertyInfo = this.GetType().GetProperty(x);
            var listData = (ObservableCollection<SystemSettingShow>)propertyInfo.GetValue(this);
            ushort temp = (ushort)1;
            for (int i = 0; i < 16; i++)
            {
                if (listData[i].IsCheck == true)
                {
                    value |= (ushort)(temp << i);
                }
            }
            objPropertyInfo.SetValue(objMasterConductor, value);
        }

        private void ExecuteRead(string x)
        {
            Type type = objMasterConductor.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(ButtonToPropertyForSet[x]);
            var value = (ushort)propertyInfo.GetValue(objMasterConductor);
            propertyInfo = this.GetType().GetProperty(x);
            var listData = (ObservableCollection<SystemSettingShow>)propertyInfo.GetValue(this);
            ushort temp = (ushort)1;
            for(int i = 0; i < 16; i ++)
            {
                if ((value & temp) != 0)
                {
                    listData[i].Light = false;
                }
                temp <<= 1;
            }
        }
        /// <summary>
        /// Initializes a new instance of the SystemSettingViewModel class.
        /// </summary>
        public SystemSettingViewModel()
        {
            InitSystemSetting();
            ButtonToPropertyForSet = new Dictionary<string, string>
            {
                {"MCUSystemSetting","McuModuleModeForSet" },
                {"GridSystemSetting", "GridModuleModeForSet"},
                {"GenSystemSetting", "GenModuleModeForSet"}
            };
          //  SetCommand = new RelayCommand<int>(ExecuteSet);
          // ReadCommand = new RelayCommand(ExecuteRead);
        }
    }
}