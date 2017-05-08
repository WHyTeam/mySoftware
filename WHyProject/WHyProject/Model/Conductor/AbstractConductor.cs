using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHyProject.Model.Modules;

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
            set { _control1 = value; }
        }

        public double[] Control2
        {
            get { return _control2; }
            set { _control2 = value; }
        }

        public double[] Control3
        {
            get { return _control3; }
            set { _control3 = value; }
        }

        public GenModule GenModule { get; set; }
        public GridModule GridModule { get; set; }
        public McuModule McuModule { get; set; }
        public ushort TorqueRef { get; set; }//转矩
        public short PowerRef { get; set; }//
        public short PhiRed { get; set; }//
    }
}
