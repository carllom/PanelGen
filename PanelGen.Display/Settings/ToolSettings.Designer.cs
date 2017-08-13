namespace PanelGen.Display
{
    partial class ToolSettings
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.GroupBox groupBox1;
            this.numDiameter = new System.Windows.Forms.NumericUpDown();
            this.numZStep = new System.Windows.Forms.NumericUpDown();
            this.numToolNumber = new System.Windows.Forms.NumericUpDown();
            this.toolList = new System.Windows.Forms.ListBox();
            this.applyChanges = new System.Windows.Forms.Button();
            this.removePoint = new System.Windows.Forms.Button();
            this.addPoint = new System.Windows.Forms.Button();
            this.chkToolGenSeparate = new System.Windows.Forms.CheckBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToolNumber)).BeginInit();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(this.numDiameter);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(this.numZStep);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(this.numToolNumber);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new System.Drawing.Point(169, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(147, 107);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tool info";
            // 
            // numDiameter
            // 
            this.numDiameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numDiameter.DecimalPlaces = 3;
            this.numDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDiameter.Location = new System.Drawing.Point(61, 40);
            this.numDiameter.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numDiameter.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numDiameter.Name = "numDiameter";
            this.numDiameter.Size = new System.Drawing.Size(76, 20);
            this.numDiameter.TabIndex = 9;
            this.numDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 42);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(49, 13);
            label4.TabIndex = 8;
            label4.Text = "Diameter";
            // 
            // numZStep
            // 
            this.numZStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numZStep.DecimalPlaces = 3;
            this.numZStep.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numZStep.Location = new System.Drawing.Point(87, 66);
            this.numZStep.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            196608});
            this.numZStep.Name = "numZStep";
            this.numZStep.Size = new System.Drawing.Size(50, 20);
            this.numZStep.TabIndex = 15;
            this.numZStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 16);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(44, 13);
            label5.TabIndex = 6;
            label5.Text = "Number";
            // 
            // numToolNumber
            // 
            this.numToolNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numToolNumber.Location = new System.Drawing.Point(87, 14);
            this.numToolNumber.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numToolNumber.Name = "numToolNumber";
            this.numToolNumber.Size = new System.Drawing.Size(50, 20);
            this.numToolNumber.TabIndex = 7;
            this.numToolNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 68);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(37, 13);
            label3.TabIndex = 14;
            label3.Text = "Z-step";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.toolList);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(151, 211);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tools";
            // 
            // toolList
            // 
            this.toolList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolList.FormattingEnabled = true;
            this.toolList.Location = new System.Drawing.Point(3, 16);
            this.toolList.Name = "toolList";
            this.toolList.Size = new System.Drawing.Size(145, 192);
            this.toolList.TabIndex = 0;
            this.toolList.SelectedIndexChanged += new System.EventHandler(this.pointList_SelectedIndexChanged);
            // 
            // applyChanges
            // 
            this.applyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyChanges.Location = new System.Drawing.Point(262, 151);
            this.applyChanges.Name = "applyChanges";
            this.applyChanges.Size = new System.Drawing.Size(50, 23);
            this.applyChanges.TabIndex = 16;
            this.applyChanges.Text = "<=";
            this.applyChanges.UseVisualStyleBackColor = true;
            this.applyChanges.Click += new System.EventHandler(this.button_Click);
            // 
            // removePoint
            // 
            this.removePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removePoint.Location = new System.Drawing.Point(262, 180);
            this.removePoint.Name = "removePoint";
            this.removePoint.Size = new System.Drawing.Size(50, 23);
            this.removePoint.TabIndex = 15;
            this.removePoint.Text = "-";
            this.removePoint.UseVisualStyleBackColor = true;
            this.removePoint.Click += new System.EventHandler(this.button_Click);
            // 
            // addPoint
            // 
            this.addPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addPoint.Location = new System.Drawing.Point(262, 122);
            this.addPoint.Name = "addPoint";
            this.addPoint.Size = new System.Drawing.Size(50, 23);
            this.addPoint.TabIndex = 14;
            this.addPoint.Text = "+";
            this.addPoint.UseVisualStyleBackColor = true;
            this.addPoint.Click += new System.EventHandler(this.button_Click);
            // 
            // chkToolGenSeparate
            // 
            this.chkToolGenSeparate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkToolGenSeparate.AutoSize = true;
            this.chkToolGenSeparate.Location = new System.Drawing.Point(166, 209);
            this.chkToolGenSeparate.Name = "chkToolGenSeparate";
            this.chkToolGenSeparate.Size = new System.Drawing.Size(146, 17);
            this.chkToolGenSeparate.TabIndex = 17;
            this.chkToolGenSeparate.Text = "Generate in separate files";
            this.chkToolGenSeparate.UseVisualStyleBackColor = true;
            this.chkToolGenSeparate.CheckedChanged += new System.EventHandler(this.chk_Changed);
            // 
            // ToolSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 235);
            this.Controls.Add(this.chkToolGenSeparate);
            this.Controls.Add(this.addPoint);
            this.Controls.Add(this.applyChanges);
            this.Controls.Add(this.removePoint);
            this.Controls.Add(groupBox1);
            this.Controls.Add(groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ToolSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tool settings";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToolNumber)).EndInit();
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numDiameter;
        private System.Windows.Forms.NumericUpDown numToolNumber;
        private System.Windows.Forms.ListBox toolList;
        private System.Windows.Forms.Button removePoint;
        private System.Windows.Forms.Button addPoint;
        private System.Windows.Forms.Button applyChanges;
        private System.Windows.Forms.NumericUpDown numZStep;
        private System.Windows.Forms.CheckBox chkToolGenSeparate;
    }
}