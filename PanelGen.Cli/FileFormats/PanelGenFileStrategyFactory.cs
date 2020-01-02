using System.IO;

namespace PanelGen.Cli.FileFormats
{
    public class PanelGenFileStrategyFactory
    {
        public IPanelGenFileStrategy GetStrategy(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".xpnl": // XML format
                    return new XmlPanelGenFileStrategy();
                case ".pnl": // Binary format
                    return new BinaryPanelGenFileStrategy();
                default: // (Default to binary for unknown extensions)
                    return new BinaryPanelGenFileStrategy();
            }

        }
    }
}
