#region Copyright Bert O'Neill
// Copyright Bert O'Neill 2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// Any infringement will be prosecuted under applicable laws. 
#endregion
using Microsoft.Extensions.Logging;
using Syncfusion.SVG.IO;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using TableDiagramExtension.Classes;
using TableDiagramExtension.Controllers;
using TableDiagramExtension.Interfaces;
using TableDiagramExtension.Models;
using TableDiagramExtension.Resources;

namespace DatabaseDiagram
{
    public partial class DiagramGenerator : Form
    {
        #region Members
        SharedData _sharedData = null;
        bool needSave = false;
        public string fileName;
        private Point mousePt;
        private Node prevbNode = null;
        private OpenFileDialog fileDialog = new OpenFileDialog();

        private readonly ISQLController _sqlController;
        private readonly ILogger _logger;
        private readonly IErrorController _errorController;
        private readonly XMLController _xmlController;

        #endregion

        #region Form initialize
        public DiagramGenerator()
        {
            InitializeComponent();
            sqlDependencyDiagram.BeginUpdate();
            this.sqlDependencyDiagram.Model.BoundaryConstraintsEnabled = false;
            this.sqlDependencyDiagram.Model.LineStyle.LineColor = Color.LightGray;
            this.sqlDependencyDiagram.Model.RenderingStyle.SmoothingMode = SmoothingMode.HighQuality;
            InitailizeDiagram();
            sqlDependencyDiagram.EndUpdate();

            sqlDependencyDiagram.EventSink.NodeClick += new NodeMouseEventHandler(EventSink_NodeClick);
            Application.Idle += new EventHandler(Application_Idle); // Wire up OnIdle processing
        }

