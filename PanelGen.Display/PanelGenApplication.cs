using PanelGen.Cli;

namespace PanelGen.Display
{
    public class PanelGenApplication
    {
        public PanelStock panel;

        public void LoadPanel(string path)
        {
            System.Diagnostics.Debug.WriteLine($"Load Panel @{path}");
        }

        public void SavePanel(string path)
        {
            System.Diagnostics.Debug.WriteLine($"Save Panel @{path}");
        }

        public void NewPanel(float width, float height, float thickness)
        {
            panel = new PanelStock()
            {
                width = width,
                height = height,
                thickness = thickness
            };
            //System.Diagnostics.Debug.WriteLine($"New Panel ({width}x{height}x{thickness})");
        }
    }
}
