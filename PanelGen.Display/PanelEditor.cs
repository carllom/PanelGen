using PanelGen.Cli;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class PanelEditor : Form
    {
        private PanelGenApplication _app = new PanelGenApplication();
        //private ScreenDraw _drw;
        private Pen _p = new Pen(Color.Black);

        public PanelEditor()
        {
            InitializeComponent();
            //var pGfx = viewPanel.CreateGraphics();
            //_drw = new ScreenDraw(pGfx, _p, 1, new PointF(viewPanel.Width / 2, viewPanel.Height / 2));
            viewPanel.Model = _app;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (sender == fileNewMenuItem)
            {
                _app.NewPanel(10, 10, 1);
                var settings = new PanelSettings(_app.panel);
                settings.ShowDialog(); // Allow user to set dimensions
                UpdateView();
            }
            else if (sender == fileOpenMenuItem)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    _app.LoadPanel(openFileDialog1.FileName);
            }
            else if (sender == fileSaveMenuItem)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    _app.SavePanel(saveFileDialog1.FileName);
            }
            else if (sender == fileExitMenuItem)
            {
                Application.Exit();
            }
            else if (sender == panelSettingsMenuItem)
            {
                var p = new PanelStock();
                var settings = new PanelSettings(p);
                settings.ShowDialog();
            }
            else if (sender == panelAddDialMenuItem)
            {
                var d = CreateDummyDial();//new Dial();
                var settings = new DialSettings(d);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(d);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddRectPocket)
            {
                var rp = new RectangularPocket();
                var settings = new RectangularPocketSettings(rp);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(rp);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddCircPocket)
            {
                var cp = new CircularPocket();
                var settings = new CircularPocketSettings(cp);
                if (settings.ShowDialog()==DialogResult.OK)
                {
                    _app.panel.items.Add(cp);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddText)
            {
                var cp = new Text("TEXT");
                var settings = new TextSettings(cp);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(cp);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == viewShowGridMenuItem)
            {
                viewPanel.ShowGrid = !viewPanel.ShowGrid;
                viewShowGridMenuItem.Checked = viewPanel.ShowGrid;

            }
        }

        private void tool_Click(object sender, EventArgs e)
        {
            if (sender == dialTool)
            {
                MenuItem_Click(panelAddDialMenuItem, e);
            }
            else if (sender == circPocketTool)
            {
                MenuItem_Click(panelAddCircPocket, e);
            }
            else if (sender == rectPocketTool)
            {
                MenuItem_Click(panelAddRectPocket, e);
            }
            else if (sender == textTool)
            {
                MenuItem_Click(panelAddText, e);
            }
            else if (sender == toolEditSelected)
            {
                var sel = _app?.selected;
                if (sel == null)
                    return;

                if (sel is CircularPocket)
                {
                    var settings = new CircularPocketSettings(sel as CircularPocket);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is Dial)
                {
                    var settings = new DialSettings(sel as Dial);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is RectangularPocket)
                {
                    var settings = new RectangularPocketSettings(sel as RectangularPocket);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is Text)
                {
                    var settings = new TextSettings(sel as Text);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
            }
        }


        private Dial CreateDummyDial()
        {
            return new Dial
            {
                pos = new Vertex3(),
                holeRadius = 6,

                minValue = 0,
                maxValue = 10,
                step = 1,
                innerRadius = 7.5f,
                arcSpan = 300,
                markerLength = 3.5f,
                tickCount = 4, // 4 lines for 5 intervals
                tickLength = 2,
                text = "Frequency".ToUpper()
            };
        }


        private void UpdateView()
        {
            viewPanel.Refresh();
        }

        private void viewPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void viewPanel_DoubleClick(object sender, EventArgs e)
        {
            var dp = viewPanel.DrawPosition;
            var found = false;
            foreach (var item in _app.panel.items)
            {
                if (item.Inside(dp.x, dp.y))
                {
                    if (_app.selected != item)
                    {
                        _app.selected = item;
                        found = true;
                    }
                    break;
                }
            }
            if (!found)
                _app.selected = null;
            viewPanel.Refresh();
        }
    }
}
