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
    public abstract class StairDecorator : IStair
    {

        protected IStair stair;

        public double Ang { get => stair.Ang; set => stair.Ang = value; }
        public double Width { get => stair.Width; set => stair.Width = value; }
        public double Offset { get => stair.Offset; set => stair.Offset = value; }
        public bool IsUpFloor { get => stair.IsUpFloor; set => stair.IsUpFloor = value; }
        public bool IsDownFloor { get => stair.IsDownFloor; set => stair.IsDownFloor = value; }
        public Point StartStepsPoint { get => stair.StartStepsPoint; set => stair.StartStepsPoint = value; }
        public Point StartPoint { get => stair.StartPoint; set => stair.StartPoint = value; }
        public Point EndPoint { get => stair.EndPoint; set => stair.EndPoint = value; }
        public Vector StairVector { get => stair.StairVector; set => stair.StairVector = value; }
        public Part MainStringer { get => stair.MainStringer; set => stair.MainStringer = value; }
        public Part SecondStringer { get => stair.SecondStringer; set => stair.SecondStringer = value; }

        public StairDecorator(IStair stair)
        {
            this.stair = stair;
        }


        public virtual void Create()
        {
            stair.Create();
        }


    }
}
