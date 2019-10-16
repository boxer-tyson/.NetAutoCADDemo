Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class Edit
    ' 移动的过程.
    Public Shared Sub Move(ByVal ent As Entity, ByVal sourcePt As Point3d, ByVal targetPt As Point3d)
        ' 移动的方向矢量.
        Dim vec As Vector3d = targetPt - sourcePt
        ' 移动的矩阵.
        Dim mt As Matrix3d = Matrix3d.Displacement(vec)
        ' 实施移动.
        ent.TransformBy(mt)
    End Sub

    ' 移动的过程.
    Public Shared Sub Move(ByVal id As ObjectId, ByVal sourcePt As Point3d, ByVal targetPt As Point3d)
        Dim mt As Matrix3d = Matrix3d.Displacement(targetPt - sourcePt)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            ent.TransformBy(mt)
            trans.Commit()
        End Using
    End Sub

    ' 复制的过程.
    Public Shared Sub Copy(ByVal ent As Entity, ByVal sourcePt As Point3d, ByVal targetPt As Point3d)
        Dim mt As Matrix3d = Matrix3d.Displacement(targetPt - sourcePt)
        Dim entCopy As Entity = ent.GetTransformedCopy(mt)
        AppendEntity(entCopy)
    End Sub

    ' 复制的过程.
    Public Shared Sub Copy(ByVal id As ObjectId, ByVal sourcePt As Point3d, ByVal targetPt As Point3d)
        Dim mt As Matrix3d = Matrix3d.Displacement(targetPt - sourcePt)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim entCopy As Entity
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            entCopy = ent.GetTransformedCopy(mt)
            trans.Commit()
        End Using
        AppendEntity(entCopy)
    End Sub

    ' 旋转的过程.
    Public Shared Sub Rotate(ByVal ent As Entity, ByVal basePt As Point3d, ByVal angle As Double)
        Dim mt As Matrix3d = Matrix3d.Rotation(angle, Vector3d.ZAxis, basePt)
        ent.TransformBy(mt)
    End Sub

    ' 旋转的过程.
    Public Shared Sub Rotate(ByVal id As ObjectId, ByVal basePt As Point3d, ByVal angle As Double)
        Dim mt As Matrix3d = Matrix3d.Rotation(angle, Vector3d.ZAxis, basePt)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            ent.TransformBy(mt)
            trans.Commit()
        End Using
    End Sub

    ' 缩放的过程.
    Public Shared Sub Scale(ByVal ent As Entity, ByVal basePt As Point3d, ByVal scaleFactor As Double)
        Dim mt As Matrix3d = Matrix3d.Scaling(scaleFactor, basePt)
        ent.TransformBy(mt)
    End Sub

    ' 缩放的过程.
    Public Shared Sub Scale(ByVal id As ObjectId, ByVal basePt As Point3d, ByVal scaleFactor As Double)
        Dim mt As Matrix3d = Matrix3d.Scaling(scaleFactor, basePt)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            ent.TransformBy(mt)
            trans.Commit()
        End Using
    End Sub

    ' 镜像的过程.
    Public Shared Sub Mirror(ByVal ent As Entity, ByVal mirrorPt1 As Point3d, ByVal mirrorPt2 As Point3d, ByVal eraseSourceObject As Boolean)
        ' 镜像线.
        Dim mirrorLine As New Line3d(mirrorPt1, mirrorPt2)
        ' 镜像矩阵.
        Dim mt As Matrix3d = Matrix3d.Mirroring(mirrorLine)
        If eraseSourceObject = True Then
            ent.TransformBy(mt)
        Else
            Dim entCopy As Entity = ent.GetTransformedCopy(mt)
            AppendEntity(entCopy)
        End If
    End Sub

    ' 镜像的过程.
    Public Shared Sub Mirror(ByVal id As ObjectId, ByVal mirrorPt1 As Point3d, ByVal mirrorPt2 As Point3d, ByVal eraseSourceObject As Boolean)
        Dim miLine As New Line3d(mirrorPt1, mirrorPt2)
        Dim mt As Matrix3d = Matrix3d.Mirroring(miLine)
        Dim db As Database = HostApplicationServices.WorkingDatabase

        Dim ent As Entity
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ent = trans.GetObject(id, OpenMode.ForWrite)
        End Using

        If eraseSourceObject = True Then
            ent.TransformBy(mt)
        Else
            Dim entCopy As Entity = ent.GetTransformedCopy(mt)
            AppendEntity(entCopy)
        End If
    End Sub

    ' 偏移的过程.
    Public Shared Sub Offset(ByVal cur As Curve, ByVal dis As Double)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Try
            Dim offsetCur As DBObjectCollection = cur.GetOffsetCurves(dis)
            '将偏移的对象加入到数据库
            For i As Integer = 0 To offsetCur.Count - 1
                AppendEntity(offsetCur(i))
            Next
        Catch
            ed.WriteMessage("无法偏移！")
        End Try
    End Sub

    ' 偏移的过程.
    Public Shared Sub Offset(ByVal id As ObjectId, ByVal dis As Double)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            If TypeOf (ent) Is Curve Then
                Dim cur As Curve = ent
                Try
                    Dim offsetCur As DBObjectCollection = cur.GetOffsetCurves(dis)
                    '将偏移的对象加入到数据库
                    For i As Integer = 0 To offsetCur.Count - 1
                        AppendEntity(offsetCur(i))
                    Next
                    trans.Commit()
                Catch
                    ed.WriteMessage("无法偏移！")
                End Try
            Else
                ed.WriteMessage("无法偏移！")
            End If
        End Using
    End Sub

    ' 矩形阵列的过程.
    Public Shared Sub ArrayRectang(ByVal ent As Entity, ByVal numRows As Integer, ByVal numCols As Integer, ByVal disRows As Double, ByVal disCols As Double)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            For m As Integer = 0 To numRows - 1
                For n As Integer = 0 To numCols - 1
                    Dim mt As Matrix3d = Matrix3d.Displacement(New Vector3d(n * disCols, m * disRows, 0))
                    Dim entCopy As Entity = ent.GetTransformedCopy(mt)
                    AppendEntity(entCopy)
                Next
            Next
            ' 删除多余的对象.
            ent.Erase()
            trans.Commit()
        End Using
    End Sub

    ' 矩形阵列的过程.
    Public Shared Sub ArrayRectang(ByVal id As ObjectId, ByVal numRows As Integer, ByVal numCols As Integer, ByVal disRows As Double, ByVal disCols As Double)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            For m As Integer = 0 To numRows - 1
                For n As Integer = 0 To numCols - 1
                    Dim mt As Matrix3d = Matrix3d.Displacement(New Vector3d(n * disCols, m * disRows, 0))
                    Dim entCopy As Entity = ent.GetTransformedCopy(mt)
                    AppendEntity(entCopy)
                Next
            Next
            ' 删除多余的对象.
            ent.Erase()
            trans.Commit()
        End Using
    End Sub

    ' 环形阵列的过程.
    Public Shared Sub ArrayPolar(ByVal ent As Entity, ByVal cenPt As Point3d, ByVal numObj As Integer, ByVal Angle As Double)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            For i As Integer = 0 To numObj - 2
                Dim mt As Matrix3d = Matrix3d.Rotation(Angle * (i + 1) / numObj, Vector3d.ZAxis, cenPt)
                Dim entCopy As Entity = ent.GetTransformedCopy(mt)
                AppendEntity(entCopy)
            Next
            trans.Commit()
        End Using
    End Sub

    ' 环形阵列的过程.
    Public Shared Sub ArrayPolar(ByVal id As ObjectId, ByVal cenPt As Point3d, ByVal numObj As Integer, ByVal Angle As Double)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            For i As Integer = 0 To numObj - 2
                Dim mt As Matrix3d = Matrix3d.Rotation(Angle * (i + 1) / numObj, Vector3d.ZAxis, cenPt)
                Dim entCopy As Entity = ent.GetTransformedCopy(mt)
                AppendEntity(entCopy)
            Next
            trans.Commit()
        End Using
    End Sub

    ' 删除的过程.
    Public Shared Sub [Erase](ByVal ent As Entity)
        ent.Erase()
    End Sub

    ' 删除的过程.
    Public Shared Sub [Erase](ByVal id As ObjectId)
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim ent As Entity = trans.GetObject(id, OpenMode.ForWrite)
            ent.Erase()
            trans.Commit()
        End Using
    End Sub

    ' 度化弧度的函数.
    Public Shared Function Rad2Ang(ByVal angle As Double) As Double
        Dim pi As Double = 4 * Math.Atan(1)
        Rad2Ang = angle * pi / 180
    End Function

    ' 将图形对象加入到模型空间的函数.
    Public Shared Function AppendEntity(ByVal obj As Entity) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim objId As ObjectId
        Using trans As Transaction = db.TransactionManager.StartTransaction
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            objId = btr.AppendEntity(obj)
            trans.AddNewlyCreatedDBObject(obj, True)
            trans.Commit()
        End Using
        Return objId
    End Function
End Class
