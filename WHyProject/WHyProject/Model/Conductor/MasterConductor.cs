using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHyProject.Model.Conductor
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MasterConductor : AbstractConductor
    {
        /// <summary>
        /// Initializes a new instance of the MasterConductor class.
        /// </summary>
        public MasterConductor()
        {
            Control1 = new double[8];
            Control2 = new double[17];
            Control3 = new double[16];
        }
    }
}
