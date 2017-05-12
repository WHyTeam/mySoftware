using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHyProject.Model.Modules
{  /// <summary>
   /// This class contains properties that a View can data bind to.
   /// <para>
   /// See http://www.galasoft.ch/mvvm
   /// </para>
   /// </summary>
    public abstract class AbstractModule 
    {

        /// <summary>
        /// Initializes a new instance of the AbstractModule class.
        /// </summary>
        public ushort ModeForDisplay { get; set; }

        public ushort StatusForDisplay { get; set; }


        public ushort[] FaultForDisplay { get; set; }

        public ushort ModeForSet { get; set; }

        public ushort ModeReadForSet { get; set; }
    }
}
