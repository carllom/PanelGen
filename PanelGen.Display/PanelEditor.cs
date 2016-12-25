﻿using PanelGen.Cli;
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
                _app.LoadPanel("");
            }
            else if (sender == fileSaveMenuItem)
            {
                _app.SavePanel("");
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

            }
            else if (sender == panelAddCircPocket)
            {
                var cp = new CircularPocket();
                var settings = new CircularPocketSettings(cp);
                settings.ShowDialog();
            }
        }

        private Dial CreateDummyDial()
        {
            return new Dial
            {
                x = 0,
                y = 0,
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
    }
}
