Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class ViewPost
    <CommandMethod("netViewPost")> Public Sub CreateViewPost()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Using trans As Transaction = db.TransactionManager.StartTransaction
            ' 以写方式打开视口表
            Dim vpt As ViewportTable = trans.GetObject(db.ViewportTableId, OpenMode.ForWrite)
            ' 新建四个视口表记录.
            Dim vName As String = "abc"
            Dim vptr1 As New ViewportTableRecord
            vptr1.LowerLeftCorner = New Point2d(0, 0)
            vptr1.UpperRightCorner = New Point2d(0.5, 0.5)
            vptr1.Name = vName
            Dim vptr2 As New ViewportTableRecord
            vptr2.LowerLeftCorner = New Point2d(0.5, 0)
            vptr2.UpperRightCorner = New Point2d(1, 0.5)
            vptr2.Name = vName
            Dim vptr3 As New ViewportTableRecord
            vptr3.LowerLeftCorner = New Point2d(0, 0.5)
            vptr3.UpperRightCorner = New Point2d(0.5, 1)
            vptr3.Name = vName
            Dim vptr4 As New ViewportTableRecord
            vptr4.LowerLeftCorner = New Point2d(0.5, 0.5)
            vptr4.UpperRightCorner = New Point2d(1, 1)
            vptr4.Name = vName
            ' 把视口表记录添加到视口表中.
            vpt.Add(vptr1)
            vpt.Add(vptr2)
            vpt.Add(vptr3)
            vpt.Add(vptr4)
            ' 把视口表记录添加到事务处理中.
            trans.AddNewlyCreatedDBObject(vptr1, True)
            trans.AddNewlyCreatedDBObject(vptr2, True)
            trans.AddNewlyCreatedDBObject(vptr3, True)
            trans.AddNewlyCreatedDBObject(vptr4, True)
            ' 得到当前文档.
            Dim doc As Document = Application.DocumentManager.MdiActiveDocument
            ' 分析当前空间是模型空间还是图纸空间.
            If Application.GetSystemVariable("TILEMODE") = 1 Then
                doc.SendStringToExecute("-VPORTS 4 ", False, False, False)
            Else
                doc.SendStringToExecute("-VPORTS 4  ", False, False, False)
            End If
            trans.Commit()
        End Using
    End Sub
End Class
