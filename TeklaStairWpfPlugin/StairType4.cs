using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace TeklaStairWpfPlugin
{
    public class StairType4 : IStair
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


        public StairType4(Point startPoint, Point endPoint, double ang, double width, double offset, string profile, string material)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Ang = ang;
            Width = width;
            Offset = offset;
            Profile = profile;
            Material = material;
            IsUpFloor = false;
            IsDownFloor = true;
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
            //   Vector horizontalVectorLength = horizontalVector * lengthStartSegment;
            Point leftBeamStartPt = new Point(Point1.X + leftOffsetVector.X, Point1.Y + leftOffsetVector.Y, Point1.Z + leftOffsetVector.Z);
            Point leftBeamEndPt = new Point(lengthStairEndPoint.X + leftOffsetVector.X, lengthStairEndPoint.Y + leftOffsetVector.Y, lengthStairEndPoint.Z + leftOffsetVector.Z);
            // offset for fixture beam corner
            Vector offsetAlongStairVector = normallengthStair * 15;
            leftBeamStartPt = new Point(leftBeamStartPt.X + offsetAlongStairVector.X, leftBeamStartPt.Y + offsetAlongStairVector.Y, leftBeamStartPt.Z + offsetAlongStairVector.Z);
            leftBeamEndPt = new Point(leftBeamEndPt.X + offsetAlongStairVector.X, leftBeamEndPt.Y + offsetAlongStairVector.Y, leftBeamEndPt.Z + offsetAlongStairVector.Z);

            Vector rightOffsetVector = leftOffsetVector * (-1);
            Point rightBeamStartPt = new Point(Point1.X + rightOffsetVector.X, Point1.Y + rightOffsetVector.Y, Point1.Z + rightOffsetVector.Z);
            Point rightBeamEndPt = new Point(lengthStairEndPoint.X + rightOffsetVector.X, lengthStairEndPoint.Y + rightOffsetVector.Y, lengthStairEndPoint.Z + rightOffsetVector.Z);
            // offset for fixture beam corner
            rightBeamStartPt = new Point(rightBeamStartPt.X + offsetAlongStairVector.X, rightBeamStartPt.Y + offsetAlongStairVector.Y, rightBeamStartPt.Z + offsetAlongStairVector.Z);
            rightBeamEndPt = new Point(rightBeamEndPt.X + offsetAlongStairVector.X, rightBeamEndPt.Y + offsetAlongStairVector.Y, rightBeamEndPt.Z + offsetAlongStairVector.Z);

            TSM.Beam rightBeam = new TSM.Beam(leftBeamStartPt, leftBeamEndPt);
            rightBeam.Class = "11";
            rightBeam.Position.Plane = TSM.Position.PlaneEnum.RIGHT;
            rightBeam.Position.Depth = Position.DepthEnum.BEHIND;
            rightBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            rightBeam.Profile.ProfileString = Profile;
            rightBeam.Material.MaterialString = Material;
            rightBeam.Finish = "PAINT";
            rightBeam.Insert();

            MainStringer = rightBeam;
            //   TSM.Operations.Operation.DisplayPrompt($"Высота профиля: {hprofile}");

            TSM.Beam leftBeam = new TSM.Beam(rightBeamStartPt, rightBeamEndPt);
            leftBeam.Class = "11";
            leftBeam.Position.Plane = TSM.Position.PlaneEnum.LEFT;
            leftBeam.Position.Depth = Position.DepthEnum.BEHIND;
            leftBeam.Position.Rotation = TSM.Position.RotationEnum.BELOW;
            leftBeam.Profile.ProfileString = Profile;
            leftBeam.Material.MaterialString = Material;
            leftBeam.Finish = "PAINT";
            leftBeam.Insert();

            SecondStringer = leftBeam;
            // fitting kosours

            Vector xaxis = normallengthStair;
            Vector yaxis = normalOffsetBeamVector;
            CuttingBeam(rightBeam, leftBeamStartPt, zaxis, yaxis);
            CuttingBeam(rightBeam, leftBeamEndPt, xaxis, yaxis);
            CuttingBeam(leftBeam, rightBeamStartPt, zaxis, yaxis);
            CuttingBeam(leftBeam, rightBeamEndPt, xaxis, yaxis);

            // add beam corner
            Vector offsetLeftStartCrossStairVector = normalOffsetBeamVector * ((Width / 2) - 20) * (-1);
            Vector offsetLeftEndCrossStairVector = normalOffsetBeamVector * (100) * (-1);
            Vector offsetOrtVector = zaxis * (-1) * 5;
            Point offsetOrtPoint = new Point(Point1.X + offsetOrtVector.X, Point1.Y + offsetOrtVector.Y, Point1.Z + offsetOrtVector.Z);
            Point offsetCrossPoint = new Point(offsetOrtPoint.X + offsetLeftStartCrossStairVector.X, offsetOrtPoint.Y + offsetLeftStartCrossStairVector.Y, offsetOrtPoint.Z + offsetLeftStartCrossStairVector.Z);
            Point startLeftBeamCornerPoint = new Point(offsetCrossPoint.X + offsetAlongStairVector.X, offsetCrossPoint.Y + offsetAlongStairVector.Y, offsetCrossPoint.Z + offsetAlongStairVector.Z);
            Point endLeftBeamCornerPoint = new Point(startLeftBeamCornerPoint.X + offsetLeftEndCrossStairVector.X, startLeftBeamCornerPoint.Y + offsetLeftEndCrossStairVector.Y, startLeftBeamCornerPoint.Z + offsetLeftEndCrossStairVector.Z);

            TSM.Beam leftBeamCorner = new TSM.Beam(startLeftBeamCornerPoint, endLeftBeamCornerPoint);

            leftBeamCorner.Class = "8";
            leftBeamCorner.Position.Plane = TSM.Position.PlaneEnum.LEFT; // LEFT
            leftBeamCorner.Position.Depth = Position.DepthEnum.BEHIND;
            leftBeamCorner.Position.Rotation = TSM.Position.RotationEnum.BELOW; // BELOW
            leftBeamCorner.Profile.ProfileString = "L63X5_8509_93";
            leftBeamCorner.Finish = "PAINT";
            leftBeamCorner.Insert();

            Vector offsetRightStartCrossStairVector = normalOffsetBeamVector * ((Width / 2) - 20);
            Vector offsetRightEndCrossStairVector = normalOffsetBeamVector * (100);
            offsetCrossPoint = new Point(offsetOrtPoint.X + offsetRightStartCrossStairVector.X, offsetOrtPoint.Y + offsetRightStartCrossStairVector.Y, offsetOrtPoint.Z + offsetRightStartCrossStairVector.Z);
            Point startRightBeamCornerPoint = new Point(offsetCrossPoint.X + offsetAlongStairVector.X, offsetCrossPoint.Y + offsetAlongStairVector.Y, offsetCrossPoint.Z + offsetAlongStairVector.Z);
            Point endRightBeamCornerPoint = new Point(startRightBeamCornerPoint.X + offsetRightEndCrossStairVector.X, startRightBeamCornerPoint.Y + offsetRightEndCrossStairVector.Y, startRightBeamCornerPoint.Z + offsetRightEndCrossStairVector.Z);

            TSM.Beam rightBeamCorner = new TSM.Beam(startRightBeamCornerPoint, endRightBeamCornerPoint);

            rightBeamCorner.Class = "8";
            rightBeamCorner.Position.Plane = TSM.Position.PlaneEnum.RIGHT; // LEFT
            rightBeamCorner.Position.Depth = Position.DepthEnum.BEHIND;
            rightBeamCorner.Position.Rotation = TSM.Position.RotationEnum.BACK; // BELOW
            rightBeamCorner.Profile.ProfileString = "L63X5_8509_93";
            rightBeamCorner.Finish = "PAINT";
            rightBeamCorner.Insert();

            // calculate offsets

            //   var hprofile = GetHeightBeam(rightBeam, leftBeamStartPt);
            double hprofile = 0;
            rightBeam.GetReportProperty("HEIGHT", ref hprofile);
            var hoffset = hprofile / Math.Cos(Ang * Math.PI / 180);

            Vector hOrtVect = zaxis * hoffset;
            StartStepsPoint = new Point(Point1.X + offsetAlongStairVector.X, Point1.Y + offsetAlongStairVector.Y, Point1.Z + offsetAlongStairVector.Z);
            StairVector = normallengthStair;
            // Height = height - hprofile;

            Weld rightWeldStringer = new Weld();
            rightWeldStringer.MainObject = rightBeam;
            rightWeldStringer.SecondaryObject = rightBeamCorner;
            rightWeldStringer.ConnectAssemblies = false;
            rightWeldStringer.ShopWeld = true;
            rightWeldStringer.AroundWeld = false;
            rightWeldStringer.TypeAbove = PolygonWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            rightWeldStringer.TypeBelow = PolygonWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            rightWeldStringer.SizeAbove = -100;
            rightWeldStringer.SizeBelow = -100;
            rightWeldStringer.Insert();

            Weld leftWeldStringer = new Weld();
            leftWeldStringer.MainObject = leftBeam;
            leftWeldStringer.SecondaryObject = leftBeamCorner;
            leftWeldStringer.ConnectAssemblies = false;
            leftWeldStringer.ShopWeld = true;
            leftWeldStringer.AroundWeld = false;
            leftWeldStringer.TypeAbove = PolygonWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            leftWeldStringer.TypeBelow = PolygonWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            leftWeldStringer.SizeAbove = -100;
            leftWeldStringer.SizeBelow = -100;
            leftWeldStringer.Insert();

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
