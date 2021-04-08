using System.ComponentModel;
using TD = Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;
using System.Globalization;

namespace TeklaStairWpfPlugin
{
  
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private double _stairWidth = 0;
        private double _stairOffset = 0;
        private string _stringerProfile = string.Empty;
        private string _stringerMaterial = string.Empty;
        private string _stepProfile = string.Empty;
        private string _stepMaterial = string.Empty;
        private int _stepTypeIndex;
        private int _stairTypeIndex;
        private int _angleOfStairIndex;
        private int _isStepsRibbed;
        private int _isStepsCells;
        private int _isStepsBoxShaped;

        [StructuresDialog("stairWidth", typeof(TD.Double))]
        public double StairWidth
        {
            get { return _stairWidth; }
            set { _stairWidth = value; OnPropertyChanged("StairWidth"); }
        }

        [StructuresDialog("stairOffset", typeof(TD.Double))]
        public double StairOffset
        {
            get { return _stairOffset; }
            set { _stairOffset = value; OnPropertyChanged("StairOffset"); }
        }

        [StructuresDialog("stringerProfile", typeof(TD.String))]
        public string StringerProfile
        {
            get { return _stringerProfile; }
            set { _stringerProfile = value; OnPropertyChanged("StringerProfile"); }
        }

        [StructuresDialog("stringerMaterial", typeof(TD.String))]
        public string StringerMaterial
        {
            get { return _stringerMaterial; }
            set { _stringerMaterial = value; OnPropertyChanged("StringerMaterial"); }
        }

        [StructuresDialog("stepProfile", typeof(TD.String))]
        public string StepProfile
        {
            get { return _stepProfile; }
            set { _stepProfile = value; OnPropertyChanged("StepProfile"); }
        }

        [StructuresDialog("stepMaterial", typeof(TD.String))]
        public string StepMaterial
        {
            get { return _stepMaterial; }
            set { _stepMaterial = value; OnPropertyChanged("StepMaterial"); }
        }

        [StructuresDialog("stepTypeIndex", typeof(TD.Integer))]
        public int StepTypeIndex
        {
            get { return _stepTypeIndex; }
            set { _stepTypeIndex = value; OnPropertyChanged("StepTypeIndex"); }
        }

        [StructuresDialog("stairTypeIndex", typeof(TD.Integer))]
        public int StairTypeIndex
        {
            get { return _stairTypeIndex; }
            set { _stairTypeIndex = value; OnPropertyChanged("StairTypeIndex"); }
        }

        [StructuresDialog("angleOfStairIndex", typeof(TD.Integer))]
        public int AngleOfStairIndex
        {
            get { return _angleOfStairIndex; }
            set { _angleOfStairIndex = value; OnPropertyChanged("AngleOfStairIndex"); }
        }

        [StructuresDialog("isStepsRibbed", typeof(TD.Integer))]
        public int IsStepsRibbed
        {
            get { return _isStepsRibbed; }
            set { _isStepsRibbed = value; OnPropertyChanged("IsStepsRibbed"); }
        }

        [StructuresDialog("isStepsCells", typeof(TD.Integer))]
        public int IsStepsCells
        {
            get { return _isStepsCells; }
            set { _isStepsCells = value; OnPropertyChanged("IsStepsCells"); }
        }

        [StructuresDialog("isStepsBoxShaped", typeof(TD.Integer))]
        public int IsStepsBoxShaped
        {
            get { return _isStepsBoxShaped; }
            set { _isStepsBoxShaped = value; OnPropertyChanged("IsStepsBoxShaped"); }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }

}
