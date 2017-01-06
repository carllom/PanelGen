using PanelGen.Cli;
using System;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class DialSettings : Form
    {
        private Dial _dial;

        public DialSettings(Dial dial)
        {
            _dial = dial;
            InitializeComponent();
            Load += CircularPocketSettings_Load;
            FormClosing += CircularPocketSettings_FormClosing;
        }

        private void GetValues(Dial dial)
        {
            numHoleRadius.Value = Convert.ToDecimal(dial.holeRadius);

            numX.Value = Convert.ToDecimal(dial.pos.x);
            numY.Value = Convert.ToDecimal(dial.pos.y);

            numScaleInnerRad.Value = Convert.ToDecimal(dial.innerRadius);
            numScaleArc.Value = Convert.ToDecimal(dial.arcSpan);

            numMarkerLength.Value = Convert.ToDecimal(dial.markerLength);
            numMinValue.Value = Convert.ToDecimal(dial.minValue);
            numMaxValue.Value = Convert.ToDecimal(dial.maxValue);
            numStep.Value = Convert.ToDecimal(dial.step);
            numLabelOffset.Value = Convert.ToDecimal(dial.markerLabelOffset);

            numTickLength.Value = Convert.ToDecimal(dial.tickLength);
            numTickCount.Value = Convert.ToDecimal(dial.tickCount);

            txtLabelText.Text = dial.text;
        }

        private void SetValues(Dial dial)
        {
            dial.holeRadius = Convert.ToSingle(numHoleRadius.Value);

            dial.pos.x = Convert.ToSingle(numX.Value);
            dial.pos.y = Convert.ToSingle(numY.Value);

            dial.innerRadius = Convert.ToSingle(numScaleInnerRad.Value);
            dial.arcSpan = Convert.ToSingle(numScaleArc.Value);

            dial.markerLength = Convert.ToSingle(numMarkerLength.Value);
            dial.minValue = Convert.ToInt32(numMinValue.Value);
            dial.maxValue = Convert.ToInt32(numMaxValue.Value);
            dial.step = Convert.ToInt32(numStep.Value);
            dial.markerLabelOffset = Convert.ToSingle(numLabelOffset.Value);

            dial.tickLength = Convert.ToSingle(numTickLength.Value);
            dial.tickCount = Convert.ToInt32(numTickCount.Value);

            dial.text = txtLabelText.Text;
        }

        private void CircularPocketSettings_Load(object sender, EventArgs e)
        {
            GetValues(_dial);
        }

        private void CircularPocketSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SetValues(_dial);
            }
        }
    }
}
