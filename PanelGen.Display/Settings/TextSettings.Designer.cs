namespace PanelGen.Display
{
    partial class TextSettings
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
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLabelText = new System.Windows.Forms.TextBox();
            this.numFontsize = new System.Windows.Forms.NumericUpDown();
            this.cboAlign = new System.Windows.Forms.ComboBox();
            this.cboTool = new System.Windows.Forms.ComboBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontsize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.numY);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(this.numX);
            groupBox2.Location = new System.Drawing.Point(12, 12);
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
            // label13
            // 
            label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(8, 114);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(28, 13);
            label13.TabIndex = 8;
            label13.Text = "Text";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(150, 28);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 13);
            label1.TabIndex = 13;
            label1.Text = "Font size";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(150, 54);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(30, 13);
            label2.TabIndex = 16;
            label2.Text = "Align";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(246, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtLabelText
            // 
            this.txtLabelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabelText.Location = new System.Drawing.Point(42, 111);
            this.txtLabelText.Name = "txtLabelText";
            this.txtLabelText.Size = new System.Drawing.Size(198, 20);
            this.txtLabelText.TabIndex = 12;
            // 
            // numFontsize
            // 
            this.numFontsize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numFontsize.DecimalPlaces = 1;
            this.numFontsize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFontsize.Location = new System.Drawing.Point(205, 26);
            this.numFontsize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numFontsize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFontsize.Name = "numFontsize";
            this.numFontsize.Size = new System.Drawing.Size(43, 20);
            this.numFontsize.TabIndex = 14;
            this.numFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFontsize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cboAlign
            // 
            this.cboAlign.FormattingEnabled = true;
            this.cboAlign.Location = new System.Drawing.Point(205, 51);
            this.cboAlign.Name = "cboAlign";
            this.cboAlign.Size = new System.Drawing.Size(116, 21);
            this.cboAlign.TabIndex = 15;
            // 
            // cboTool
            // 
            this.cboTool.FormattingEnabled = true;
            this.cboTool.Location = new System.Drawing.Point(205, 82);
            this.cboTool.Name = "cboTool";
            this.cboTool.Size = new System.Drawing.Size(116, 21);
            this.cboTool.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(150, 85);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(28, 13);
            label3.TabIndex = 18;
            label3.Text = "Tool";
            // 
            // TextSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 146);
            this.Controls.Add(label3);
            this.Controls.Add(this.cboTool);
            this.Controls.Add(label2);
            this.Controls.Add(this.cboAlign);
            this.Controls.Add(label1);
            this.Controls.Add(this.numFontsize);
            this.Controls.Add(this.txtLabelText);
            this.Controls.Add(label13);
            this.Controls.Add(this.button1);
            this.Controls.Add(groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TextSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TextSettings";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontsize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLabelText;
        private System.Windows.Forms.NumericUpDown numFontsize;
        private System.Windows.Forms.ComboBox cboAlign;
        private System.Windows.Forms.ComboBox cboTool;
    }
}