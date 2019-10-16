Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Internal

Public Class View
    <CommandMethod("netViewScale")> Public Sub testViewScale()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Using trans As Transaction = db.TransactionManager.StartTransaction
            ' 以写方式打开视图表.
            Dim vt As ViewTable = trans.GetObject(db.ViewTableId, OpenMode.ForWrite)
            ' 得到当前视图表记录.
            Dim curVtr As ViewTableRecord = ed.GetCurrentView
            ' 得到当前视图的中心点.
            Dim cen As Point2d = curVtr.CenterPoint
            ' 得到当前视图的宽度.
            Dim width As Double = curVtr.Width
            ' 得到当前视图的高度.
            Dim height As Double = curVtr.Height
            ' 新建视图表记录.
            Dim newVtr As ViewTableRecord = curVtr
            newVtr.Name = "newView"
            ' 设置宽度.       
            newVtr.Width = width / 2
            ' 设置高度.
            newVtr.Height = height / 2
            ' 设置为当前视图.
            ed.SetCurrentView(newVtr)
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("netZoom")> Public Sub testZoom()
        '范围缩放
        Utils.ZoomObjects(True)
    End Sub
End Class
