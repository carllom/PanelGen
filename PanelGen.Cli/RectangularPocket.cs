using System;
using System.IO;

namespace PanelGen.Cli
{
    public class RectangularPocket : PanelStockItem
    {
        public float width; // Pocket width
        public float height; // Pocket height
        public float depth; // Pocket depth

        //private enum Corner
        //{
        //    BottomLeft,
        //    TopLeft,
        //    TopRight,
        //    BottomRight
        //};

        private struct Rect
        {
            public float centerX;
            public float centerY;
            public float width;
            public float height;
            public float left => centerX - width/2;
            public float right => centerX + width/2;
            public float top => centerY + height/2;
            public float bottom => centerY - height/2;
        }

        private const float Stepover = 0.1f; // Tool overlap (fraction of tool diameter) when doing surface milling

        public void Draw(StringWriter output, Tool tool)
        {
            if (width < tool.diameter || height < tool.diameter)
            {
                output.WriteLine("(ERROR: Pocket is too small for tool)");
                return;
            }


            output.WriteLine("(DEBUG: RectangularPocket start)");

            // Create tool compensated rectangle
            var toolOutline = new Rect
            {
                centerX = pos.x,
                centerY = pos.y,
                width = width - tool.diameter,
                height = height - tool.diameter
            };

            for (var z = pos.z - tool.zStep; z > -depth; z -= tool.zStep)
            {
                MillPlane(output, tool, z, toolOutline);
            }
            MillPlane(output, tool, -depth, toolOutline);

            output.WriteLine("(DEBUG: RectangularPocket end)");
        }

        private void MillPlane(TextWriter output, Tool tool, float z, Rect toolOutline)
        {
            var toolSurface = toolOutline;
            toolSurface.width = toolOutline.width - tool.diameter;
            toolSurface.height = toolOutline.height - tool.diameter;

            output.WriteLine("G00 X{0:0.###} Y{1:0.###}", toolSurface.left, toolSurface.bottom); // Quick move to surface start
            output.WriteLine("G01 Z{0:0.###}", z); // Next z-step
            // Is there any point in milling a surface or have we covered it with the outline?
            if (Math.Min(width, height) - 2*tool.diameter > 0)
            {
                MillSurfaceSnake(output, toolSurface, tool.diameter*(1-Stepover));
            }
            output.WriteLine("G01 X{0:0.###} Y{1:0.###}", toolOutline.left, toolOutline.bottom); // Move to outline start
            MillOutline(output, toolOutline);
        }

        // Mill outermost rectangle - assume (for now) that we are at bottom left when starting
        private static void MillOutline(TextWriter output, Rect r)
        {
            output.WriteLine("G01 Y{0:0.###}", r.top); // Mill left side
            output.WriteLine("G01 X{0:0.###}", r.right); // Mill top side
            output.WriteLine("G01 Y{0:0.###}", r.bottom); // Mill right side
            output.WriteLine("G01 X{0:0.###}", r.left); // Mill bottom side
        }

        private static void MillSurfaceSnake(TextWriter output, Rect r, float step)
        {
            var pos = true; // Positive (right/up) movement 
            if (r.width > r.height) // horizontal strips
            {
                var ypos = r.bottom + step; // we are already @r.bottom, ypos represents "next" line
                while (ypos < r.top)
                {
                    output.WriteLine("G01 X{0:0.###}", pos ? r.right : r.left); // long mill
                    output.WriteLine("G01 Y{0:0.###}", ypos); // short mill
                    ypos += step;
                    pos = !pos;
                }
                output.WriteLine("G01 X{0:0.###}", pos ? r.right : r.left); // long mill
                output.WriteLine("G01 Y{0:0.###}", r.top); // short mill
                pos = !pos;
                output.WriteLine("G01 X{0:0.###}", pos ? r.right : r.left); // long mill
            }
            else // vertical strips
            {
                var xpos = r.left + step;
                while (xpos < r.right)
                {
                    output.WriteLine("G01 Y{0:0.###}", pos ? r.top : r.bottom); // long mill
                    output.WriteLine("G01 X{0:0.###}", xpos); // short mill
                    xpos += step;
                    pos = !pos;
                }
                output.WriteLine("G01 Y{0:0.###}", pos ? r.top : r.bottom); // long mill
                output.WriteLine("G01 X{0:0.###}", r.right); // short mill
                pos = !pos;
                output.WriteLine("G01 Y{0:0.###}", pos ? r.top : r.bottom); // long mill
            }
        }
#if OPTIMIZE
        private static Corner Snake(TextWriter output, Corner start, Rect r, float step)
        {
            var horiz = r.width > r.height;
            var longLabel = horiz ? "X" : "Y"; // Coordinate label for long side
            var shortLabel = horiz ? "Y" : "X"; // Coordinate label for short side

            // Does the long side start with positive direction?
            var longPos = horiz && (start == Corner.TopLeft || start == Corner.BottomLeft) ||
                          !horiz && (start == Corner.BottomLeft || start == Corner.BottomRight);

            // Does the short side have positive direction?
            var shortPos = horiz && (start == Corner.BottomLeft || start == Corner.BottomRight) ||
                           !horiz && (start == Corner.BottomLeft || start == Corner.TopLeft);

            // Negate step direction if necessary
            if (!shortPos)
                step = -step;

            var shortStart = 0f;
            var shortEnd = 0f;
            var longStart = 0f;
            var longEnd = 0f;

            switch (start)
            {
                case Corner.BottomLeft:
                    break;
                case Corner.BottomRight:
                    break;
                case Corner.TopLeft:
                    break;
                case Corner.TopRight:
                    break;
            }

            var sPos = shortStart;
            while (step < 0 ? sPos > shortEnd : sPos < shortEnd)
            {
                output.WriteLine($"G01 {longLabel}{(longPos ? longEnd : longStart):0.###}"); // long mill
                output.WriteLine($"G01 {shortLabel}{sPos:0.###}"); // short mill
                sPos += step;
                longPos = !longPos;
            }
            output.WriteLine($"G01 {longLabel}{(longPos ? longEnd : longStart):0.###}"); // long mill
            output.WriteLine($"G01 {shortLabel}{shortEnd:0.###}"); // short mill
            longPos = !longPos;
        }
#endif
        #region Save/Restore object
        public override void Load(BinaryReader data)
        {
            base.Load(data);
            width = data.ReadSingle();
            height = data.ReadSingle();
            depth = data.ReadSingle();
        }
        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(width);
            data.Write(height);
            data.Write(depth);
        }
        #endregion
    }
}