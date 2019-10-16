Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Windows
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.DatabaseServices
Public Class ContextMenu
<CommandMethod("AddDefaultContextMenu")> _
Public Sub AddContextMenu()
    '定义一个ContextMenuExtension对象，用于表示快捷菜单
   Dim contextMenu As New ContextMenuExtension()
    '设置快捷菜单的标题
   contextMenu.Title = "我的快捷菜单"
    '添加一个名为"复制"的菜单项，用于调用复制命令
   Dim mi As New MenuItem("复制")
    '为"复制"菜单项添加单击事件
   AddHandler mi.Click, AddressOf mi_Click
    '将"复制"菜单项添加到快捷菜单中
   contextMenu.MenuItems.Add(mi)
    '添加一个名为"删除"的菜单项，用于调用删除命令
   mi = New MenuItem("删除")
    '为"删除"菜单项添加单击事件
   AddHandler mi.Click, AddressOf mi_Click
    '将"删除"菜单项添加到快捷菜单中
   contextMenu.MenuItems.Add(mi)
    '为应用程序添加定义的快捷菜单
   Application.AddDefaultContextMenuExtension(contextMenu)
End Sub

Sub mi_Click(ByVal sender As Object, ByVal e As EventArgs)
    '获取发出命令的快捷菜单项
    Dim mi As MenuItem = sender
    '获取当前活动文档
    Dim doc As Document = Application.DocumentManager.MdiActiveDocument
    '根据快捷菜单项的名字，分别调用对应的命令
    If mi.Text = "复制" Then
        doc.SendStringToExecute("_Copy ", True, False, True)
    ElseIf mi.Text = "删除" Then
        doc.SendStringToExecute("_Erase ", True, False, True)
    End If
End Sub
<CommandMethod("AddObjectContextMenu")> _
Public Sub AddObjectContextMenu()
    '定义一个ContextMenuExtension对象，用于表示快捷菜单
   Dim contextMenu As New ContextMenuExtension()
    '对于对象级别的快捷菜单，不必设置菜单名
   'contextMenu.Title = "圆的快捷菜单"
    '添加一个名为"圆面积"的菜单项，用于在AutoCAD命令行上显示所选择的圆面积
   Dim miCircle As New MenuItem("圆面积")
    '为"圆面积"菜单项添加单击事件
   AddHandler miCircle.Click, AddressOf miCircle_Click
    '将"圆面积"菜单项添加到快捷菜单中
   contextMenu.MenuItems.Add(miCircle)
    '为圆对象添加定义的快捷菜单
   Application.AddObjectContextMenuExtension(RXClass.GetClass(GetType(Circle)), contextMenu)
End Sub
Sub miCircle_Click(ByVal sender As Object, ByVal e As EventArgs)
   Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
   Dim db As Database = HostApplicationServices.WorkingDatabase
    '获取当前的选择集对象
   Dim ss As SelectionSet = ed.SelectImplied().Value
   Using trans As Transaction = db.TransactionManager.StartTransaction()
        '循环遍历选择集中的对象
        For Each id As ObjectId In ss.GetObjectIds()
            Dim obj As DBObject = trans.GetObject(id, OpenMode.ForRead)
            '如果所选择的对象是圆
            If TypeOf (obj) Is Circle Then
                '获取所选择的圆对象
                Dim circ As Circle = CType(obj, Circle)
                '在命令行上显示圆面积信息
                ed.WriteMessage(vbCrLf & "圆面积为:" & circ.Area.ToString())
            End If
        Next
   End Using
End Sub
End Class
