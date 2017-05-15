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

        #region GenModule
        public ushort[] GenFaultForDisplay
        {
            get { return GenModule.FaultForDisplay; }
            set
            {
                GenModule.FaultForDisplay = value;
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
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "GenModuleStatusForDisplay"));
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
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "GenModuleModeForDisplay"));
            }
        }
        #endregion

        #region GridModule
        public ushort[] GridFaultForDisplay
        {
            get { return GridModule.FaultForDisplay; }
            set
            {
                GridModule.FaultForDisplay = value;
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
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "GridModuleStatusForDisplay"));
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
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "GridModuleModeForDisplay"));
            }
        }

        #endregion

        #region MCUModule

        public ushort McuModuleModeForDisplay
        {
            get
            {
                return McuModule.ModeForDisplay;
            }
            set
            {
                McuModule.ModeForDisplay = value;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "McuModuleModeForDisplay"));
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
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "McuModuleStatusForDisplay"));
            }
        }

        public ushort[] SystemFaultForDisplay
        {
            get { return McuModule.FaultForDisplay; }
            set { McuModule.FaultForDisplay = value; }
        }
        #endregion

        public GenModule GenModule { get; set; }
        public GridModule GridModule { get; set; }
        public McuModule McuModule { get; set; }
        public ushort TorqueRef { get; set; }//转矩
        public short PowerRef { get; set; }//
        public short PhiRed { get; set; }//

        private void OnTimerHandler(Object sender, EventArgs e)
        {
    
            for (var i = 0; i < 8; i++)
            {
                Control1[i] = rand.Next(100, 2000);
            }
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this,"Control1"));
            for (var i = 0; i < 17; i++)
            {
                Control2[i] = rand.Next(100, 2000);
            }
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this,"Control2"));
            for (var i = 0; i < 16; i++)
            {
                Control3[i] = rand.Next(100, 2000);
            }
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "Control3"));

            for (var i = 0; i < 4; i++)
            {
                SystemFaultForDisplay[i] = (ushort)rand.Next(0, 2000);
            }
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "SystemFaultForDisplay"));

            for (int i = 0; i < 4; i++)
            {
                GridFaultForDisplay[i] = (ushort)rand.Next(100, 2000);
            }
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "GridFaultForDisplay"));

            for (int i = 0; i < 4; i++)
            {
                GenFaultForDisplay[i] = (ushort)rand.Next(100, 2000);
            }
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "GenFaultForDisplay"));
            GenModuleStatusForDisplay = (ushort)rand.Next(0, 2000);
            GenModuleModeForDisplay = (ushort)rand.Next(0, 2000);

            GridModuleModeForDisplay = (ushort)rand.Next(0, 2000);
            GridModuleStatusForDisplay = (ushort)rand.Next(0, 2000);

            McuModuleModeForDisplay = (ushort)rand.Next(0, 2000);
            McuModuleStatusForDisplay = (ushort)rand.Next(0, 2000);
        }

    }
}
