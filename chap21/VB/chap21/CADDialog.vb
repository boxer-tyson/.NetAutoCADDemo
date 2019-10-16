Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Public Class CADDialog
<CommandMethod("ModalForm")> _
Public Sub ShowModalForm()
    '显示模态对话框
    Dim modalForm As New ModalForm()
    Application.ShowModalDialog(modalForm)
End Sub
End Class
