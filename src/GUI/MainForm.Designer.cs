﻿namespace Draw
{
	partial class MainForm
	{
        private System.ComponentModel.ComponentResourceManager resources;

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
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomSave = new System.Windows.Forms.ToolStripMenuItem();
            this.JSONSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.currentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.speedMenu = new System.Windows.Forms.ToolStrip();
            this.ButtonMainNavigator = new System.Windows.Forms.ToolStripButton();
            this.ButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.ButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.ButtonPlus = new System.Windows.Forms.ToolStripButton();
            this.ButtonMinus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonDrawRectangle = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrowTriangle = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrawEllipse = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrawReuleauxTriangle = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrawLine = new System.Windows.Forms.ToolStripButton();
            this.ButtonDrawEnvelope = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonMultiSelect = new System.Windows.Forms.ToolStripButton();
            this.ButtonRotateLeft = new System.Windows.Forms.ToolStripButton();
            this.ButtonRotateRight = new System.Windows.Forms.ToolStripButton();
            this.ButtonMultiMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonFillColorRed = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorDarkOrange = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorYellow = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorHotPink = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorDarkViolet = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorLightSkyBlue = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorSpringGreen = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorBlack = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorSaddleBrown = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorSilver = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColorWhite = new System.Windows.Forms.ToolStripButton();
            this.ButtonFillColor = new System.Windows.Forms.ToolStripButton();
            this.ButtonBorderColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TabContainer = new System.Windows.Forms.ToolStrip();
            this.Tab1 = new System.Windows.Forms.ToolStripButton();
            this.CloseTab1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator001 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewTab = new System.Windows.Forms.ToolStripButton();
            this.viewPort = new Draw.DoubleBufferedPanel();
            this.ButtonDrawGenericCircle = new System.Windows.Forms.ToolStripButton();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.speedMenu.SuspendLayout();
            this.TabContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(791, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadToolStripMenuItem,
            //this.CustomSave,
            this.JSONSave,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // LoadToolStripMenuItem
            // 
            this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            this.LoadToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.LoadToolStripMenuItem.Text = "Load";
            this.LoadToolStripMenuItem.Click += new System.EventHandler(this.MainMenuButtonLoad_Click);
            // 
            // CustomSave
            // 
            this.CustomSave.Name = "CustomSave";
            this.CustomSave.Size = new System.Drawing.Size(145, 22);
            this.CustomSave.Text = "Save";
            this.CustomSave.Click += new System.EventHandler(this.MainMenuButtonSave_Click);
            // 
            // JSONSave
            // 
            this.JSONSave.Name = "JSONSave";
            this.JSONSave.Size = new System.Drawing.Size(145, 22);
            this.JSONSave.Text = "Save As JSON";
            this.JSONSave.Click += new System.EventHandler(this.MainMenuButtonSave_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentStatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 24);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(791, 399);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Size = new System.Drawing.Size(0, 394);
            // 
            // speedMenu
            // 
            this.speedMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.speedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonMainNavigator,
            this.ButtonCopy,
            this.ButtonDelete,
            this.ButtonPlus,
            this.ButtonMinus,
            this.toolStripSeparator3,
            this.ButtonDrawRectangle,
            this.ButtonDrowTriangle,
            this.ButtonDrawEllipse,
            this.ButtonDrawReuleauxTriangle,
            this.ButtonDrawLine,
            this.ButtonDrawEnvelope,
            this.ButtonDrawGenericCircle,
            this.toolStripSeparator1,
            this.ButtonMultiSelect,
            this.ButtonRotateLeft,
            this.ButtonRotateRight,
            this.ButtonMultiMove,
            this.toolStripSeparator2,
            this.ButtonFillColorRed,
            this.ButtonFillColorDarkOrange,
            this.ButtonFillColorYellow,
            this.ButtonFillColorHotPink,
            this.ButtonFillColorDarkViolet,
            this.ButtonFillColorLightSkyBlue,
            this.ButtonFillColorSpringGreen,
            this.ButtonFillColorBlack,
            this.ButtonFillColorSaddleBrown,
            this.ButtonFillColorSilver,
            this.ButtonFillColorWhite,
            this.ButtonFillColor,
            this.ButtonBorderColor,
            this.toolStripSeparator4});
            this.speedMenu.Location = new System.Drawing.Point(0, 24);
            this.speedMenu.Name = "speedMenu";
            this.speedMenu.Size = new System.Drawing.Size(791, 27);
            this.speedMenu.TabIndex = 3;
            this.speedMenu.Text = "MainContainer";
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
            this.ButtonMainNavigator.Click += new System.EventHandler(this.OnMainNavigator_Click);
            // 
            // ButtonCopy
            // 
            this.ButtonCopy.CheckOnClick = true;
            this.ButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject("ButtonCopy.Image")));
            this.ButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonCopy.Name = "ButtonCopy";
            this.ButtonCopy.Size = new System.Drawing.Size(24, 24);
            this.ButtonCopy.Text = "ButtonCopy";
            this.ButtonCopy.ToolTipText = "ButtonCopy";
            this.ButtonCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.CheckOnClick = true;
            this.ButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDelete.Image")));
            this.ButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(24, 24);
            this.ButtonDelete.Text = "ButtonDelete";
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // ButtonPlus
            // 
            this.ButtonPlus.CheckOnClick = true;
            this.ButtonPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonPlus.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPlus.Image")));
            this.ButtonPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonPlus.Name = "ButtonPlus";
            this.ButtonPlus.Size = new System.Drawing.Size(24, 24);
            this.ButtonPlus.Text = "ButtonPlus";
            this.ButtonPlus.ToolTipText = "ButtonPlus";
            this.ButtonPlus.Click += new System.EventHandler(this.Plus_Click);
            // 
            // ButtonMinus
            // 
            this.ButtonMinus.CheckOnClick = true;
            this.ButtonMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonMinus.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMinus.Image")));
            this.ButtonMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonMinus.Name = "ButtonMinus";
            this.ButtonMinus.Size = new System.Drawing.Size(24, 24);
            this.ButtonMinus.Text = "ButtonMinus";
            this.ButtonMinus.ToolTipText = "ButtonMinus";
            this.ButtonMinus.Click += new System.EventHandler(this.Minus_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // ButtonDrawRectangle
            // 
            this.ButtonDrawRectangle.CheckOnClick = true;
            this.ButtonDrawRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrawRectangle.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrawRectangle.Image")));
            this.ButtonDrawRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrawRectangle.Name = "ButtonDrawRectangle";
            this.ButtonDrawRectangle.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrawRectangle.Text = "ButtonDrowRectangle";
            this.ButtonDrawRectangle.Click += new System.EventHandler(this.DrawRectangleSpeedButton_Click);
            // 
            // ButtonDrowTriangle
            // 
            this.ButtonDrowTriangle.CheckOnClick = true;
            this.ButtonDrowTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrowTriangle.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrowTriangle.Image")));
            this.ButtonDrowTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrowTriangle.Name = "ButtonDrowTriangle";
            this.ButtonDrowTriangle.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrowTriangle.Text = "ButtonDrowTriangle";
            this.ButtonDrowTriangle.Click += new System.EventHandler(this.DrawTriangleSpeedButton_Click);
            // 
            // ButtonDrawEllipse
            // 
            this.ButtonDrawEllipse.CheckOnClick = true;
            this.ButtonDrawEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrawEllipse.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrawEllipse.Image")));
            this.ButtonDrawEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrawEllipse.Name = "ButtonDrawEllipse";
            this.ButtonDrawEllipse.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrawEllipse.Text = "ButtonDrowElipse";
            this.ButtonDrawEllipse.Click += new System.EventHandler(this.DrawEllipseSpeedButton_Click);
            // 
            // ButtonDrawReuleauxTriangle
            // 
            this.ButtonDrawReuleauxTriangle.CheckOnClick = true;
            this.ButtonDrawReuleauxTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrawReuleauxTriangle.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrawReuleauxTriangle.Image")));
            this.ButtonDrawReuleauxTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrawReuleauxTriangle.Name = "ButtonDrawReuleauxTriangle";
            this.ButtonDrawReuleauxTriangle.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrawReuleauxTriangle.Text = "ButtonDrawReuleauxTriangle";
            this.ButtonDrawReuleauxTriangle.ToolTipText = "ButtonDrawReuleauxTriangle";
            this.ButtonDrawReuleauxTriangle.Click += new System.EventHandler(this.DrawRelauxTriangleSpeedButton_Click);
            // 
            // ButtonDrawLine
            // 
            this.ButtonDrawLine.CheckOnClick = true;
            this.ButtonDrawLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrawLine.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrawLine.Image")));
            this.ButtonDrawLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrawLine.Name = "ButtonDrawLine";
            this.ButtonDrawLine.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrawLine.Text = "ButtonDrowLine";
            this.ButtonDrawLine.ToolTipText = "ButtonDrowLine";
            this.ButtonDrawLine.Click += new System.EventHandler(this.DrawLineSpeedButton_Click);
            // 
            // ButtonDrawEnvelope
            // 
            this.ButtonDrawEnvelope.CheckOnClick = true;
            this.ButtonDrawEnvelope.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrawEnvelope.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrawEnvelope.Image")));
            this.ButtonDrawEnvelope.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrawEnvelope.Name = "ButtonDrawEnvelope";
            this.ButtonDrawEnvelope.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrawEnvelope.Text = "ButtonDrawEnvelope";
            this.ButtonDrawEnvelope.ToolTipText = "ButtonDrawEnvelope";
            this.ButtonDrawEnvelope.Click += new System.EventHandler(this.DrawEnvelopeSpeedButton_Click);
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
            this.ButtonMultiSelect.Click += new System.EventHandler(this.OnMultiSelect_Click);
            // 
            // ButtonRotateLeft
            // 
            this.ButtonRotateLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonRotateLeft.Image = ((System.Drawing.Image)(resources.GetObject("ButtonRotateLeft.Image")));
            this.ButtonRotateLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonRotateLeft.Name = "ButtonRotateLeft";
            this.ButtonRotateLeft.Size = new System.Drawing.Size(24, 24);
            this.ButtonRotateLeft.Text = "ButtonRotateLeft";
            this.ButtonRotateLeft.Click += new System.EventHandler(this.RotateLeft);
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
            // ButtonMultiMove
            // 
            this.ButtonMultiMove.CheckOnClick = true;
            this.ButtonMultiMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonMultiMove.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMultiMove.Image")));
            this.ButtonMultiMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonMultiMove.Name = "ButtonMultiMove";
            this.ButtonMultiMove.Size = new System.Drawing.Size(24, 24);
            this.ButtonMultiMove.Text = "ButtonMultiMove";
            this.ButtonMultiMove.ToolTipText = "ButtonMultiMove";
            this.ButtonMultiMove.Click += new System.EventHandler(this.ButtonMultiMove_Click);
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
            // ButtonFillColorDarkOrange
            // 
            this.ButtonFillColorDarkOrange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorDarkOrange.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorDarkOrange.Image")));
            this.ButtonFillColorDarkOrange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorDarkOrange.Name = "ButtonFillColorDarkOrange";
            this.ButtonFillColorDarkOrange.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorDarkOrange.Text = "ButtonFillColorDarkOrange";
            this.ButtonFillColorDarkOrange.ToolTipText = "ButtonFillColorDarkOrange";
            this.ButtonFillColorDarkOrange.Click += new System.EventHandler(this.SetFillColor);
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
            // ButtonFillColorDarkViolet
            // 
            this.ButtonFillColorDarkViolet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorDarkViolet.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorDarkViolet.Image")));
            this.ButtonFillColorDarkViolet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorDarkViolet.Name = "ButtonFillColorDarkViolet";
            this.ButtonFillColorDarkViolet.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorDarkViolet.Text = "ButtonFillColorDarkViolet";
            this.ButtonFillColorDarkViolet.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorLightSkyBlue
            // 
            this.ButtonFillColorLightSkyBlue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorLightSkyBlue.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorLightSkyBlue.Image")));
            this.ButtonFillColorLightSkyBlue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorLightSkyBlue.Name = "ButtonFillColorLightSkyBlue";
            this.ButtonFillColorLightSkyBlue.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorLightSkyBlue.Text = "ButtonFillColorLightSkyBlue";
            this.ButtonFillColorLightSkyBlue.ToolTipText = "ButtonFillColorLightSkyBlue";
            this.ButtonFillColorLightSkyBlue.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorSpringGreen
            // 
            this.ButtonFillColorSpringGreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorSpringGreen.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorSpringGreen.Image")));
            this.ButtonFillColorSpringGreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorSpringGreen.Name = "ButtonFillColorSpringGreen";
            this.ButtonFillColorSpringGreen.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorSpringGreen.Text = "ButtonFillColorSpringGreen";
            this.ButtonFillColorSpringGreen.ToolTipText = "ButtonFillColorSpringGreen";
            this.ButtonFillColorSpringGreen.Click += new System.EventHandler(this.SetFillColor);
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
            // ButtonFillColorSaddleBrown
            // 
            this.ButtonFillColorSaddleBrown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorSaddleBrown.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorSaddleBrown.Image")));
            this.ButtonFillColorSaddleBrown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorSaddleBrown.Name = "ButtonFillColorSaddleBrown";
            this.ButtonFillColorSaddleBrown.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorSaddleBrown.Text = "ButtonFillColorSaddleBrown";
            this.ButtonFillColorSaddleBrown.ToolTipText = "ButtonFillColorSaddleBrown";
            this.ButtonFillColorSaddleBrown.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorSilver
            // 
            this.ButtonFillColorSilver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorSilver.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorSilver.Image")));
            this.ButtonFillColorSilver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorSilver.Name = "ButtonFillColorSilver";
            this.ButtonFillColorSilver.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorSilver.Text = "ButtonFillColorSilver";
            this.ButtonFillColorSilver.ToolTipText = "ButtonFillColorSilver";
            this.ButtonFillColorSilver.Click += new System.EventHandler(this.SetFillColor);
            // 
            // ButtonFillColorWhite
            // 
            this.ButtonFillColorWhite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFillColorWhite.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFillColorWhite.Image")));
            this.ButtonFillColorWhite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFillColorWhite.Name = "ButtonFillColorWhite";
            this.ButtonFillColorWhite.Size = new System.Drawing.Size(24, 24);
            this.ButtonFillColorWhite.Text = "ButtonFillColorWhite";
            this.ButtonFillColorWhite.ToolTipText = "ButtonFillColorWhite";
            this.ButtonFillColorWhite.Click += new System.EventHandler(this.SetFillColor);
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
            this.ButtonFillColor.Click += new System.EventHandler(this.FillColor_Click);
            // 
            // ButtonBorderColor
            // 
            this.ButtonBorderColor.CheckOnClick = true;
            this.ButtonBorderColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonBorderColor.Image = ((System.Drawing.Image)(resources.GetObject("ButtonBorderColor.Image")));
            this.ButtonBorderColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonBorderColor.Name = "ButtonBorderColor";
            this.ButtonBorderColor.Size = new System.Drawing.Size(24, 24);
            this.ButtonBorderColor.Text = "ButtonBorderColor";
            this.ButtonBorderColor.ToolTipText = "ButtonBorderColor";
            this.ButtonBorderColor.Click += new System.EventHandler(this.BorderColor_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // TabContainer
            // 
            this.TabContainer.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TabContainer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tab1,
            this.CloseTab1,
            this.toolStripSeparator001,
            this.AddNewTab});
            this.TabContainer.Location = new System.Drawing.Point(0, 51);
            this.TabContainer.Name = "TabContainer";
            this.TabContainer.Size = new System.Drawing.Size(791, 25);
            this.TabContainer.TabIndex = 5;
            this.TabContainer.Text = "TabContainer";
            // 
            // Tab1
            // 
            this.Tab1.Checked = true;
            this.Tab1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Tab1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Tab1.Image = ((System.Drawing.Image)(resources.GetObject("Tab1.Image")));
            this.Tab1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tab1.Name = "Tab1";
            this.Tab1.Size = new System.Drawing.Size(35, 22);
            this.Tab1.Text = "Tab1";
            this.Tab1.Click += new System.EventHandler(this.Tab_Click);
            // 
            // CloseTab1
            // 
            this.CloseTab1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CloseTab1.Image = ((System.Drawing.Image)(resources.GetObject("CloseTab1.Image")));
            this.CloseTab1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTab1.Name = "CloseTab1";
            this.CloseTab1.Size = new System.Drawing.Size(23, 22);
            this.CloseTab1.Text = "X";
            this.CloseTab1.ToolTipText = "Close Tab 1";
            this.CloseTab1.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // toolStripSeparator001
            // 
            this.toolStripSeparator001.Name = "toolStripSeparator001";
            this.toolStripSeparator001.Size = new System.Drawing.Size(6, 25);
            // 
            // AddNewTab
            // 
            this.AddNewTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddNewTab.Image = ((System.Drawing.Image)(resources.GetObject("AddNewTab.Image")));
            this.AddNewTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddNewTab.Name = "AddNewTab";
            this.AddNewTab.Size = new System.Drawing.Size(23, 22);
            this.AddNewTab.Text = "+";
            this.AddNewTab.Click += new System.EventHandler(this.AddNewTab_Click);
            // 
            // viewPort
            // 
            this.viewPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewPort.AutoSize = true;
            this.viewPort.Location = new System.Drawing.Point(0, 67);
            this.viewPort.Margin = new System.Windows.Forms.Padding(4);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(791, 357);
            this.viewPort.TabIndex = 4;
            this.viewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPortPaint);
            this.viewPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewPort_KeyDown);
            this.viewPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseDown);
            this.viewPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseMove);
            this.viewPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseUp);
            // 
            // ButtonDrawGenericCircle
            // 
            this.ButtonDrawGenericCircle.CheckOnClick = true;
            this.ButtonDrawGenericCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonDrawGenericCircle.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDrawGenericCircle.Image")));
            this.ButtonDrawGenericCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDrawGenericCircle.Name = "ButtonDrawGenericCircle";
            this.ButtonDrawGenericCircle.Size = new System.Drawing.Size(24, 24);
            this.ButtonDrawGenericCircle.Text = "ButtonDrawGenericCircle";
            this.ButtonDrawGenericCircle.Click += new System.EventHandler(this.DrawGenericCircleSpeedButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 423);
            this.Controls.Add(this.TabContainer);
            this.Controls.Add(this.viewPort);
            this.Controls.Add(this.speedMenu);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Draw";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.speedMenu.ResumeLayout(false);
            this.speedMenu.PerformLayout();
            this.TabContainer.ResumeLayout(false);
            this.TabContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
		private System.Windows.Forms.ToolStripStatusLabel currentStatusLabel;
		private Draw.DoubleBufferedPanel viewPort;
		private System.Windows.Forms.ToolStripButton ButtonMainNavigator;
		private System.Windows.Forms.ToolStripButton ButtonDrawRectangle;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip speedMenu;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripButton ButtonDrowTriangle;
        private System.Windows.Forms.ToolStripButton ButtonDrawEllipse;
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
        private System.Windows.Forms.ToolStripButton ButtonDelete;
        private System.Windows.Forms.ToolStripButton ButtonMultiMove;
        private System.Windows.Forms.ToolStripButton ButtonFillColorSilver;
        private System.Windows.Forms.ToolStripMenuItem CustomSave;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem JSONSave;
        private System.Windows.Forms.ToolStripButton ButtonCopy;
        private System.Windows.Forms.ToolStripButton ButtonBorderColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton ButtonPlus;
        private System.Windows.Forms.ToolStripButton ButtonMinus;
        private System.Windows.Forms.ToolStripButton ButtonRotateLeft;
        private System.Windows.Forms.ToolStripButton ButtonDrawReuleauxTriangle;
        private System.Windows.Forms.ToolStripButton ButtonDrawLine;
        private System.Windows.Forms.ToolStripButton ButtonFillColorDarkViolet;
        private System.Windows.Forms.ToolStripButton ButtonFillColorLightSkyBlue;
        private System.Windows.Forms.ToolStripButton ButtonFillColorSpringGreen;
        private System.Windows.Forms.ToolStripButton ButtonFillColorDarkOrange;
        private System.Windows.Forms.ToolStripButton ButtonFillColorSaddleBrown;
        private System.Windows.Forms.ToolStripButton ButtonFillColorWhite;
        private System.Windows.Forms.ToolStrip TabContainer;
        private System.Windows.Forms.ToolStripButton Tab1;
        private System.Windows.Forms.ToolStripButton AddNewTab;
        private System.Windows.Forms.ToolStripButton CloseTab1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator001;
        private System.Windows.Forms.ToolStripButton ButtonDrawEnvelope;
        private System.Windows.Forms.ToolStripButton ButtonDrawGenericCircle;
    }
}
