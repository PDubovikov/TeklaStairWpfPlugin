using System;
using System.Collections;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace TeklaStairWpfPlugin
{

    [Plugin("1.450 Лестницы")]
    [PluginUserInterface("TeklaStairWpfPlugin.MainWindow")]
    public class TeklaStairWpfPlugin : PluginBase
    {
        private double _stairWidth = 0 ;
        private double _stairOffset = 0;
        private string _stringerProfile = string.Empty ;
        private string _stringerMaterial = string.Empty ;
        private string _stepProfile = string.Empty ;
        private string _stepMaterial = string.Empty ;
        private int _stepTypeIndex = 0;
        private int _stairTypeIndex = 0;
        private int _angleOfStairIndex = 0;
        private int _isStepsRibbed = 0;
        private int _isStepsCells = 0;
        private int _isStepsBoxShaped = 0;

        private StairData Data { get; set; }
        private Model Model { get; set; }

        public TeklaStairWpfPlugin(StairData data)
        {
            Model = new Model();
            Data = data;
        }


        public override List<InputDefinition> DefineInput()
        {
            List<InputDefinition> PointList = new List<InputDefinition>();
            ArrayList points = new ArrayList();
            Point Point1, Point2;
            Picker pointPicker = new Picker();
           // if(!Model.GetConnectionStatus()) { return PointList; }

            try
            {
                points = pointPicker.PickPoints(Picker.PickPointEnum.PICK_TWO_POINTS);
                Point1 = points[0] as Point;
                Point2 = points[1] as Point;

                InputDefinition Input1 = new InputDefinition(Point1);
                InputDefinition Input2 = new InputDefinition(Point2);

                //Add inputs to InputDefinition list.
                PointList.Add(Input1);
                PointList.Add(Input2);
            }
            catch (Exception ex)
            {
                Operation.DisplayPrompt(ex.Message);
            }

            return PointList;
        }

        private void GetValuesFromDialog()
        {
            _stairWidth = Data.stairWidth;
            _stairOffset = Data.stairOffset;
            _stringerProfile = Data.stringerProfile;
            _stringerMaterial = Data.stringerMaterial;
            _stepProfile = Data.stepProfile;
            _stepMaterial = Data.stepMaterial;
            _stepTypeIndex = Data.stepTypeIndex;
            _stairTypeIndex = Data.stairTypeIndex;
            _angleOfStairIndex = Data.angleOfStairIndex;
            _isStepsRibbed = Data.isStepsRibbed;
            _isStepsCells = Data.isStepsCells;
            _isStepsBoxShaped = Data.isStepsBoxShaped;

            if (IsDefaultValue(_stairWidth))
            {
                _stairWidth = 700;
            }
            if (IsDefaultValue(_stairOffset))
            {
                _stairOffset = 1000;
            }
            if (IsDefaultValue(_stepTypeIndex))
            {
                _stepTypeIndex = 0;
            }
            if (IsDefaultValue(_stairTypeIndex))
            {
                _stairTypeIndex = 1;
            }
            if (IsDefaultValue(_angleOfStairIndex))
            {
                _angleOfStairIndex = 0;
            }

            if (IsDefaultValue(_stringerProfile))
            {
                _stringerProfile = "U16P_8240_97";
            }
            if (IsDefaultValue(_stringerMaterial))
            {
                _stringerMaterial = "C245-4";
            }

        }

        public override bool Run(List<InputDefinition> Input)
        {
            try
            {
                GetValuesFromDialog();

                double ang = 45;

                Point Point1 = (Point)(Input[0]).GetInput();
                Point Point2 = (Point)(Input[1]).GetInput();

                if (_angleOfStairIndex == 1) { ang = 60; }

                Data.StairStepsBuilder(Point1, Point2, ang, _stairWidth, _stairOffset, _stringerProfile, _stringerMaterial, _stairTypeIndex, _stepTypeIndex);

                Model.CommitChanges();

            }
            catch (Exception ex)
            {
                Tekla.Structures.Model.Operations.Operation.DisplayPrompt(ex.Message);
            }

            return true;
        }

    }


}