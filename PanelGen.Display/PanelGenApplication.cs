﻿using PanelGen.Cli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PanelGen.Display
{
    /// <summary>
    /// CAD application operations (no UI dependency)
    /// Translates user/ui operations to model updates
    /// </summary>
    public class PanelGenApplication
    {
        public PanelStock panel;
        private PanelGenProject _project;
        public PanelComponent selected;

        public bool genToolsSeparate;

        public PanelGenApplication()
        {
            // Default engraving tool - used by all components by default
            _tools.Add(new Tool()
            {
                diameter = 3.175f,
                zStep = 0.256f
            });
        }

        public void LoadPanel(string path)
        {
            if (_project == null)
                _project = new PanelGenProject();
            using (var str = File.OpenRead(path))
            {
                using (var br = new BinaryReader(str))
                {
                    var _project = new PanelGenProject();
                    _project.Load(br);
                    // TODO: Load view state
                    panel = _project.Stock;
                    _tools = _project.Tools;
                }
            }
        }

        public void SavePanel(string path)
        {
            using (var str = File.Create(path))
            {
                using (var br = new BinaryWriter(str))
                {
                    _project.Stock = panel;
                    _project.Tools = _tools;
                    _project.Save(br);
                    // TODO: Save view state
                }
            }
        }

        public void NewPanel(float width, float height, float thickness)
        {
            _project = new PanelGenProject();
            _project.Stock = new PanelStock()
            {
                width = width,
                height = height,
                thickness = thickness
            };
            panel = _project.Stock;
        }

        internal void Generate(string path)
        {
            var saveCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            if (genToolsSeparate) // Generate one file per tool
            {
                foreach (var tool in _tools)
                {
                    Generate(path, tool);
                }
            }
            else // All tools in one file
            {
                using (var file = new StreamWriter(path))
                {
                    var engraver = new GCodeEngraver();
                    // Write prologue

                    foreach (var tool in _tools)
                    {
                        file.WriteLine("T{0}", tool.number + 1); //TODO: fix for gcode simulator (tools 1+)
                        file.WriteLine("M06");
                        GenerateToolPath(file, tool);
                    }
                    // Write epilogue

                }
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = saveCulture;
        }

        private void Generate(string path, Tool tool)
        {
            var ext = Path.GetExtension(path);
            // rewrite filename with suffix "-T<number>"
            path = Path.Combine(Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path) + $"-T{tool.number+1}{ext}");
            using (var file = new StreamWriter(path))
            {
                var engraver = new GCodeEngraver();
                // Write prologue
                GenerateToolPath(file, tool);
                // Write epilogue
            }
        }

        private void GenerateToolPath(StreamWriter wr, Tool tool)
        {
            wr.WriteLine("T{0}", tool.number + 1); //TODO: fix for gcode simulator (tools 1+)
            wr.WriteLine("M06");
            foreach (var item in panel.items)
            {
                if (!item.UsesTool(tool.number))
                    continue; // Do not render in this tool pass
                item.GenerateCode(wr, tool);
                wr.WriteLine("G0 Z{0:0.###} F{1}", 1, 1500); // move to travel height
            }
        }

        #region Tools
        private List<Tool> _tools = new List<Tool>();

        public void AddTool(Tool tool)
        {
            if (_tools.Any(t => t.number == tool.number))
                throw new ApplicationException($"Tool number {tool.number} already exists!");

            _tools.Add(tool);
        }

        public void RemoveTool(int toolNumber)
        {
            var tool = _tools.SingleOrDefault(t => t.number == toolNumber);
            if (tool == null)
                throw new ApplicationException($"Tool number {toolNumber} not found!");
            _tools.Remove(tool);
        }

        public IEnumerable<Tool> Tools => _tools;

        public Tool GetTool(int toolNumber)
        {
            return _tools.SingleOrDefault(t => t.number == toolNumber);
        }
        #endregion
    }
}