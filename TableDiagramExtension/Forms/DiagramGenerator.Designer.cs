#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.Forms;

namespace DatabaseDiagram
{
    partial class DiagramGenerator
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
            this.components = new System.ComponentModel.Container();
            Syncfusion.Windows.Forms.Diagram.Binding binding2 = new Syncfusion.Windows.Forms.Diagram.Binding();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramGenerator));
            this.sqlDependencyDiagram = new Syncfusion.Windows.Forms.Diagram.Controls.Diagram(this.components);
            this.model1 = new Syncfusion.Windows.Forms.Diagram.Model(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboDatabaseDiagram = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboDatabase = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cboTable = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.pNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jPEGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sVGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.generateDiagramStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.sqlDependencyDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.model1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sqlDependencyDiagram
            // 
            this.sqlDependencyDiagram.BackColor = System.Drawing.Color.White;
            binding2.DefaultConnector = null;
            binding2.DefaultNode = null;
            binding2.Diagram = this.sqlDependencyDiagram;
            binding2.Id = null;
            binding2.Label = ((System.Collections.Generic.List<string>)(resources.GetObject("binding2.Label")));
            binding2.ParentId = null;
            this.sqlDependencyDiagram.Binding = binding2;
            this.sqlDependencyDiagram.Controller.Constraint = Syncfusion.Windows.Forms.Diagram.Constraints.PageEditable;
            this.sqlDependencyDiagram.Controller.DefaultConnectorTool = Syncfusion.Windows.Forms.Diagram.ConnectorTool.OrgLineConnectorTool;
            this.sqlDependencyDiagram.Controller.PasteOffset = new System.Drawing.SizeF(10F, 10F);
            this.sqlDependencyDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlDependencyDiagram.EnableTouchMode = false;
            this.sqlDependencyDiagram.HScroll = true;
            this.sqlDependencyDiagram.LayoutManager = null;
            this.sqlDependencyDiagram.Location = new System.Drawing.Point(0, 0);
            this.sqlDependencyDiagram.Model = this.model1;
            this.sqlDependencyDiagram.Name = "sqlDependencyDiagram";
            this.sqlDependencyDiagram.ScrollVirtualBounds = ((System.Drawing.RectangleF)(resources.GetObject("sqlDependencyDiagram.ScrollVirtualBounds")));
            this.sqlDependencyDiagram.Size = new System.Drawing.Size(1028, 549);
            this.sqlDependencyDiagram.SmartSizeBox = false;
            this.sqlDependencyDiagram.TabIndex = 1;
            this.sqlDependencyDiagram.Text = "diagram1";
            // 
            // 
            // 
            this.sqlDependencyDiagram.View.BackgroundColor = System.Drawing.Color.White;
            this.sqlDependencyDiagram.View.ClientRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.sqlDependencyDiagram.View.Controller = this.sqlDependencyDiagram.Controller;
            this.sqlDependencyDiagram.View.Grid.MinPixelSpacing = 4F;
            this.sqlDependencyDiagram.View.Grid.Visible = false;
            this.sqlDependencyDiagram.View.ScrollVirtualBounds = ((System.Drawing.RectangleF)(resources.GetObject("resource.ScrollVirtualBounds")));
            this.sqlDependencyDiagram.View.ZoomType = Syncfusion.Windows.Forms.Diagram.ZoomType.Center;
            this.sqlDependencyDiagram.VScroll = true;
            // 
            // model1
            // 
            this.model1.AlignmentType = Syncfusion.Windows.Forms.Diagram.AlignmentType.SelectedNode;
            this.model1.BackgroundStyle.PathBrushStyle = Syncfusion.Windows.Forms.Diagram.PathGradientBrushStyle.RectangleCenter;
            this.model1.DocumentScale.DisplayName = "No Scale";
            this.model1.DocumentScale.Height = 1F;
            this.model1.DocumentScale.Width = 1F;
            this.model1.DocumentSize.Height = 1169F;
            this.model1.DocumentSize.Width = 827F;
            this.model1.LineStyle.DashPattern = null;
            this.model1.LineStyle.LineColor = System.Drawing.Color.LightGray;
            this.model1.LineStyle.LineWidth = 0F;
            this.model1.LogicalSize = new System.Drawing.SizeF(827F, 1169F);
            this.model1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.model1.ShadowStyle.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.model1.ShadowStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.cboDatabaseDiagram);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 60);
            this.panel1.TabIndex = 0;
            // 
            // cboDatabaseDiagram
            // 
            this.cboDatabaseDiagram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDatabaseDiagram.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDatabaseDiagram.FormattingEnabled = true;
            this.cboDatabaseDiagram.Items.AddRange(new object[] {
            "Database Diagram 1",
            "Database Diagram 2",
            "Database Diagram 3"});
            this.cboDatabaseDiagram.Location = new System.Drawing.Point(975, 22);
            this.cboDatabaseDiagram.Margin = new System.Windows.Forms.Padding(2);
            this.cboDatabaseDiagram.Name = "cboDatabaseDiagram";
            this.cboDatabaseDiagram.Size = new System.Drawing.Size(42, 25);
            this.cboDatabaseDiagram.TabIndex = 1;
            this.cboDatabaseDiagram.Visible = false;
            this.cboDatabaseDiagram.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseDiagram_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboDatabase,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.cboTable,
            this.toolStripSeparator3,
            this.saveToolStripButton,
            this.zoomInToolStripButton,
            this.exportToolStripButton,
            this.toolStripSeparator1,
            this.saveAsToolStripButton,
            this.generateDiagramStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1028, 60);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(101, 57);
            this.toolStripLabel1.Text = "Select Database";
            // 
            // cboDatabase
            // 
            this.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(151, 60);
            this.cboDatabase.SelectedIndexChanged += new System.EventHandler(this.cboDatabase_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 60);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(77, 57);
            this.toolStripLabel2.Text = "Select Table";
            // 
            // cboTable
            // 
            this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(151, 60);
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 60);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.AutoSize = false;
            this.saveToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.saveToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(77, 55);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.saveToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // exportToolStripButton
            // 
            this.exportToolStripButton.AutoSize = false;
            this.exportToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.exportToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pNGToolStripMenuItem,
            this.jPEGToolStripMenuItem,
            this.gIFToolStripMenuItem,
            this.sVGToolStripMenuItem});
            this.exportToolStripButton.Enabled = false;
            this.exportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripButton.Image")));
            this.exportToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.exportToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportToolStripButton.Name = "exportToolStripButton";
            this.exportToolStripButton.ShowDropDownArrow = false;
            this.exportToolStripButton.Size = new System.Drawing.Size(77, 55);
            this.exportToolStripButton.Text = "Export";
            this.exportToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exportToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // pNGToolStripMenuItem
            // 
            this.pNGToolStripMenuItem.Name = "pNGToolStripMenuItem";
            this.pNGToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.pNGToolStripMenuItem.Text = "PNG";
            this.pNGToolStripMenuItem.Click += new System.EventHandler(this.pNGToolStripMenuItem_Click);
            // 
            // jPEGToolStripMenuItem
            // 
            this.jPEGToolStripMenuItem.Name = "jPEGToolStripMenuItem";
            this.jPEGToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.jPEGToolStripMenuItem.Text = "JPEG";
            this.jPEGToolStripMenuItem.Click += new System.EventHandler(this.jPEGToolStripMenuItem_Click);
            // 
            // gIFToolStripMenuItem
            // 
            this.gIFToolStripMenuItem.Name = "gIFToolStripMenuItem";
            this.gIFToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.gIFToolStripMenuItem.Text = "GIF";
            this.gIFToolStripMenuItem.Click += new System.EventHandler(this.gIFToolStripMenuItem_Click);
            // 
            // sVGToolStripMenuItem
            // 
            this.sVGToolStripMenuItem.Name = "sVGToolStripMenuItem";
            this.sVGToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.sVGToolStripMenuItem.Text = "SVG";
            this.sVGToolStripMenuItem.Click += new System.EventHandler(this.sVGToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // saveAsToolStripButton
            // 
            this.saveAsToolStripButton.AutoSize = false;
            this.saveAsToolStripButton.Enabled = false;
            this.saveAsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripButton.Image")));
            this.saveAsToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.saveAsToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveAsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsToolStripButton.Name = "saveAsToolStripButton";
            this.saveAsToolStripButton.Size = new System.Drawing.Size(77, 55);
            this.saveAsToolStripButton.Text = "Save As";
            this.saveAsToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.saveAsToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.saveAsToolStripButton.Visible = false;
            this.saveAsToolStripButton.Click += new System.EventHandler(this.saveAsToolStripButton_Click);
            // 
            // zoomInToolStripButton
            // 
            this.zoomInToolStripButton.AutoSize = false;
            this.zoomInToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.zoomInToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.zoomInToolStripButton.Enabled = false;
            this.zoomInToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInToolStripButton.Image")));
            this.zoomInToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.zoomInToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInToolStripButton.Name = "zoomInToolStripButton";
            this.zoomInToolStripButton.ShowDropDownArrow = false;
            this.zoomInToolStripButton.Size = new System.Drawing.Size(77, 55);
            this.zoomInToolStripButton.Text = "Zoom";
            this.zoomInToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.zoomInToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.zoomInToolStripMenuItem.Text = "ZoomIn";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click_1);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.zoomOutToolStripMenuItem.Text = "ZoomOut";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click_1);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sqlDependencyDiagram);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 549);
            this.panel2.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // generateDiagramStripButton
            // 
            this.generateDiagramStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.generateDiagramStripButton.Image = ((System.Drawing.Image)(resources.GetObject("generateDiagramStripButton.Image")));
            this.generateDiagramStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateDiagramStripButton.Name = "generateDiagramStripButton";
            this.generateDiagramStripButton.Size = new System.Drawing.Size(25, 57);
            this.generateDiagramStripButton.Text = "toolStripButton1";
            // 
            // DiagramGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagramGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Diagram";
            this.Load += new System.EventHandler(this.DiagramGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sqlDependencyDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.model1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton saveAsToolStripButton;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.ToolStripDropDownButton exportToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem pNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jPEGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sVGToolStripMenuItem;
        private Syncfusion.Windows.Forms.Diagram.Controls.Diagram sqlDependencyDiagram;
        private Syncfusion.Windows.Forms.Diagram.Model model1;
        private System.Windows.Forms.ToolStripDropDownButton zoomInToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboDatabaseDiagram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboDatabase;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cboTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton generateDiagramStripButton;
    }
}

