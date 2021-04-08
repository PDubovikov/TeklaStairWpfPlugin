using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using TSM = Tekla.Structures.Model;

namespace TeklaStairWpfPlugin
{
    class StairType1 : IStair
    {
        public double Ang { get; set; }
        public double Width { get; set; }
        public double Offset { get; set; }
        public string Profile { get; set; }
        public string Material { get; set; }
        public bool IsUpFloor { get; set; }
        public bool IsDownFloor { get; set; }
        public Point StartStepsPoint { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Vector StairVector { get; set; }
        public Part MainStringer { get; set; }
        public Part SecondStringer { get; set; }

        public StairType1(Point startPoint, Point endPoint, double ang, double width, double offset, string profile, string material)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Ang = ang;
            Width = width;
            Offset = offset;
            Profile = profile;
            Material = material;
            IsUpFloor = true;
            IsDownFloor = false;
        }

        public void Create()
        {
            Point Point1 = StartPoint;
            Point Point2 = EndPoint;
            Vector mainVector = new Vector(Point2.X - Point1.X, Point2.Y - Point1.Y, Point2.Z - Point1.Z);
            Vector zaxis = new Vector(0, 0, -1);
            double height = Point1.Z - Point2.Z;
            Vector heightStair = zaxis * height;
            Point heightStairEndPt = new Point(Point1.X + heightStair.X, Point1.Y + heightStair.Y, Point1.Z + heightStair.Z);
            Vector subMainVector = new Vector(Point2.X - heightStairEndPt.X, Point2.Y - heightStairEndPt.Y, Point2.Z - heightStairEndPt.Z);
            double stairLength = height / Math.Tan(Ang * Math.PI / 180);
            Vector normallengthStair = subMainVector.GetNormal();
            Vector lengthStair = normallengthStair * stairLength;
            Point lengthStairEndPoint = new Point(heightStairEndPt.X + lengthStair.X, heightStairEndPt.Y + lengthStair.Y, heightStairEndPt.Z + lengthStair.Z);
            Vector mainStairVector = new Vector(lengthStairEndPoint.X - Point1.X, lengthStairEndPoint.Y - Point1.Y, lengthStairEndPoint.Z - Point1.Z);

            Vector leftOffsetVector = Vector.Cross(heightStair, mainVector);
            Vector normalOffsetBeamVector = leftOffsetVector.GetNormal();
            leftOffsetVector = normalOffsetBeamVector * (Width / 2);
            Vector horizontalVector = normallengthStair * (-1);
            Vector horizontalVectorLength = horizontalVector * Offset;
            Point leftBeamMidlePt = new Point(Point1.X + leftOffsetVector.X, Point1.Y + leftOffsetVector.Y, Point1.Z + leftOffsetVector.Z);
            Point leftBeamStartPt = new Point(leftBeamMidlePt.X + horizontalVectorLength.X, leftBeamMidlePt.Y + horizontalVectorLength.Y, leftBeamMidlePt.Z + horizontalVectorLength.Z);
            Point leftBeamEndPt = new Point(lengthStairEndPoint.X + leftOffsetVector.X, lengthStairEndPoint.Y + leftOffsetVector.Y, lengthStairEndPoint.Z + leftOffsetVector.Z);

            Vector rightOffsetVector = leftOffsetVector * (-1);
            Point rightBeamMidlePt = new Point(Point1.X + rightOffsetVector.X, Point1.Y + rightOffsetVector.Y, Point1.Z + rightOffsetVector.Z);
            Point rightBeamStartPt = new Point(rightBeamMidlePt.X + horizontalVectorLength.X, rightBeamMidlePt.Y + horizontalVectorLength.Y, rightBeamMidlePt.Z + horizontalVectorLength.Z);
            Point rightBeamEndPt = new Point(lengthStairEndPoint.X + rightOffsetVector.X, lengthStairEndPoint.Y + rightOffsetVector.Y, lengthStairEndPoint.Z + rightOffsetVector.Z);

            // offset along length stair
            Vector offsetVect = normallengthStair * Offset;

            Point leftBeamMidlePtOffset = new Point(leftBeamMidlePt.X + offsetVect.X, leftBeamMidlePt.Y + offsetVect.Y, leftBeamMidlePt.Z + offsetVect.Z);
            Point leftBeamStartPtOffset = new Point(leftBeamStartPt.X + offsetVect.X, leftBeamStartPt.Y + offsetVect.Y, leftBeamStartPt.Z + offsetVect.Z);
            Point leftBeamEndPtOffset = new Point(leftBeamEndPt.X + offsetVect.X, leftBeamEndPt.Y + offsetVect.Y, leftBeamEndPt.Z + offsetVect.Z);

            Point rightBeamMidlePtOffset = new Point(rightBeamMidlePt.X + offsetVect.X, rightBeamMidlePt.Y + offsetVect.Y, rightBeamMidlePt.Z + offsetVect.Z);
            Point rightBeamStartPtOffset = new Point(rightBeamStartPt.X + offsetVect.X, rightBeamStartPt.Y + offsetVect.Y, rightBeamStartPt.Z + offsetVect.Z);
            Point rightBeamEndPtOffset = new Point(rightBeamEndPt.X + offsetVect.X, rightBeamEndPt.Y + offsetVect.Y, rightBeamEndPt.Z + offsetVect.Z);

            TSM.Beam centerLineBeam = new TSM.Beam(Point1, lengthStairEndPoint);
            TSM.PolyBeam rightPolyBeam = new TSM.PolyBeam(PolyBeam.PolyBeamTypeEnum.BEAM);
            TSM.PolyBeam leftPolyBeam = new TSM.PolyBeam(PolyBeam.PolyBeamTypeEnum.BEAM);

            ContourPoint rightBeamPt1 = new ContourPoint(rightBeamStartPtOffset, null);
            ContourPoint rightBeamPt2 = new ContourPoint(rightBeamMidlePtOffset, null);
            ContourPoint rightBeamPt3 = new ContourPoint(rightBeamEndPtOffset, null);

            ContourPoint leftBeamPt1 = new ContourPoint(leftBeamStartPtOffset, null);
            ContourPoint leftBeamPt2 = new ContourPoint(leftBeamMidlePtOffset, null);
            ContourPoint leftBeamPt3 = new ContourPoint(leftBeamEndPtOffset, null);

            // build right kosour
            rightPolyBeam.Class = "11";
            rightPolyBeam.AddContourPoint(leftBeamPt1);
            rightPolyBeam.AddContourPoint(leftBeamPt2);
            rightPolyBeam.AddContourPoint(leftBeamPt3);
            rightPolyBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT;
            rightPolyBeam.Position.Depth = Position.DepthEnum.BEHIND;
            rightPolyBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            rightPolyBeam.Profile.ProfileString = Profile;
            rightPolyBeam.Material.MaterialString = Material;
            rightPolyBeam.Finish = "PAINT";
            rightPolyBeam.Insert();

            MainStringer = rightPolyBeam;

            // build left kosour
            leftPolyBeam.Class = "11";
            leftPolyBeam.AddContourPoint(rightBeamPt1);
            leftPolyBeam.AddContourPoint(rightBeamPt2);
            leftPolyBeam.AddContourPoint(rightBeamPt3);
            leftPolyBeam.Position.Plane = TSM.Position.PlaneEnum.LEFT;
            leftPolyBeam.Position.Depth = Position.DepthEnum.BEHIND;
            leftPolyBeam.Position.Rotation = TSM.Position.RotationEnum.BELOW;
            leftPolyBeam.Profile.ProfileString = Profile;
            leftPolyBeam.Material.MaterialString = Material;
            leftPolyBeam.Finish = "PAINT";
            leftPolyBeam.Insert();

            SecondStringer = leftPolyBeam;

            // fitting kosours
            Vector xaxis = normallengthStair;
            Vector yaxis = normalOffsetBeamVector;
            Fitting cuttingRightBeam = new Fitting();
            cuttingRightBeam.Plane = new Plane();
            cuttingRightBeam.Plane.Origin = lengthStairEndPoint;
            cuttingRightBeam.Plane.AxisX = xaxis;
            cuttingRightBeam.Plane.AxisY = yaxis;
            cuttingRightBeam.Father = rightPolyBeam;
            cuttingRightBeam.Insert();

            Fitting cuttingLeftBeam = new Fitting();
            cuttingLeftBeam.Plane = new Plane();
            cuttingLeftBeam.Plane.Origin = lengthStairEndPoint;
            cuttingLeftBeam.Plane.AxisX = xaxis;
            cuttingLeftBeam.Plane.AxisY = yaxis;
            cuttingLeftBeam.Father = leftPolyBeam;
            cuttingLeftBeam.Insert();

            double hprofile = 0;
            rightPolyBeam.GetReportProperty("HEIGHT", ref hprofile);
            Vector hOrtVect = zaxis * hprofile;

            Point stepsOffsetZPt = new Point(Point1.X + hOrtVect.X, Point1.Y + hOrtVect.Y, Point1.Z + hOrtVect.Z);
            //   var offsetStep = lengthStartSegment - ( hprofile / Math.Tan( (90.0 - ang /2) * Math.PI / 180) );
            var offsetStep = Offset;
            Vector offsetStepsVect = normallengthStair * offsetStep;
            StartStepsPoint = new Point(Point1.X + offsetStepsVect.X, Point1.Y + offsetStepsVect.Y, Point1.Z + offsetStepsVect.Z);
           // Height = height - hprofile;
            StairVector = normallengthStair;
        }

        private double GetHeightBeam(Part beam, Point beamInputPt)
        {
            if (beam == null) { return 0; }
            if (beamInputPt == null) { return 0; }


            var beamCS = beam.GetCoordinateSystem();
            Vector zaxis = new Vector(0, 0, 1);
            Vector yaxis = beamCS.AxisY;

            Vector beamCSZaxis = Vector.Cross(beamCS.AxisX, beamCS.AxisY);

            double ang = yaxis.GetAngleBetween(zaxis);


            var diff = Math.Abs(beamCS.Origin.Z - beamInputPt.Z) / Math.Cos(ang);
            var hprofile = diff * 2;
            hprofile = Math.Abs(Math.Round(hprofile, 3));

            return hprofile;

        }

        private void CuttingBeam(Part beam, Point inputPt, Vector axisX, Vector axisY)
        {
            Fitting cuttingBeam = new Fitting();
            cuttingBeam.Plane = new Plane();
            cuttingBeam.Plane.Origin = inputPt;
            cuttingBeam.Plane.AxisX = axisX;
            cuttingBeam.Plane.AxisY = axisY;
            cuttingBeam.Father = beam;
            cuttingBeam.Insert();
        }
    }
}
