Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports AcadApp = Autodesk.AutoCAD.ApplicationServices.Application
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Internal
Imports AcadWnd = Autodesk.AutoCAD.Windows

Public Class ModalForm

Private Sub buttonBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonBrowse.Click
    Dim db As Database = HostApplicationServices.WorkingDatabase
    '清空显示块名的下拉列表框中的内容
    Me.comboBoxBlockName.Items.Clear()
    '新建一个打开文件对话框，并设置对话框的标题和显示文件类型为dwg或者dxf
    Dim dlg As New AcadWnd.OpenFileDialog("选择图形文件", Nothing, "dwg;dxf", Nothing, AcadWnd.OpenFileDialog.OpenFileDialogFlags.AllowMultiple)
    '如果打开对话框成功
    If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
        '显示所选择文件的路径
        Me.labelPath.Text = "路径:  " & dlg.Filename
        '导入所选择文件中的块
        Dim blockImport As New BlockImportClass
        blockImport.ImportBlocksFromDwg(dlg.Filename)
        '开始事务处理
        Using trans As Transaction = db.TransactionManager.StartTransaction
            '打开块表
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            '循环遍历块表中的块表记录
            For Each blockRecordId As ObjectId In bt
                '打开块表记录对象
                Dim btr As BlockTableRecord = trans.GetObject(blockRecordId, OpenMode.ForRead)
                '在下拉列表框中只加入非匿名块和非布局块的名称
                If Not btr.IsAnonymous And Not btr.IsLayout Then
                    Me.comboBoxBlockName.Items.Add(btr.Name)
                End If
            Next
        End Using
    End If
    '在下拉列表框中显示字母顺序排在第一个的块名
    If Me.comboBoxBlockName.Items.Count > 0 Then
        Me.comboBoxBlockName.Text = Me.comboBoxBlockName.Items(0)
    End If
End Sub

Private Sub buttonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
    '关闭窗体
    Me.Close()
End Sub

Private Sub comboBoxBlockName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBoxBlockName.SelectedIndexChanged
    Dim db As Database = HostApplicationServices.WorkingDatabase
    '获取下拉列表框对象
    Dim blockNames As ComboBox = sender
    '获取下拉列表框中当前选择的块名
    Dim blockName As String = blockNames.SelectedItem.ToString
    '开始事务处理
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '打开块表
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
        '打开名字为下拉列表框中当前选择的块名的块
        Dim btr As BlockTableRecord = trans.GetObject(bt(blockName), OpenMode.ForRead)
        '利用acmgdinternal.dll中的块浏览器对象来获取块的预览图案
        Dim blockThumbnail As Bitmap = BlockThumbnailHelper.GetBlockThumbanail(btr.ObjectId)
        '如果获取块预览图案成功，则设置图片框中的图案为该预览图案
        If Not (blockThumbnail Is Nothing) Then
            Me.pictureBoxBlock.Image = blockThumbnail
        End If
        '根据当前选择的块，设置块的单位和比例
        Select Case btr.Units
            Case UnitsValue.Inches
                Me.textBoxBlockUnit.Text = "英寸"
                Me.textBoxBlockRatio.Text = "25.4"
            Case UnitsValue.Meters
                Me.textBoxBlockUnit.Text = "米"
                Me.textBoxBlockRatio.Text = "1000"
            Case UnitsValue.Millimeters
                Me.textBoxBlockUnit.Text = "毫米"
                Me.textBoxBlockRatio.Text = "1"
            Case UnitsValue.Undefined
                Me.textBoxBlockUnit.Text = "无单位"
                Me.textBoxBlockRatio.Text = "1"
            Case Else
                Me.textBoxBlockUnit.Text = btr.Units.ToString
                Me.textBoxBlockRatio.Text = ""
        End Select
    End Using
End Sub