        public DiagramGenerator(SharedData sharedData)
        {
            InitializeComponent();
            sqlDependencyDiagram.BeginUpdate();
            this.sqlDependencyDiagram.Model.BoundaryConstraintsEnabled = false;
            this.sqlDependencyDiagram.Model.LineStyle.LineColor = Color.LightGray;
            this.sqlDependencyDiagram.Model.RenderingStyle.SmoothingMode = SmoothingMode.HighQuality;
            InitailizeDiagram();

            sqlDependencyDiagram.EndUpdate();
            sqlDependencyDiagram.EventSink.NodeClick += new NodeMouseEventHandler(EventSink_NodeClick);

            _sharedData = sharedData;
            _sqlController = new SQLController();
            _errorController = new ErrorController();
            _xmlController = new XMLController();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Change's the appearance of the Diagram 
        /// </summary>
        private void DiagramAppearance()
        {

            this.sqlDependencyDiagram.HorizontalRuler.BackgroundColor = Color.White;
            this.sqlDependencyDiagram.VerticalRuler.BackgroundColor = Color.White;
            this.sqlDependencyDiagram.View.Grid.GridStyle = GridStyle.Line;
            this.sqlDependencyDiagram.View.Grid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.sqlDependencyDiagram.View.Grid.Color = Color.LightGray;
            this.sqlDependencyDiagram.View.Grid.VerticalSpacing = 15;
            this.sqlDependencyDiagram.View.Grid.HorizontalSpacing = 15;
            this.sqlDependencyDiagram.Model.BackgroundStyle.GradientCenter = 0.5f;
            this.sqlDependencyDiagram.View.HandleRenderer.HandleColor = Color.AliceBlue;
            this.sqlDependencyDiagram.View.HandleRenderer.HandleOutlineColor = Color.SkyBlue;
            this.sqlDependencyDiagram.Model.DocumentSize = new PageSize(this.sqlDependencyDiagram.View.ClientRectangle.Size.Width, sqlDependencyDiagram.View.ClientRectangle.Size.Height);
            this.sqlDependencyDiagram.Model.BoundaryConstraintsEnabled = false;
            this.sqlDependencyDiagram.Model.MinimumSize = sqlDependencyDiagram.View.ClientRectangle.Size;
            this.sqlDependencyDiagram.Model.SizeToContent = true;
            this.sqlDependencyDiagram.View.SelectionList.Clear();
        }
        #endregion

        #region Initailize Diagram 
        /// <summary>
        /// Initializes the diagram
        /// </summary>
        private void InitailizeDiagram()
        {
            sqlDependencyDiagram.View.SelectionList.Clear();
        }

        /// <summary>
        /// Make connection establishment between nodes
        /// </summary>
        private void ConnectNodes(DataTable Relationships, List<string> dependencyTables)
        {
            // loop dependencyTables and create each table
            int pointLeft = 100, pointRight = 150;
            foreach (string table in dependencyTables)
            {
                pointLeft += 150;
                pointRight = +100;

                sqlDependencyDiagram.Model.Nodes[table].PinPoint = new PointF(pointLeft, pointRight);
            }

            foreach (DataRow row in Relationships.Rows)
            {
                ConnectNodes(sqlDependencyDiagram.Model.Nodes[(string)row["ReferencedTableName"]], sqlDependencyDiagram.Model.Nodes[(string)row["ParentTableName"]], (string)row["RelationshipType"]);
            }
        }

        /// <summary>
        /// Initialize the Diagram from XML file
        /// </summary>
        protected void InitializeDiagramFromXMLData(string updatedXml, DataTable Relationships, List<string> dependencyTables)
        {
            sqlDependencyDiagram.Model.Clear();
            sqlDependencyDiagram.Model.BeginUpdate();
            RoundRect rrect = new RoundRect(900, 30, 250, 70, MeasureUnits.Pixel);
            rrect.FillStyle.Color = Color.Transparent;
            rrect.EditStyle.AllowSelect = false;
            rrect.LineStyle.LineColor = Color.Gray;

            Syncfusion.Windows.Forms.Diagram.Label lbl = new Syncfusion.Windows.Forms.Diagram.Label(rrect, "Relationships");
            lbl.Position = Position.TopLeft;
            lbl.FontStyle.Family = "Segoe UI";
            lbl.FontStyle.Size = 9;
            rrect.Labels.Add(lbl);

            Syncfusion.Windows.Forms.Diagram.Label lblOneToOne = new Syncfusion.Windows.Forms.Diagram.Label(rrect, ": OneToOne");
            lblOneToOne.FontStyle.Family = "Segoe UI";
            lblOneToOne.FontStyle.Size = 9;
            lblOneToOne.OffsetX = 120;
            lblOneToOne.OffsetY = 10;
            rrect.Labels.Add(lblOneToOne);

            Syncfusion.Windows.Forms.Diagram.Label lblOneToMany = new Syncfusion.Windows.Forms.Diagram.Label(rrect, ": OneToMany");
            lblOneToMany.FontStyle.Family = "Segoe UI";
            lblOneToMany.FontStyle.Size = 9;
            lblOneToMany.OffsetX = 120;
            lblOneToMany.OffsetY = 40;
            rrect.Labels.Add(lblOneToMany);

            sqlDependencyDiagram.Model.AppendChild(rrect);
            LineConnector oneToOne = new LineConnector(new PointF(910, 50), new PointF(1010, 50));
            oneToOne.EditStyle.AllowSelect = false;
            oneToOne.TailDecorator.DecoratorShape = DecoratorShape.None;
            oneToOne.HeadDecorator.DecoratorShape = DecoratorShape.None;
            sqlDependencyDiagram.Model.AppendChild(oneToOne);

            LineConnector oneToMany = new LineConnector(new PointF(910, 80), new PointF(1010, 80));
            oneToMany.EditStyle.AllowSelect = false;
            oneToMany.TailDecorator.DecoratorShape = DecoratorShape.None;
            oneToMany.HeadDecorator.DecoratorShape = DecoratorShape.Circle;
            sqlDependencyDiagram.Model.AppendChild(oneToMany);

            // Read data from the XML data file and populate a Hashtable using the employee ID as the key
            Hashtable lstEmployees = this.ReadEmployeeDataFromXMLFile(updatedXml);

            // Create a manager-employee nested list with top-level employees forming the root nodes
            //ArrayList sortedlist = this.GetSortedEmployeesList(lstEmployees);

            // convert for downstream fnx's
            ArrayList sortedlist = new ArrayList(lstEmployees.Values);            

            // Create diagram symbol nodes for each employee and initialize the diagram
            this.CreateOrgDiagramFromList(sortedlist);
            lstEmployees.Clear();
            sortedlist.Clear();

            //Make connection between nodes
            ConnectNodes(Relationships, dependencyTables); // pass in array of tables and relationship[s
            sqlDependencyDiagram.Model.EndUpdate();
            sqlDependencyDiagram.View.SelectionList.Clear();
        }

        /// <summary>
        /// Generates the SubEmployee count
        /// </summary>
        /// <param name="empl">Employee</param>
        protected void IterUpdateSubEmployeeCount(Employee empl)
        {
            empl.RecSubEmployeeCount = empl.SubEmployees.Count;
            foreach (Employee subempl in empl.SubEmployees)
            {
                this.IterUpdateSubEmployeeCount(subempl);
                empl.RecSubEmployeeCount += subempl.RecSubEmployeeCount;
            }
        }

        /// <summary>
        /// Read data from the XML file
        /// </summary>
        /// <param name="datasrc">XML file</param>
        /// <returns>returns the table</returns>
        protected Hashtable ReadEmployeeDataFromXMLFile(string updatedXml)
        {
            Hashtable lstEmployee = new Hashtable();

            // Use the XML DOM to read data from the employees XML data file
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(updatedXml);
                //xmldoc.Load(datasrc);

                if (xmldoc.DocumentElement.HasChildNodes)
                {
                    XmlNodeList employeelist = xmldoc.DocumentElement.ChildNodes;
                    for (int i = 0; i < employeelist.Count; i++)
                    {
                        // Create an Employee instance to represent each employee
                        Employee emply = new Employee();

                        XmlNode employeenode = employeelist[i];
                        emply.TableName = employeenode.Name;
                        //emply.EmployeeID = employeenode.Name; // added
                        string strTableName = employeenode.Name;
                        ArrayList strColoumnName = new ArrayList();

                        XmlNodeList employeedatalist = employeenode.ChildNodes;
                        IEnumerator employeedata = employeedatalist.GetEnumerator();
                        employeedata.Reset();
                        while (employeedata.MoveNext())
                        {
                            XmlNode dataelement = employeedata.Current as XmlNode;
                            strColoumnName.Add(dataelement.Name);

                            switch (dataelement.Name)
                            {
                                case "PrimaryKey":
                                    emply.PrimaryKeyID.Add(dataelement.InnerText);
                                    break;
                                case "ForeignKey":
                                    emply.ForeignKeyID.Add(dataelement.InnerText);
                                    break;
                                case "TableId":
                                    emply.EmployeeID = dataelement.InnerText;
                                    break;
                                default:
                                    emply.Coloumns.Add(dataelement.Name);
                                    break;
                            }
                        }
                        lstEmployee.Add(emply.EmployeeID, emply);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lstEmployee;
        }

        /// <summary>
        ///  Rearrange the employees collection as a manager-subemployee nested tree
        /// </summary>
        /// <param name="htemployees">List of data</param>
        /// <returns>returns sorted data list</returns>
        private ArrayList GetSortedEmployeesList(Hashtable htemployees)
        {
            ICollection employees = htemployees.Values;
            ArrayList sortedlist = new ArrayList();
            foreach (Employee emply in employees)
            {
                sortedlist.Add(emply);
                //if (emply.ForeignKeyID == string.Empty)
                //{
                //    // Employees without a ManagerID are top-level nodes in the org tree
                //    sortedlist.Add(emply);
                //}
                //else
                //{
                //    // Find the employee instance associated with the managerID and add this object as 
                //    // a subemployee of the manager node.
                //    Employee manager = (Employee)htemployees[emply.ForeignKeyID];
                //    if (manager != null)
                //        manager.SubEmployees.Add(emply);
                //}
            }

            return sortedlist;
        }

        /// <summary>
        /// Create diagram symbol nodes for each employee in the organization and add these symbols to the diagram
        /// Create links between manager and sub-employees to depict the org structure
        /// </summary>
        /// <param name="alemployees"></param>
        private void CreateOrgDiagramFromList(ArrayList alemployees)
        {
            // Temporarily suspend the Diagram Model redrawing
            this.sqlDependencyDiagram.Model.BeginUpdate();

            foreach (Employee emply in alemployees)
            {
                Group emplysymbol = InsertSymbolEmployee(emply);
                this.IterCreateEmployeeSymbol(emply, emplysymbol);
            }

            // ReEnable the Model redraw
            this.sqlDependencyDiagram.Model.EndUpdate();
        }

        /// <summary>
        /// Iterative sub-employee symbol node creation
        /// </summary>
        /// <param name="emply">Employees business object</param>
        /// <param name="emplysymbol">Node</param>
        private void IterCreateEmployeeSymbol(Employee emply, Group emplysymbol)
        {
            foreach (Employee subemply in emply.SubEmployees)
            {
                // Create a EmployeeSymbol for each of the sub-employees of the particular employee //Waterfall, Horizontal
                Group subemplysymbol = InsertSymbolEmployee(subemply);
                this.IterCreateEmployeeSymbol(subemply, subemplysymbol);
            }
        }

        /// <summary>
        /// Insert Node
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>returns the node</returns>
        private Group InsertSymbolEmployee(Employee employee)
        {
            DataSymbol Symbol = new DataSymbol(employee.Coloumns, employee.TableName, employee.PrimaryKeyID, employee.ForeignKeyID);
            this.sqlDependencyDiagram.Model.AppendChild(Symbol);
            return Symbol;
        }


        /// <summary>
        /// Connect Nodes with connectors
        /// </summary>
        /// <param name="emplysymbol">Parent</param>
        /// <param name="subemplysymbol">Child</param>
        /// <param name="relation">relationship</param>
        private void ConnectNodes(Node emplysymbol, Node subemplysymbol, string relation)
        {
            LineConnector ortholink = new LineConnector(PointF.Empty, new PointF(0, 1));
            ortholink.LineStyle.DashStyle = DashStyle.Solid;

            ortholink.LineStyle.LineWidth = 1f;
            ortholink.LineStyle.LineColor = Color.Black;
            if (relation == "OneToMany")
                ortholink.TailDecorator.DecoratorShape = DecoratorShape.None;
            else
                ortholink.TailDecorator.DecoratorShape = DecoratorShape.Circle;

            ortholink.HeadDecorator.DecoratorShape = DecoratorShape.Circle;

            this.sqlDependencyDiagram.Model.AppendChild(ortholink);

            if (emplysymbol.CentralPort != null && subemplysymbol.CentralPort != null)
            {
                emplysymbol.CentralPort.TryConnect(ortholink.TailEndPoint);
                subemplysymbol.CentralPort.TryConnect(ortholink.HeadEndPoint);
            }

            this.sqlDependencyDiagram.Controller.SendToBack();
        }

        #endregion

        #region Properties

        public bool SaveChanges
        {
            get
            {
                if (sqlDependencyDiagram.Model.Modified || needSave)
                    return true;
                else
                    return false;
            }
            set
            {
                needSave = value;
            }
        }

        public bool HasFileName
        {
            get
            {
                return (this.fileName != null && this.fileName.Length > 0);
            }
        }

        #endregion

        #region Event Handlers
        void EventSink_NodeClick(NodeMouseEventArgs evtArgs)
        {
            if (sqlDependencyDiagram.Controller.TextEditor.IsEditing)
            {
                sqlDependencyDiagram.Controller.TextEditor.EndEdit(true);
            }
            NodeCollection nodes = sqlDependencyDiagram.Controller.GetAllNodesAtPoint(sqlDependencyDiagram.Model, sqlDependencyDiagram.Controller.MouseLocation) as NodeCollection;

            foreach (Node gnode in nodes)
            {
                if (gnode is TextNode)
                {
                    sqlDependencyDiagram.Controller.TextEditor.BeginEdit(gnode, false);
                }
                if (gnode.Name == "BaseNode")
                {
                    if (prevbNode == null)
                        prevbNode = gnode;
                    if (prevbNode == gnode)
                    {
                        ((FilledPath)gnode).FillStyle.Color = Color.FromArgb(117, 186, 255);
                        //((FilledPath)prevbNode).FillStyle.Color = Color.WhiteSmoke;
                    }
                    else
                    {
                        ((FilledPath)gnode).FillStyle.Color = Color.FromArgb(117, 186, 255);
                        ((FilledPath)prevbNode).FillStyle.Color = Color.WhiteSmoke;
                        prevbNode = gnode;
                    }
                }
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (this.sqlDependencyDiagram != null && this.sqlDependencyDiagram.Model != null)
            {
                this.saveToolStripButton.Enabled = true;
                this.saveAsToolStripButton.Enabled = true;


            }
            else
            {
                this.saveToolStripButton.Enabled = false;
                this.saveAsToolStripButton.Enabled = false;

            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileName = "Diagram";
            saveFileDialog1.Filter = "EDD file(*.edd)|*.edd";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sqlDependencyDiagram.SaveBinary(saveFileDialog1.FileName);
            }
        }

        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileName = "Diagram";
            saveFileDialog1.Filter = @"EDD file(*.edd)|*.edd|XML file(*.xml)|*.xml|All files|*.*";
            saveFileDialog1.Title = "Save File As:";
            saveFileDialog1.FileName = "Diagram";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        sqlDependencyDiagram.SaveBinary(saveFileDialog1.FileName);
                        break;
                    case 2:
#if !NETCORE
                        sqlDependencyDiagram.SaveSoap(saveFileDialog1.FileName);
#endif
                        break;
                    default:
                        sqlDependencyDiagram.SaveBinary(saveFileDialog1.FileName);
                        break;
                }
            }
        }

        private void pNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = @"W3C Portable Network Graphics(*.png)|*.png";
            saveFileDialog1.Title = "Export Diagram As:";
            saveFileDialog1.FileName = "Diagram";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imgformat = ImageFormat.Png;
                SaveImage(saveFileDialog1.FileName, imgformat);
            }
            else
            {
                return;
            }
        }

