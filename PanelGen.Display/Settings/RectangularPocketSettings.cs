using PanelGen.Cli;
using System;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class RectangularPocketSettings : Form
    {
        private RectangularPocket _pocket;

        public RectangularPocketSettings(RectangularPocket pocket)
        {
            _pocket = pocket;
            InitializeComponent();
            Load += CircularPocketSettings_Load;
            FormClosing += CircularPocketSettings_FormClosing;
        }

        private void GetValues(RectangularPocket pocket)
        {
            numWidth.Value = Convert.ToDecimal(pocket.width);
            numHeight.Value = Convert.ToDecimal(pocket.height);
            numDepth.Value = Convert.ToDecimal(pocket.depth);
            numX.Value = Convert.ToDecimal(pocket.x);
            numY.Value = Convert.ToDecimal(pocket.y);
            numZ.Value = Convert.ToDecimal(pocket.z);
        }

        private void SetValues(RectangularPocket pocket)
        {
            pocket.width = Convert.ToSingle(numWidth.Value);
            pocket.height = Convert.ToSingle(numHeight.Value);
            pocket.depth = Convert.ToSingle(numDepth.Value);
            pocket.x = Convert.ToSingle(numX.Value);
            pocket.y = Convert.ToSingle(numY.Value);
            pocket.z = Convert.ToSingle(numZ.Value);
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
