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
    public class CellsStepsStairDecorator : StairDecorator
    {
        public double StepWidht { get; set; }
        public double StepLength { get; set; }
        public double StepHeight { get; set; }
        public string StepProfile { get; set; }
        public string StepFrameProfile { get; set; }

        private Point leftStartPt, rightStartPt;
        private Point leftEndPt, rightEndPt;

        public CellsStepsStairDecorator(IStair stair) : base(stair)
        {
            StepWidht = 200;
            StepLength = 700;
            StepHeight = 200;
            StepFrameProfile = "L32X3_8509_93";
            StepProfile = "PL200*20";
        }

        public override void Create()
        {
            stair.Create();
            AddCellsSteps(stair.StartStepsPoint, stair.StairVector, StepWidht, stair.Width, StepHeight, StepFrameProfile, StepProfile);
            if (stair.IsUpFloor)
            {
                new FloorDecorator(stair).AddUpFloor(stair.StartStepsPoint, stair.StairVector, stair.Width, stair.Offset, 50, "PL20", "ПВ406");
            }
            if (stair.IsDownFloor)
            {
                new FloorDecorator(stair).AddDownFloor(stair.StartStepsPoint, stair.StairVector, stair.Width, 50, "PL20", "ПВ406");
            }
        }

        private void AddCellsSteps(Point input, Vector alongStairVector, double width, double length, double stepHeight, string frameProfile, string floorProfile)
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

            while (height <= (heightStair - stepHeight) - 52)
            {
                height = height + stepHeight;

                stairLength = height / Math.Tan(ang * Math.PI / 180);
                heightStairVect = zaxis * height;

                lengthStairVect = alongStairVector * stairLength;
                heightStairEndPt = new Point(input.X + heightStairVect.X, input.Y + heightStairVect.Y, input.Z + heightStairVect.Z);
                stepStartPt = new Point(heightStairEndPt.X + lengthStairVect.X, heightStairEndPt.Y + lengthStairVect.Y, heightStairEndPt.Z + lengthStairVect.Z);

                Part currentStep = DrawStep(stepStartPt, alongStairVector, width, length, frameProfile, floorProfile);
                WeldingSteps(stair.MainStringer, currentStep, leftStartPt, leftEndPt);
                WeldingSteps(stair.SecondStringer, currentStep, rightStartPt, rightEndPt);
            }

        }

        private Part DrawStep(Point input, Vector shortEdge, double width, double length, string frameProfile, string floorProfile)
        {

            Vector zaxis = new Vector(0, 0, -1);
            Vector longEdgeLeftDir = Vector.Cross(zaxis, shortEdge);
            longEdgeLeftDir = longEdgeLeftDir * (length / 2);
            Vector longEdgeRightDir = longEdgeLeftDir * (-1);
            shortEdge = shortEdge * width * (-1);

            Point leftEdgePt1 = new Point(input.X + longEdgeLeftDir.X, input.Y + longEdgeLeftDir.Y, input.Z + longEdgeLeftDir.Z);
            Point leftEdgePt2 = new Point(leftEdgePt1.X + shortEdge.X, leftEdgePt1.Y + shortEdge.Y, leftEdgePt1.Z + shortEdge.Z);

            Point rightEdgePt1 = new Point(input.X + longEdgeRightDir.X, input.Y + longEdgeRightDir.Y, input.Z + longEdgeRightDir.Z);
            Point rightEdgePt2 = new Point(rightEdgePt1.X + shortEdge.X, rightEdgePt1.Y + shortEdge.Y, rightEdgePt1.Z + shortEdge.Z);

            var floorBeam = new Beam(Beam.BeamTypeEnum.PANEL);
            floorBeam.Class = "1";
            floorBeam.Name = "Ступень";
            floorBeam.StartPoint = leftEdgePt1;
            floorBeam.EndPoint = rightEdgePt1;
            floorBeam.Position.Plane = TSM.Position.PlaneEnum.LEFT;
            floorBeam.Position.Depth = Position.DepthEnum.BEHIND;
            floorBeam.Position.Rotation = TSM.Position.RotationEnum.BACK;
            floorBeam.Profile.ProfileString = $"PL{width}*20";
            floorBeam.Material.MaterialString = "ПВ406";
            floorBeam.Finish = "PAINT";
            floorBeam.Insert();


            // для вычисления высоты выбранного профиля находим его систему координат (находится в центре)
            // и вычисляем разницу по Z точек вставки профиля и точки в системе координат профиля.
            var floorSolid = floorBeam.GetSolid();
            var thickness = floorSolid.MaximumPoint.Z - floorSolid.MinimumPoint.Z;

            var hprofile = thickness; // GetHeightBeam(floorBeam, leftEdgePt1);
            Vector hOffsetVect = zaxis * hprofile;
            Point shiftLeftEdgePt1 = new Point(leftEdgePt1.X + hOffsetVect.X, leftEdgePt1.Y + hOffsetVect.Y, leftEdgePt1.Z + hOffsetVect.Z);
            Point shiftLeftEdgePt2 = new Point(leftEdgePt2.X + hOffsetVect.X, leftEdgePt2.Y + hOffsetVect.Y, leftEdgePt2.Z + hOffsetVect.Z);
            Point shiftRightEdgePt1 = new Point(rightEdgePt1.X + hOffsetVect.X, rightEdgePt1.Y + hOffsetVect.Y, rightEdgePt1.Z + hOffsetVect.Z);
            Point shiftRightEdgePt2 = new Point(rightEdgePt2.X + hOffsetVect.X, rightEdgePt2.Y + hOffsetVect.Y, rightEdgePt2.Z + hOffsetVect.Z);

            leftStartPt = shiftLeftEdgePt1;
            leftEndPt = shiftLeftEdgePt2;
            rightStartPt = shiftRightEdgePt1;
            rightEndPt = shiftRightEdgePt2;

            TSM.Beam leftshortBeam = new TSM.Beam(shiftLeftEdgePt1, shiftLeftEdgePt2);

            leftshortBeam.Class = "8";
            leftshortBeam.Name = "Ступень";
            leftshortBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT; // LEFT
            leftshortBeam.Position.Depth = Position.DepthEnum.BEHIND;
            leftshortBeam.Position.Rotation = TSM.Position.RotationEnum.BACK; // BELOW
            leftshortBeam.Profile.ProfileString = StepFrameProfile;
            leftshortBeam.Material.MaterialString = "C245-4";
            leftshortBeam.Finish = "PAINT";
            leftshortBeam.Insert();

            TSM.Beam leftlongBeam = new TSM.Beam(shiftLeftEdgePt2, shiftRightEdgePt2);

            leftlongBeam.Class = "8";
            leftlongBeam.Name = "Ступень";
            leftlongBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT; // LEFT
            leftlongBeam.Position.Depth = Position.DepthEnum.BEHIND;
            leftlongBeam.Position.Rotation = TSM.Position.RotationEnum.BACK; // BELOW
            leftlongBeam.Profile.ProfileString = StepFrameProfile;
            leftlongBeam.Material.MaterialString = "C245-4";
            leftlongBeam.Finish = "PAINT";
            leftlongBeam.Insert();


            TSM.Beam rightshortBeam = new TSM.Beam(shiftRightEdgePt2, shiftRightEdgePt1);

            rightshortBeam.Class = "8";
            rightshortBeam.Name = "Ступень";
            rightshortBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT;
            rightshortBeam.Position.Depth = Position.DepthEnum.BEHIND;
            rightshortBeam.Position.Rotation = TSM.Position.RotationEnum.BACK;
            rightshortBeam.Profile.ProfileString = StepFrameProfile;
            rightshortBeam.Material.MaterialString = "C245-4";
            rightshortBeam.Finish = "PAINT";
            rightshortBeam.Insert();


            TSM.Beam rightlongBeam = new TSM.Beam(shiftRightEdgePt1, shiftLeftEdgePt1);

            rightlongBeam.Class = "8";
            rightlongBeam.Name = "Ступень";
            rightlongBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT;
            rightlongBeam.Position.Depth = Position.DepthEnum.BEHIND;
            rightlongBeam.Position.Rotation = TSM.Position.RotationEnum.BACK;
            rightlongBeam.Profile.ProfileString = StepFrameProfile;
            rightlongBeam.Material.MaterialString = "C245-4";
            rightlongBeam.Finish = "PAINT";
            rightlongBeam.Insert();

            var stepAssy = floorBeam.GetAssembly();
            stepAssy.Add(leftshortBeam);
            stepAssy.Add(leftlongBeam);
            stepAssy.Add(rightshortBeam);
            stepAssy.Add(rightlongBeam);

            bool isStepAssy = stepAssy.Modify();

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
