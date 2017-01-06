using PanelGen.Cli;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace PanelGen.Display
{
    public partial class PolyLineSettings : Form
    {
        private PolyLine _polyline;

        public PolyLineSettings(PolyLine polyline)
        {
            _polyline = polyline;
            InitializeComponent();
            Load += PolyLineSettings_Load;
            FormClosing += PolyLineSettings_Closing;
        }

        private void GetValues(PolyLine polyline)
        {
            numX.Value = Convert.ToDecimal(polyline.pos.x);
            numY.Value = Convert.ToDecimal(polyline.pos.y);
            numRadius.Value = Convert.ToDecimal(polyline.radius);
            pointList.BeginUpdate();
            pointList.Items.Clear();
            foreach (var point in polyline.points)
            {
                pointList.Items.Add(new P(point));
            }
            pointList.EndUpdate();
        }

        private void SetValues(PolyLine polyline)
        {
            polyline.pos.x = Convert.ToSingle(numX.Value);
            polyline.pos.y = Convert.ToSingle(numY.Value);
            polyline.radius = Convert.ToSingle(numRadius.Value);
            polyline.points.Clear();
            foreach (P point in pointList.Items)
            {
                polyline.points.Add(point.Vertex);
            }
        }

        private void PolyLineSettings_Load(object sender, EventArgs e)
        {
            GetValues(_polyline);
        }

        private void PolyLineSettings_Closing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SetValues(_polyline);
            }
        }

        private void pointList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (P)pointList.SelectedItem;
            numPointX.Value = Convert.ToDecimal(item.x);
            numPointY.Value = Convert.ToDecimal(item.y); 
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender == addPoint)
            {
                pointList.Items.Add(new P(Convert.ToSingle(numPointX.Value), Convert.ToSingle(numPointY.Value)));
            }
            else if (sender == removePoint)
            {
                pointList.Items.RemoveAt(pointList.SelectedIndex);
            }
            else if (sender == applyChanges)
            {
                var item = (P)pointList.SelectedItem;
                item.x = Convert.ToSingle(numPointX.Value);
                item.y = Convert.ToSingle(numPointY.Value);
                pointList.Refresh();
            }
        }

        private class P {
            public float x;
            public float y;

            public P(float x, float y) { this.x = x; this.y = y; }
            public P(Vertex2 v) { x = v.x; y = v.y; }
            public override string ToString () => $"[X:{x}, Y:{y}]";
            public Vertex2 Vertex => new Vertex2(x, y);
        }
    }
}
