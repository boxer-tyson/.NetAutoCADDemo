Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Public Class TabbedDialog
<CommandMethod("CreateNewOptionsTab")> _
Public Sub CreateNewOptionsTab()
    '当DisplayingOptionDialog事件被触发时（即选项对话框显示），运行displayingOptionDialog函数
   AddHandler Application.DisplayingOptionDialog, AddressOf displayingOptionDialog
End Sub
Sub displayingOptionDialog(ByVal sender As Object, ByVal e As TabbedDialogEventArgs)
    '创建一个自定义控件的实例，这里使用的是显示图片的自定义控件
   Dim optionTab As New PictureTab()
    '为确定按钮添加动作，也可以为取消、应用、帮助按钮添加动作
   Dim onOkPress As New TabbedDialogAction(AddressOf OnOptionOK)
    '将显示图片的控件作为标签页添加到选项对话框，并处理确定按钮所引发的动作
   e.AddTab("图片", New TabbedDialogExtension(optionTab, onOkPress))
End Sub

Sub OnOptionOK()
   '当确定按钮被按下时，显示一个警告对话框
   Application.ShowAlertDialog("添加选项对话框标签页成功！")
End Sub

End Class
