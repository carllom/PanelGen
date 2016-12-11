using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelGen.Cli
{
    public class Dial
    {
        public float x;
        public float y;

        public float holeRadius; // radius of hole for pot axis
        public float innerRadius; // inner radius for marker/tick lines

        // Marker lines are labeled lines
        public float arcSpan; // Dial segment arc angle
        public float markerLength; // Length of marker line
        public int minValue; // Min marker value
        public int maxValue; // Max marker value
        public int step; // Value step between markers

        // Tick lines are lines between markers
        public float tickLength; // Length of tick line
        public int tickCount; // Number of tick lines between each marker

        public string text;
    }
}
