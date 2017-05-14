using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using WHyProject.Model.Conductor;

namespace WHyProject.ModelForShow
{
    public class MasterModel : ViewModelBase
    {
        private double _data;
        private string _img;
        private bool _light;
        public string Info { get; set; }

        public MasterModel()
        {

        }

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

                return _img;

            }
            set
            {
                if (Light == true)
                    _img = AppDomain.CurrentDomain.BaseDirectory + "Images\\red.jpg";
                else
                    _img = AppDomain.CurrentDomain.BaseDirectory + "Images\\green.jpg";
                RaisePropertyChanged(() => Img);
            }

        }
        public string Unit { get; set; }
    }
}
