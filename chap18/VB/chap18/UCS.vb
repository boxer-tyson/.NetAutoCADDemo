Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class UCS
    <CommandMethod("netUCS")> Public Sub CreateUCS()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Using trans As Transaction = db.TransactionManager.StartTransaction
            ' 以写方式打开UCS表
            Dim ut As UcsTable = trans.GetObject(db.UcsTableId, OpenMode.ForWrite)
            Dim ucsName As String = "myUCS"
            ' 如果名为"myUCS"的UCS不存在，则新建一个UCS.
            If ut.Has(ucsName) = False Then
                ' 新建一个UCS表记录.
                Dim utr As UcsTableRecord = New UcsTableRecord
                ' 设置UCS表记录的名称.
                utr.Name = ucsName
                ' 设置UCS原点.
                utr.Origin = New Point3d(0, 0, 0)
                ' 设置UCS的X轴方向矢量（WCS中）.
                utr.XAxis = New Vector3d(0, 1, 0)
                ' 设置UCS的Y轴方向矢量（WCS中）.
                utr.YAxis = New Vector3d(-1, 0, 0)
                ' 得到该UCS的矩阵.
                Dim mt As Matrix3d = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, utr.Origin, utr.XAxis, utr.YAxis, utr.XAxis.CrossProduct(utr.YAxis))
                ' 将该UCS设置为当前UCS.
                ed.CurrentUserCoordinateSystem = mt
            End If
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("netUCSO")> Public Sub CreateUcsOrigin()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 提示用户确定新原点.
        Dim opt As New PromptPointOptions("指定新原点")
        Dim res As PromptPointResult = ed.GetPoint(opt)
        Dim pt As Point3d = res.Value
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 得到当前UCS的X轴方向矢量（WCS中）.
            Dim xAxis As Vector3d = db.Ucsxdir
            ' 得到当前UCS的Y轴方向矢量（WCS中）.
            Dim yAxis As Vector3d = db.Ucsydir
            ' 得到当前UCS的Z轴方向矢量（WCS中）.
            Dim zAxis As Vector3d = xAxis.CrossProduct(yAxis)
            ' 得到当前UCS矩阵.
            Dim cmt As Matrix3d = ed.CurrentUserCoordinateSystem
            ' 设置新的UCS原点.
            Dim newOrg As Point3d = pt.TransformBy(cmt)
            ' 建立新的UCS矩阵.
            Dim mT As Matrix3d = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, newOrg, xAxis, yAxis, zAxis)
            ' 将该UCS设置为当前UCS.
            ed.CurrentUserCoordinateSystem = mT
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("netUCSX")> Public Sub CreateUcsxAxis()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 提示用户确定旋转角度.
        Dim opt As New PromptAngleOptions("指定绕 X 轴的旋转角度")
        ' 允许使用默认值
        opt.UseDefaultValue = True
        ' 设置默认值
        opt.DefaultValue = Math.PI / 2
        Dim res As PromptDoubleResult = ed.GetAngle(opt)
        Dim ang As Double = res.Value
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim xAxis As Vector3d = db.Ucsxdir
            Dim yAxis As Vector3d = db.Ucsydir.RotateBy(ang, xAxis)
            Dim zAxis As Vector3d = xAxis.CrossProduct(yAxis)
            ' 得到当前UCS的原点.
            Dim org As Point3d = db.Ucsorg
            ' 建立新的UCS矩阵.
            Dim mT As Matrix3d = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, org, xAxis, yAxis, zAxis)
            ' 将该UCS设置为当前UCS.
            ed.CurrentUserCoordinateSystem = mT
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("ucsCircle")> Public Sub CreateUcsCircle()
        Dim ent As New Circle(New Point3d(90, 30, 0), Vector3d.ZAxis, 80)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 得到当前UCS矩阵.
        Dim mt As Matrix3d = ed.CurrentUserCoordinateSystem
        ' 实施矩阵变换.
        ent.TransformBy(mt)
        Using trans As Transaction = db.TransactionManager.StartTransaction
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            btr.AppendEntity(ent)
            trans.AddNewlyCreatedDBObject(ent, True)
            trans.Commit()
        End Using
    End Sub
End Class
