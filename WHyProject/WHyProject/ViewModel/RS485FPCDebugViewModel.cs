using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;
using System.Collections.ObjectModel;
using System;
using System.Windows.Threading;

namespace WHyProject.ViewModel
{
     ///<summary>
    /// This class is created to show data in listbox
    /// </summary>
    public class RS485DebugFPCData : ViewModelBase
    {

        private double _data;
        private string _img;
        private bool _light;
        public string Info { get; set; }
       
        public bool Light
        {
            get { return _light; }
            set
            {
                _light = value;
                Img = null;
            }

        }
        public double Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
            }
        }

        public string Img
        {
            get
            {
                if (Light == true)
                    return "D:/huyue/C/C#_pipi/WHyProject/WHyProject/Res/red.jpg";
                else
                    return "D:/huyue/C/C#_pipi/WHyProject/WHyProject/Res/green.jpg";

            }
            set
            {
                if (Light == true)
                    _img = "D:/huyue/C/C#_pipi/WHyProject/WHyProject/Res/red.jpg";
                else
                    _img =  "D:/huyue/C/C#_pipi/WHyProject/WHyProject/Res/green.jpg";
                RaisePropertyChanged(() => Img);

            }

        } 
        public string Unit { get; set; }
    }

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class RS485FPCDebugViewModel : ViewModelBase
    {
        private MasterConductor objMasterConductor = new MasterConductor();
        private ObservableCollection<RS485DebugFPCData> rs485ListData1;
        private ObservableCollection<RS485DebugFPCData> rs485ListData2;
        private ObservableCollection<RS485DebugFPCData> rs485ListData3;
        private ObservableCollection<RS485DebugFPCData> rs485Light1;
        private ObservableCollection<RS485DebugFPCData> rs485Light2;
        private ObservableCollection<RS485DebugFPCData> rs485Light3;
        private ObservableCollection<RS485DebugFPCData> rs485Light4;
        private ObservableCollection<RS485DebugFPCData> rs485Light5;
        private ObservableCollection<RS485DebugFPCData> rs485Light6;


        string[] LightInfo1 = { "初始化", "待机", "启动", "并网", "关机", "急停" };
        string[] LightInfo2 = { "准备好启动", "制动自检", "风扇启动", "紧急状态", "TBD", "TBD", "发生故障", "心跳" };
        string[] LightInfo3 = { "待机", "软启", "运行", "关机", "放电", "调试" };
        string[] LightInfo4 = { "准备好并网", "制动状态", "软启状态", "断路器状态", "TBD", "制动休眠", "发生故障", "心跳" };
        string[] LightInfo5 = { "待机", "运行", "关机", "调试" };
        string[] LightInfo6 = { "网侧故障", "励磁状态", "断路器状态", "弱磁状态", "电流限幅", "能量循环", "发生故障", "心跳" };
        public override void Cleanup()
        {
            base.Cleanup();
            timer.Stop();
        }

        public ObservableCollection<RS485DebugFPCData> RS485ListData1
        {
            get
            {
                return rs485ListData1;
            }
            set
            {
                rs485ListData1 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485ListData2
        {
            get
            {
                return rs485ListData2;
            }
            set
            {
                rs485ListData2 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485Light1
        {
            get
            {
                return rs485Light1;
            }
            set
            {
                rs485Light1 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485ListData3
        {
            get
            {
                return rs485ListData3;
            }
            set
            {
                rs485ListData3 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485Light2
        {
            get
            {
                return rs485Light2;
            }
            set
            {
                rs485Light2 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485Light3
        {
            get
            {
                return rs485Light3;
            }
            set
            {
                rs485Light3 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485Light4
        {
            get
            {
                return rs485Light4;
            }
            set
            {
                rs485Light4 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485Light5
        {
            get
            {
                return rs485Light5;
            }
            set
            {
                rs485Light5 = value;
            }
        }

        public ObservableCollection<RS485DebugFPCData> RS485Light6
        {
            get
            {
                return rs485Light6;
            }
            set
            {
                rs485Light6 = value;
            }
        }

        private DispatcherTimer timer = null;
        private Random rand;
        /// <summary>
        /// Initializes a new instance of the RS485FPCDebugViewModel class.
        /// </summary>
        public RS485FPCDebugViewModel()
        {
            string[] TextBlockInfo = { "控制柜温度", "TBD", "TBD", "开关柜温度",
                                        "冷却水温度","冷却水压力","TBD","TBD",
                                        "电网电压RS","电网电压ST","电网电压TR","电网频率",
                                        "电网电流R","电网电流S","电网电流T","电网有功",
                                        "电感电流R","电感电流S","电感电流T","电网无功",
                                        "母线电压","模块温度R","模块温度S","模块温度T",
                                        "制动恢复时间",
                                        "定子电压UV","定子电压VW","定子电压WU","电机频率",
                                        "定子电流U","定子电流V","定子电流W","电机有功",
                                        "励磁电流","故障编号","机侧功率因数","电磁转矩",
                                        "母线电压","模块温度U","模块温度V","模块温度W"
                                     };

            string[] TextBlockUnit =
            {
                "C"," "," ","C",
                "C","bar"," "," ",
                "V","V","V","Hz",
                "A","A","A","KW",
                "A","A","A","KVar",
                "V","C","C","C",
                "min",
                "V","V","V","Hz",
                "A","A","A","KW",
                "A"," "," ","%",
                "V","C","C","C"

            };

            
           
            RS485ListData1 = new ObservableCollection<RS485DebugFPCData>();
            for(int i = 0; i < 8; i ++)
            {
                RS485ListData1.Add(new RS485DebugFPCData() { Info = TextBlockInfo[i], Data = 0, Unit = TextBlockUnit[i]});
            }

            RS485ListData2 = new ObservableCollection<RS485DebugFPCData>();
            for(int i = 8; i < 25; i ++)
            {
                RS485ListData2.Add(new RS485DebugFPCData() { Info = TextBlockInfo[i], Data = 0, Unit = TextBlockUnit[i] });
            }

            RS485ListData3 = new ObservableCollection<RS485DebugFPCData>();
            for(int i = 25; i < 41; i ++)
            {
                RS485ListData3.Add(new RS485DebugFPCData() { Info = TextBlockInfo[i], Data = 0, Unit = TextBlockUnit[i] });
            }

            RS485Light1 = new ObservableCollection<RS485DebugFPCData>();
            for(int i = 0; i < LightInfo1.Length; i ++)
            {
                RS485Light1.Add(new RS485DebugFPCData() { Info = LightInfo1[i],Light = true });
            }

            RS485Light2 = new ObservableCollection<RS485DebugFPCData>();
            for(int i = 0; i < LightInfo2.Length;i ++)
            {
                RS485Light2.Add(new RS485DebugFPCData() { Info = LightInfo2[i], Light = true });
            }

            RS485Light3 = new ObservableCollection<RS485DebugFPCData>();
            for (int i = 0; i < LightInfo3.Length; i++)
            {
                RS485Light3.Add(new RS485DebugFPCData() { Info = LightInfo3[i], Light = true });
            }

            RS485Light4 = new ObservableCollection<RS485DebugFPCData>();
            for (int i = 0; i < LightInfo4.Length; i++)
            {
                RS485Light4.Add(new RS485DebugFPCData() { Info = LightInfo4[i], Light = true });
            }

            RS485Light5 = new ObservableCollection<RS485DebugFPCData>();
            for (int i = 0; i < LightInfo5.Length; i++)
            {
                RS485Light5.Add(new RS485DebugFPCData() { Info = LightInfo5[i], Light = true });
            }

            RS485Light6 = new ObservableCollection<RS485DebugFPCData>();
            for (int i = 0; i < LightInfo6.Length; i++)
            {
                RS485Light6.Add(new RS485DebugFPCData() { Info = LightInfo6[i], Light = true });
            }

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(OnTimerHandler);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Start();

            rand = new Random();
        }


        private void OnTimerHandler(Object Sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                RS485ListData1[i].Data = rand.Next(100, 2000);
            }

            for(int i = 0; i < 17; i ++)
            {
                RS485ListData2[i].Data = rand.Next(100, 2000);
            }

            for(int i = 0;i < 16; i ++)
            {
                RS485ListData3[i].Data = rand.Next(100, 2000);
            }

            for(int i = 0; i < LightInfo1.Length; i ++ )
            {
                int temp = rand.Next(0, 100);
                if (temp <= 50)
                    RS485Light1[i].Light = true;
                else
                    RS485Light1[i].Light = false;    

            }
        }
    }
}