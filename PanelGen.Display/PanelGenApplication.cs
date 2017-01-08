using PanelGen.Cli;
using System.IO;

namespace PanelGen.Display
{
    public class PanelGenApplication
    {
        public PanelStock panel;
        private PanelGenProject _project;
        public PanelComponent selected;

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
            var t = new Tool()
            {
                diameter = 3.175f,
                zStep = 0.256f
            };

            var saveCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            using (var file = new StreamWriter(path))
            {

                var engraver = new GCodeEngraver();
                // Write prologue
                foreach (var item in panel.items)
                {
                    item.GenerateCode(file, t);
                    file.WriteLine("G0 Z{0:0.###} F{1}", 1, 1500); // move to travel height
                }
                // Write epilogue

            }
            System.Threading.Thread.CurrentThread.CurrentCulture = saveCulture;
        }
    }
}
