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
        


        //HY::用于系统维护窗口显示的底层数据
        string _recvMaintainStr;
        /// <summary>
        /// Initializes a new instance of the AbstractConductor class.
        /// </summary>
        public AbstractConductor()
        {

            
        }

        //参数
        public double[] Control1
        {
            get { return _control1; }
            set { _control1 = value;
                // Messenger.Default.Send("Control1");
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "Control1"));
            }
        }


        public double[] Control2
        {
            get { return _control2; }
            set { _control2 = value;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "Control2"));
            }
        }

        public double[] Control3
        {
            get { return _control3; }
            set {
                _control3 = value;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "Control3"));
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

        public ushort GenModuleModeForSet
        {
            get
            {
                return GenModule.ModeForSet;
            }
            set
            {
                GenModule.ModeForSet = value;
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

        public ushort GridModuleModeForSet
        {
            get { return GridModule.ModeForSet;}
            set
            {
                GridModule.ModeForSet = value;
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

        public ushort McuModuleModeForSet
        {
            get { return McuModule.ModeForSet; }
            set
            {
                McuModule.ModeForSet = value;
            }
        }
        #endregion

        public string RecvMaintainStr
        {
            get { return _recvMaintainStr; }
            set
            {
                if (_recvMaintainStr == value) return;
                _recvMaintainStr = value;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "RecvMaintainStr"));
            }
        }

        public GenModule GenModule { get; set; }
        public GridModule GridModule { get; set; }
        public McuModule McuModule { get; set; }
        public ushort TorqueRef { get; set; }//转矩
        public short PowerRef { get; set; }//
        public short PhiRed { get; set; }//
       
    }
}
