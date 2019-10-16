Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Public Class MyXrecord
<CommandMethod("CreateXrecord")> _
Public Sub CreateXrecord()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '新建一个扩展记录对象
        Dim xrec As New Xrecord()
        '设置扩展记录中包含的数据列表，包括文本、坐标、数值、角度、颜色
        Dim pt As New Point3d(1.0, 2.0, 0.0)
        xrec.Data = New ResultBuffer( _
                    New TypedValue(DxfCode.Text, "这是一个测试用的扩展记录列表"), _
                    New TypedValue(DxfCode.XCoordinate, pt), _
                    New TypedValue(DxfCode.Real, 3.14159), _
                    New TypedValue(DxfCode.Angle, 3.14159), _
                    New TypedValue(DxfCode.Color, 1), _
                    New TypedValue(DxfCode.Int16, 180))
        '下面的操作用来选择要添加扩展记录的对象
        Dim opt As New PromptEntityOptions("请选择要添加扩展记录的对象")
        Dim res As PromptEntityResult = ed.GetEntity(opt)
        If res.Status <> PromptStatus.OK Then
            Return
        End If
        Dim ent As Entity = trans.GetObject(res.ObjectId, OpenMode.ForWrite)
        '判断所选对象是否已包含扩展记录
        If ent.ExtensionDictionary <> ObjectId.Null Then
            ed.WriteMessage("对象已包含扩展记录，无法再创建")
            Return
        End If
        '为所选择的对象创建一个扩展字典
        ent.CreateExtensionDictionary()
        Dim dictEntId As ObjectId = ent.ExtensionDictionary()
        Dim entXrecord As DBDictionary = trans.GetObject(dictEntId, OpenMode.ForWrite)
        '在扩展字典中加入上面新建的扩展记录对象，并指定它的搜索关键字为MyXrecord
        entXrecord.SetAt("MyXrecord", xrec)
        '通知事务处理完成扩展记录对象的加入
        trans.AddNewlyCreatedDBObject(xrec, True)
        trans.Commit()
    End Using
End Sub

<CommandMethod("ListXrecord")> _
Public Sub ListXrecord()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '下面的操作用来选择显示扩展记录的对象
        Dim opt As New PromptEntityOptions("请选择要显示扩展记录的对象")
        Dim res As PromptEntityResult = ed.GetEntity(opt)
        If res.Status <> PromptStatus.OK Then
            Return
        End If
        Dim ent As Entity = trans.GetObject(res.ObjectId, OpenMode.ForRead)
        '打开所选择对象的扩展字典
        Dim entXrecord As DBDictionary = trans.GetObject(ent.ExtensionDictionary, OpenMode.ForRead)
        '在扩展字典中搜索关键字为MyXrecord的扩展记录对象，如果找到则返回它的ObjectId
        Dim xrecordId As ObjectId = entXrecord.GetAt("MyXrecord")
        '打开找到的扩展记录对象
        Dim xrecord As Xrecord = trans.GetObject(xrecordId, OpenMode.ForRead)
        '获取扩展记录中包含的数据列表并循环遍历显示它们
        Dim rb As ResultBuffer = xrecord.Data
        For Each value As TypedValue In rb
            ed.WriteMessage(String.Format(vbCrLf & "TypeCode={0},Value={1}", value.TypeCode, value.Value))
        Next
        trans.Commit()
    End Using
End Sub
End Class
