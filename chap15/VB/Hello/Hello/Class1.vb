Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput

Public Class Class1
    <CommandMethod("Hello")> _
    Public Sub Hello()
        ' 获取当前活动文档的Editor对象，也就是命令行
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 调用Editor对象的WriteMessage方法在命令行上显示文本
        ed.WriteMessage("欢迎进入.NET开发AutoCAD的世界！")
    End Sub
End Class
