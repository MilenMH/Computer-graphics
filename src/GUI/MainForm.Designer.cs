﻿namespace Draw
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.currentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.speedMenu = new System.Windows.Forms.ToolStrip();
            this.ButtonMainNavigator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonDrowRectangle = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrowTriangle = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrowElipse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonMultiSelect = new System.Windows.Forms.ToolStripButton();
            this.ButtonRotateRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonFillColorRed = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorYellow = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorHotPink = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorBlack = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColor = new System.Windows.Forms.ToolStripButton();
            this.viewPort = new Draw.DoubleBufferedPanel();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.speedMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(924, 28);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentStatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 499);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(924, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // speedMenu
            // 
            this.speedMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.speedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonMainNavigator,
            this.toolStripSeparator3,
            this.ButtonDrowRectangle,
            this.ButtonDrowTriangle,
            this.ButtonDrowElipse,
            this.toolStripSeparator1,
            this.ButtonMultiSelect,
            this.ButtonRotateRight,
            this.toolStripSeparator2,
            this.ButtonFillColorRed,
            this.ButtonFillColorYellow,
            this.ButtonFillColorHotPink,
            this.ButtonFillColorBlack,
            this.ButtonFillColor});
            this.speedMenu.Location = new System.Drawing.Point(0, 28);
            this.speedMenu.Name = "speedMenu";
            this.speedMenu.Size = new System.Drawing.Size(924, 27);
            this.speedMenu.TabIndex = 3;
            this.speedMenu.Text = "toolStrip1";
            // 
            // ButtonMainNavigator
            // 
            this.ButtonMainNavigator.CheckOnClick = true;
            this.ButtonMainNavigator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonMainNavigator.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMainNavigator.Image")));
            this.ButtonMainNavigator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonMainNavigator.Name = "ButtonMainNavigator";
            this.ButtonMainNavigator.Size = new System.Drawing.Size(24, 24);
            this.ButtonMainNavigator.Text = "ButtonMainNavigator";
            this.ButtonMainNavigator.Click += new System.EventHandler(this.OnMainNavigatorClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // ButtonDrowRectangle
            // 
            this.ButtonDrowRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrowRectangle.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrowRectangle.Image")));
            this.ButtonDrowRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrowRectangle.Name = "ButtonDrowRectangle";
            this.ButtonDrowRectangle.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrowRectangle.Text = "ButtonDrowRectangle";
            this.ButtonDrowRectangle.Click += new System.EventHandler(this.DrawRectangleSpeedButtonClick);
            // 
            // ButtonDrowTriangle
            // 
            this.ButtonDrowTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrowTriangle.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrowTriangle.Image")));
            this.ButtonDrowTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrowTriangle.Name = "ButtonDrowTriangle";
            this.ButtonDrowTriangle.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrowTriangle.Text = "ButtonDrowTriangle";
            this.ButtonDrowTriangle.Click += new System.EventHandler(this.DrawTriangleSpeedButtonClick);
            // 
            // ButtonDrowElipse
            // 
            this.ButtonDrowElipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrowElipse.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrowElipse.Image")));
            this.ButtonDrowElipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrowElipse.Name = "ButtonDrowElipse";
            this.ButtonDrowElipse.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrowElipse.Text = "ButtonDrowElipse";
            this.ButtonDrowElipse.Click += new System.EventHandler(this.DrawEllipseSpeedButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // ButtonMultiSelect
            // 
            this.ButtonMultiSelect.CheckOnClick = true;
            this.ButtonMultiSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonMultiSelect.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMultiSelect.Image")));
            this.ButtonMultiSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonMultiSelect.Name = "ButtonMultiSelect";
            this.ButtonMultiSelect.Size = new System.Drawing.Size(24, 24);
            this.ButtonMultiSelect.Text = "ButtonMultiSelect";
            this.ButtonMultiSelect.ToolTipText = "ButtonMultiSelect";
            this.ButtonMultiSelect.Click += new System.EventHandler(this.OnMultiSelectClick);
            // 
            // ButtonRotateRight
            // 
            this.ButtonRotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonRotateRight.Image = ((System.Drawing.Image)(resources.GetObject("ButtonRotateRight.Image")));
            this.ButtonRotateRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonRotateRight.Name = "ButtonRotateRight";
            this.ButtonRotateRight.Size = new System.Drawing.Size(24, 24);
            this.ButtonRotateRight.Text = "ButtonRotateRight";
            this.ButtonRotateRight.Click += new System.EventHandler(this.RotateRight);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // ButtonFillColorRed
            // 
            this.ButtonFillColorRed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorRed.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorRed.Image")));
            this.ButtonFillColorRed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorRed.Name = "ButtonFillColorRed";
            this.ButtonFillColorRed.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorRed.Text = "ButtonFillColorRed";
            this.ButtonFillColorRed.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorYellow
            // 
            this.ButtonFillColorYellow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorYellow.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorYellow.Image")));
            this.ButtonFillColorYellow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorYellow.Name = "ButtonFillColorYellow";
            this.ButtonFillColorYellow.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorYellow.Text = "ButtonFillColorYellow";
            this.ButtonFillColorYellow.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorHotPink
            // 
            this.ButtonFillColorHotPink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorHotPink.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorHotPink.Image")));
            this.ButtonFillColorHotPink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorHotPink.Name = "ButtonFillColorHotPink";
            this.ButtonFillColorHotPink.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorHotPink.Text = "ButtonFillColorHotPink";
            this.ButtonFillColorHotPink.ToolTipText = "ButtonFillColorHotPink";
            this.ButtonFillColorHotPink.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorBlack
            // 
            this.ButtonFillColorBlack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorBlack.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorBlack.Image")));
            this.ButtonFillColorBlack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorBlack.Name = "ButtonFillColorBlack";
            this.ButtonFillColorBlack.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorBlack.Text = "ButtonFillColorBlack";
            this.ButtonFillColorBlack.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColor
            // 
            this.ButtonFillColor.CheckOnClick = true;
            this.ButtonFillColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColor.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColor.Image")));
            this.ButtonFillColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColor.Name = "ButtonFillColor";
            this.ButtonFillColor.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColor.Text = "ButtonFillColor";
            this.ButtonFillColor.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.ButtonFillColor.Click += new System.EventHandler(this.FillColor);
            // 
            // viewPort
            // 
            this.viewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPort.Location = new System.Drawing.Point(0, 55);
            this.viewPort.Margin = new System.Windows.Forms.Padding(5);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(924, 444);
            this.viewPort.TabIndex = 4;
            this.viewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPortPaint);
            this.viewPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseDown);
            this.viewPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseMove);
            this.viewPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 521);
            this.Controls.Add(this.viewPort);
            this.Controls.Add(this.speedMenu);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Draw";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.speedMenu.ResumeLayout(false);
            this.speedMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
		private System.Windows.Forms.ToolStripStatusLabel currentStatusLabel;
		private Draw.DoubleBufferedPanel viewPort;
		private System.Windows.Forms.ToolStripButton ButtonMainNavigator;
		private System.Windows.Forms.ToolStripButton ButtonDrowRectangle;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip speedMenu;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripButton ButtonDrowTriangle;
        private System.Windows.Forms.ToolStripButton ButtonDrowElipse;
        private System.Windows.Forms.ToolStripButton ButtonFillColorBlack;
        private System.Windows.Forms.ToolStripButton ButtonFillColorHotPink;
        private System.Windows.Forms.ToolStripButton ButtonFillColorYellow;
        private System.Windows.Forms.ToolStripButton ButtonFillColorRed;
        private System.Windows.Forms.ToolStripButton ButtonRotateRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonMultiSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ButtonFillColor;
    }
}
