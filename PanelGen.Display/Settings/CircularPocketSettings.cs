using PanelGen.Cli;
using System;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class CircularPocketSettings : Form
    {
        private CircularPocket _pocket;

        public CircularPocketSettings(CircularPocket pocket)
        {
            _pocket = pocket;
            InitializeComponent();
            Load += CircularPocketSettings_Load;
            FormClosing += CircularPocketSettings_FormClosing;
        }

        private void GetValues(CircularPocket pocket)
        {
            numDiameter.Value = Convert.ToDecimal(pocket.diameter);
            numDepth.Value = Convert.ToDecimal(pocket.depth);
            numX.Value = Convert.ToDecimal(pocket.x);
            numY.Value = Convert.ToDecimal(pocket.y);
        }

        private void SetValues(CircularPocket pocket)
        {
            pocket.diameter = Convert.ToSingle(numDiameter.Value);
            pocket.depth = Convert.ToSingle(numDepth.Value);
            pocket.x = Convert.ToSingle(numX.Value);
            pocket.y = Convert.ToSingle(numY.Value);
        }

        private void CircularPocketSettings_Load(object sender, EventArgs e)
        {
            GetValues(_pocket);
        }

        private void CircularPocketSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SetValues(_pocket);
            }
        }
    }
}
