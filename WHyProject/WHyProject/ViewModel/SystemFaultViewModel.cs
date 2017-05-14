using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WHyProject.Model.Conductor;
using WHyProject.ModelForShow;

namespace WHyProject.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SystemFaultViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;
        private string[] _systemFaultText;
        private ObservableCollection<MasterModel> _systemFaultNo1;
        private ObservableCollection<MasterModel> _systemFaultNo2;
        private ObservableCollection<MasterModel> _systemFaultNo3;
        private ObservableCollection<MasterModel> _systemFaultNo4;

        private string[] LightInfo1 =
        {
            "控制柜过温", "控制柜欠温", "电感柜过温", "电感柜欠温",
            "功率柜过温", "功率柜欠温", "开关柜过温", "开关柜欠温",
            "冷却水过温", "冷却水欠温", "冷却水过压", "冷却水欠压",
            "保留", "保留", "保留", "保留"
        };

        private string[] LightInfo2 =
        {
            "UPS/220V_24V", "380V/220V_24V", "编码盘_24V", "控制电_24V",
            "保留", "保留", "保留", "保留",
            "保留", "保留", "保留", "保留",
            "保留", "保留", "写存储器故障", "读存储器故障"
        };

        private string[] LightInfo3 =
        {
            "网侧防雷器", "机侧防雷器", "辅助电源防雷器", "功率柜风扇",
            "功率柜风扇_CT", "主从状态不一致", "转矩反馈不匹配", "380V掉电",
            "UPS掉电", "机侧断路器过流", "网侧断路器过流", "主控通讯丢失",
            "从MCU通讯丢失", "DSP通讯丢失", "保留", "保留"
        };

        private string[] LightInfo4 =
        {
            "网侧555", "机侧555", "EPO", "控制_12V",
            "安全链", "保留", "保留", "保留",
            "保留", "保留", "保留", "保留",
            "保留", "保留", "保留", "保留"
        };

        private string[] headlineText =
        {
            "系统故障字1", "系统故障字2", "系统故障字3", "系统故障字4"
        };

        /// <summary>
        /// Initializes a new instance of the SystemFaultViewModel class.
        /// </summary>
        public SystemFaultViewModel()
        {
            InitSystemFault();
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if(message.Sender is MasterConductor)
                    ChangePropertyAccordingToMessage(message.Notification);
            });
        }

        public string[] SystemFaultText
        {
            get { return _systemFaultText; }
            set
            {
                _systemFaultText = value;
            }
        }

        public ObservableCollection<MasterModel> SystemFaultNo1
        {
            get { return _systemFaultNo1; }
            set { _systemFaultNo1 = value; }
        }

        public ObservableCollection<MasterModel> SystemFaultNo2
        {
            get { return _systemFaultNo2; }
            set { _systemFaultNo2 = value; }
        }

        public ObservableCollection<MasterModel> SystemFaultNo3
        {
            get { return _systemFaultNo3; }
            set { _systemFaultNo3 = value; }
        }

        public ObservableCollection<MasterModel> SystemFaultNo4
        {
            get { return _systemFaultNo4; }
            set { _systemFaultNo4 = value; }
        }

        private void InitSystemFault()
        {
            SystemFaultNo1 = new ObservableCollection<MasterModel>();
            SystemFaultNo2 = new ObservableCollection<MasterModel>();
            SystemFaultNo3 = new ObservableCollection<MasterModel>();
            SystemFaultNo4 = new ObservableCollection<MasterModel>();
            _systemFaultText = new string[4];
            _systemFaultText = headlineText;
            InitLight(SystemFaultNo1, LightInfo1);
            InitLight(SystemFaultNo2, LightInfo2);
            InitLight(SystemFaultNo3, LightInfo3);
            InitLight(SystemFaultNo4, LightInfo4);
        }

        private void InitLight(ObservableCollection<MasterModel> listData, string[] info)
        {
            for (int i = 0; i < info.Length; i++)
            {
                listData.Add(new MasterModel() { Info = info[i], Light = true });
            }
        }

        private void ChangePropertyAccordingToMessage(string propertyName)
        {
            if(propertyName == "SystemFaultForDisplay")
                ChangeLight(propertyName);
        }

        private void ChangeLight(string propertyName)
        {
            ushort temp = 1;
            Type type = objMasterConductor.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            var value = (ushort[])propertyInfo.GetValue(objMasterConductor);
            for (var i = 0; i < value.Length; i++)
            {
                var propertyValue = (ObservableCollection<MasterModel>) this.GetType().GetProperty("SystemFaultNo" + (i+1).ToString()).GetValue(this);
                for (var j = 0; j < propertyValue.Count; j++)
                {
                    if ((value[i] & temp) == temp)
                    {
                        propertyValue[j].Light = true;
                    }
                    else
                        propertyValue[j].Light = false;
                    temp <<= 1;
                }
                temp = 1;
            }
        }

    }
}