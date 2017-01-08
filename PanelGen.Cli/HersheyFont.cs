using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PanelGen.Cli
{
    public class HersheyFont
    {
        public HersheyFont(string fontfile)
        {
            LoadFont(fontfile);
        }

        private float _scale = .1f; // Relation between glyph coordinates and external coordinates

        private float _height = 2f;
        public float Size {
            get { return _height; }
            set {
                _height = value;
                var g = GetGlyph('M');
                _scale = _height / g.height;
            }
        }

        public void DrawString(IDraw drw, string text, float x, float y, bool centerglyph = true)
        {
            var offx = x;
            var offy = y;

            foreach (var c in text)
            {
                var glyph = GetGlyph(c) ?? new Glyph(); // Replace unknown with empty glyph
                var raised = true; // We start in raised mode
                if (offx > x || !centerglyph) // Do not apply left offset for first character when doing centerglyph
                    offx -= glyph.posL * _scale; // Add left offset portion of this glyph (note: we assume it to be negative);
                // Render glyph
                foreach (var gp in glyph.data)
                {
                    if (gp.X == -50 && gp.Y == 0) // Special raise coordinate
                    {
                        raised = true; // Raise and read next point
                        continue;
                    }

                    if (raised)
                    {
                        drw.MoveTo(gp.X*_scale + offx, -gp.Y*_scale + offy); // -Y because Hershey y is positive down and IDraw is positive up
                        raised = false;
                    }
                    else
                        drw.LineTo(gp.X*_scale + offx, -gp.Y*_scale + offy); // -Y because Hershey y is positive down and IDraw is positive up
                }
                offx += glyph.posR * _scale; // Add right offset portion of this glyph
            }
        }

        /// <summary>
        /// Total width of text
        /// </summary>
        /// <param name="text">Text to measure</param>
        /// <returns></returns>
        public float Width(string text)
        {
            return text.Select(c => GetGlyph(c) ?? new Glyph()).Select(glyph => glyph.posR - glyph.posL).Sum() * _scale;
        }

        /// <summary>
        /// CC-width (from center of first character to center of last).
        /// </summary>
        /// <param name="text">Text to measure</param>
        /// <returns></returns>
        public float InnerWidth(string text)
        {
            return Width(text) - ((GetGlyph(text.Last()).posR - GetGlyph(text.First()).posL) * _scale);
        }

        private void LoadFont(string file)
        {
            using (StreamReader rdr = new StreamReader(file))
            {
                while (!rdr.EndOfStream)
                {
                    var line = rdr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty lines
                    /*
                         * The structure is bascially as follows: 
                         * each character consists of a number 1->4000 (not all used) in column 0:4
                         * the number of vertices in columns 5:7
                         * the left hand position in column 8
                         * the right hand position in column 9
                         * and finally the vertices in single character pairs.
                         * All coordinates are given relative to the ascii value of 'R'.
                         * If the coordinate value is " R" that indicates a pen up operation.
                         */
                    var charNum = int.Parse(line.Substring(0, 5));
                    var numVert = int.Parse(line.Substring(5, 3)); // Number of vertices *following* this data - including L/R pos
                    var glyph = new Glyph
                    {
                        posL = line[8] - 'R',
                        posR = line[9] - 'R',
                        data = new GlyphPoint[numVert-1]
                    };

                    // Check if string is shorter than numVerts - adjust data size accordingly and read only available data
                    if (line.Length - 8 < numVert*2)
                    {
                        System.Diagnostics.Debug.WriteLine("input line too short");
                        glyph.data = new GlyphPoint[(line.Length - 10)/2];
                        glyph.error = true;
                    }

                    for (var i = 0; i < glyph.data.Length; i++)
                    {
                        glyph.data[i] = new GlyphPoint(
                            (sbyte) (line[2*i + 10] - 'R'),
                            (sbyte) (line[2*i + 11] - 'R'));
                    }
                    _font.Add(charNum, glyph);
                }
            }
        }

        private Glyph GetGlyph(char c)
        {
            if (c > 0x7F) // Illegal ASCII range
                return null;
            var idx = _asciiMapRomanSimplex[c];
            if (idx == -1) // No mapped character
                return null;

            return _font[idx]; // TODO: Only maps Roman simplex currently
        }

        private readonly int[] _asciiMapRomanSimplex = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            699, 714, 717, 733, 719, 2271, 734, 731, 721, 722, 2219, 725, 711, 724, 710, 720, // 20-2Fh
            700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 712, 713, 2241, 726, 2242, 715, // 30-3Fh
            2273, 501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, // 40-4Fh
            516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526, 2223, 804, 2224, 2262, 999, // 50-5Fh
            730, 601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, // 60-6Fh
            616, 617, 618, 619, 620, 621, 622, 623, 624, 625, 626, 2225, 723, 2226, 2246, 718 // 70-7Fh
        };

        private readonly Dictionary<int, Glyph> _font = new Dictionary<int, Glyph>();
        private class Glyph
        {
            public int posL;
            public int posR;
            public GlyphPoint[] data = new GlyphPoint[0];
            public bool error;

            public float vMax => data.Max(d => d.Y);
            public float vMin => data.Min(d => d.Y);
            public float height => vMax - vMin;
        }

        private struct GlyphPoint
        {
            public GlyphPoint(sbyte x, sbyte y)
            {
                X = x;
                Y = y;
            }

            public readonly sbyte X;
            public readonly sbyte Y;
        }
    }
}
