using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNAMouse
{
    public class MouseClickEventArgs:EventArgs
    {
        public Point Position { get; set; }

        public MouseClickEventArgs(Point position)
        {
            Position = position;
        }
    }
}