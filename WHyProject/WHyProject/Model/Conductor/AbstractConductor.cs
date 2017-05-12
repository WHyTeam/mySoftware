using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHyProject.Model.Modules;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Threading;

namespace WHyProject.Model.Conductor
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AbstractConductor
    {
        private double[] _control1/* = new double[8] */;
        private double[] _control2/* = new double[17]*/;
        private double[] _control3/* = new double[16]*/;
        
        private DispatcherTimer timer = null;
        private Random rand;
        /// <summary>
        /// Initializes a new instance of the AbstractConductor class.
        /// </summary>
        public AbstractConductor()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(OnTimerHandler);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Start();

            rand = new Random();
        }

        //参数
        public double[] Control1
        {
            get { return _control1; }
            set { _control1 = value;
                // Messenger.Default.Send("Control1");
            }
        }


        public double[] Control2
        {
            get { return _control2; }
            set { _control2 = value;

            }
        }

        public double[] Control3
        {
            get { return _control3; }
            set {
                _control3 = value;

            }
        }

        public ushort GenModuleStatusForDisplay
        {
            get
            {     
                return GenModule.StatusForDisplay; 
            }
            set
            {
                GenModule.StatusForDisplay = value;
                Messenger.Default.Send("GenModuleStatusForDisplay");
            }
        }

        public ushort GenModuleModeForDisplay
        {
            get
            {
                return GenModule.ModeForDisplay;
            }
            set
            {
                GenModule.ModeForDisplay = value;
                Messenger.Default.Send("GenModuleModeForDisplay");
            }
        }

        public ushort GridModuleStatusForDisplay
        {
            get
            {
                return GridModule.StatusForDisplay;
            }
            set
            {
                GridModule.StatusForDisplay = value;
                Messenger.Default.Send("GridModuleStatusForDisplay");
            }
        }

        public ushort GridModuleModeForDisplay
        {
            get
            {
                return GridModule.ModeForDisplay;
            }
            set
            {
                GridModule.ModeForDisplay = value;
                Messenger.Default.Send("GridModuleModeForDisplay");
            }
        }

        public ushort McuModuleModeForDisplay
        {
            get
            {
                return McuModule.ModeForDisplay;
            }
            set
            {
                McuModule.ModeForDisplay = value;
                Messenger.Default.Send("McuModuleModeForDisplay");
            }
        }

        public ushort McuModuleStatusForDisplay
        {
            get
            {
                return McuModule.StatusForDisplay;
            }
            set
            {
                McuModule.StatusForDisplay = value;
                Messenger.Default.Send("McuModuleStatusForDisplay");
            }
        }
        protected GenModule GenModule { get; set; }
        protected GridModule GridModule { get; set; }
        protected McuModule McuModule { get; set; }
        public ushort TorqueRef { get; set; }//转矩
        public short PowerRef { get; set; }//
        public short PhiRed { get; set; }//

        private void OnTimerHandler(Object Sender, EventArgs e)
        {
    
            for (int i = 0; i < 8; i++)
            {
                Control1[i] = rand.Next(100, 2000);
            }
            Messenger.Default.Send("Control1");
            for (int i = 0; i < 17; i++)
            {
                Control2[i] = rand.Next(100, 2000);
            }
            Messenger.Default.Send("Control2");
            for (int i = 0; i < 16; i++)
            {
                Control3[i] = rand.Next(100, 2000);
            }
            Messenger.Default.Send("Control3");

            GenModuleStatusForDisplay = (ushort)rand.Next(0, 2000);
            GenModuleModeForDisplay = (ushort)rand.Next(0, 2000);

            GridModuleModeForDisplay = (ushort)rand.Next(0, 2000);
            GridModuleStatusForDisplay = (ushort)rand.Next(0, 2000);

            McuModuleModeForDisplay = (ushort)rand.Next(0, 2000);
            McuModuleStatusForDisplay = (ushort)rand.Next(0, 2000);
        }

    }
}
