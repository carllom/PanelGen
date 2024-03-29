﻿using System;
using System.IO;

namespace PanelGen.Cli
{
    public class Dial : PanelStockItem
    {
        public byte holeToolNumber; // Tool number for hole
        public float holeRadius; // radius of hole for pot axis
        public float holeDepth; // depth of pot axis hole

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

        // Labels and text
        public string text; // Dial label
        public HersheyFont LabelFont { get; set; } = new HersheyFont(FontFace.RomanSimplex, 3f);
        public HersheyFont MarkerFont { get; set; } = new HersheyFont(FontFace.RomanSimplex, 1.5f);
        public float markerLabelOffset = 1.5f;

        public override bool UsesTool(int toolNumber)
        {
            return base.UsesTool(toolNumber) || toolNumber == holeToolNumber;
        }

        public override Vertex3 Extents
        {
            get
            {
                ExtentsRenderer xr = new ExtentsRenderer();
                Draw(xr);
                return xr.Extents;
            }
        }

        public override bool Inside(float x, float y)
        {
            ExtentsRenderer xr = new ExtentsRenderer();
            Draw(xr);
            return xr.Inside(x, y);
        }

        public override PanelStockItem Clone()
        {
            var copy = new Dial
            {
                pos = pos,
                toolNumber = toolNumber,
                holeToolNumber = holeToolNumber,
                holeRadius = holeRadius,
                holeDepth = holeDepth,
                innerRadius = innerRadius,
                arcSpan = arcSpan,
                markerLength = markerLength,
                minValue = minValue,
                maxValue = maxValue,
                step = step,
                tickLength = tickLength,
                tickCount = tickCount,
                text = text,
                LabelFont = LabelFont,
                MarkerFont = MarkerFont,
                markerLabelOffset = markerLabelOffset
            };
            return copy;
        }

        public void Draw(IDraw drw)
        {
            var startAng = (360 - arcSpan) * Math.PI / 360;
            var mCount = (maxValue - minValue) / step;
            var markerArc = arcSpan / mCount * Math.PI / 180;
            var tickArc = markerArc / (tickCount + 1);
            var outerRadius = innerRadius + markerLength;

            for (var i = 0; i < mCount; i++)
            {
                var mArc = startAng + i * markerArc;
                DrawTick(mArc, pos.x, pos.y, innerRadius, outerRadius, drw);
                DrawTickLabel((minValue + i * step).ToString(), mArc, pos.x, pos.y, outerRadius + markerLabelOffset, MarkerFont, drw);
                for (var j = tickArc; j < markerArc; j += tickArc)
                {
                    DrawTick(mArc + j, pos.x, pos.y, innerRadius, innerRadius + tickLength, drw);
                }
            }
            // Max marker
            var maxAngle = startAng + mCount * markerArc;
            DrawTick(maxAngle, pos.x, pos.y, innerRadius, outerRadius, drw);
            DrawTickLabel(maxValue.ToString(), maxAngle, pos.x, pos.y, outerRadius + markerLabelOffset, MarkerFont, drw);

            // Dial text
            var fWidth = LabelFont.Width(text);
            LabelFont.DrawString(drw, text, pos.x - fWidth / 2, pos.y - innerRadius - markerLength - 3f, false); //TODO: Fix offsets - letters are rendered offset to center
        }

        public override void GenerateCode(TextWriter writer, Tool tool)
        {
            if (tool.number == toolNumber)
            {
                var engr = new GCodeEngraver();
                Draw(engr);
                writer.WriteLine(engr.GCode());
            }
            else if (tool.number == holeToolNumber)
            {
                var cp = new CircularPocket()
                {
                    diameter = holeRadius * 2,
                    pos = pos,
                    toolNumber = holeToolNumber,
                    depth = holeDepth
                };
                cp.GenerateCode(writer, tool);
            }
        }

        private static void DrawTick(double angle, float xc, float yc, float rInner, float rOuter, IDraw drw)
        {
            var xk = -(float)Math.Sin(angle);
            var yk = (float)Math.Cos(angle);
            drw.MoveTo(xc + xk * rInner, yc - yk * rInner);
            drw.LineTo(xc + xk * rOuter, yc - yk * rOuter);
        }

        private void DrawTickLabel(string text, double angle, float xc, float yc, float dist, HersheyFont font, IDraw drw)
        {
            if (font.Size < 1)
                return;

            var w = font.InnerWidth(text);
            var xk = -(float)Math.Sin(angle);
            var yk = (float)Math.Cos(angle);
            if (xk < -1e-8)
                w = -w; // Left side, align to end
            else if (xk <= 0)
                w = -w / 2; // Top side, align to center
            else
                w = 0; // Right side align to start

            font.DrawString(drw, text, xc + xk * dist + w, yc - yk * dist);
        }

        #region Save/Restore object
        public override void Load(BinaryReader data)
        {
            base.Load(data);
            holeRadius = data.ReadSingle();
            holeDepth = data.ReadSingle();
            innerRadius = data.ReadSingle();
            arcSpan = data.ReadSingle();
            markerLength = data.ReadSingle();
            minValue = data.ReadInt32();
            maxValue = data.ReadInt32();
            step = data.ReadInt32();
            tickLength = data.ReadSingle();
            tickCount = data.ReadInt32();
            text = data.ReadString();
            markerLabelOffset = data.ReadSingle();
            MarkerFont.Size = data.ReadSingle();
            LabelFont.Size = data.ReadSingle();
            holeToolNumber = data.ReadByte();
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(holeRadius);
            data.Write(holeDepth);
            data.Write(innerRadius);
            data.Write(arcSpan);
            data.Write(markerLength);
            data.Write(minValue);
            data.Write(maxValue);
            data.Write(step);
            data.Write(tickLength);
            data.Write(tickCount);
            data.Write(text);
            data.Write(markerLabelOffset);
            data.Write(MarkerFont.Size);
            data.Write(LabelFont.Size);
            data.Write(holeToolNumber);
        }
        #endregion
    }
}
