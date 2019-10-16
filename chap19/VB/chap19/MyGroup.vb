Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput

Public Class MyGroup
<CommandMethod("MakeGroup")> _
Public Sub MakeGroup()
    '创建名为MyGroup的组
    createGroup("MyGroup")
End Sub
<CommandMethod("RemoveButLines")> _
Public Sub RemoveButLines()
    '在MyGroup组中移除所有不是直线的对象
    removeAllButLines("MyGroup")
End Sub

Private Sub createGroup(ByVal groupName As String)
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '新建一个组对象
        Dim gp As New Group(groupName, True)
        '打开当前数据库的组字典对象以加入新建的组对象
        Dim dict As DBDictionary = trans.GetObject(db.GroupDictionaryId, OpenMode.ForWrite)
        '在组字典中将组对象作为一个新条目加入，并指定它的搜索关键字为groupName
        dict.SetAt(groupName, gp)
        '下面的操作用来选择组中要包含的对象
        Dim opt As New PromptSelectionOptions()
        opt.MessageForAdding = "请选择组中要包含的对象"
        Dim res As PromptSelectionResult = ed.GetSelection(opt)
        If res.Status <> PromptStatus.OK Then
            Return
        End If
        '获取所选择对象的ObjectId集合
        Dim ss As SelectionSet = res.Value
        Dim ids As New ObjectIdCollection(ss.GetObjectIds())
        '在组对象中加入所选择的对象
        gp.Append(ids)
        '通知事务处理完成组对象的加入
        trans.AddNewlyCreatedDBObject(gp, True)
        trans.Commit()
    End Using
End Sub
Private Sub removeAllButLines(ByVal groupName As String)
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '打开当前数据库的组字典对象
        Dim dict As DBDictionary = trans.GetObject(db.GroupDictionaryId, OpenMode.ForRead)
        '在组字典中搜索关键字为groupName的组对象，如果找到则返回它的ObjectId
        Dim gpid As ObjectId = dict.GetAt(groupName)
        '由于要在组中进行去除对象的操作，因此以写的方式打开找到的组对象
        Dim gp As Group = trans.GetObject(gpid, OpenMode.ForWrite)
        '获取组对象中的所有实体的ObjectId并进行循环遍历
        Dim ids As ObjectId() = gp.GetAllEntityIds()
        For Each id As ObjectId In ids
            '打开组中的当前对象，并判断是否为直线
            Dim obj As DBObject = trans.GetObject(id, OpenMode.ForRead)
            If Not TypeOf (obj) Is Line Then
                '如果对象不是直线，则在组中移除它
                gp.Remove(id)
            End If
        Next
        '设置组中所有实体的颜色为红色
        gp.SetColorIndex(1)
        trans.Commit()
    End Using
End Sub
End Class
