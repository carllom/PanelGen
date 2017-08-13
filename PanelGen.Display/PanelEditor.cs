using PanelGen.Cli;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public partial class PanelEditor : Form
    {
        private readonly PanelGenApplication _app = new PanelGenApplication();
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
                if (openProjectFileDialog.ShowDialog() == DialogResult.OK)
                    _app.LoadPanel(openProjectFileDialog.FileName);
            }
            else if (sender == fileSaveMenuItem)
            {
                if (saveProjectFileDialog.ShowDialog() == DialogResult.OK)
                    _app.SavePanel(saveProjectFileDialog.FileName);
            }
            else if (sender == fileExitMenuItem)
            {
                Application.Exit();
            }
            else if (sender == panelSettingsMenuItem)
            {
                var settings = new PanelSettings(_app.panel);
                settings.ShowDialog();
            }
            else if (sender == panelAddDialMenuItem)
            {
                var d = CreateDummyDial();//new Dial();
                var settings = new DialSettings(d, _app);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(d);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddRectPocket)
            {
                var rp = new RectangularPocket();
                var settings = new RectangularPocketSettings(rp, _app);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(rp);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddCircPocket)
            {
                var cp = new CircularPocket();
                var settings = new CircularPocketSettings(cp, _app);
                if (settings.ShowDialog()==DialogResult.OK)
                {
                    _app.panel.items.Add(cp);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddText)
            {
                var t = new Text("TEXT");
                var settings = new TextSettings(t, _app);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(t);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelAddPolyline)
            {
                var pl = new PolyLine();
                var settings = new PolyLineSettings(pl, _app);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    _app.panel.items.Add(pl);
                    viewPanel.Invalidate();
                }
            }
            else if (sender == panelToolSettingsMenuItem)
            {
                var settings = new ToolSettings(_app);
                settings.ShowDialog();
            }
            else if (sender == panelGenGCode)
            {
                if (saveGCodeFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _app.Generate(saveGCodeFileDialog.FileName);
                }
            }
            else if (sender == viewShowGridMenuItem)
            {
                viewPanel.ShowGrid = !viewPanel.ShowGrid;
                viewShowGridMenuItem.Checked = viewPanel.ShowGrid;

            }
            else if (sender == editParametersMenuItem)
            {
                var sel = _app?.selected;
                if (sel == null)
                    return;

                if (sel is CircularPocket)
                {
                    var settings = new CircularPocketSettings(sel as CircularPocket, _app);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is Dial)
                {
                    var settings = new DialSettings(sel as Dial, _app);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is RectangularPocket)
                {
                    var settings = new RectangularPocketSettings(sel as RectangularPocket, _app);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is Text)
                {
                    var settings = new TextSettings(sel as Text, _app);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
                else if (sel is PolyLine)
                {
                    var settings = new PolyLineSettings(sel as PolyLine, _app);
                    if (settings.ShowDialog() == DialogResult.OK)
                        viewPanel.Refresh();
                }
            }
            else if (sender == editCloneMenuItem)
            {
                var sel = _app?.selected;
                if (!(sel is PanelStockItem))
                    return;

                var newComponent = ((PanelStockItem)sel).Clone();
                newComponent.pos = newComponent.pos + new Vertex3(10, 10);
                _app.panel.items.Add(newComponent);
                _app.selected = newComponent;
                viewPanel.Refresh();
            }
            else if (sender == editDeleteMenuItem)
            {
                var sel = _app?.selected;
                if (!(sel is PanelStockItem))
                    return;

                _app.panel.items.Remove((PanelStockItem)sel);
                _app.selected = null;
                viewPanel.Refresh();
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
            else if (sender == polylineTool)
            {
                MenuItem_Click(panelAddPolyline, e);
            }
            else if (sender == toolEditSelected)
            {
                MenuItem_Click(editParametersMenuItem, e);
            }
            else if (sender == toolCopySelected)
            {
                MenuItem_Click(editCloneMenuItem, e);
            }
            else if (sender == toolDeleteSelected)
            {
                MenuItem_Click(editDeleteMenuItem, e);
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

        private void PanelEditor_KeyUp(object sender, KeyEventArgs e)
        {
            // Move selected object
            if (_app.selected != null)
            {
                e.SuppressKeyPress = true; // Assume we handle the keystroke
                var moved = false;
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        if (e.Modifiers == Keys.Control)
                            _app.selected.pos.x -= .1f;
                        else
                            _app.selected.pos.x -= 1;
                        moved = true;
                        break;
                    case Keys.Right:
                        if (e.Modifiers == Keys.Control)
                            _app.selected.pos.x += .1f;
                        else
                            _app.selected.pos.x += 1;
                        moved = true;
                        break;
                    case Keys.Up:
                        if (e.Modifiers == Keys.Control)
                            _app.selected.pos.y += .1f;
                        else
                            _app.selected.pos.y += 1;
                        moved = true;
                        break;
                    case Keys.Down:
                        if (e.Modifiers == Keys.Control)
                            _app.selected.pos.y -= .1f;
                        else
                            _app.selected.pos.y -= 1;
                        moved = true;
                        break;
                    default:
                        e.SuppressKeyPress = false; // Reset if we did not handle key
                        break;
                }
                if (moved)
                    viewPanel.Refresh();
            }
        }
    }
}
