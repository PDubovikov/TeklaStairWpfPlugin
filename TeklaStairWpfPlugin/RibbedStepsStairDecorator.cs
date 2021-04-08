using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using TSM = Tekla.Structures.Model;

namespace TeklaStairWpfPlugin
{
    public class RibbedStepsStairDecorator : StairDecorator
    {
        public double StepWidht { get; set; }
        public double StepLength { get; set; }
        public double StepHeight { get; set; }
        public string StepProfile { get; set; }

        private Point leftStartPt, rightStartPt;
        private Point leftMiddlePt, rightMiddlePt;
        private Point leftEndPt, rightEndPt;


        public RibbedStepsStairDecorator(IStair stair) : base(stair)
        {
            StepWidht = 200;
            StepLength = 700;
            StepHeight = 200;
            StepProfile = $"PL4*700";
        }

        public override void Create()
        {
            stair.Create();
            AddRibbedSteps(stair.StartStepsPoint, stair.StairVector, StepWidht, stair.Width, StepHeight, StepProfile);
            if (stair.IsUpFloor)
            {
               new FloorDecorator(stair).AddUpFloor(stair.StartStepsPoint, stair.StairVector, stair.Width, stair.Offset, 50, "PL4", "Риф4");
            }
            if(stair.IsDownFloor)
            {
                new FloorDecorator(stair).AddDownFloor(stair.StartStepsPoint, stair.StairVector, stair.Width, 50, "PL4", "Риф4");
            }
        }

        private void AddRibbedSteps(Point input, Vector alongStairVector, double width, double length, double stepHeight, string floorProfile)
        {
            double ang = stair.Ang;
            double height = 0;
            double heightStair = Math.Abs(stair.StartPoint.Z - stair.EndPoint.Z);
            double stairLength = 0;
            Point heightStairEndPt = new Point();
            Point stepStartPt = new Point();
            Vector zaxis = new Vector(0, 0, -1);
            Vector heightStairVect = new Vector();
            Vector lengthStairVect = new Vector();


            while (height <= (heightStair - stepHeight) - 50)
            {
                height = height + stepHeight;

                stairLength = height / Math.Tan(ang * Math.PI / 180);
                heightStairVect = zaxis * height;

                lengthStairVect = alongStairVector * stairLength;
                heightStairEndPt = new Point(input.X + heightStairVect.X, input.Y + heightStairVect.Y, input.Z + heightStairVect.Z);
                stepStartPt = new Point(heightStairEndPt.X + lengthStairVect.X, heightStairEndPt.Y + lengthStairVect.Y, heightStairEndPt.Z + lengthStairVect.Z);

                Part currentStep = DrawStep(stepStartPt, alongStairVector, width, length, floorProfile);
                WeldingSteps(stair.MainStringer, currentStep, leftStartPt, leftMiddlePt, leftEndPt);
                WeldingSteps(stair.SecondStringer, currentStep, rightStartPt, rightMiddlePt, rightEndPt);

            }

        }

        private Part DrawStep(Point input, Vector shortEdge, double width, double length, string floorProfile)
        {

            Vector zaxis = new Vector(0, 0, -1);
            Vector longEdgeLeftDir = Vector.Cross(zaxis, shortEdge);
            longEdgeLeftDir = longEdgeLeftDir * (length / 2);
            Vector longEdgeRightDir = longEdgeLeftDir * (-1);
            shortEdge = shortEdge * width * (-1);
            Vector ortVect = zaxis * 50;

            leftMiddlePt = new Point(input.X + longEdgeLeftDir.X, input.Y + longEdgeLeftDir.Y, input.Z + longEdgeLeftDir.Z);
            leftEndPt = new Point(leftMiddlePt.X + shortEdge.X, leftMiddlePt.Y + shortEdge.Y, leftMiddlePt.Z + shortEdge.Z);
            leftStartPt = new Point(leftMiddlePt.X + ortVect.X, leftMiddlePt.Y + ortVect.Y, leftMiddlePt.Z + ortVect.Z);

            rightMiddlePt = new Point(input.X + longEdgeRightDir.X, input.Y + longEdgeRightDir.Y, input.Z + longEdgeRightDir.Z);
            rightEndPt = new Point(rightMiddlePt.X + shortEdge.X, rightMiddlePt.Y + shortEdge.Y, rightMiddlePt.Z + shortEdge.Z);
            rightStartPt = new Point(rightMiddlePt.X + ortVect.X, rightMiddlePt.Y + ortVect.Y, rightMiddlePt.Z + ortVect.Z);

            TSM.PolyBeam stepPolyBeam = new TSM.PolyBeam(PolyBeam.PolyBeamTypeEnum.BEAM);
            ContourPoint middleContourPt = new ContourPoint(rightMiddlePt, null);
            ContourPoint endContourPt = new ContourPoint(rightEndPt, null);
            ContourPoint startContourPt = new ContourPoint(rightStartPt, null);

            stepPolyBeam.Class = "1";
            stepPolyBeam.Name = "Ступень";
            stepPolyBeam.AddContourPoint(endContourPt);
            stepPolyBeam.AddContourPoint(middleContourPt);
            stepPolyBeam.AddContourPoint(startContourPt);
            stepPolyBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT;
            stepPolyBeam.Position.Depth = Position.DepthEnum.BEHIND;
            stepPolyBeam.Position.Rotation = TSM.Position.RotationEnum.FRONT;
            stepPolyBeam.Profile.ProfileString = $"PL4*{length}";
            stepPolyBeam.Material.MaterialString = "Риф4";
            stepPolyBeam.Finish = "PAINT";
            stepPolyBeam.Insert();

            return stepPolyBeam;
        }

         
        private void WeldingSteps(ModelObject mainPart, Part secondPart, Point startPt, Point middlePt, Point endPt)
        {
            if (mainPart == null) return ;
            if (secondPart == null) return ; 

            PolygonWeld polygonWeld = new PolygonWeld();
            polygonWeld.MainObject = mainPart;
            polygonWeld.SecondaryObject = secondPart;
            polygonWeld.Polygon.Points.Add(startPt);
            polygonWeld.Polygon.Points.Add(middlePt);
            polygonWeld.Polygon.Points.Add(endPt);
            polygonWeld.ConnectAssemblies = false;
            polygonWeld.ShopWeld = true;
            polygonWeld.AroundWeld = false;
            polygonWeld.TypeAbove = PolygonWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            polygonWeld.TypeBelow = PolygonWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            polygonWeld.SizeAbove = -100;
            polygonWeld.SizeBelow = -100;
            polygonWeld.Insert();

        }

    }
}
