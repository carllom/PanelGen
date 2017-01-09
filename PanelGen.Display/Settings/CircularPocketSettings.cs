using PanelGen.Cli;
using System;
using System.Windows.Forms;
using System.Linq;

namespace PanelGen.Display
{
    public partial class CircularPocketSettings : Form
    {
        private CircularPocket _pocket;

        public CircularPocketSettings(CircularPocket pocket, PanelGenApplication app)
        {
            _pocket = pocket;
            InitializeComponent();

            cboTool.Items.AddRange(app.Tools.ToArray());

            Load += CircularPocketSettings_Load;
            FormClosing += CircularPocketSettings_FormClosing;
        }

        private void GetValues(CircularPocket pocket)
        {
            numDiameter.Value = Convert.ToDecimal(pocket.diameter);
            numDepth.Value = Convert.ToDecimal(pocket.depth);
            numX.Value = Convert.ToDecimal(pocket.pos.x);
            numY.Value = Convert.ToDecimal(pocket.pos.y);
            numZ.Value = Convert.ToDecimal(pocket.pos.z);

            foreach (Tool t in cboTool.Items)
            {
                if (t.number == pocket.toolNumber)
                    cboTool.SelectedItem = t;
            }
        }

        private void SetValues(CircularPocket pocket)
        {
            pocket.diameter = Convert.ToSingle(numDiameter.Value);
            pocket.depth = Convert.ToSingle(numDepth.Value);
            pocket.pos.x = Convert.ToSingle(numX.Value);
            pocket.pos.y = Convert.ToSingle(numY.Value);
            pocket.pos.z = Convert.ToSingle(numZ.Value);
            pocket.toolNumber = (byte)((Tool)cboTool.SelectedItem).number;
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
