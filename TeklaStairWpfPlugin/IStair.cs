using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace TeklaStairWpfPlugin
{
    public interface IStair
    {
        double Ang { get; set; }
        double Width { get; set; }
        double Offset { get; set; }
        bool IsUpFloor { get; set; }
        bool IsDownFloor { get; set; }
        Point StartStepsPoint { get; set; }
        Point StartPoint { get; set; }
        Point EndPoint { get; set; }
        Vector StairVector { get; set; }
        Part MainStringer { get; set; }
        Part SecondStringer { get; set; }

        void Create();

    }
}
