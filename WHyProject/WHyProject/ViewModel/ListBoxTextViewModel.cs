using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;
using System.Collections;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Threading;
using System;



namespace WHyProject.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ListBoxShow: ViewModelBase
    {
        //private MasterConductor objMaster = new MasterConductor();
        private int _data;
        public string Info { get; set; }
        public int Data {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
            } }
    }
    public class ListBoxTextViewModel : ViewModelBase
    {
        private MasterConductor objMaster = new MasterConductor();
        private ObservableCollection<ListBoxShow> listBoxData;

        public RelayCommand ChangeDataCommand { get; private set; }

        public override void Cleanup()
        {
            base.Cleanup();
            timer.Stop();
        }
        void ExecuteChangeData()
       {
            //ListBoxData[0].Data = 1400;
            //ListBoxData[1].Data = 700;
            //ListBoxData[2].Data = 800;
            //ListBoxData[3].Data = 900;
            this.ListBoxData[0].Data = 1400;
            EditTextContro1l = 1000;
        }

        bool CanExecuteChangeData()
        {
            return true;
        }
        public ObservableCollection<ListBoxShow> ListBoxData
        {
            get
            {
                return listBoxData;
            }
            set
            {
                listBoxData = value;
               // RaisePropertyChanged(() => ListBoxData);
            }
        }

        private DispatcherTimer timer = null;
        private Random rand;
        /// <summary>
        /// Initializes a new instance of the ListBoxTextViewModel class.
        /// </summary>
        /// 
        #region ListBox
        public ListBoxTextViewModel()
        {
            string[] Infod = { "TCB", "TBV", "电网电压", "电网电流" };
            //ListBoxData = new ObservableCollection<ListBoxShow>()
            //   {
            //       new ListBoxShow(){Info="TVB",Data= 120 },
            //       new ListBoxShow(){Info="TCB",Data= 1200},
            //       new ListBoxShow(){Info="电网",Data= 1330},
            //       new ListBoxShow(){Info="电压",Data= 800},
            //   };
            ListBoxData = new ObservableCollection<ListBoxShow>();
            for (int i = 0; i < 4; i ++)
            { 
                ListBoxData.Add(new ListBoxShow() { Info = Infod[i], Data = 0 });
            }

            ChangeDataCommand = new RelayCommand(ExecuteChangeData, CanExecuteChangeData);
            EditTextContro1l = 120;

            /////////
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(OnTimerHandler);
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();

            ////
            rand = new Random();
        }

        private void OnTimerHandler(Object Sender,EventArgs e)
        {
            EditTextContro1l = rand.Next(100,2000);
            for(int i = 0; i < 4; i ++)
            {
                ListBoxData[i].Data = rand.Next(100, 2000);
            }
        }
        #endregion
        public double[] EditControl1
        {
            get { return objMaster.Control1; }
            set
            {
                objMaster.Control1 = value;
                RaisePropertyChanged(() => EditControl1);
            }
        }

        public double EditTextContro1l
        {
            get { return objMaster.Control1[0]; }
            set
            {
                objMaster.Control1[0] = value;
                RaisePropertyChanged(() => EditTextContro1l);
            }
        }

   
    }
}