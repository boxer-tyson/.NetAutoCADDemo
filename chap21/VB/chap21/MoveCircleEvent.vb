Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Namespace CH20
    Public Class MoveCircleEvent
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Dim bMove As Boolean
        Dim startPoint As Point3d
        Sub commandWillStart(ByVal sender As Object, ByVal e As CommandEventArgs)
            '如果AutoCAD命令为MOVE。
            If e.GlobalCommandName = "MOVE" Then
                '设置全局变量bMove为True，表示移动命令开始
                bMove = True
            End If
        End Sub


        Sub objectOpenedForModify(ByVal sender As Object, ByVal e As ObjectEventArgs)
            '判断AutoCAD命令是否为移动
            If bMove = False Then
                '如果AutoCAD命令为非移动，则返回
                Return
            End If
            '判断将要移动的对象是否为圆
            If TypeOf (e.DBObject) Is Circle Then
                '获取将要移动的圆对象，但还没移动
                Dim circle As Circle = CType(e.DBObject, Circle)
                '获取圆的中心，就是同心圆的圆心
                startPoint = circle.Center
            End If
        End Sub

        Sub objectModified(ByVal sender As Object, ByVal e As ObjectEventArgs)
            '判断AutoCAD命令是否为移动
            If bMove = False Then
                '如果AutoCAD命令为非移动，则返回
                Return
            End If
            '断开所有的事件处理函数
            removeEvents()
            '判断移动过的对象是否为圆
            If TypeOf (e.DBObject) Is Circle Then
                '获取移动的圆对象
                Dim startCircle As Circle = CType(e.DBObject, Circle)
                '设置选择集过滤器，只选择图形中的圆
                Dim values() As TypedValue = {New TypedValue(DxfCode.Start, "Circle")}
                Dim filter As New SelectionFilter(values)
                Dim resSel As PromptSelectionResult = ed.SelectAll(filter)
                '如果选择的是圆
                If resSel.Status = PromptStatus.OK Then
                    '获取选择集中的圆对象
                    Dim sSet As SelectionSet = resSel.Value
                    Dim ids As ObjectId() = sSet.GetObjectIds()
                    '开始事务处理
                    Using trans As Transaction = db.TransactionManager.StartTransaction
                        '循环遍历选择集中的圆
                        For Each id As ObjectId In ids
                            '以读的方式打开圆对象
                            Dim followedCirlce As Circle = CType(trans.GetObject(id, OpenMode.ForRead), Circle)
                            '通过判断圆的圆心与所移动的圆的圆心是否相同，来移动所有的同心圆
                            If followedCirlce.Center = startPoint Then
                                '因为上面以读的方式打开了圆，所以为了改变圆的圆心必须改变为写
                                followedCirlce.UpgradeOpen()
                                '改变圆的圆心，以达到移动的目的
                                followedCirlce.Center = startCircle.Center
                            End If
                        Next id
                        '提交事务处理
                        trans.Commit()
                    End Using
                End If
            End If
            '连接所有的事件处理函数
            addEvents()
        End Sub
        Sub commandEnded(ByVal sender As Object, ByVal e As CommandEventArgs)
            '判断AutoCAD命令是否为移动
            If bMove = True Then
                '设置全局变量bMove为False，表示移动命令结束
                bMove = False
            End If
        End Sub


        <CommandMethod("AddEvents")> _
        Public Sub addEvents()
            '把事件处理函数与相应的事件进行连接
            AddHandler db.ObjectOpenedForModify, AddressOf objectOpenedForModify
            AddHandler db.ObjectModified, AddressOf objectModified
            AddHandler doc.CommandWillStart, AddressOf commandWillStart
            AddHandler doc.CommandEnded, AddressOf commandEnded
        End Sub

        <CommandMethod("RemoveEvents")> _
        Public Sub removeEvents()
            '断开所有的事件处理函数
            RemoveHandler db.ObjectOpenedForModify, AddressOf objectOpenedForModify
            RemoveHandler db.ObjectModified, AddressOf objectModified
            RemoveHandler doc.CommandWillStart, AddressOf commandWillStart
            RemoveHandler doc.CommandEnded, AddressOf commandEnded
        End Sub
    End Class
End Namespace
