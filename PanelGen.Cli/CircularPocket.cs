using System;
using System.Data.SqlTypes;
using System.IO;

namespace PanelGen.Cli
{
    public class CircularPocket
    {
        public float x; // Pocket X center
        public float y; // Pocket Y center
        public float diameter; // Pocket diameter
        public float depth; // Pocket depth

        private const float Stepover = 0.1f; // Tool overlap when doing surface milling

        public void Draw(StringWriter output, Tool tool)
        {
            if (diameter < tool.diameter)
            {
                output.WriteLine("(ERROR: Pocket is too small for tool)");
                return;
            }

            output.WriteLine($"G00 X{x:0.###} Y{y:0.###}"); // Move to center (x,y)
            // z = 0 (surface)
            for (var z = -tool.zStep; z < depth; z -= tool.zStep)
            {
                output.WriteLine($"G01 X{x:0.###}"); // Move to center - we assume to be at safe height
                output.WriteLine($"G01 Z{z:0.###}"); // Next z-step
                MillSurfaceCircular(output, tool);
            }

            output.WriteLine($"G01 Z{depth:0.###}"); // Finish with surface @z=depth
            MillSurfaceCircular(output, tool);
        }

        private void MillSurfaceCircular(TextWriter output, Tool tool)
        {
            var maxRadius = x + (diameter/2) - tool.radius;
            var xDelta = tool.diameter*(1 - Stepover); // Amount to move for each
            var xr = x + xDelta;

            while (xr < maxRadius)
            {
                output.WriteLine($"G01 X{xr:0.###}"); // Move to next radius
                output.WriteLine($"G02 I{x:0.###}"); // Circle w center @x,y
                xr = x + xDelta; // Next circle/radius
            }
            // Do outermost circle
            output.WriteLine($"G01 X{maxRadius:0.###}"); // Move to outer radius
            output.WriteLine($"G02 I{x:0.###}"); // Circle w center @x,y
        }

        #region Spiral cutting

        private void MillSurfaceSpiral(TextWriter output, Tool tool)
        {
            var maxRadius = x + (diameter/2) - tool.radius;
            var xDelta = tool.diameter*(1 - Stepover); // Amount to move for each
            var xr = x + xDelta;

            while (xr < maxRadius)
            {
                SpiralSegment(output, xr, Math.Min(xr + xDelta, maxRadius), 36);
                xr += xDelta; // Next circle/radius
            }
            // Do outermost circle
            output.WriteLine($"G01 X{maxRadius:0.###} Y{y:0.###}");
                // Explicit move to outer radius just in case that the spiral calculations are a bit off
            output.WriteLine($"G02 I{x:0.###}"); // Circle w center @x,y
        }

        // Create 1 spiral segment 360 degrees by plain G1 moves
        private void SpiralSegment(TextWriter output, float beginRadius, float endRadius, int steps)
        {
            var radStep = 2*Math.PI/steps;

            for (var step = 0; step <= steps; step++)
            {
                var nRad = beginRadius + (endRadius - beginRadius)*((float) step/steps);
                var nX = (float) Math.Cos(radStep*step)*(nRad) + x;
                var nY = (float) Math.Sin(radStep*step)*(nRad) + y;
                output.WriteLine($"G01 X{nX:0.###} Y{nY:0.###}");
            }
        }

        #endregion
    }
}

//http://okumacnc.blogspot.se/2011/06/how-to-make-spiral-interpolation-g021.html