Private Sub checkBoxSameScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBoxSameScale.CheckedChanged
    '获取检查框对象
    Dim uniformScale As CheckBox = sender
    '如果检查框对象处于选中状态
    If uniformScale.Checked = True Then
        'Y、Z方向的缩放比例与X一致
        Me.textBoxScaleY.Text = Me.textBoxScaleX.Text
        Me.textBoxScaleZ.Text = Me.textBoxScaleX.Text
        'Y、Z方向的缩放比例设置文本框不能输入
        Me.textBoxScaleY.Enabled = False
        Me.textBoxScaleZ.Enabled = False
    Else
        'Y、Z方向的缩放比例设置文本框可以输入
        Me.textBoxScaleY.Enabled = True
        Me.textBoxScaleZ.Enabled = True
    End If
End Sub

Private Sub textBoxScaleX_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBoxScaleX.TextChanged
    '如果检查框处于选中状态，则Y、Z方向的缩放比例与X方向一致
    If Me.checkBoxSameScale.Checked = True Then
        Me.textBoxScaleY.Text = Me.textBoxScaleX.Text
        Me.textBoxScaleZ.Text = Me.textBoxScaleX.Text
    End If
End Sub

Private Sub checkBoxScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBoxScale.CheckedChanged
    '获取检查框对象
    Dim bchecked As CheckBox = sender
    '如果检查框对象处于选中状态，则不能在设置缩放比例的文本框输入
    If bchecked.Checked = True Then
        Me.textBoxScaleX.Enabled = False
        Me.textBoxScaleY.Enabled = False
        Me.textBoxScaleZ.Enabled = False
    '否则，可以在设置缩放比例的文本框输入
    Else
        Me.textBoxScaleX.Enabled = True
        Me.textBoxScaleY.Enabled = True
        Me.textBoxScaleZ.Enabled = True
    End If
End Sub

Private Sub checkBoxInsertPoint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBoxInsertPoint.CheckedChanged
    '获取检查框对象
    Dim bchecked As CheckBox = sender
    '如果检查框对象处于选中状态，则不能在设置插入点的文本框输入
    If bchecked.Checked = True Then
        Me.textBoxInsertPointX.Enabled = False
        Me.textBoxInsertPointY.Enabled = False
        Me.textBoxInsertPointZ.Enabled = False
    '否则，可以在设置插入点的文本框输入
    Else
        Me.textBoxInsertPointX.Enabled = True
        Me.textBoxInsertPointY.Enabled = True
        Me.textBoxInsertPointZ.Enabled = True
    End If
End Sub

Private Sub checkBoxRotate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBoxRotate.CheckedChanged
    '获取检查框对象
    Dim bchecked As CheckBox = sender
    '如果检查框对象处于选中状态，则不能在设置旋转角度的文本框输入
    If bchecked.Checked = True Then
        Me.textBoxRotateAngle.Enabled = False
    '否则，可以在设置旋转角度的文本框输入
    Else
        Me.textBoxRotateAngle.Enabled = True
    End If
End Sub

Private Sub ModalForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '初始状态下图形中没有块，所以显示为无单位字样和比例为1
    Me.textBoxBlockUnit.Text = "无单位"
    Me.textBoxBlockRatio.Text = "1"
End Sub

Private Sub buttonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonOK.Click
    '获取块参照插入点坐标
    Dim insertPointX As Double = Convert.ToDouble(Me.textBoxInsertPointX.Text)
    Dim insertPointY As Double = Convert.ToDouble(Me.textBoxInsertPointY.Text)
    Dim insertPointZ As Double = Convert.ToDouble(Me.textBoxInsertPointZ.Text)
    Dim insertPoint As New Point3d(insertPointX, insertPointY, insertPointZ)
    '获取块参照的缩放比例
    Dim scaleX As Double = Convert.ToDouble(Me.textBoxScaleX.Text)
    Dim scaleY As Double = Convert.ToDouble(Me.textBoxScaleY.Text)
    Dim scaleZ As Double = Convert.ToDouble(Me.textBoxScaleZ.Text)
    Dim scale As New Scale3d(scaleX, scaleY, scaleZ)
    '获取块参照的旋转角度
    Dim rotationAngle As Double = Convert.ToDouble(Me.textBoxRotateAngle.Text)
    '关闭窗体
    Me.Close()
    '插入块参照
    Dim myBlock As New MyBlock()
    myBlock.InsertBlockRef(Me.comboBoxBlockName.Text, insertPoint, scale, rotationAngle)
End Sub
End Class