using PanelGen.Cli;
using System;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class PanelSettings : Form
    {
        private PanelStock _panel;

        public PanelSettings(PanelStock panel)
        {
            _panel = panel;
            InitializeComponent();

            Load += PanelSettings_Load;
            FormClosing += PanelSettings_FormClosing;
        }

        private void GetValues(PanelStock panel)
        {
            numWidth.Value = Convert.ToDecimal(panel.width);
            numHeight.Value = Convert.ToDecimal(panel.height);
            numThickness.Value = Convert.ToDecimal(panel.thickness);
            numX.Value = Convert.ToDecimal(panel.x);
            numY.Value = Convert.ToDecimal(panel.y);
        }

        private void SetValues(PanelStock panel)
        {
            panel.width = Convert.ToSingle(numWidth.Value);
            panel.height = Convert.ToSingle(numHeight.Value);
            panel.thickness = Convert.ToSingle(numThickness.Value);
            panel.x = Convert.ToSingle(numX.Value);
            panel.y = Convert.ToSingle(numY.Value);
        }

        private void PanelSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SetValues(_panel);
            }
        }

        private void PanelSettings_Load(object sender, EventArgs e)
        {
            GetValues(_panel);
        }
    }
}