        private void jPEGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = @"Joint Photographic Experts Group(*.jpeg)|*.jpeg";
            saveFileDialog1.Title = "Export Diagram As:";
            saveFileDialog1.FileName = "Diagram";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imgformat = ImageFormat.Jpeg;
                SaveImage(saveFileDialog1.FileName, imgformat);
            }
            else
            {
                return;
            }
        }

        private void gIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = @"Graphics Interchange Format(*.gif)|*.gif";
            saveFileDialog1.Title = "Export Diagram As:";
            saveFileDialog1.FileName = "Diagram";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imgformat = ImageFormat.Gif;
                SaveImage(saveFileDialog1.FileName, imgformat);

            }
            else
            {
                return;
            }
        }

        private void sVGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = @"Scalable Vector Graphics Format(*.svg)|*.svg";
            saveFileDialog1.FileName = "Diagram";
            saveFileDialog1.Title = "Export Diagram As:";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                ToSvg tosvg = new ToSvg();
                // Get Diagram Nodes Bounding Rectangle.
                RectangleF rectBounding = this.sqlDependencyDiagram.Model.GetBoundingRect();
                Graphics gfx = tosvg.GetRealGraphics(new Size((int)rectBounding.Width, (int)rectBounding.Height));
                this.sqlDependencyDiagram.ExportDiagramToGraphics(gfx);
                tosvg.Save(saveFileDialog1.FileName);

            }
            else
            {
                return;
            }
        }

        private void zoomInToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            sqlDependencyDiagram.View.ZoomIn();
            sqlDependencyDiagram.Controller.ActivateTool("PanTool");
        }

        private void zoomOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            sqlDependencyDiagram.View.ZoomOut();
        }

        private void resetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            sqlDependencyDiagram.View.ZoomToActual();
        }

        #endregion

        #region Helper Methods
        /// Save diagram as Image
        /// </summary>
        /// <param name="filename">Filename </param>
        /// <param name="imageformat">image format</param>
        private void SaveImage(string filename, ImageFormat imageformat)
        {
            Image img = this.sqlDependencyDiagram.View.ExportDiagramAsImage(false);
            img.Save(filename, imageformat);
        }

        #endregion

        private void DiagramGenerator_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> activeDBs;
                this.cboDatabase.Enabled = false;
                this.cboTable.Enabled = false;

                // retrieve dropdown values
                activeDBs = _sqlController.RetrieveDatabases(this._sharedData.SqlOlapConnectionInfoBase.ConnectionString);

                activeDBs.Insert(0, "Please select database");

                if (activeDBs.Count > 1)
                {
                    this.cboDatabase.ComboBox.DataSource = null;
                    this.cboDatabase.ComboBox.DataSource = activeDBs;
                    this.cboDatabase.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
            }
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.submitStripButton.Enabled = false;
                //string selectedTable = this.cboTable.ComboBox.SelectedValue.ToString();
                DatabaseMetaData selectedTable = (DatabaseMetaData)this.cboTable.ComboBox.SelectedValue;

                if (selectedTable.TABLE_NAME == TextStrings.PleaseSelectTable) return;

                this.submitStripButton.Enabled = true;
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
            }
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<DatabaseMetaData> initialData;
                string selectedDatabase = this.cboDatabase.ComboBox.SelectedValue.ToString();
                this.cboTable.Enabled = false;

                if (selectedDatabase == TextStrings.PleaseSelectDatabase) return;

                initialData = _sqlController.RetrieveDatabaseMetaData(this._sharedData.SqlOlapConnectionInfoBase.ConnectionString, selectedDatabase);

                initialData.Insert(0, new DatabaseMetaData() { TABLE_NAME = TextStrings.PleaseSelectTable });

                if (initialData.Count > 1)
                {
                    var distinctTables = initialData.GroupBy(p => p.TABLE_NAME).Select(g => g.First()).ToList();

                    //var comboData = initialData.Select(tbl => tbl.TABLE_NAME).Distinct().ToList();
                    this.cboTable.ComboBox.DataSource = null;
                    this.cboTable.ComboBox.DataSource = distinctTables;
                    this.cboTable.ComboBox.DisplayMember = "TABLE_NAME";
                    this.cboTable.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
            }
        }

        private void AreToolStripButtonsEnabled()
        {
            try
            {
                if (this.sqlDependencyDiagram != null && this.sqlDependencyDiagram.Model != null)
                {
                    this.zoomInToolStripButton.Enabled = this.saveAsToolStripButton.Enabled = this.exportToolStripButton.Enabled = this.saveToolStripButton.Enabled = true;
                }
                else
                {
                    this.zoomInToolStripButton.Enabled = this.saveAsToolStripButton.Enabled = this.exportToolStripButton.Enabled = this.saveToolStripButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
            }
        }

        private void submitStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                // get server & table name
                string selectedDatabase = this.cboDatabase.ComboBox.SelectedValue.ToString();
                DatabaseMetaData selectedTable = (DatabaseMetaData)this.cboTable.ComboBox.SelectedValue;

                // call SQL to get dependency tables
                var dependencyTables = _sqlController.RetrieveDependencyTables(this._sharedData.SqlOlapConnectionInfoBase.ConnectionString, selectedDatabase, $"[{selectedTable.TABLE_SCHEMA}].[{selectedTable.TABLE_NAME}]");
                dependencyTables.Add(selectedTable.TABLE_NAME);

                // call SQL to build XML of dependency tables (inc. selected table)
                var dependencyTablesMetaDataXML = _sqlController.RetrieveDependencyTablesMetaData(this._sharedData.SqlOlapConnectionInfoBase.ConnectionString, selectedDatabase, dependencyTables);

                // add multiple PK's to XML
                string updatedXml = _xmlController.GenerateXmlDocFromDBData(dependencyTablesMetaDataXML);

                // get relationship data
                DataTable Relationships = _sqlController.RetrieveRelationshipData(this._sharedData.SqlOlapConnectionInfoBase.ConnectionString, selectedDatabase, dependencyTables);

                InitializeDiagramFromXMLData(updatedXml, Relationships, dependencyTables);
                sqlDependencyDiagram.View.SelectionList.Clear();

                // enable buttons
                this.AreToolStripButtonsEnabled();
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
            }
        }
    }
}
