Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.GraphicsInterface
Imports Autodesk.AutoCAD.Runtime

Public Class Jig_Move
    ' 从DrawJig类继承.
    Inherits DrawJig
    ' 声明全局变量.
    Private sourcePt, targetPt, curPt As Point3d
    Private i As Integer
    Private entCopy() As Entity, ids As ObjectId()

    <CommandMethod("jigMove")> Sub testJigMove()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        ' 普通的选择集交互操作.
        Dim opt As New PromptSelectionOptions
        opt.MessageForAdding = "选择对象"
        opt.AllowDuplicates = True
        Dim res As PromptSelectionResult = ed.GetSelection(opt)
        If res.Status <> PromptStatus.OK Then Return
        Dim sSet As SelectionSet = res.Value
        ids = sSet.GetObjectIds()

        ReDim entCopy(ids.Length - 1)
        Dim oldEnt(ids.Length - 1) As Entity
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            For i = 0 To ids.Length - 1
                oldEnt(i) = trans.GetObject(ids(i), OpenMode.ForWrite)
                ' 将源对象设置为高亮状态.
                oldEnt(i).Highlight()
                ' 复制.
                entCopy(i) = oldEnt(i).Clone()
            Next

            ' 得到移动的源点-----------------------------------------------
            Dim optPoint As New PromptPointOptions(vbCrLf & "请输入基点<0,0,0>")
            optPoint.AllowNone = True
            Dim resPoint As PromptPointResult = ed.GetPoint(optPoint)
            If resPoint.Status <> PromptStatus.Cancel Then
                If resPoint.Status = PromptStatus.None Then
                    sourcePt = New Point3d(0, 0, 0)
                Else
                    sourcePt = resPoint.Value
                End If
            End If

            ' 设置目标点和拖拽临时点的初值.
            targetPt = sourcePt
            curPt = targetPt

            ' 开始拖拽.
            Dim jigRes As PromptResult = ed.Drag(Me)
            If jigRes.Status = PromptStatus.OK Then
                Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
                Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
                For i = 0 To ids.Length - 1
                    btr.AppendEntity(entCopy(i))
                    trans.AddNewlyCreatedDBObject(entCopy(i), True)
                Next
                ' 删除源对象.
                For i = 0 To ids.Length - 1
                    oldEnt(i).Erase()
                Next
            Else
                ' 取消源对象的高亮状态.
                For i = 0 To ids.Length - 1
                    oldEnt(i).Unhighlight()
                Next
            End If
            trans.Commit()
        End Using
    End Sub

    ' Sampler函数用于检测用户的输入.
    Protected Overrides Function Sampler(ByVal prompts As JigPrompts) As SamplerStatus
        ' 定义一个点拖动交互类.
        Dim optJig As New JigPromptPointOptions(vbCrLf & "请指定第二点:")
        ' 设置拖拽光标类型.
        optJig.Cursor = CursorType.RubberBand
        ' 设置拖动光标基点.
        optJig.BasePoint = sourcePt
        optJig.UseBasePoint = True
        ' 用AcquirePoint函数得到用户输入的点.
        Dim resJig As PromptPointResult = prompts.AcquirePoint(optJig)
        targetPt = resJig.Value
        ' 如果用户拖拽，则用矩阵变换的方法移动选择集中的全部对象.
        If curPt <> targetPt Then
            Dim moveMt As Matrix3d = Matrix3d.Displacement(targetPt - curPt)
            For i = 0 To ids.Length - 1
                entCopy(i).TransformBy(moveMt)
            Next
            ' 保存当前点.
            curPt = targetPt
            Return SamplerStatus.OK
        Else
            Return SamplerStatus.NoChange
        End If
    End Function

    ' WorldDraw函数用于刷新屏幕上显示的图形.
    Protected Overrides Function WorldDraw(ByVal draw As WorldDraw) As Boolean
        For i = 0 To ids.Length - 1
            ' 刷新画面.
            draw.Geometry.Draw(entCopy(i))
        Next
        Return True
    End Function
End Class

