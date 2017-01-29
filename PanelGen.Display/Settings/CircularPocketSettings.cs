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


            if (pocket.steps.Count > 0)
                chkSteps.Checked = true;
            stepList.Items.Clear();
            foreach (var step in pocket.steps)
            {
                stepList.Items.Add(step);
            }
            ApplyEnabledState();
        }

        private void SetValues(CircularPocket pocket)
        {
            pocket.diameter = Convert.ToSingle(numDiameter.Value);
            pocket.depth = Convert.ToSingle(numDepth.Value);
            pocket.pos.x = Convert.ToSingle(numX.Value);
            pocket.pos.y = Convert.ToSingle(numY.Value);
            pocket.pos.z = Convert.ToSingle(numZ.Value);
            pocket.toolNumber = (byte)((Tool)cboTool.SelectedItem).number;

            pocket.steps.Clear();
            foreach (CircularPocket.Step step in stepList.Items)
            {
                pocket.steps.Add(step);
            }
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

        private void Step_Click(object sender, EventArgs e)
        {
            if (sender == addStep)
            {
                stepList.Items.Add(new CircularPocket.Step(Convert.ToSingle(numStepDiam.Value), Convert.ToSingle(numStepDepth.Value)));
                ApplyEnabledState();
            }
            else if (sender == applyChanges)
            {
                var item = (CircularPocket.Step)stepList.SelectedItem;
                item.diameter = Convert.ToSingle(numStepDiam.Value);
                item.depth = Convert.ToSingle(numStepDepth.Value);
                stepList.Refresh();
            }
            else if (sender == removeStep)
            {
                stepList.Items.RemoveAt(stepList.SelectedIndex);
                ApplyEnabledState();
            }

        }

        private void chkSteps_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSteps.Checked)
            {
                stepList.Items.Clear();
                stepList.Items.Add(
                    new CircularPocket.Step(
                        Convert.ToSingle(numDiameter.Value), 
                        Convert.ToSingle(numDepth.Value)));
                ApplyEnabledState();
            }
            else
            {
                stepList.Items.Clear();
                ApplyEnabledState();
            }
        }

        private void ApplyEnabledState()
        {
            var stepsActive = chkSteps.Checked;
            var numItems = stepList.Items.Count;
            stepList.Enabled = stepsActive;
            addStep.Enabled = stepsActive;
            applyChanges.Enabled = stepsActive;
            removeStep.Enabled = stepsActive && numItems > 1;
            numStepDiam.Enabled = stepsActive;
            numStepDepth.Enabled = stepsActive;

            numDiameter.Enabled = !stepsActive;
        }

        private void stepList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (CircularPocket.Step)stepList.SelectedItem;
            numStepDiam.Value = Convert.ToDecimal(item.diameter);
            numStepDepth.Value = Convert.ToDecimal(item.depth);
        }
    }
}
