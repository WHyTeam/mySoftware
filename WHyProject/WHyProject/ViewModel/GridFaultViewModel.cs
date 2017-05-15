using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;
using System.Collections.ObjectModel;
using System;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using WHyProject.ModelForShow;

namespace WHyProject.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GridFaultViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;
        private ObservableCollection<MasterModel> _gridFaultData1;
        private ObservableCollection<MasterModel> _gridFaultData2;
        private ObservableCollection<MasterModel> _gridFaultData3;
        private ObservableCollection<MasterModel> _gridFaultData4;
        private ObservableCollection<string> _gridFaultText;

        string[] GridFaultText1 =
        {
            "网侧RS过压","网侧ST过压","网侧TR过压","网侧过频",
            "网侧欠频","网侧LVRT","网侧电压不平衡","电压紧急过压",
            "网侧RS欠压","网侧ST欠压","网侧TR欠压","保留",
            "保留","保留","保留","初始化失败"
        };

        string[] GridFaultText2 =
        {
            "网侧硬件过流","网侧模块R","网侧模块S","网侧模块T",
            "网侧霍尔+24V","母线硬件过压","网侧断路器闭合","网侧断路器断开",
            "机侧555","系统555","网侧控制电源","网侧软启闭合",
            "网侧软启断开","网侧霍尔-24V","网侧断路器放电","网侧电感过温"
        };

        string[] GridFaultText3 =
        {
            "网侧电感过流R","网侧电感过流S","网侧电感过流T","网侧零序过流",
            "网侧母线过压","网侧母线欠压","网侧模块过温R","网侧模块过温S",
            "网侧模块过温T","系统通讯丢失","保留","网侧电阻软启",
            "网侧PWM软启","网侧相序不匹配","机侧故障","网侧AD零漂大"
        };

        string[] GridFaultText4 =
        {
            "保留","保留","零功率关机失败","网侧放电故障",
            "网侧电网断线","网侧瞬时过流","网侧电流Hall断线","母线快速欠压",
            "Chopper超时","母线快速过压","网侧制动模块","网侧多次过流",
            "网侧RS温度不平衡","网侧ST温度不平衡","网侧TR温度不平衡","网侧并联同步丢失"
        };

        string[] GridLabel =
        {
            "网侧故障字1（主）",
            "网侧故障字2（主）",
            "网侧故障字3（主）",
            "网侧故障字4（主）"
        };
        public ObservableCollection<MasterModel> GridFaultData1
        {
            get
            {
                return _gridFaultData1;
            }
            set
            {
                if (_gridFaultData1 == value)
                    return;
                _gridFaultData1 = value;
            }
        }

        public ObservableCollection<MasterModel> GridFaultData2
        {
            get { return _gridFaultData2; }
            set
            {
                if (_gridFaultData2 == value)
                    return;
                _gridFaultData2 = value;
            }
        }

        public ObservableCollection<MasterModel> GridFaultData3
        {
            get { return _gridFaultData3; }
            set
            {
                if (_gridFaultData3 == value)
                    return;
                _gridFaultData3 = value;
            }
        }

        public ObservableCollection<MasterModel> GridFaultData4
        {
            get { return _gridFaultData4; }
            set
            {
                if (_gridFaultData4 == value)
                    return;
                _gridFaultData4 = value;
            }
        }

        public ObservableCollection<string> GridFaultText
        {
            get { return _gridFaultText; }
            set
            {
                if (_gridFaultText == value)
                    return;
                _gridFaultText = value;
            }
        }

        private void InitGridFault(ObservableCollection<MasterModel> listData, string[] Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                listData.Add(new MasterModel() { Info = Text[i], Light = true });
            }
        }

        private void InitGrid()
        {
            GridFaultData1 = new ObservableCollection<MasterModel>();
            InitGridFault(GridFaultData1, GridFaultText1);

            GridFaultData2 = new ObservableCollection<MasterModel>();
            InitGridFault(GridFaultData2, GridFaultText2);

            GridFaultData3 = new ObservableCollection<MasterModel>();
            InitGridFault(GridFaultData3, GridFaultText3);

            GridFaultData4 = new ObservableCollection<MasterModel>();
            InitGridFault(GridFaultData4, GridFaultText4);
        }
        /// <summary>
        /// Initializes a new instance of the GridFaultViewModel class.
        /// </summary>
        public GridFaultViewModel()
        {

            InitGrid();
            GridFaultText = new ObservableCollection<string>();
            for (int i = 0; i < GridLabel.Length; i++)
            {
                GridFaultText.Add(GridLabel[i]);
            }

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Sender is MasterConductor)
                {
                    ChangeModelData(message.Notification);
                }
            }
            );

        }

        private void ChangeLight(string propertyName)
        {
            ushort temp = 1;
            Type type = objMasterConductor.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            var value = (ushort[])propertyInfo.GetValue(objMasterConductor);
            for (var i = 0; i < value.Length; i++)
            {
                var propertyValue = (ObservableCollection<MasterModel>)this.GetType().GetProperty("GridFaultData" + (i + 1).ToString()).GetValue(this);
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

        private void ChangeModelData(string PropertyName)
        {

            if (PropertyName == "GridFaultForDisplay")
            {
                ChangeLight(PropertyName);
            }
        }
    }
}