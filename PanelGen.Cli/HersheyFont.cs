using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PanelGen.Cli
{
    public enum FontFace
    {
        GothicEnglishTriplex,
        GothicGermanTriplex,
        GothicItalianTriplex,
        GreekComplex,
        GreekComplexSmall,
        GreekPlain,
        GreekSimplex,
        CyrillicComplex,
        ItalicComplex,
        ItalicComplexSmall,
        ItalicTriplex,
        ScriptComplex,
        ScriptSimplex,
        RomanComplex,
        RomanComplexSmall,
        RomanDuplex,
        RomanPlain,
        RomanSimplex,
        RomanTriplex,
        Japanese
    }

    public class HersheyFont
    {
        private FontDefinition _fDef;

        public HersheyFont(FontFace face, float size = 2f)
        {
            _fDef = _defs.Single(d => d.face == face);
            Size = size;
        }

        private float _scale = .1f; // Relation between glyph coordinates and external coordinates
        private float _height = 2f;
        public float Size
        {
            get { return _height; }
            set
            {
                _height = value;
                var g = _fDef.GetGlyph('M');
                _scale = _height / g.height;
            }
        }

        public void DrawString(IDraw drw, string text, float x, float y, bool centerglyph = true)
        {
            var offx = x;
            var offy = y;

            foreach (var c in text)
            {
                var glyph = _fDef.GetGlyph(c) ?? new Glyph(); // Replace unknown with empty glyph
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
                        drw.MoveTo(gp.X * _scale + offx, -gp.Y * _scale + offy); // -Y because Hershey y is positive down and IDraw is positive up
                        raised = false;
                    }
                    else
                        drw.LineTo(gp.X * _scale + offx, -gp.Y * _scale + offy); // -Y because Hershey y is positive down and IDraw is positive up
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
            return text.Select(c => _fDef.GetGlyph(c) ?? new Glyph()).Select(glyph => glyph.posR - glyph.posL).Sum() * _scale;
        }

        /// <summary>
        /// CC-width (from center of first character to center of last).
        /// </summary>
        /// <param name="text">Text to measure</param>
        /// <returns></returns>
        public float InnerWidth(string text)
        {
            return Width(text) - ((_fDef.GetGlyph(text.Last()).posR - _fDef.GetGlyph(text.First()).posL) * _scale);
        }

        #region Font Definitions

        private static Dictionary<int, Glyph> LoadFont(string file)
        {
            var _font = new Dictionary<int, Glyph>();
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
                        data = new GlyphPoint[numVert - 1]
                    };

                    // Check if string is shorter than numVerts - adjust data size accordingly and read only available data
                    if (line.Length - 8 < numVert * 2)
                    {
                        System.Diagnostics.Debug.WriteLine("input line too short");
                        glyph.data = new GlyphPoint[(line.Length - 10) / 2];
                        glyph.error = true;
                    }

                    for (var i = 0; i < glyph.data.Length; i++)
                    {
                        glyph.data[i] = new GlyphPoint(
                            (sbyte)(line[2 * i + 10] - 'R'),
                            (sbyte)(line[2 * i + 11] - 'R'));
                    }
                    _font.Add(charNum, glyph);
                }
            }
            return _font;
        }

        private static Dictionary<int, Glyph> _hershey;
        private static Dictionary<int, Glyph> _japanese;
        static HersheyFont()
        {
            _hershey = LoadFont(@".\data\hershey");
            _japanese = LoadFont(@".\data\japanese");
        }

        private static Dictionary<int, Glyph> Hershey() => _hershey ?? (_hershey = LoadFont(@".\data\hershey"));
        private static Dictionary<int, Glyph> Japanese() => _japanese ?? (_japanese = LoadFont(@".\data\japanese"));

        #region ASCII mappings

        private static readonly int[] _asciiMapcyrilc = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            2199, 2214, 2213, 2275, 2274, 2271, 2272, 2251, 2221, 2222, 2219, 2232, 2211, 2231, 2210, 2220, // 20-2Fh
            2200, 2201, 2202, 2203, 2204, 2205, 2206, 2207, 2208, 2209, 2212, 2213, 2241, 2238, 2242, 2215, // 30-3Fh
            2273, 2801, 2802, 2803, 2804, 2805, 2806, 2807, 2808, 2809, 2810, 2811, 2812, 2813, 2814, 2815, // 40-4Fh
            2816, 2817, 2818, 2819, 2820, 2821, 2822, 2823, 2824, 2825, 2826, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2252, 2901, 2902, 2903, 2904, 2905, 2906, 2907, 2908, 2909, 2910, 2911, 2912, 2913, 2914, 2915, // 60-6Fh
            2916, 2917, 2918, 2919, 2920, 2921, 2922, 2923, 2924, 2925, 2926, 2225, 2229, 2226, 2246, 2218 // 70-7Fh
        };

        private static readonly int[] _asciiMapgothgbt = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            3699, 3714, 3728, 2275, 3719, 2271, 3718, 3717, 3721, 3722, 3723, 3725, 3711, 3724, 3710, 3720, // 20-2Fh
            3700, 3701, 3702, 3703, 3704, 3705, 3706, 3707, 3708, 3709, 3712, 3713, 2241, 3726, 2242, 3715, // 30-3Fh
            2273, 3501, 3502, 3503, 3504, 3505, 3506, 3507, 3508, 3509, 3510, 3511, 3512, 3513, 3514, 3515, // 40-4Fh
            3516, 3517, 3518, 3519, 3520, 3521, 3522, 3523, 3524, 3525, 3526, 2223, 804, 2224, 2262, 999, // 50-5Fh
            3716, 3601, 3602, 3603, 3604, 3605, 3606, 3607, 3608, 3609, 3610, 3611, 3612, 3613, 3614, 3615, // 60-6Fh
            3616, 3617, 3618, 3619, 3620, 3621, 3622, 3623, 3624, 3625, 3626, 2225, 2229, 2226, 2246, 3729 // 70-7Fh
        };

        private static readonly int[] _asciiMapgothgrt = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            3699, 3714, 3728, 2275, 3719, 2271, 3718, 3717, 3721, 3722, 3723, 3725, 3711, 3724, 3710, 3720, // 20-2Fh
            3700, 3701, 3702, 3703, 3704, 3705, 3706, 3707, 3708, 3709, 3712, 3713, 2241, 3726, 2242, 3715, // 30-3Fh
            2273, 3301, 3302, 3303, 3304, 3305, 3306, 3307, 3308, 3309, 3310, 3311, 3312, 3313, 3314, 3315, // 40-4Fh
            3316, 3317, 3318, 3319, 3320, 3321, 3322, 3323, 3324, 3325, 3326, 2223, 804, 2224, 2262, 999, // 50-5Fh
            3716, 3401, 3402, 3403, 3404, 3405, 3406, 3407, 3408, 3409, 3410, 3411, 3412, 3413, 3414, 3415, // 60-6Fh
            3416, 3417, 3418, 3419, 3420, 3421, 3422, 3423, 3424, 3425, 3426, 2225, 2229, 2226, 2246, 3729 // 70-7Fh
        };
        private static readonly int[] _asciiMapgothitt = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            3699, 3714, 3728, 2275, 3719, 2271, 3718, 3717, 3721, 3722, 3723, 3725, 3711, 3724, 3710, 3720, // 20-2Fh
            3700, 3701, 3702, 3703, 3704, 3705, 3706, 3707, 3708, 3709, 3712, 3713, 2241, 3726, 2242, 3715, // 30-3Fh
            2273, 3801, 3802, 3803, 3804, 3805, 3806, 3807, 3808, 3809, 3810, 3811, 3812, 3813, 3814, 3815, // 40-4Fh
            3816, 3817, 3818, 3819, 3820, 3821, 3822, 3823, 3824, 3825, 3826, 2223, 804, 2224, 2262, 999, // 50-5Fh
            3716, 3901, 3902, 3903, 3904, 3905, 3906, 3907, 3908, 3909, 3910, 3911, 3912, 3913, 3914, 3915, // 60-6Fh
            3916, 3917, 3918, 3919, 3920, 3921, 3922, 3923, 3924, 3925, 3926, 2225, 2229, 2226, 2246, 3729 // 70-7Fh
        };
        private static readonly int[] _asciiMapgreekc = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            2199, 2214, 2213, 2275, 2274, 2271, 2272, 2251, 2221, 2222, 2219, 2232, 2211, 2231, 2210, 2220, // 20-2Fh
            2200, 2201, 2202, 2203, 2204, 2205, 2206, 2207, 2208, 2209, 2212, 2213, 2241, 2238, 2242, 2215, // 30-3Fh
            2273, 2027, 2028, 2029, 2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037, 2038, 2039, 2040, 2041, // 40-4Fh
            2042, 2043, 2044, 2045, 2046, 2047, 2048, 2049, 2050, 2199, 2199, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2252, 2127, 2128, 2129, 2130, 2131, 2132, 2133, 2134, 2135, 2136, 2137, 2138, 2139, 2140, 2141, // 60-6Fh
            2142, 2143, 2144, 2145, 2146, 2147, 2148, 2149, 2150, 2199, 2199, 2225, 2229, 2226, 2246, 2218 // 70-7Fh
        };
        private static readonly int[] _asciiMapgreekcs = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            1199, 1214, 1213, 1275, 1274, 1271, 1272, 1251, 1221, 1222, 1219, 1232, 1211, 1231, 1210, 1220, // 20-2Fh
            1200, 1201, 1202, 1203, 1204, 1205, 1206, 1207, 1208, 1209, 1212, 1213, 1241, 1238, 1242, 1215, // 30-3Fh
            1273, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040, 1041, // 40-4Fh
            1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1199, 1199, 1223, 804, 1224, 1262, 998, // 50-5Fh
            1252, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, // 60-6Fh
            1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150, 1199, 1199, 1225, 1229, 1226, 1246, 1218 // 70-7Fh
        };
        private static readonly int[] _asciiMapgreekp = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            199, 214, 217, 233, 219, 1271, 234, 231, 221, 222, 1219, 225, 211, 224, 210, 220, // 20-2Fh
            200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 212, 213, 1241, 226, 1242, 215, // 30-3Fh
            1273, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, // 40-4Fh
            42, 43, 44, 45, 46, 47, 48, 49, 50, 199, 199, 1223, 809, 1224, 1262, 997, // 50-5Fh
            230, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, // 60-6Fh
            42, 43, 44, 45, 46, 47, 48, 49, 50, 199, 199, 1225, 223, 1226, 1246, 218 // 70-7Fh
        };
        private static readonly int[] _asciiMapgreeks = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            699, 714, 717, 733, 719, 2271, 734, 731, 721, 722, 2219, 725, 711, 724, 710, 720, // 20-2Fh
            700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 712, 713, 2241, 726, 2242, 715, // 30-3Fh
            2273, 527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539, 540, 541, // 40-4Fh
            542, 543, 544, 545, 546, 547, 548, 549, 550, 699, 699, 2223, 804, 2224, 2262, 999, // 50-5Fh
            730, 627, 628, 629, 630, 631, 632, 633, 634, 635, 636, 637, 638, 639, 640, 641, // 60-6Fh
            642, 643, 644, 645, 646, 647, 648, 649, 650, 699, 699, 2225, 723, 2226, 2246, 718 // 70-7Fh
        };
        private static readonly int[] _asciiMapitalicc = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            2749, 2764, 2778, 2275, 2769, 2271, 2768, 2767, 2771, 2772, 2773, 2775, 2761, 2774, 2760, 2770, // 20-2Fh
            2750, 2751, 2752, 2753, 2754, 2755, 2756, 2757, 2758, 2759, 2762, 2763, 2241, 2776, 2242, 2765, // 30-3Fh
            2273, 2051, 2052, 2053, 2054, 2055, 2056, 2057, 2058, 2059, 2060, 2061, 2062, 2063, 2064, 2065, // 40-4Fh
            2066, 2067, 2068, 2069, 2070, 2071, 2072, 2073, 2074, 2075, 2076, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2766, 2151, 2152, 2153, 2154, 2155, 2156, 2157, 2158, 2159, 2160, 2161, 2162, 2163, 2164, 2165, // 60-6Fh
            2166, 2167, 2168, 2169, 2170, 2171, 2172, 2173, 2174, 2175, 2176, 2225, 2229, 2226, 2246, 2779 // 70-7Fh
        };
        private static readonly int[] _asciiMapitaliccs = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            1199, 1214, 1213, 1275, 1274, 1271, 1272, 1251, 1221, 1222, 1219, 1232, 1211, 1231, 1210, 802, // 20-2Fh
            1200, 1201, 1202, 1203, 1204, 1205, 1206, 1207, 1208, 1209, 1212, 1213, 1241, 1238, 1242, 1215, // 30-3Fh
            1273, 1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 1060, 1061, 1062, 1063, 1064, 1065, // 40-4Fh
            1066, 1067, 1068, 1069, 1070, 1071, 1072, 1073, 1074, 1075, 1076, 1223, 804, 1224, 1262, 998, // 50-5Fh
            1252, 1151, 1152, 1153, 1154, 1155, 1156, 1157, 1158, 1159, 1160, 1161, 1162, 1163, 1164, 1165, // 60-6Fh
            1166, 1167, 1168, 1169, 1170, 1171, 1172, 1173, 1174, 1175, 1176, 1225, 1229, 1226, 1246, 1218 // 70-7Fh
        };
        private static readonly int[] _asciiMapitalict = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            3249, 3264, 3278, 2275, 3269, 2271, 3268, 3267, 3271, 3272, 3273, 3275, 3261, 3274, 3260, 3270, // 20-2Fh
            3250, 3251, 3252, 3253, 3254, 3255, 3256, 3257, 3258, 3259, 3262, 3263, 2241, 3276, 2242, 3265, // 30-3Fh
            2273, 3051, 3052, 3053, 3054, 3055, 3056, 3057, 3058, 3059, 3060, 3061, 3062, 3063, 3064, 3065, // 40-4Fh
            3066, 3067, 3068, 3069, 3070, 3071, 3072, 3073, 3074, 3075, 3076, 2223, 804, 2224, 2262, 999, // 50-5Fh
            3266, 3151, 3152, 3153, 3154, 3155, 3156, 3157, 3158, 3159, 3160, 3161, 3162, 3163, 3164, 3165, // 60-6Fh
            3166, 3167, 3168, 3169, 3170, 3171, 3172, 3173, 3174, 3175, 3176, 2225, 2229, 2226, 2246, 3279 // 70-7Fh
        };
        private static readonly int[] _asciiMapromanc = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            2199, 2214, 2213, 2275, 2274, 2271, 2272, 2251, 2221, 2222, 2219, 2232, 2211, 2231, 2210, 2220, // 20-2Fh
            2200, 2201, 2202, 2203, 2204, 2205, 2206, 2207, 2208, 2209, 2212, 2213, 2241, 2238, 2242, 2215, // 30-3Fh
            2273, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, // 40-4Fh
            2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2252, 2101, 2102, 2103, 2104, 2105, 2106, 2107, 2108, 2109, 2110, 2111, 2112, 2113, 2114, 2115, // 60-6Fh
            2116, 2117, 2118, 2119, 2120, 2121, 2122, 2123, 2124, 2125, 2126, 2225, 2229, 2226, 2246, 2218 // 70-7Fh
        };
        private static readonly int[] _asciiMapromancs = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            1199, 1214, 1213, 1275, 1274, 1271, 1272, 1251, 1221, 1222, 1219, 1232, 1211, 1231, 1210, 1220, // 20-2Fh
            1200, 1201, 1202, 1203, 1204, 1205, 1206, 1207, 1208, 1209, 1212, 1213, 1241, 1238, 1242, 1215, // 30-3Fh
            1273, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, // 40-4Fh
            1016, 1017, 1018, 1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1223, 804, 1224, 1262, 998, // 50-5Fh
            1252, 1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113, 1114, 1115, // 60-6Fh
            1116, 1117, 1118, 1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1225, 1229, 1226, 1246, 1218 // 70-7Fh
        };
        private static readonly int[] _asciiMapromand = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            2699, 2714, 2728, 2275, 2719, 2271, 2718, 2717, 2721, 2722, 2723, 2725, 2711, 2724, 2710, 2720, // 20-2Fh
            2700, 2701, 2702, 2703, 2704, 2705, 2706, 2707, 2708, 2709, 2712, 2713, 2241, 2726, 2242, 2715, // 30-3Fh
            2273, 2501, 2502, 2503, 2504, 2505, 2506, 2507, 2508, 2509, 2510, 2511, 2512, 2513, 2514, 2515, // 40-4Fh
            2516, 2517, 2518, 2519, 2520, 2521, 2522, 2523, 2524, 2525, 2526, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2716, 2601, 2602, 2603, 2604, 2605, 2606, 2607, 2608, 2609, 2610, 2611, 2612, 2613, 2614, 2615, // 60-6Fh
            2616, 2617, 2618, 2619, 2620, 2621, 2622, 2623, 2624, 2625, 2626, 2225, 2229, 2226, 2246, 2729 // 70-7Fh
        };
        private static readonly int[] _asciiMapromanp = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            199, 214, 217, 233, 219, 1271, 234, 231, 221, 222, 1219, 225, 211, 224, 210, 220, // 20-2Fh
            200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 212, 213, 1241, 226, 1242, 215, // 30-3Fh
            1273, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, // 40-4Fh
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 1223, 809, 1224, 1262, 997, // 50-5Fh
            230, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, // 60-6Fh
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 1225, 223, 1226, 1246, 218 // 70-7Fh
        };
        private static readonly int[] _asciiMapromans = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            699, 714, 717, 733, 719, 2271, 734, 731, 721, 722, 2219, 725, 711, 724, 710, 720, // 20-2Fh
            700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 712, 713, 2241, 726, 2242, 715, // 30-3Fh
            2273, 501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, // 40-4Fh
            516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526, 2223, 804, 2224, 2262, 999, // 50-5Fh
            730, 601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, // 60-6Fh
            616, 617, 618, 619, 620, 621, 622, 623, 624, 625, 626, 2225, 723, 2226, 2246, 718 // 70-7Fh
        };
        private static readonly int[] _asciiMapromant = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            3199, 3214, 3228, 2275, 3219, 2271, 3218, 3217, 3221, 3222, 3223, 3225, 3211, 3224, 3210, 3220, // 20-2Fh
            3200, 3201, 3202, 3203, 3204, 3205, 3206, 3207, 3208, 3209, 3212, 3213, 2241, 3226, 2242, 3215, // 30-3Fh
            2273, 3001, 3002, 3003, 3004, 3005, 3006, 3007, 3008, 3009, 3010, 3011, 3012, 3013, 3014, 3015, // 40-4Fh
            3016, 3017, 3018, 3019, 3020, 3021, 3022, 3023, 3024, 3025, 3026, 2223, 804, 2224, 2262, 999, // 50-5Fh
            3216, 3101, 3102, 3103, 3104, 3105, 3106, 3107, 3108, 3109, 3110, 3111, 3112, 3113, 3114, 3115, // 60-6Fh
            3116, 3117, 3118, 3119, 3120, 3121, 3122, 3123, 3124, 3125, 3126, 2225, 2229, 2226, 2246, 3229 // 70-7Fh
        };
        private static readonly int[] _asciiMapscriptc = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            2749, 2764, 2778, 2275, 2769, 2271, 2768, 2767, 2771, 2772, 2773, 2775, 2761, 2774, 2760, 2770, // 20-2Fh
            2750, 2751, 2752, 2753, 2754, 2755, 2756, 2757, 2758, 2759, 2762, 2763, 2241, 2776, 2242, 2765, // 30-3Fh
            2273, 2551, 2552, 2553, 2554, 2555, 2556, 2557, 2558, 2559, 2560, 2561, 2562, 2563, 2564, 2565, // 40-4Fh
            2566, 2567, 2568, 2569, 2570, 2571, 2572, 2573, 2574, 2575, 2576, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2766, 2651, 2652, 2653, 2654, 2655, 2656, 2657, 2658, 2659, 2660, 2661, 2662, 2663, 2664, 2665, // 60-6Fh
            2666, 2667, 2668, 2669, 2670, 2671, 2672, 2673, 2674, 2675, 2676, 2225, 2229, 2226, 2246, 2779 // 70-7Fh
        };
        private static readonly int[] _asciiMapscripts = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 00-0Fh
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, // 10-1Fh
            699, 2764, 2778, 733, 2769, 2271, 2768, 2767, 2771, 2772, 2773, 725, 2761, 724, 710, 2770, // 20-2Fh
            2750, 2751, 2752, 2753, 2754, 2755, 2756, 2757, 2758, 2759, 2762, 2763, 2241, 726, 2242, 2765, // 30-3Fh
            2273, 551, 552, 553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565, // 40-4Fh
            566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 2223, 804, 2224, 2262, 999, // 50-5Fh
            2766, 651, 652, 653, 654, 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, // 60-6Fh
            666, 667, 668, 669, 670, 671, 672, 673, 674, 675, 676, 2225, 723, 2226, 2246, 718 // 70-7Fh
        };
        #endregion

        private static FontDefinition[] _defs =
        {
            new FontDefinition { face=FontFace.GothicEnglishTriplex, name="Gothic English Triplex", asciiMap =_asciiMapgothgbt, glyphs=Hershey },
            new FontDefinition { face=FontFace.GothicGermanTriplex,name="Gothic German Triplex", asciiMap =_asciiMapgothgrt, glyphs=Hershey},
            new FontDefinition { face=FontFace.GothicItalianTriplex, name="Gothic Italian Triplex", asciiMap =_asciiMapgothitt, glyphs=Hershey},
            new FontDefinition { face=FontFace.GreekComplex, name="Greek Complex", asciiMap =_asciiMapgreekc, glyphs=Hershey},
            new FontDefinition { face=FontFace.GreekComplexSmall, name="Greek Complex Small", asciiMap =_asciiMapgreekcs, glyphs=Hershey},
            new FontDefinition { face=FontFace.GreekPlain, name="Greek Plain", asciiMap =_asciiMapgreekp, glyphs=Hershey},
            new FontDefinition { face=FontFace.GreekSimplex, name="Greek Simplex", asciiMap =_asciiMapgreeks, glyphs=Hershey},
            new FontDefinition { face=FontFace.CyrillicComplex, name="Cyrillic Complex", asciiMap =_asciiMapcyrilc, glyphs=Hershey},
            new FontDefinition { face=FontFace.ItalicComplex, name="Italic Complex", asciiMap =_asciiMapitalicc, glyphs=Hershey },
            new FontDefinition { face=FontFace.ItalicComplexSmall, name="Italic Complex Small", asciiMap =_asciiMapitaliccs, glyphs=Hershey},
            new FontDefinition { face=FontFace.ItalicTriplex, name="Italic Triplex", asciiMap =_asciiMapitalict, glyphs=Hershey},
            new FontDefinition { face=FontFace.ScriptComplex, name="Script Complex", asciiMap =_asciiMapscriptc, glyphs=Hershey},
            new FontDefinition { face=FontFace.ScriptSimplex, name="Script Simplex", asciiMap =_asciiMapscripts, glyphs=Hershey},
            new FontDefinition { face=FontFace.RomanComplex, name="Roman Complex", asciiMap =_asciiMapromanc, glyphs=Hershey },
            new FontDefinition { face=FontFace.RomanComplexSmall, name="Roman Complex Small", asciiMap =_asciiMapromancs, glyphs=Hershey},
            new FontDefinition { face=FontFace.RomanDuplex, name="Roman Duplex", asciiMap =_asciiMapromand, glyphs=Hershey },
            new FontDefinition { face=FontFace.RomanPlain, name="Roman Plain", asciiMap =_asciiMapromanp, glyphs=Hershey },
            new FontDefinition { face=FontFace.RomanSimplex, name="Roman Simplex", asciiMap =_asciiMapromans, glyphs=Hershey },
            new FontDefinition { face=FontFace.RomanTriplex, name="Roman Triplex", asciiMap =_asciiMapromant, glyphs=Hershey },
            new FontDefinition { face=FontFace.Japanese, name="Japanese", asciiMap = { }, glyphs=Japanese }
        };

        private class FontDefinition
        {
            public FontFace face;
            public int[] asciiMap;
            public string name;
            public Func<Dictionary<int, Glyph>> glyphs;

            public Glyph GetGlyph(char c)
            {
                if (c > 0x7F) // Illegal ASCII range
                    return null;
                var idx = asciiMap[c];
                if (idx == -1) // No mapped character
                    return null;

                return glyphs()[idx];
            }
        }

        #endregion

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
