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
    public class SlaveConductor : AbstractConductor
    {
        /// <summary>
        /// Initializes a new instance of the SlaveConductor class.
        /// </summary>
        public SlaveConductor()
        {
            Control1 = new double[8];
            Control2 = new double[17];
            Control3 = new double[16];
            GridModule = new GridModule();
            GenModule = new GenModule();
            McuModule = new McuModule();
        }

        private static SlaveConductor _slave = new SlaveConductor();

        public static SlaveConductor Slave
        {
            get { return _slave; }
        }
    }
}
