Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls

Namespace WindowsApplication159
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()

			dataTable1.Rows.Add(New Object() { "Value 1" })
			dataTable1.Rows.Add(New Object() { "Value 2" })
			dataTable1.Rows.Add(New Object() { "Value 3" })
			dataTable1.Rows.Add(New Object() { "Value 4" })

			Dim ri As New MyRepositoryItemButtonEdit()
			ri.Buttons(0).Kind = ButtonPredefines.Glyph
			ri.TextEditStyle = TextEditStyles.HideTextEditor
			gridControl1.RepositoryItems.Add(ri)
			gridView1.Columns("Column1").ColumnEdit = ri

			AddHandler ri.ButtonClick, AddressOf repositoryItemButtonEdit1_ButtonClick
		End Sub

		Private Sub repositoryItemButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
			MessageBox.Show(e.Button.Caption)
		End Sub

		Private Sub gridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gridView1.CustomDrawCell
			If e.Column.FieldName = "Column1" Then
				Dim editInfo As ButtonEditViewInfo = CType((CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)).ViewInfo, ButtonEditViewInfo)
				editInfo.RightButtons(0).Button.Caption = e.DisplayText
			End If
		End Sub

		Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.ShownEditor
			Dim view As GridView = CType(sender, GridView)
			If view.FocusedColumn.FieldName = "Column1" Then
				Dim ed As ButtonEdit = CType(view.ActiveEditor, ButtonEdit)
				ed.Properties.Buttons(0).Caption = view.GetFocusedDisplayText()
			End If
		End Sub

	End Class


	'
	' Custom RepositoryItemButtonEdit
	'
	Public Class MyRepositoryItemButtonEdit
		Inherits RepositoryItemButtonEdit
		Public Overrides Function CreateViewInfo() As DevExpress.XtraEditors.ViewInfo.BaseEditViewInfo
			Return New MyRepositoryItemButtonEditViewInfo(Me)
		End Function
	End Class
	Public Class MyRepositoryItemButtonEditViewInfo
		Inherits ButtonEditViewInfo
		Public Sub New(ByVal item As RepositoryItem)
			MyBase.New(item)
		End Sub

		Protected Overrides Function CreateButtonInfo(ByVal button As EditorButton, ByVal index As Integer) As DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs
			Return MyBase.CreateButtonInfo(New MyEditorButton(), index)
		End Function
	End Class
	Public Class MyEditorButton
		Inherits EditorButton
		Public Sub New()
			Me.New(String.Empty)
		End Sub
		Public Sub New(ByVal myCaption As String)
			Me.myCaption = myCaption
			Kind = ButtonPredefines.Glyph
		End Sub
		Private myCaption As String = ""
		Public Overrides Property Caption() As String
			Get
				Return myCaption
			End Get
			Set(ByVal value As String)
				myCaption = value
			End Set
		End Property
	End Class
End Namespace