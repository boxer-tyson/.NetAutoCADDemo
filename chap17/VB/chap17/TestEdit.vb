Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class TestEdit
    ' 简单选择集.
    <CommandMethod("testSel")> Public Sub testSelection1()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 定义一个选择集交互类.
        Dim optSel As New PromptSelectionOptions
        ' 选择操作时的提示文本.
        optSel.MessageForAdding = "请选择对象"
        ' 返回选择集的用户提示类.
        Dim resSel As PromptSelectionResult = ed.GetSelection(optSel)
        ' 得到选择集对象.
        Dim sSet As SelectionSet = resSel.Value
        ' 得到选择集中所有对象的ObjectId集合.
        Dim ids As ObjectId() = sSet.GetObjectIds()
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 遍历选择集.
            For Each sSetEntId As ObjectId In ids
                Dim en As Entity = trans.GetObject(sSetEntId, OpenMode.ForRead)
                ed.WriteMessage((vbCrLf & "您选择的是: " & en.GetType().Name))
            Next sSetEntId
            trans.Commit()
        End Using
    End Sub

    ' 带过滤的选择集.
    <CommandMethod("testFilSel")> Public Sub testSelection2()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        '定义过滤条件
        Dim value1 As TypedValue = New TypedValue(DxfCode.Start, "CIRCLE,LINE")
        Dim value2 As TypedValue = New TypedValue(DxfCode.LayerName, "0")
        Dim value3 As TypedValue = New TypedValue(DxfCode.Color, "1")
        Dim values() As TypedValue = {value1, value2, value3}
        Dim sfilter As New SelectionFilter(values)

        ' 定义一个选择集交互类.
        Dim optSel As New PromptSelectionOptions
        ' 选择操作时的提示文本.
        optSel.MessageForAdding = "请选择位于0层的红色的圆和红色的直线"
        ' 返回选择集的用户提示类.
        Dim resSel As PromptSelectionResult = ed.GetSelection(optSel, sfilter)
        ' 得到选择集对象.
        Dim sSet As SelectionSet = resSel.Value
        ' 得到选择集中所有对象的ObjectId集合.
        Dim ids As ObjectId() = sSet.GetObjectIds()
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 遍历选择集.
            For Each sSetEntId As ObjectId In ids
                Dim ent As Entity = trans.GetObject(sSetEntId, OpenMode.ForWrite)
                ' 修改所选择对象的颜色.
                ent.Color = Color.FromColorIndex(ColorMethod.ByColor, 2)
                ed.WriteMessage((vbCrLf & "您选择的是: " & ent.GetType().Name))
            Next sSetEntId
            trans.Commit()
        End Using
    End Sub

    ' 移动.
    <CommandMethod("netMove")> Public Sub testMove()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"
        'opt.AllowDuplicates = True

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sset As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sset.GetObjectIds()


        For Each id As ObjectId In ids
            Edit.Move(id, New Point3d(0, 0, 0), New Point3d(300, 200, 0))
        Next id
    End Sub

    ' 复制.
    <CommandMethod("netCopy")> Public Sub testCopy()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        For Each id As ObjectId In ids
            Edit.Copy(id, New Point3d(0, 0, 0), New Point3d(300, 200, 0))
        Next id
    End Sub

    ' 旋转.
    <CommandMethod("netRotate")> Public Sub testRotate()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        For Each id As ObjectId In ids
            Edit.Rotate(id, New Point3d(0, 0, 0), 30)
        Next id
    End Sub

    ' 缩放.
    <CommandMethod("netScale")> Public Sub testScale()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        For Each id As ObjectId In ids
            Edit.Scale(id, New Point3d(0, 0, 0), 3)
        Next id
    End Sub

    ' 镜像.
    <CommandMethod("netMirror")> Public Sub testMirror()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        For Each id As ObjectId In ids
            Edit.Mirror(id, New Point3d(0, 0, 0), New Point3d(300, 200, 0), False)
        Next id
    End Sub

    ' 偏移.
    <CommandMethod("netOffset")> Public Sub testOffset()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        '拾取对象---------------------------------------------------------- 
        Dim optEnt As New PromptEntityOptions(vbCrLf & "请选择要偏移的对象")

        Dim resEnt As PromptEntityResult = ed.GetEntity(optEnt)
        If resEnt.Status = PromptStatus.OK Then
            Using ta As Transaction = ed.Document.TransactionManager.StartTransaction()
                Dim ent As Entity = ta.GetObject(resEnt.ObjectId, OpenMode.ForRead)

                Edit.Offset(ent, -10)
                ta.Commit()
            End Using
        End If
    End Sub

    ' 矩形阵列.
    <CommandMethod("netArrayRectang")> Public Sub testArrayRectang()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        For Each id As ObjectId In ids
            Edit.ArrayRectang(id, 5, 8, 300, 200)
        Next id
    End Sub

    ' 环形阵列.
    <CommandMethod("netArrayPolar")> Public Sub testArrayPolar()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        For Each id As ObjectId In ids
            Edit.ArrayPolar(id, New Point3d(0, 0, 0), 8, Edit.Rad2Ang(360))
        Next id
    End Sub

    ' 删除.
    <CommandMethod("netErase")> Public Sub testErase()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optSel As New PromptSelectionOptions
        optSel.MessageForAdding = "请选择对象"

        Dim resSel As PromptSelectionResult = ed.GetSelection
        If resSel.Status <> PromptStatus.OK Then Return

        Dim sSet As SelectionSet = resSel.Value
        Dim ids As ObjectId() = sSet.GetObjectIds()

        Using trans As Transaction = db.TransactionManager.StartTransaction()
            For Each id As ObjectId In ids
                Edit.Erase(id)
            Next id
        End Using
    End Sub
End Class
