namespace PanelGen.Display
{
    partial class CircularPocketSettings
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            this.numDepth = new System.Windows.Forms.NumericUpDown();
            this.numDiameter = new System.Windows.Forms.NumericUpDown();
            this.numZ = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.applyChanges = new System.Windows.Forms.Button();
            this.removeStep = new System.Windows.Forms.Button();
            this.addStep = new System.Windows.Forms.Button();
            this.chkSteps = new System.Windows.Forms.CheckBox();
            this.stepList = new System.Windows.Forms.ListBox();
            this.numStepDepth = new System.Windows.Forms.NumericUpDown();
            this.numStepDiam = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.cboTool = new System.Windows.Forms.ComboBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameter)).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStepDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStepDiam)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.numDepth);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.numDiameter);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(146, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(161, 71);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dimensions";
            // 
            // numDepth
            // 
            this.numDepth.DecimalPlaces = 3;
            this.numDepth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDepth.Location = new System.Drawing.Point(86, 40);
            this.numDepth.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numDepth.Name = "numDepth";
            this.numDepth.Size = new System.Drawing.Size(66, 20);
            this.numDepth.TabIndex = 3;
            this.numDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 42);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 13);
            label2.TabIndex = 2;
            label2.Text = "Depth (mm)";
            // 
            // numDiameter
            // 
            this.numDiameter.DecimalPlaces = 3;
            this.numDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDiameter.Location = new System.Drawing.Point(86, 14);
            this.numDiameter.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numDiameter.Name = "numDiameter";
            this.numDiameter.Size = new System.Drawing.Size(66, 20);
            this.numDiameter.TabIndex = 1;
            this.numDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(74, 13);
            label1.TabIndex = 0;
            label1.Text = "Diameter (mm)";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.numZ);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(this.numY);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(this.numX);
            groupBox2.Location = new System.Drawing.Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(127, 103);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Position (Center)";
            // 
            // numZ
            // 
            this.numZ.DecimalPlaces = 3;
            this.numZ.Location = new System.Drawing.Point(51, 66);
            this.numZ.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numZ.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numZ.Name = "numZ";
            this.numZ.Size = new System.Drawing.Size(66, 20);
            this.numZ.TabIndex = 11;
            this.numZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 68);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 13);
            label3.TabIndex = 10;
            label3.Text = "Z (mm)";
            // 
            // numY
            // 
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
            this.numY.Size = new System.Drawing.Size(66, 20);
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
            this.numX.Size = new System.Drawing.Size(66, 20);
            this.numX.TabIndex = 7;
            this.numX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(152, 92);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(28, 13);
            label6.TabIndex = 4;
            label6.Text = "Tool";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.applyChanges);
            groupBox3.Controls.Add(this.removeStep);
            groupBox3.Controls.Add(this.addStep);
            groupBox3.Controls.Add(this.chkSteps);
            groupBox3.Controls.Add(this.stepList);
            groupBox3.Controls.Add(this.numStepDepth);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(this.numStepDiam);
            groupBox3.Controls.Add(label8);
            groupBox3.Location = new System.Drawing.Point(12, 121);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(214, 103);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Steps";
            // 
            // applyChanges
            // 
            this.applyChanges.Enabled = false;
            this.applyChanges.Location = new System.Drawing.Point(134, 71);
            this.applyChanges.Name = "applyChanges";
            this.applyChanges.Size = new System.Drawing.Size(32, 23);
            this.applyChanges.TabIndex = 19;
            this.applyChanges.Text = "<=";
            this.applyChanges.UseVisualStyleBackColor = true;
            this.applyChanges.Click += new System.EventHandler(this.Step_Click);
            // 
            // removeStep
            // 
            this.removeStep.Enabled = false;
            this.removeStep.Location = new System.Drawing.Point(174, 71);
            this.removeStep.Name = "removeStep";
            this.removeStep.Size = new System.Drawing.Size(32, 23);
            this.removeStep.TabIndex = 18;
            this.removeStep.Text = "-";
            this.removeStep.UseVisualStyleBackColor = true;
            this.removeStep.Click += new System.EventHandler(this.Step_Click);
            // 
            // addStep
            // 
            this.addStep.Enabled = false;
            this.addStep.Location = new System.Drawing.Point(91, 71);
            this.addStep.Name = "addStep";
            this.addStep.Size = new System.Drawing.Size(36, 23);
            this.addStep.TabIndex = 17;
            this.addStep.Text = "+";
            this.addStep.UseVisualStyleBackColor = true;
            this.addStep.Click += new System.EventHandler(this.Step_Click);
            // 
            // chkSteps
            // 
            this.chkSteps.AutoSize = true;
            this.chkSteps.Location = new System.Drawing.Point(40, 0);
            this.chkSteps.Name = "chkSteps";
            this.chkSteps.Size = new System.Drawing.Size(15, 14);
            this.chkSteps.TabIndex = 13;
            this.chkSteps.UseVisualStyleBackColor = true;
            this.chkSteps.CheckedChanged += new System.EventHandler(this.chkSteps_CheckedChanged);
            // 
            // stepList
            // 
            this.stepList.Enabled = false;
            this.stepList.FormattingEnabled = true;
            this.stepList.Location = new System.Drawing.Point(6, 21);
            this.stepList.Name = "stepList";
            this.stepList.Size = new System.Drawing.Size(68, 69);
            this.stepList.TabIndex = 12;
            this.stepList.SelectedIndexChanged += new System.EventHandler(this.stepList_SelectedIndexChanged);
            // 
            // numStepDepth
            // 
            this.numStepDepth.DecimalPlaces = 3;
            this.numStepDepth.Enabled = false;
            this.numStepDepth.Location = new System.Drawing.Point(142, 45);
            this.numStepDepth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numStepDepth.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numStepDepth.Name = "numStepDepth";
            this.numStepDepth.Size = new System.Drawing.Size(66, 20);
            this.numStepDepth.TabIndex = 11;
            this.numStepDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(80, 47);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(56, 13);
            label7.TabIndex = 10;
            label7.Text = "Startdepth";
            // 
            // numStepDiam
            // 
            this.numStepDiam.DecimalPlaces = 3;
            this.numStepDiam.Enabled = false;
            this.numStepDiam.Location = new System.Drawing.Point(142, 19);
            this.numStepDiam.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numStepDiam.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numStepDiam.Name = "numStepDiam";
            this.numStepDiam.Size = new System.Drawing.Size(66, 20);
            this.numStepDiam.TabIndex = 9;
            this.numStepDiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(80, 21);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(56, 13);
            label8.TabIndex = 8;
            label8.Text = "Diam (mm)";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(232, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cboTool
            // 
            this.cboTool.FormattingEnabled = true;
            this.cboTool.Location = new System.Drawing.Point(186, 89);
            this.cboTool.Name = "cboTool";
            this.cboTool.Size = new System.Drawing.Size(121, 21);
            this.cboTool.TabIndex = 3;
            // 
            // CircularPocketSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 227);
            this.Controls.Add(groupBox3);
            this.Controls.Add(label6);
            this.Controls.Add(this.cboTool);
            this.Controls.Add(this.button1);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CircularPocketSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PanelSettings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameter)).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStepDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStepDiam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numDepth;
        private System.Windows.Forms.NumericUpDown numDiameter;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numZ;
        private System.Windows.Forms.ComboBox cboTool;
        private System.Windows.Forms.ListBox stepList;
        private System.Windows.Forms.NumericUpDown numStepDepth;
        private System.Windows.Forms.NumericUpDown numStepDiam;
        private System.Windows.Forms.CheckBox chkSteps;
        private System.Windows.Forms.Button applyChanges;
        private System.Windows.Forms.Button removeStep;
        private System.Windows.Forms.Button addStep;
    }
}