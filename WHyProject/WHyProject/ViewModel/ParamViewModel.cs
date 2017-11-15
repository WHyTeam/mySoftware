using GalaSoft.MvvmLight;
using System;
using System.Data;
using System.Xml;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WHyProject.ViewModel
{
    public class ParamModel:ViewModelBase
    {

        public ParamModel() {
            Children = new ObservableCollection<ParamModel>();
        }

        private string _machineCode;

        public string MachineCode
        {
            get { return _machineCode; }
            set { _machineCode = value; }
        }

        private double _index;
        public double Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _cDescription;
        public string CDescription
        {
            get { return _cDescription; }
            set { _cDescription = value; }
        }

        private double _currentValue;
        public double CurrentValue
        {
            get { return _currentValue; }
            set { _currentValue = value;
                RaisePropertyChanged(() => CurrentValue);
            }
        }

        private double _defaultValue;
        public double DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        private double _max;
        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        private double _min;
        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public ObservableCollection<ParamModel> Children { get; set; }
    }
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ParamViewModel : ViewModelBase
    {
        private XmlDocument _xDoc;
        private ObservableCollection<ParamModel> _paramList;
        string[] _dataGrid = { "变流器", "索引", "参数名", "英文名", "当前值", "默认值", "最小值", "最大值" };

        private ObservableCollection<ParamModel> _treeview;
        private RelayCommand<ParamModel> _selectItemCommand;

        public RelayCommand<ParamModel> SelectItemCommand
        {
            get
            {
                if(_selectItemCommand == null)
                {
                    _selectItemCommand = new RelayCommand<ParamModel>(x => ExecuteSelectItem(x));
                }
                return _selectItemCommand;
            }
        }

        public ObservableCollection<ParamModel> treeview
        {
            get { return _treeview; }
            set { _treeview = value;
                RaisePropertyChanged(() => treeview);
            }
        }
        

        public ObservableCollection<ParamModel> ParamList
        {
            get { return _paramList; }
            set
            {
                if (_paramList == value) return;
                _paramList = value;
                RaisePropertyChanged(() => ParamList);
            }
        }
        public string[] DataGrid
        {
            get { return _dataGrid; }
        }
        public XmlDocument XDoc
        {
            get
            {
                return _xDoc;
            }
            set
            {
                _xDoc = value;
                RaisePropertyChanged(() => XDoc);
            }
        }

        private void ExecuteSelectItem(ParamModel Item)
        {
            ParamList.Clear();
            if (Item.Index == 0)
            {
                foreach (var param in Item.Children)
                    ParamList.Add(param);
            }
           else
                ParamList.Add(Item);
        }
        /// <summary>
        /// Initializes a new instance of the ParamViewModel class.
        /// </summary>

        public ParamViewModel()
        {
            ParamList = new ObservableCollection<ParamModel>();
            treeview = new ObservableCollection<ParamModel>();
            

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("D:/HY/C#/Why3/WHyProject/WHyProject/Images/ConfigFPC232.xml");

            XmlNode Machine = xmlDoc.SelectSingleNode("//MachineCode");
            string code = Machine.InnerText;
            ParamModel root = new ParamModel() { CDescription = code };

            XmlNodeList ParamNodeList = xmlDoc.SelectSingleNode("//Parameter").ChildNodes;
            //XmlDataProvider xmlDP = new XmlDataProvider();
            //xmlDP.XPath = "//Performance";
  
            if(ParamNodeList != null)
            {
                foreach (XmlNode ParamNode in ParamNodeList)
                {
                    //获得三组参数表
                    XmlNodeList EachParamList = ParamNode.ChildNodes;
                    ParamModel Node = new ParamModel() { CDescription = ParamNode.Name };
                    foreach(XmlNode EachParm in EachParamList)
                    {
                        XmlNodeList a = EachParm.ChildNodes;
                        //根据XML文件分别获取各参数的值
                        //ParamList.Add(new ParamModel()
                        //{
                        //    Index = Convert.ToDouble(a.Item(0).InnerText),
                        //    Name = a.Item(1).InnerText,
                        //    CDescription = a.Item(2).InnerText,
                        //    CurrentValue = 0,
                        //    DefaultValue = Convert.ToDouble(a.Item(3).InnerText),
                        //    Max = Convert.ToDouble(a.Item(4).InnerText),
                        //    Min = Convert.ToDouble(a.Item(5).InnerText),
                        //});
                        ParamModel temp = new ParamModel()
                        {
                            MachineCode = code,
                            Index = Convert.ToDouble(a.Item(0).InnerText),
                            Name = a.Item(1).InnerText,
                            CDescription = a.Item(2).InnerText,
                            CurrentValue = 0,
                            DefaultValue = Convert.ToDouble(a.Item(3).InnerText),
                            Max = Convert.ToDouble(a.Item(4).InnerText),
                            Min = Convert.ToDouble(a.Item(5).InnerText),
                        };
                        Node.Children.Add(temp);
                        ParamList.Add(temp);
      
                    }
                    root.Children.Add(Node);
                  
                  
                }
                treeview.Add(root);
            } 
           
        }
    }
}