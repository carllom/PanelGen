namespace PanelGen.Display
{
    partial class PolyLineSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label6;
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.applyChanges = new System.Windows.Forms.Button();
            this.removePoint = new System.Windows.Forms.Button();
            this.addPoint = new System.Windows.Forms.Button();
            this.numPointY = new System.Windows.Forms.NumericUpDown();
            this.numPointX = new System.Windows.Forms.NumericUpDown();
            this.pointList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numRadius = new System.Windows.Forms.NumericUpDown();
            this.cboTool = new System.Windows.Forms.ComboBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPointY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPointX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(this.numY);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(this.numX);
            groupBox2.Location = new System.Drawing.Point(330, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(129, 71);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Position (Center)";
            // 
            // numY
            // 
            this.numY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numY.DecimalPlaces = 3;
            this.numY.Location = new System.Drawing.Point(51, 40);
            this.numY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(68, 20);
            this.numY.TabIndex = 9;
            this.numY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 42);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(39, 13);
            label4.TabIndex = 8;
            label4.Text = "Y (mm)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 16);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(39, 13);
            label5.TabIndex = 6;
            label5.Text = "X (mm)";
            // 
            // numX
            // 
            this.numX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numX.DecimalPlaces = 3;
            this.numX.Location = new System.Drawing.Point(51, 14);
            this.numX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(68, 20);
            this.numX.TabIndex = 7;
            this.numX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.applyChanges);
            groupBox1.Controls.Add(this.removePoint);
            groupBox1.Controls.Add(this.addPoint);
            groupBox1.Controls.Add(this.numPointY);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.numPointX);
            groupBox1.Controls.Add(this.pointList);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(308, 235);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Points";
            // 
            // applyChanges
            // 
            this.applyChanges.Location = new System.Drawing.Point(192, 66);
            this.applyChanges.Name = "applyChanges";
            this.applyChanges.Size = new System.Drawing.Size(50, 23);
            this.applyChanges.TabIndex = 16;
            this.applyChanges.Text = "<=";
            this.applyChanges.UseVisualStyleBackColor = true;
            this.applyChanges.Click += new System.EventHandler(this.button_Click);
            // 
            // removePoint
            // 
            this.removePoint.Location = new System.Drawing.Point(192, 95);
            this.removePoint.Name = "removePoint";
            this.removePoint.Size = new System.Drawing.Size(50, 23);
            this.removePoint.TabIndex = 15;
            this.removePoint.Text = "-";
            this.removePoint.UseVisualStyleBackColor = true;
            this.removePoint.Click += new System.EventHandler(this.button_Click);
            // 
            // addPoint
            // 
            this.addPoint.Location = new System.Drawing.Point(248, 66);
            this.addPoint.Name = "addPoint";
            this.addPoint.Size = new System.Drawing.Size(54, 23);
            this.addPoint.TabIndex = 14;
            this.addPoint.Text = "+";
            this.addPoint.UseVisualStyleBackColor = true;
            this.addPoint.Click += new System.EventHandler(this.button_Click);
            // 
            // numPointY
            // 
            this.numPointY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numPointY.DecimalPlaces = 3;
            this.numPointY.Location = new System.Drawing.Point(234, 40);
            this.numPointY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numPointY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numPointY.Name = "numPointY";
            this.numPointY.Size = new System.Drawing.Size(68, 20);
            this.numPointY.TabIndex = 13;
            this.numPointY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(189, 42);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 13);
            label1.TabIndex = 12;
            label1.Text = "Y (mm)";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(189, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(39, 13);
            label2.TabIndex = 10;
            label2.Text = "X (mm)";
            // 
            // numPointX
            // 
            this.numPointX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numPointX.DecimalPlaces = 3;
            this.numPointX.Location = new System.Drawing.Point(234, 14);
            this.numPointX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numPointX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numPointX.Name = "numPointX";
            this.numPointX.Size = new System.Drawing.Size(68, 20);
            this.numPointX.TabIndex = 11;
            this.numPointX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pointList
            // 
            this.pointList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointList.FormattingEnabled = true;
            this.pointList.Location = new System.Drawing.Point(6, 19);
            this.pointList.Name = "pointList";
            this.pointList.Size = new System.Drawing.Size(177, 199);
            this.pointList.TabIndex = 0;
            this.pointList.SelectedIndexChanged += new System.EventHandler(this.pointList_SelectedIndexChanged);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(336, 91);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(40, 13);
            label3.TabIndex = 14;
            label3.Text = "Radius";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(380, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // numRadius
            // 
            this.numRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numRadius.DecimalPlaces = 3;
            this.numRadius.Location = new System.Drawing.Point(381, 89);
            this.numRadius.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numRadius.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numRadius.Name = "numRadius";
            this.numRadius.Size = new System.Drawing.Size(68, 20);
            this.numRadius.TabIndex = 15;
            this.numRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboTool
            // 
            this.cboTool.FormattingEnabled = true;
            this.cboTool.Location = new System.Drawing.Point(339, 134);
            this.cboTool.Name = "cboTool";
            this.cboTool.Size = new System.Drawing.Size(110, 21);
            this.cboTool.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(336, 118);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(28, 13);
            label6.TabIndex = 17;
            label6.Text = "Tool";
            // 
            // PolyLineSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 259);
            this.Controls.Add(label6);
            this.Controls.Add(this.cboTool);
            this.Controls.Add(this.numRadius);
            this.Controls.Add(label3);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PolyLineSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Polyline Settings";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPointY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPointX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numPointY;
        private System.Windows.Forms.NumericUpDown numPointX;
        private System.Windows.Forms.ListBox pointList;
        private System.Windows.Forms.Button removePoint;
        private System.Windows.Forms.Button addPoint;
        private System.Windows.Forms.Button applyChanges;
        private System.Windows.Forms.NumericUpDown numRadius;
        private System.Windows.Forms.ComboBox cboTool;
    }
}