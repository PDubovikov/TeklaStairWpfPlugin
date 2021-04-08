using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using TSM = Tekla.Structures.Model;

namespace TeklaStairWpfPlugin
{
    public class FloorDecorator
    {

        private IStair stair;

        public FloorDecorator(IStair stair)
        {
            this.stair = stair;
        }

        public void AddUpFloor(Point input, Vector alongStairVect, double width, double length, double overlap, string floorProfile, string floorMaterial)
        {
            Part floor = DrawUpFloor(input, alongStairVect, width, length, overlap, floorProfile, floorMaterial);
            WeldFloor(stair.MainStringer, floor);
            WeldFloor(stair.SecondStringer, floor);
        }

        public void AddDownFloor(Point input, Vector alongStairVect, double width, double overlap, string floorProfile, string floorMaterial)
        {
            Part floor = DrawDownFloor(input, alongStairVect, width, overlap, floorProfile, floorMaterial);
            WeldFloor(stair.MainStringer, floor);
            WeldFloor(stair.SecondStringer, floor);
        }

        private Part DrawUpFloor(Point input, Vector alongStairVect, double width, double length, double overlap, string floorProfile, string floorMaterial)
        {
            Vector zaxis = new Vector(0, 0, -1);
            Vector shortEdge = Vector.Cross(zaxis, alongStairVect);
            alongStairVect = alongStairVect * length * (-1);
            Vector shortEdgeLeft = shortEdge * ((width + 2 * overlap) / 2);
            Vector shortEdgeRight = shortEdgeLeft * (-1);
            Point leftNearCornerPt = new Point(input.X + shortEdgeLeft.X, input.Y + shortEdgeLeft.Y, input.Z + shortEdgeLeft.Z);
            Point rightNearCornerPt = new Point(input.X + shortEdgeRight.X, input.Y + shortEdgeRight.Y, input.Z + shortEdgeRight.Z);
            Point leftFarCornerPt = new Point(leftNearCornerPt.X + alongStairVect.X, leftNearCornerPt.Y + alongStairVect.Y, leftNearCornerPt.Z + alongStairVect.Z);
            Point rightFarCornerPt = new Point(rightNearCornerPt.X + alongStairVect.X, rightNearCornerPt.Y + alongStairVect.Y, rightNearCornerPt.Z + alongStairVect.Z);

            ContourPoint leftCpStartPt = new ContourPoint(leftNearCornerPt, null);
            ContourPoint leftCpEndPt = new ContourPoint(leftFarCornerPt, null);
            ContourPoint rightCpStartPt = new ContourPoint(rightNearCornerPt, null);
            ContourPoint rightCpEndPt = new ContourPoint(rightFarCornerPt, null);

            ContourPlate floorBeam = new ContourPlate();
            floorBeam.AddContourPoint(leftCpStartPt);
            floorBeam.AddContourPoint(leftCpEndPt);
            floorBeam.AddContourPoint(rightCpEndPt);
            floorBeam.AddContourPoint(rightCpStartPt);
            floorBeam.Profile.ProfileString = floorProfile;
            floorBeam.Material.MaterialString = floorMaterial;
            floorBeam.Position.Depth = Position.DepthEnum.FRONT;

            floorBeam.Insert();

            return floorBeam;
        }

        private Part DrawDownFloor(Point input, Vector alongStairVect, double width, double overlap, string floorProfile, string floorMaterial)
        {
            double ang = stair.Ang;
            double height = stair.StartPoint.Z - stair.EndPoint.Z;
            Vector zaxis = new Vector(0, 0, -1);
            Vector shortEdge = Vector.Cross(zaxis, alongStairVect);
            Vector shortEdgeLeft = shortEdge * ((width + 2 * overlap) / 2);
            Vector shortEdgeRight = shortEdgeLeft * (-1);

            double stairLength = height / Math.Tan(ang * Math.PI / 180);
            Vector longStairVect = alongStairVect * stairLength;
            Vector heightStairVect = zaxis * height;

            Point hEndPoint = new Point(input.X + heightStairVect.X, input.Y + heightStairVect.Y, input.Z + heightStairVect.Z);
            Point stairEndPt = new Point(hEndPoint.X + longStairVect.X, hEndPoint.Y + longStairVect.Y, hEndPoint.Z + longStairVect.Z);

            Vector longEdge = new Vector(stair.EndPoint.X - stairEndPt.X, stair.EndPoint.Y - stairEndPt.Y, stair.EndPoint.Z - stairEndPt.Z);

            Point leftNearCornerPt = new Point(stairEndPt.X + shortEdgeLeft.X, stairEndPt.Y + shortEdgeLeft.Y, stairEndPt.Z + shortEdgeLeft.Z);
            Point rightNearCornerPt = new Point(stairEndPt.X + shortEdgeRight.X, stairEndPt.Y + shortEdgeRight.Y, stairEndPt.Z + shortEdgeRight.Z);
            Point leftFarCornerPt = new Point(leftNearCornerPt.X + longEdge.X, leftNearCornerPt.Y + longEdge.Y, leftNearCornerPt.Z + longEdge.Z);
            Point rightFarCornerPt = new Point(rightNearCornerPt.X + longEdge.X, rightNearCornerPt.Y + longEdge.Y, rightNearCornerPt.Z + longEdge.Z);

            ContourPoint leftCpStartPt = new ContourPoint(leftNearCornerPt, null);
            ContourPoint leftCpEndPt = new ContourPoint(leftFarCornerPt, null);
            ContourPoint rightCpStartPt = new ContourPoint(rightNearCornerPt, null);
            ContourPoint rightCpEndPt = new ContourPoint(rightFarCornerPt, null);

            ContourPlate floorBeam = new ContourPlate();
            floorBeam.AddContourPoint(leftCpStartPt);
            floorBeam.AddContourPoint(leftCpEndPt);
            floorBeam.AddContourPoint(rightCpEndPt);
            floorBeam.AddContourPoint(rightCpStartPt);
            floorBeam.Profile.ProfileString = floorProfile;
            floorBeam.Material.MaterialString = floorMaterial;
            floorBeam.Position.Depth = Position.DepthEnum.FRONT;

            floorBeam.Insert();

            return floorBeam;
        }

        private void WeldFloor(Part mainPart, Part secondPart)
        {
            Weld weld = new Weld();
            weld.MainObject = mainPart;
            weld.SecondaryObject = secondPart;
            weld.ConnectAssemblies = false;
            weld.ShopWeld = true;
            weld.AroundWeld = true;
            weld.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            weld.Insert();

        }

    }
}
