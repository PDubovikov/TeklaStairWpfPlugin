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
    public class BoxShapedStepsStairDecorator : StairDecorator
    {
        public double StepWidht { get; set; }
        public double StepLength { get; set; }
        public double StepHeight { get; set; }
        public string StepProfile { get; set; }

        private Point leftStartPt, rightStartPt;
        private Point leftEndPt, rightEndPt;

        public BoxShapedStepsStairDecorator(IStair stair) : base(stair)
        {
            StepWidht = 200;
            StepLength = 700;
            StepHeight = 200;
            StepProfile = "RECTCB50*200-25*25";
        }

        public override void Create()
        {
            stair.Create();
            AddBoxShapedSteps(stair.StartStepsPoint, stair.StairVector, StepWidht, stair.Width, StepHeight, StepProfile);
            if (stair.IsUpFloor)
            {
                new FloorDecorator(stair).AddUpFloor(stair.StartStepsPoint, stair.StairVector, stair.Width, stair.Offset, 50, "PL30", "P34x33/30x3 S2");
            }
            if (stair.IsDownFloor)
            {
                new FloorDecorator(stair).AddDownFloor(stair.StartStepsPoint, stair.StairVector, stair.Width, 50, "PL30", "P34x33/30x3 S2");
            }
        }

        private void AddBoxShapedSteps(Point input, Vector alongStairVector, double width, double length, double stepHeight, string floorProfile)
        {
            double ang = stair.Ang;
            double height = 0;
            double stairLength = 0;
            double heightStair = Math.Abs(stair.StartPoint.Z - stair.EndPoint.Z);
            Point heightStairEndPt = new Point();
            Point stepStartPt = new Point();
            Vector zaxis = new Vector(0, 0, -1);
            Vector heightStairVect = new Vector();
            Vector lengthStairVect = new Vector();

            while (height <= (heightStair - stepHeight) - 30)
            {
                height = height + stepHeight;

                stairLength = height / Math.Tan(ang * Math.PI / 180);
                heightStairVect = zaxis * height;

                lengthStairVect = alongStairVector * stairLength;
                heightStairEndPt = new Point(input.X + heightStairVect.X, input.Y + heightStairVect.Y, input.Z + heightStairVect.Z);
                stepStartPt = new Point(heightStairEndPt.X + lengthStairVect.X, heightStairEndPt.Y + lengthStairVect.Y, heightStairEndPt.Z + lengthStairVect.Z);

                Part currentStep = DrawStep(stepStartPt, alongStairVector, width, length, floorProfile);
                WeldingSteps(stair.MainStringer, currentStep, leftStartPt, leftEndPt);
                WeldingSteps(stair.SecondStringer, currentStep, rightStartPt, rightEndPt);
            }

        }

        private Part DrawStep(Point input, Vector shortEdge, double width, double length, string floorProfile)
        {

            Vector zaxis = new Vector(0, 0, -1);
            Vector longEdgeLeftDir = Vector.Cross(zaxis, shortEdge);
            longEdgeLeftDir = longEdgeLeftDir * (length / 2);
            Vector longEdgeRightDir = longEdgeLeftDir * (-1);
            shortEdge = shortEdge * width * (-1);

            leftStartPt = new Point(input.X + longEdgeLeftDir.X, input.Y + longEdgeLeftDir.Y, input.Z + longEdgeLeftDir.Z);
            leftEndPt = new Point(leftStartPt.X + shortEdge.X, leftStartPt.Y + shortEdge.Y, leftStartPt.Z + shortEdge.Z);

            rightStartPt = new Point(input.X + longEdgeRightDir.X, input.Y + longEdgeRightDir.Y, input.Z + longEdgeRightDir.Z);
            rightEndPt = new Point(rightStartPt.X + shortEdge.X, rightStartPt.Y + shortEdge.Y, rightStartPt.Z + shortEdge.Z);

            var floorBeam = new Beam(Beam.BeamTypeEnum.PANEL);
            floorBeam.Class = "1";
            floorBeam.Name = "Ступень";
            floorBeam.StartPoint = leftStartPt;
            floorBeam.EndPoint = rightStartPt;
            floorBeam.Position.Plane = TSM.Position.PlaneEnum.LEFT;
            floorBeam.Position.Depth = Position.DepthEnum.BEHIND;
            floorBeam.Position.Rotation = TSM.Position.RotationEnum.FRONT;
            floorBeam.Profile.ProfileString = $"PL{width}*30";
            floorBeam.Material.MaterialString = "P34x33/30x3 S2";
            floorBeam.Finish = "PAINT";
            floorBeam.Insert();

            return floorBeam;
        }

        private void WeldingSteps(ModelObject mainPart, Part secondPart, Point startPt, Point endPt)
        {
            if (mainPart == null) return;
            if (secondPart == null) return;

            PolygonWeld polygonWeld = new PolygonWeld();
            polygonWeld.MainObject = mainPart;
            polygonWeld.SecondaryObject = secondPart;
            polygonWeld.Polygon.Points.Add(startPt);
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
