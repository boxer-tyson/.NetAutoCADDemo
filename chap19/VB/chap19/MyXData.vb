Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput

Public Class MyXData
<CommandMethod("AddXData")> _
Public Sub AddXData()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '下面的操作用来选择实体以添加扩展数据
        Dim opt As New PromptEntityOptions("请选择实体来添加扩展数据")
        Dim res As PromptEntityResult = ed.GetEntity(opt)
        If res.Status <> PromptStatus.OK Then
            Return
        End If
        Dim circ As Circle = trans.GetObject(res.ObjectId, OpenMode.ForWrite)
        '获取当前数据库的注册应用程序表
        Dim reg As RegAppTable = trans.GetObject(db.RegAppTableId, OpenMode.ForWrite)
        '如果没有名为"实体扩展数据"的注册应用程序表记录，则
        If Not reg.Has("实体扩展数据") Then
            '创建一个注册应用程序表记录用来表示扩展数据
            Dim app As New RegAppTableRecord
            '设置扩展数据的名字
            app.Name = "实体扩展数据"
            '在注册应用程序表加入扩展数据
            reg.Add(app)
            trans.AddNewlyCreatedDBObject(app, True)
        End If
        '设置扩展数据的内容
        Dim rb As New ResultBuffer( _
        New TypedValue(DxfCode.ExtendedDataRegAppName, "实体扩展数据"), _
        New TypedValue(DxfCode.ExtendedDataAsciiString, "字符串扩展数据"), _
        New TypedValue(DxfCode.ExtendedDataLayerName, "0"), _
        New TypedValue(DxfCode.ExtendedDataReal, 1.23479137438413E+40), _
        New TypedValue(DxfCode.ExtendedDataInteger16, 32767), _
        New TypedValue(DxfCode.ExtendedDataInteger32, 32767), _
        New TypedValue(DxfCode.ExtendedDataScale, 10), _
        New TypedValue(DxfCode.ExtendedDataWorldXCoordinate, New Point3d(10, 10, 0)))
        '将新建的扩展数据附加到所选择的实体中
        circ.XData = rb
        trans.Commit()
    End Using
End Sub

<CommandMethod("ListXData")> _
Public Sub ListXData()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '下面的操作用来选择实体来显示它的扩展数据
        Dim opt As New PromptEntityOptions("请选择实体来显示它的扩展数据")
        Dim res As PromptEntityResult = ed.GetEntity(opt)
        If res.Status <> PromptStatus.OK Then
            Return
        End If
        Dim circ As Circle = trans.GetObject(res.ObjectId, OpenMode.ForRead)
        '获取所选择实体中名为“实体扩展数据”的扩展数据
        Dim rb As ResultBuffer = circ.GetXDataForApplication("实体扩展数据")
        '如果没有，就返回
        If rb = Nothing Then
            Return
        End If
        '循环遍历扩展数据
        For Each circleXData As TypedValue In rb
            ed.WriteMessage(String.Format(vbCrLf & "TypeCode={0},Value={1}", circleXData.TypeCode, circleXData.Value))
        Next
    End Using
End Sub
End Class
