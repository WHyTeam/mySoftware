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
    public class GenFaultViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;
        private ObservableCollection<MasterModel> _genFaultData1;
        private ObservableCollection<MasterModel> _genFaultData2;
        private ObservableCollection<MasterModel> _genFaultData3;
        private ObservableCollection<MasterModel> _genFaultData4;
        private ObservableCollection<string> _genFaultText;

        string[] GenFaultText1 =
        {
            "电机过频","电机过压","电机欠频","保留",
            "保留","保留","初始化失败","保留",
            "保留","保留","保留","保留",
            "保留","保留","保留","保留"
        };

        string[] GenFaultText2 =
        {
            "机侧硬件过流","机侧模块U","机侧模块V","机侧模块W",
            "机侧定子开路","母线硬件过压","机侧断路器闭合","机侧断路器断开",
            "网侧555","系统555","机侧电压不平衡","机侧并联同步丢失",
            "机侧多次过流","锁相失败","电机紧急过频","机侧电感过温"
        };

        string[] GenFaultText3 =
        {
            "机侧U相过流","机侧V相过流","机侧W相过流","机侧零序过流",
            "机侧母线过压","机侧母线欠压","机侧模块过温U","机侧模块过温V",
            "机侧模块过温W","系统通讯丢失","零功率关机失败","母线快速过压",
            "母线快速欠压","机侧相序不匹配","瞬时零序过流","机侧AD零漂大"
        };

        string[] GenFaultText4 =
        {
            "机侧Duty饱和","机侧瞬时过流","机侧电流hall断线","并联SPI通讯丢失",
            "励磁失败","励磁瞬时过流","励磁霍尔断线","励磁辅电故障",
            "励磁CBC过流","励磁过温","励磁风扇故障","励磁模块短路",
            "保留","UV温度不平衡","VW温度不平衡","WU温度不平衡"
        };

        string[] GenLabel =
        {
            "机侧故障字1（主）",
            "机侧故障字2（主）",
            "机侧故障字3（主）",
            "机侧故障字4（主）"
        };

        public ObservableCollection<MasterModel> GenFaultData1
        {
            get
            {
                return _genFaultData1;
            }
            set
            {
                if (_genFaultData1 == value)
                    return;
                _genFaultData1 = value;
            }
        }

        public ObservableCollection<MasterModel> GenFaultData2
        {
            get { return _genFaultData2; }
            set
            {
                if (_genFaultData2 == value)
                    return;
                _genFaultData2 = value;
            }
        }

        public ObservableCollection<MasterModel> GenFaultData3
        {
            get { return _genFaultData3; }
            set
            {
                if (_genFaultData3 == value)
                    return;
                _genFaultData3 = value;
            }
        }

        public ObservableCollection<MasterModel> GenFaultData4
        {
            get { return _genFaultData4; }
            set
            {
                if (_genFaultData4 == value)
                    return;
                _genFaultData4 = value;
            }
        }

        public ObservableCollection<string> GenFaultText
        {
            get { return _genFaultText; }
            set
            {
                if (_genFaultText == value)
                    return;
                _genFaultText = value;
            }
        }


        private void InitGenFault(ObservableCollection<MasterModel> listData, string[] Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                listData.Add(new MasterModel() { Info = Text[i], Light = true });
            }
        }

        private void InitGen()
        {
            GenFaultData1 = new ObservableCollection<MasterModel>();
            InitGenFault(GenFaultData1, GenFaultText1);

            GenFaultData2 = new ObservableCollection<MasterModel>();
            InitGenFault(GenFaultData2, GenFaultText2);

            GenFaultData3 = new ObservableCollection<MasterModel>();
            InitGenFault(GenFaultData3, GenFaultText3);

            GenFaultData4 = new ObservableCollection<MasterModel>();
            InitGenFault(GenFaultData4, GenFaultText4);
        }
        /// <summary>
        /// Initializes a new instance of the GenFaultViewModel class.
        /// </summary>
        public GenFaultViewModel()
        {
            InitGen();
            GenFaultText = new ObservableCollection<string>();
            for (int i = 0; i < GenLabel.Length; i++)
            {
                GenFaultText.Add(GenLabel[i]);
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
                var propertyValue = (ObservableCollection<MasterModel>)this.GetType().GetProperty("GenFaultData" + (i + 1).ToString()).GetValue(this);
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
            if (PropertyName == "GenFaultForDisplay")
            {
                ChangeLight(PropertyName);
            }

        }
    }
}