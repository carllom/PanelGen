using System.Collections.Generic;

namespace PanelGen.Cli
{
    /// <summary>
    /// PanelGen project file
    /// Contains panel component data and tool parameters
    /// </summary>
    public class PanelGenProject
    {
        public PanelStock Stock { get; set; } = new PanelStock();
        public List<Tool> Tools { get; set; } = new List<Tool>();
    }
}
