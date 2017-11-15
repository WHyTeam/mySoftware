using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
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
    public class DebugViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;
        private ObservableCollection<MasterModel> rs485ListData1;
        private ObservableCollection<MasterModel> rs485ListData2;
        private ObservableCollection<MasterModel> rs485ListData3;
        private ObservableCollection<MasterModel> rs485Light1;
        private ObservableCollection<MasterModel> rs485Light2;
        private ObservableCollection<MasterModel> rs485Light3;
        private ObservableCollection<MasterModel> rs485Light4;
        private ObservableCollection<MasterModel> rs485Light5;
        private ObservableCollection<MasterModel> rs485Light6;

        string[] TextBlockInfo1 = { "控制柜温度", "TBD", "TBD", "开关柜温度",
                                    "冷却水温度","冷却水压力","TBD","TBD"};

        string[] TextBlockInfo2 = { "电网电压RS","电网电压ST","电网电压TR","电网频率",
                                    "电网电流R","电网电流S","电网电流T","电网有功",
                                    "电感电流R","电感电流S","电感电流T","电网无功",
                                    "母线电压","模块温度R","模块温度S","模块温度T",
                                    "制动恢复时间"};

        string[] TextBlockInfo3 = { "定子电压UV","定子电压VW","定子电压WU","电机频率",
                                        "定子电流U","定子电流V","定子电流W","电机有功",
                                        "励磁电流","故障编号","机侧功率因数","电磁转矩",
                                        "母线电压","模块温度U","模块温度V","模块温度W"};


        string[] TextBlockUnit1 =
        {
            "C"," "," ","C",
            "C","bar"," "," "
        };
        string[] TextBlockUnit2 =
        {
            "V","V","V","Hz",
            "A","A","A","KW",
            "A","A","A","KVar",
            "V","C","C","C",
            "min"
        };

        string[] TextBlockUnit3 =
        {
            "V","V","V","Hz",
            "A","A","A","KW",
            "A"," "," ","%",
            "V","C","C","C"
        }; 



        string[] LightInfo1 = { "初始化", "待机", "启动", "并网", "关机", "急停" };
        string[] LightInfo2 = { "准备好启动", "制动自检", "风扇启动", "紧急状态", "TBD", "TBD", "发生故障", "心跳" };
        string[] LightInfo3 = { "待机", "软启", "运行", "关机", "放电", "调试" };
        string[] LightInfo4 = { "准备好并网", "制动状态", "软启状态", "断路器状态", "TBD", "制动休眠", "发生故障", "心跳" };
        string[] LightInfo5 = { "待机", "运行", "关机", "调试" };
        string[] LightInfo6 = { "网侧故障", "励磁状态", "断路器状态", "弱磁状态", "电流限幅", "能量循环", "发生故障", "心跳" };
        public override void Cleanup()
        {
            base.Cleanup();
            //timer.Stop();
        }

        public ObservableCollection<MasterModel> RS485ListData1
        {
            get
            {
                return rs485ListData1;
            }
            set
            {
                if (rs485ListData1 == value)
                    return;
                rs485ListData1 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485ListData2
        {
            get
            {
                return rs485ListData2;
            }
            set
            {
                if (rs485ListData2 == value)
                    return;
                rs485ListData2 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485Light1
        {
            get
            {
                return rs485Light1;
            }
            set
            {
                if (rs485Light1 == value)
                    return;
                rs485Light1 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485ListData3
        {
            get
            {
                return rs485ListData3;
            }
            set
            {
                if (rs485ListData3 == value)
                    return;
                rs485ListData3 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485Light2
        {
            get
            {
                return rs485Light2;
            }
            set
            {
                if (rs485Light2 == value)
                    return;
                rs485Light2 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485Light3
        {
            get
            {
                return rs485Light3;
            }
            set
            {
                if (rs485Light3 == value)
                    return;
                rs485Light3 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485Light4
        {
            get
            {
                return rs485Light4;
            }
            set
            {
                if (rs485Light4 == value)
                    return;
                rs485Light4 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485Light5
        {
            get
            {
                return rs485Light5;
            }
            set
            {
                if (rs485Light5 == value)
                    return;
                rs485Light5 = value;
            }
        }

        public ObservableCollection<MasterModel> RS485Light6
        {
            get
            {
                return rs485Light6;
            }
            set
            {
                if (rs485Light6 == value)
                    return;
                rs485Light6 = value;
            }
        }

        private void InitRS485List(
            ObservableCollection<MasterModel> ListData,
            string[] TextBlock,
            string[] TextBox)
        {
            //ListData = new ObservableCollection<MasterModel>();
            for(int i = 0; i < TextBlock.Length;i ++)
            {
                ListData.Add(new MasterModel() { Info = TextBlock[i], Data = 0, Unit = TextBox[i]});
             }
        }

        private void InitDebugData()
        {
            RS485ListData1 = new ObservableCollection<MasterModel>();
            InitRS485List(RS485ListData1, TextBlockInfo1, TextBlockUnit1);

            RS485ListData2 = new ObservableCollection<MasterModel>();
            InitRS485List(RS485ListData2, TextBlockInfo2, TextBlockUnit2);

            RS485ListData3 = new ObservableCollection<MasterModel>();
            InitRS485List(RS485ListData3, TextBlockInfo3, TextBlockUnit3);
        }

        private void InitLight(ObservableCollection<MasterModel> ListData, string[] Info)
        {
            for(int i = 0; i < Info.Length; i ++)
            {
               ListData.Add(new MasterModel() { Info = Info[i], Light = true });
            }
        }

        private void InitDebugLight()
        {
            RS485Light1 = new ObservableCollection<MasterModel>();
            InitLight(RS485Light1, LightInfo1);

            RS485Light2 = new ObservableCollection<MasterModel>();
            InitLight(RS485Light2, LightInfo2);

            RS485Light3 = new ObservableCollection<MasterModel>();
            InitLight(RS485Light3, LightInfo3);

            RS485Light4 = new ObservableCollection<MasterModel>();
            InitLight(RS485Light4, LightInfo4);

            RS485Light5 = new ObservableCollection<MasterModel>();
            InitLight(RS485Light5, LightInfo5);

            RS485Light6 = new ObservableCollection<MasterModel>();
            InitLight(RS485Light6, LightInfo6);
        }

        /// <summary>
        /// Initializes a new instance of the DebugViewModel class.
        /// </summary>
        public DebugViewModel()
        {

            InitDebugData();
            InitDebugLight();
            MessageToPropertyForText = new Dictionary<string, string>
            {
                {"Control1", "RS485ListData1"},
                {"Control2", "RS485ListData2"},
                {"Control3", "RS485ListData3"}
            };

            MessageToPropertyForLight = new Dictionary<string, string>
            {
                {"GenModuleStatusForDisplay", "RS485Light1" },
                {"GenModuleModeForDisplay", "RS485Light2" },
                {"GridModuleModeForDisplay", "RS485Light3" },
                {"GridModuleStatusForDisplay", "RS485Light4" },
                {"McuModuleModeForDisplay", "RS485Light5" },
                {"McuModuleStatusForDisplay", "RS485Light6" },
            };

        //Messenger.Default.Register<string>(this,
            //(message) => {
            //            ChangeModelData(message);
            //            //do what you want 
            //             });
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Sender is MasterConductor)
                {
                    ChangeModelData(message.Notification);
                }
            });
        }

        //private void ChangeListData(string propertyName, ObservableCollection<MasterModel> RS485ListData)
        //{
        //    Type type = objMasterConductor.GetType();
        //    System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
        //    double[] temp = (double[])propertyInfo.GetValue(objMasterConductor);
        //    for (int i = 0; i < RS485ListData.Count; i ++)
        //    {

        //        RS485ListData[i].Data = temp[i];
        //    }
        //}

        private void ChangeListData(string propertyName)
        {
            if (!MessageToPropertyForText.ContainsKey(propertyName))
                return;
            Type type = objMasterConductor.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            var temp = (double[])propertyInfo.GetValue(objMasterConductor);
            propertyInfo = this.GetType().GetProperty(MessageToPropertyForText[propertyName]);
            var listData = (ObservableCollection<MasterModel>)propertyInfo.GetValue(this);
            for (int i = 0; i < listData.Count; i++)
            {
                listData[i].Data = temp[i];
            }
        }


        //private void ChangeLight(string propertyName, ObservableCollection<MasterModel> RS485Light)
        //{
        //    ushort temp = 1;
        //    Type type = objMasterConductor.GetType();
        //    System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
        //    ushort value = (ushort)propertyInfo.GetValue(objMasterConductor);
        //    for (int i = 0; i < RS485Light.Count; i++)
        //    {
        //        if ((value & temp) == temp)
        //        {
        //            RS485Light[i].Light = true;
        //        }
        //        else
        //            RS485Light[i].Light = false;
        //        temp <<= 1;
        //    }
        //}

        private void ChangeLight(string propertyName)
        {
            if(!MessageToPropertyForLight.ContainsKey(propertyName)) return;
            ushort temp = 1;
            Type type = objMasterConductor.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            var value = (ushort)propertyInfo.GetValue(objMasterConductor);
            propertyInfo = this.GetType().GetProperty(MessageToPropertyForLight[propertyName]);
            var light = (ObservableCollection<MasterModel>)propertyInfo.GetValue(this);
            for (int i = 0; i < light.Count; i++)
            {
                if ((value & temp) == temp)
                {
                    light[i].Light = true;
                }
                else
                    light[i].Light = false;
                temp <<= 1;
            }
        }

        private readonly Dictionary<string, string> MessageToPropertyForText;
        private readonly Dictionary<string, string> MessageToPropertyForLight;

        void ChangeModelData(string propertyName)
        {
            //ChangeListData("Control1", RS485ListData1);
            //ChangeListData("Control2", RS485ListData2);
            // ChangeListData("Control3", RS485ListData3);
            if(MessageToPropertyForText.ContainsKey(propertyName))
                ChangeListData(propertyName);
            if(MessageToPropertyForLight.ContainsKey(propertyName))
                ChangeLight(propertyName);
            //ChangeLight("GenModuleStatusForDisplay", RS485Light1);
            //ChangeLight("GenModuleModeForDisplay", RS485Light2);
            //ChangeLight("GridModuleModeForDisplay", RS485Light3);
            //ChangeLight("GridModuleStatusForDisplay", RS485Light4);
            //ChangeLight("McuModuleModeForDisplay", RS485Light5);
            //ChangeLight("McuModuleStatusForDisplay", RS485Light6);
        }

        
    }
}