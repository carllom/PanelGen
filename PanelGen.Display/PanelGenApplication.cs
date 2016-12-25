using PanelGen.Cli;
using System.IO;

namespace PanelGen.Display
{
    public class PanelGenApplication
    {
        public PanelStock panel;

        public void LoadPanel(string path)
        {
            if (panel == null)
                panel = new PanelStock();
            using (var str = File.OpenRead(path))
            {
                using (var br = new BinaryReader(str))
                {
                    panel.Load(br);
                    // TODO: Load view state
                }
            }
        }

        public void SavePanel(string path)
        {
            using (var str = File.Create(path))
            {
                using (var br = new BinaryWriter(str))
                {
                    panel.Save(br);
                    // TODO: Save view state
                }
            }
        }

        public void NewPanel(float width, float height, float thickness)
        {
            panel = new PanelStock()
            {
                width = width,
                height = height,
                thickness = thickness
            };
        }
    }
}
