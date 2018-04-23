using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace WindowsApplication159
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataTable1.Rows.Add(new object[] { "Value 1" });
            dataTable1.Rows.Add(new object[] { "Value 2" });
            dataTable1.Rows.Add(new object[] { "Value 3" });
            dataTable1.Rows.Add(new object[] { "Value 4" });

            MyRepositoryItemButtonEdit ri = new MyRepositoryItemButtonEdit();
            ri.Buttons[0].Kind = ButtonPredefines.Glyph;
            ri.TextEditStyle = TextEditStyles.HideTextEditor;
            gridControl1.RepositoryItems.Add(ri);
            gridView1.Columns["Column1"].ColumnEdit = ri;

            ri.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
        }

        void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show(e.Button.Caption);
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Column1")
            {
                ButtonEditViewInfo editInfo = (ButtonEditViewInfo)((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell).ViewInfo;
                editInfo.RightButtons[0].Button.Caption = e.DisplayText;
            }
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view.FocusedColumn.FieldName == "Column1")
            {
                ButtonEdit ed = (ButtonEdit)view.ActiveEditor;
                ed.Properties.Buttons[0].Caption = view.GetFocusedDisplayText();
            }
        }

    }


    //
    // Custom RepositoryItemButtonEdit
    //
    public class MyRepositoryItemButtonEdit : RepositoryItemButtonEdit
    {
        public override DevExpress.XtraEditors.ViewInfo.BaseEditViewInfo CreateViewInfo()
        {
            return new MyRepositoryItemButtonEditViewInfo(this);
        }
    }
    public class MyRepositoryItemButtonEditViewInfo : ButtonEditViewInfo
    {
        public MyRepositoryItemButtonEditViewInfo(RepositoryItem item) : base(item) { }

        protected override DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs CreateButtonInfo(EditorButton button, int index)
        {
            return base.CreateButtonInfo(new MyEditorButton(), index);
        }
    }
    public class MyEditorButton : EditorButton
    {
        public MyEditorButton() : this(string.Empty) { }
        public MyEditorButton(string myCaption)
        {
            this.myCaption = myCaption;
            Kind = ButtonPredefines.Glyph;
        }
        string myCaption = "";
        public override string Caption
        {
            get
            {
                return myCaption;
            }
            set
            {
                myCaption = value;
            }
        }
    }
}