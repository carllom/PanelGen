﻿using System;
using System.IO;

namespace PanelGen.Cli
{
    public class CircularPocket : PanelStockItem
    {
        public float diameter; // Pocket diameter
        public float depth; // Pocket depth

        public override Vertex3 Extents
        {
            get
            {
                return new Vertex3(diameter, diameter, depth);
            }
        }

        public override bool Inside(float x, float y)
        {
            var p = new Vertex2(x, y) - pos.Xy;
            return p.Length <= diameter / 2;
        }

        private const float Stepover = 0.1f; // Tool overlap when doing surface milling

        public override void GenerateCode(TextWriter output, Tool tool)
        {
            if (diameter < tool.diameter)
            {
                output.WriteLine("(ERROR: Pocket is too small for tool)");
                return;
            }

            output.WriteLine("(DEBUG: CircularPocket start)");

            if (diameter < tool.diameter * 2) // Pocket is small enough to mill using a helix
            {
                var maxRadius = (diameter / 2) - tool.radius; // tool compensated outer radius

                output.WriteLine("G00 X{0:0.###} Y{1:0.###}", pos.x + maxRadius, pos.y); // Move to center (x,y)
                                                                                 // z = 0 (surface)
                for (var z = pos.z - tool.zStep; z > -depth; z -= tool.zStep)
                {
                    output.WriteLine("G02 I{0:0.###} Z{1:0.###}", -maxRadius, z); // Helix w center @x,y
                }
                output.WriteLine("G02 I{0:0.###} Z{1:0.###}", -maxRadius, -depth); // Helix w center @x,y
                output.WriteLine("G02 I{0:0.###}", -maxRadius); // Circle w center @x,y
            }
            else // Pocket must be surface milled
            {
                output.WriteLine("G00 X{0:0.###} Y{1:0.###}", pos.x, pos.y); // Move to center (x,y)
                                                                     // z = 0 (surface)

                for (var z = pos.z - tool.zStep; z > -depth; z -= tool.zStep)
                {
                    output.WriteLine("G01 X{0:0.###}", pos.x); // Move to center - we assume to be at safe height
                    output.WriteLine("G01 Z{0:0.###}", z); // Next z-step
                                                           //MillSurfaceCircular(output, tool);
                    MillSurfaceSpiral(output, tool);
                }
                output.WriteLine("G01 X{0:0.###}", pos.x);
                output.WriteLine("G01 Z{0:0.###}", -depth); // Finish with surface @z=depth
                                                            //MillSurfaceCircular(output, tool);
                MillSurfaceSpiral(output, tool);
            }
            output.WriteLine("(DEBUG: CircularPocket end)");
        }

        private void MillSurfaceCircular(TextWriter output, Tool tool)
        {
            var maxRadius = (diameter/2) - tool.radius; // Tool compensated outer radius
            var xDelta = tool.diameter*(1 - Stepover); // Amount to move for each
            var xr = pos.x + xDelta;

            while (xr < maxRadius)
            {
                output.WriteLine("G01 X{0:0.###}", xr); // Move to next radius
                output.WriteLine("G02 I{0:0.###}", pos.x-xr); // Circle w center @x,y
                xr += xDelta; // Next circle/radius
            }
            // Do outermost circle
            output.WriteLine("G01 X{0:0.###}", pos.x + maxRadius); // Move to outer radius
            output.WriteLine("G02 I{0:0.###}", -maxRadius); // Circle w center @x,y
        }

        #region Spiral cutting

        private void MillSurfaceSpiral(TextWriter output, Tool tool)
        {
            var maxRadius = (diameter/2) - tool.radius;
            var xDelta = tool.diameter*(1 - Stepover); // Amount to move for each
            var xr = 0f;

            while (xr < maxRadius)
            {
                SpiralSegment(output, xr, Math.Min(xr + xDelta, maxRadius), 36);
                xr += xDelta; // Next circle/radius
            }
            // Do outermost circle
            output.WriteLine("G01 X{0:0.###} Y{1:0.###}", pos.x + maxRadius, pos.y);
            // Explicit move to outer radius just in case that the spiral calculations are a bit off
            output.WriteLine("G02 I{0:0.###}", - maxRadius); // Circle w center @x,y
        }

        // Create 1 spiral segment 360 degrees by plain G1 moves
        private void SpiralSegment(TextWriter output, float beginRadius, float endRadius, int steps)
        {
            var radStep = 2*Math.PI/steps;

            for (var step = 0; step <= steps; step++)
            {
                var nRad = beginRadius + (endRadius - beginRadius)*((float) step/steps);
                var nX = (float) Math.Cos(radStep*step)*(nRad) + pos.x;
                var nY = -(float) Math.Sin(radStep*step)*(nRad) + pos.y;
                output.WriteLine("G01 X{0:0.###} Y{1:0.###}", nX, nY);
            }
        }

        #endregion

        #region Save/Restore object
        public override void Load(BinaryReader data)
        {
            base.Load(data);
            diameter = data.ReadSingle();
            depth = data.ReadSingle();
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(diameter);
            data.Write(depth);
        }
        #endregion
    }
}

//http://okumacnc.blogspot.se/2011/06/how-to-make-spiral-interpolation-g021.html
