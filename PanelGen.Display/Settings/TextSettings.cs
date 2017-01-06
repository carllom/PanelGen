using PanelGen.Cli;
using System;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class TextSettings : Form
    {
        private Text _dial;

        public TextSettings(Text dial)
        {
            _dial = dial;
            InitializeComponent();
            Load += TextSettings_Load;
            FormClosing += TextSettings_FormClosing;
        }

        private void GetValues(Text text)
        {
            numX.Value = Convert.ToDecimal(text.pos.x);
            numY.Value = Convert.ToDecimal(text.pos.y);

            txtLabelText.Text = text.text;
        }

        private void SetValues(Text text)
        {
            text.pos.x = Convert.ToSingle(numX.Value);
            text.pos.y = Convert.ToSingle(numY.Value);

            text.text = txtLabelText.Text;
        }

        private void TextSettings_Load(object sender, EventArgs e)
        {
            GetValues(_dial);
        }

        private void TextSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SetValues(_dial);
            }
        }
    }
}
