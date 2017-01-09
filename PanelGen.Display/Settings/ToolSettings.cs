using PanelGen.Cli;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace PanelGen.Display
{
    public partial class ToolSettings : Form
    {
        private PanelGenApplication _app;

        public ToolSettings(PanelGenApplication app)
        {
            _app = app;
            InitializeComponent();
            Load += ToolSettings_Load;
        }

        private void GetValues(IEnumerable<Tool> tools)
        {
            toolList.BeginUpdate();
            toolList.Items.Clear();
            foreach (var tool in tools)
            {
                toolList.Items.Add(tool);
            }
            toolList.EndUpdate();
        }

        private void ToolSettings_Load(object sender, EventArgs e)
        {
            GetValues(_app.Tools);
        }

        private void pointList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (Tool)toolList.SelectedItem;
            numToolNumber.Value = Convert.ToDecimal(item.number);
            numDiameter.Value = Convert.ToDecimal(item.diameter);
            numZStep.Value = Convert.ToDecimal(item.zStep);
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender == addPoint)
            {
                _app.AddTool(
                    new Tool() {
                        number = Convert.ToByte(numToolNumber.Value),
                        diameter = Convert.ToSingle(numDiameter.Value),
                        zStep = Convert.ToSingle(numZStep.Value)
                    });
            }
            else if (sender == removePoint)
            {
                _app.RemoveTool(((Tool)toolList.SelectedItem).number);
            }
            else if (sender == applyChanges)
            {
                if (toolList.SelectedItem == null)
                    return;
                var item = (Tool)toolList.SelectedItem;
                item.number = Convert.ToByte(numToolNumber.Value);
                item.diameter = Convert.ToSingle(numDiameter.Value);
                item.zStep = Convert.ToSingle(numZStep.Value);
            }
            GetValues(_app.Tools);
        }
    }
}
