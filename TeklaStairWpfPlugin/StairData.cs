using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;
using TSM = Tekla.Structures.Model;

namespace TeklaStairWpfPlugin
{
    public class StairData
    {
        [StructuresField("stairWidth")]
        public double stairWidth;

        [StructuresField("stairOffset")]
        public double stairOffset;

        [StructuresField("stringerProfile")]
        public string stringerProfile;

        [StructuresField("stringerMaterial")]
        public string stringerMaterial;

        [StructuresField("stepProfile")]
        public string stepProfile;

        [StructuresField("stepMaterial")]
        public string stepMaterial;

        [StructuresField("stepTypeIndex")]
        public int stepTypeIndex;

        [StructuresField("stairTypeIndex")]
        public int stairTypeIndex;

        [StructuresField("angleOfStairIndex")]
        public int angleOfStairIndex;

        [StructuresField("isStepsRibbed")]
        public int isStepsRibbed;

        [StructuresField("isStepsCells")]
        public int isStepsCells;

        [StructuresField("isStepsBoxShaped")]
        public int isStepsBoxShaped;

        public enum StairType { type1, type2 }
        public bool StairAngle { get; set; }
        public enum StepsEnum { Ribbed, Cells, BoxShaped }
        //     public double ang = 45;

        private IStair StairBuilder(Point point1, Point point2, double angle, int stairTypeIndex, double stairWidth, double stairOffset, string profile, string material)
        {
            IStair currentStair = null;

            switch (stairTypeIndex)
            {
                case 0:
                case 1:
                    currentStair = new StairType1(point1, point2, angle, stairWidth, stairOffset, profile, material);
                    break;
                case 2:
                    currentStair = new StairType2(point1, point2, angle, stairWidth, stairOffset, profile, material);
                    break;
                case 3:
                    currentStair = new StairType3(point1, point2, angle, stairWidth, stairOffset, profile, material);
                    break;
            }

            return currentStair;
        }

        public void StairStepsBuilder(Point point1, Point point2, double angle, double stairWidth, double stairOffset, string profile, string material, int stairTypeIndex, int stepIndex)
        {
            switch (stepIndex)
            {

                case 0:
                    var ribbedStairDecorator = new RibbedStepsStairDecorator(StairBuilder(point1, point2, angle, stairTypeIndex, stairWidth, stairOffset, profile, material));
                    if (angle > 45)
                    {
                        ribbedStairDecorator.StepHeight = 300; ribbedStairDecorator.StepProfile = stepProfile;
                        ribbedStairDecorator.StepWidht = 170;
                    }
                    ribbedStairDecorator.Create();
                    break;
                case 1:
                    var cellsStairDecorator = new CellsStepsStairDecorator(StairBuilder(point1, point2, angle, stairTypeIndex, stairWidth, stairOffset, profile, material));
                    if (angle > 45)
                    {
                        cellsStairDecorator.StepHeight = 300; cellsStairDecorator.StepWidht = 170;
                        cellsStairDecorator.StepProfile = stepProfile;
                    }
                    cellsStairDecorator.Create();
                    break;
                case 2:
                    var boxShapedStairDecorator = new BoxShapedStepsStairDecorator(StairBuilder(point1, point2, angle, stairTypeIndex, stairWidth, stairOffset, profile, material));
                    if (angle > 45)
                    {
                        boxShapedStairDecorator.StepHeight = 300; boxShapedStairDecorator.StepProfile = stepProfile;
                        boxShapedStairDecorator.StepWidht = 170;
                    }
                    boxShapedStairDecorator.Create();
                    break;
            }
        }

    }
}
