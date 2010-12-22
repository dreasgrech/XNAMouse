using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMouse
{
    public class ScrollWheelValueChangedEventArgs:EventArgs
    {
        public int Value { get; set; }
        public ScrollWheelValueChangedEventArgs(int value)
        {
            Value = value;
        }
    }
}