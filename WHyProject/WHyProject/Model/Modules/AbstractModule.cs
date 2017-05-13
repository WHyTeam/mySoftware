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
        private ushort _modeForDisplay;
        private ushort _statusForDisplay;
        private ushort[] _faultForDisplay;
        private ushort _modeForSet;
        private ushort _modeReadForSet;

        /// <summary>
        /// Initializes a new instance of the AbstractModule class.
        /// </summary>
        public ushort ModeForDisplay
        {
            get { return _modeForDisplay; }
            set { _modeForDisplay = value; }
        }

        public ushort StatusForDisplay
        {
            get { return _statusForDisplay; }
            set { _statusForDisplay = value; }
        }


        public ushort[] FaultForDisplay
        {
            get { return _faultForDisplay; }
            set { _faultForDisplay = value; }
        }

        public ushort ModeForSet
        {
            get { return _modeForSet; }
            set { _modeForSet = value; }
        }

        public ushort ModeReadForSet
        {
            get { return _modeReadForSet; }
            set { _modeReadForSet = value; }
        }
    }
}
