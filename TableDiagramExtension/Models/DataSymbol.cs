#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Diagram.Controls;
using Syncfusion.Windows.Forms.Diagram;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Syncfusion.Windows.Forms;
using System.Collections;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace TableDiagramExtension.Models
{

    [Serializable]
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Name = "FullTrust")]
    public class DataSymbol:Group
    {
        public DataSymbol( ArrayList strColumnName, string strTableName, List<string> primaryKey, List<string> foreignKey)
            : base()
        {
            this.Name = strTableName;
            float rectHeight = (strColumnName.Count * 20) + 40;
            float tmpColWidth = (strTableName.Length * 10);
            float rectWidth = Math.Max(tmpColWidth, 150); // take the largest to give the table width space for name

            Syncfusion.Windows.Forms.Diagram.Rectangle rect = new Syncfusion.Windows.Forms.Diagram.Rectangle(0, 0, rectWidth, rectHeight);
            rect.FillStyle.Color = Color.WhiteSmoke;
            rect.Name = "BaseNode";            

            // label - table name
            Syncfusion.Windows.Forms.Diagram.Label lbl = new Syncfusion.Windows.Forms.Diagram.Label(rect, strTableName);
            lbl.ReadOnly = true;
            lbl.FontStyle.Family = "Arial";            
            lbl.FontStyle.Size = 9;
            lbl.FontStyle.Bold = true;
            lbl.Position = Syncfusion.Windows.Forms.Diagram.Position.TopLeft;
            lbl.OffsetX = rectWidth <= 150 ? 65: rectWidth / 2; // inline calculation for longer table names
            lbl.OffsetY = 18;
            lbl.UpdatePosition = true;            
            rect.Labels.Add(lbl);

            this.AppendChild(rect);

            float y_TextNode = 30;
            for (int i = 0; i < strColumnName.Count; i++)
            {
                var columnField = rectWidth - 40;
                TextNode txtNode = new TextNode(strColumnName[i].ToString(), new RectangleF(30, y_TextNode, columnField, 20));                
                txtNode.ReadOnly = true;
                txtNode.FontStyle.Family = "Arial";
                txtNode.FontStyle.Size = 7;
                txtNode.VerticalAlignment = StringAlignment.Center;
                txtNode.BackgroundStyle.Color = Color.White;
                txtNode.LineStyle.LineColor = Color.LightGray;
                txtNode.EditStyle.AllowDelete = false;

                Syncfusion.Windows.Forms.Diagram.Rectangle symRect = new Syncfusion.Windows.Forms.Diagram.Rectangle(10, y_TextNode, 20, 20);

                symRect.FillStyle.Color = Color.WhiteSmoke;
                symRect.FillStyle.Type = FillStyleType.LinearGradient;
                symRect.LineStyle.LineColor = Color.LightGray;
                symRect.EditStyle.AllowSelect = false;
                symRect.EditStyle.AllowDelete = false;

                this.AppendChild(txtNode);
                this.AppendChild(symRect);
                if (primaryKey.Contains(txtNode.Text))
                {
                    Bitmap bmp = new Bitmap(@"..\..\class diagram\DBPrimaryKey.png");
                    BitmapNode bmpNode = new BitmapNode(bmp, new RectangleF(symRect.BoundingRectangle.X + 5, symRect.BoundingRectangle.Y + 5, symRect.BoundingRectangle.Width - 10, symRect.BoundingRectangle.Height - 10));
                    bmpNode.LineStyle.LineColor = Color.Transparent;
                    bmpNode.EditStyle.AllowSelect = false;
                    this.AppendChild(bmpNode);
                }

                if (foreignKey.Contains(txtNode.Text))
                {
                    Bitmap bmp = new Bitmap(@"..\..\class diagram\ForeignKey.png");
                    BitmapNode bmpNode = new BitmapNode(bmp, new RectangleF(symRect.BoundingRectangle.X + 5, symRect.BoundingRectangle.Y + 5, symRect.BoundingRectangle.Width - 10, symRect.BoundingRectangle.Height - 10));
                    bmpNode.LineStyle.LineColor = Color.Transparent;
                    bmpNode.EditStyle.AllowSelect = false;
                    this.AppendChild(bmpNode);
                }

                y_TextNode = y_TextNode + 20;
            }

        }

    }
}
