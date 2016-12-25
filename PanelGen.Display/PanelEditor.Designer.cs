namespace PanelGen.Display
{
    partial class PanelEditor
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
            System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelEditor));
            this.panelAddDialMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelAddCircPocket = new System.Windows.Forms.ToolStripMenuItem();
            this.panelAddRectPocket = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.viewPanel = new PanelGen.Display.ViewPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dialTool = new System.Windows.Forms.ToolStripButton();
            this.circPocketTool = new System.Windows.Forms.ToolStripButton();
            this.rectPocketTool = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelAddDialMenuItem,
            this.panelAddCircPocket,
            this.panelAddRectPocket});
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            addToolStripMenuItem.Text = "Add";
            // 
            // panelAddDialMenuItem
            // 
            this.panelAddDialMenuItem.Name = "panelAddDialMenuItem";
            this.panelAddDialMenuItem.Size = new System.Drawing.Size(176, 22);
            this.panelAddDialMenuItem.Text = "Dial";
            this.panelAddDialMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // panelAddCircPocket
            // 
            this.panelAddCircPocket.Name = "panelAddCircPocket";
            this.panelAddCircPocket.Size = new System.Drawing.Size(176, 22);
            this.panelAddCircPocket.Text = "Circular pocket";
            this.panelAddCircPocket.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // panelAddRectPocket
            // 
            this.panelAddRectPocket.Name = "panelAddRectPocket";
            this.panelAddRectPocket.Size = new System.Drawing.Size(176, 22);
            this.panelAddRectPocket.Text = "Rectangular pocket";
            this.panelAddRectPocket.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.panelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(714, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenuItem,
            this.fileOpenMenuItem,
            this.fileSaveMenuItem,
            this.toolStripSeparator1,
            this.fileExitMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.newToolStripMenuItem.Text = "&File";
            // 
            // fileNewMenuItem
            // 
            this.fileNewMenuItem.Name = "fileNewMenuItem";
            this.fileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileNewMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fileNewMenuItem.Text = "New";
            this.fileNewMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileOpenMenuItem
            // 
            this.fileOpenMenuItem.Name = "fileOpenMenuItem";
            this.fileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpenMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fileOpenMenuItem.Text = "Open...";
            this.fileOpenMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // fileSaveMenuItem
            // 
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSaveMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fileSaveMenuItem.Text = "Save...";
            this.fileSaveMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.fileExitMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fileExitMenuItem.Text = "Exit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // panelToolStripMenuItem
            // 
            this.panelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelSettingsMenuItem,
            addToolStripMenuItem});
            this.panelToolStripMenuItem.Name = "panelToolStripMenuItem";
            this.panelToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.panelToolStripMenuItem.Text = "&Panel";
            // 
            // panelSettingsMenuItem
            // 
            this.panelSettingsMenuItem.Name = "panelSettingsMenuItem";
            this.panelSettingsMenuItem.Size = new System.Drawing.Size(125, 22);
            this.panelSettingsMenuItem.Text = "Settings...";
            this.panelSettingsMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.viewPanel);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(621, 476);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(714, 476);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // viewPanel
            // 
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 0);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(621, 476);
            this.viewPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialTool,
            this.circPocketTool,
            this.rectPocketTool});
            this.toolStrip1.Location = new System.Drawing.Point(0, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(93, 99);
            this.toolStrip1.TabIndex = 0;
            // 
            // dialTool
            // 
            this.dialTool.Image = ((System.Drawing.Image)(resources.GetObject("dialTool.Image")));
            this.dialTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dialTool.Name = "dialTool";
            this.dialTool.Size = new System.Drawing.Size(91, 20);
            this.dialTool.Text = "Dial";
            this.dialTool.Click += new System.EventHandler(this.tool_Click);
            // 
            // circPocketTool
            // 
            this.circPocketTool.Image = ((System.Drawing.Image)(resources.GetObject("circPocketTool.Image")));
            this.circPocketTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.circPocketTool.Name = "circPocketTool";
            this.circPocketTool.Size = new System.Drawing.Size(91, 20);
            this.circPocketTool.Text = "Circ. pocket";
            this.circPocketTool.Click += new System.EventHandler(this.tool_Click);
            // 
            // rectPocketTool
            // 
            this.rectPocketTool.Image = ((System.Drawing.Image)(resources.GetObject("rectPocketTool.Image")));
            this.rectPocketTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rectPocketTool.Name = "rectPocketTool";
            this.rectPocketTool.Size = new System.Drawing.Size(91, 20);
            this.rectPocketTool.Text = "Rect. pocket";
            this.rectPocketTool.Click += new System.EventHandler(this.tool_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "pnl";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PanelGen files|*.pnl|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "pnl";
            this.saveFileDialog1.Filter = "PanelGen files|*.pnl|All files|*.*";
            // 
            // PanelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 500);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PanelEditor";
            this.Text = "PanelEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelAddDialMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelAddCircPocket;
        private System.Windows.Forms.ToolStripMenuItem panelAddRectPocket;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton dialTool;
        private System.Windows.Forms.ToolStripButton circPocketTool;
        private System.Windows.Forms.ToolStripButton rectPocketTool;
        private ViewPanel viewPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}