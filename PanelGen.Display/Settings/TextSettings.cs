using PanelGen.Cli;
using System;
using System.Windows.Forms;
using System.Linq;

namespace PanelGen.Display
{
    public partial class TextSettings : Form
    {
        private Text _text;

        public TextSettings(Text text, PanelGenApplication app)
        {
            _text = text;
            InitializeComponent();
            Load += TextSettings_Load;
            FormClosing += TextSettings_FormClosing;

            cboTool.Items.AddRange(app.Tools.ToArray());

            cboAlign.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(Alignment)))
            {
                cboAlign.Items.Add(item);
            }
        }

        private void GetValues(Text text)
        {
            numX.Value = Convert.ToDecimal(text.pos.x);
            numY.Value = Convert.ToDecimal(text.pos.y);

            txtLabelText.Text = text.text;

            numFontsize.Value = Convert.ToDecimal(text.font.Size);
            cboAlign.SelectedItem = text.anchor;

            foreach (Tool t in cboTool.Items)
            {
                if (t.number == text.toolNumber)
                    cboTool.SelectedItem = t;
            }
        }

        private void SetValues(Text text)
        {
            text.pos.x = Convert.ToSingle(numX.Value);
            text.pos.y = Convert.ToSingle(numY.Value);

            text.text = txtLabelText.Text;

            text.font.Size = Convert.ToSingle(numFontsize.Value);
            text.anchor = (Alignment)cboAlign.SelectedItem;
            text.toolNumber = (byte)((Tool)cboTool.SelectedItem).number;
        }

        private void TextSettings_Load(object sender, EventArgs e)
        {
            GetValues(_text);
        }

        private void TextSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SetValues(_text);
            }
        }
    }
}
