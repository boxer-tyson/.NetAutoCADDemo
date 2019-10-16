Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class Interactive
    <CommandMethod("AddPoly")> Public Sub CreatePoly()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 初始化线宽.
        Dim width As Double = 0
        ' 初始化颜色索引值.
        Dim colorIndex As Integer = 0
        ' 初始化顶点数.
        Dim index As Integer = 2
        ' 在内存中创建多段线.
        Dim polyEnt As New Polyline
        ' 声明多段线的ObjectId.
        Dim polyEntId As ObjectId

        ' 定义一个点的用户交互类.
        Dim optPoint As New PromptPointOptions(vbCrLf & "请输入第一个点<100,200>")
        ' 允许用户回车响应.
        optPoint.AllowNone = True
        ' 返回点的用户提示类.
        Dim resPoint As PromptPointResult = ed.GetPoint(optPoint)
        ' 用户按下ESC键，退出.
        If resPoint.Status = PromptStatus.Cancel Then Return
        ' 声明第一个输入点.
        Dim ptStart As Point3d
        ' 用户按回车键.
        If resPoint.Status = PromptStatus.None Then
            ' 得到第一个输入点的默认值.
            ptStart = New Point3d(100, 200, 0)
        Else
            ' 得到第一个输入点.
            ptStart = resPoint.Value
        End If
        ' 保存当前点.
        Dim ptPrevious As Point3d = ptStart

nextPoint:
        ' 定义输入下一点的点交互类.
        Dim optPtKey As New PromptPointOptions(vbCrLf & "请输入下一个点或[线宽(W)/颜色(C)/完成(O)]<O>")
        ' 允许使用基准点.
        optPtKey.UseBasePoint = True
        ' 设置基准点.
        optPtKey.BasePoint = ptPrevious
        ' 为点交互类添加关键字.
        optPtKey.Keywords.Add("W", "W", "W", False, True)
        optPtKey.Keywords.Add("C", "C", "C", False, True)
        optPtKey.Keywords.Add("O", "O", "O", False, True)
        ' 设置默认的关键字.
        optPtKey.Keywords.Default = "O"
        ' 返回用户提示类.
        Dim resKey As PromptPointResult = ed.GetPoint(optPtKey)
        ' 用户按下ESC键，退出.
        If resKey.Status = PromptStatus.Cancel Then Return
        ' 声明下一个输入点.
        Dim ptNext As Point3d
        If resKey.Status = PromptStatus.Keyword Then
            ' 如果用户输入的是关键字集合对象中的关键字……
            Select Case resKey.StringResult
                Case Is = "W"
                    width = getWidth()
                Case Is = "C"
                    colorIndex = getcolorindex()
                Case Else
                    Using trans As Transaction = db.TransactionManager.StartTransaction
                        Try
                            trans.GetObject(polyEntId, OpenMode.ForWrite)
                            ' 程序结束前，调整整体线宽和颜色.
                            polyEnt.Color = Color.FromColorIndex(ColorMethod.ByColor, colorIndex)
                            polyEnt.ConstantWidth = width
                        Catch
                            ' 此处无需操作.
                        End Try
                        trans.Commit()
                    End Using
                    Return
            End Select
            GoTo nextPoint
        Else
            ' 得到户输入的下一点.
            ptNext = resKey.Value
            If index = 2 Then
                ' 提取三维点的X、Y坐标值，转化为二维点.
                Dim pt1 As New Point2d(ptPrevious(0), ptPrevious(1))
                Dim pt2 As New Point2d(ptNext(0), ptNext(1))
                ' 给多段线添加顶点，设置线宽.
                polyEnt.AddVertexAt(0, pt1, 0, width, width)
                polyEnt.AddVertexAt(1, pt2, 0, width, width)
                ' 设置多段线的颜色.
                polyEnt.Color = Color.FromColorIndex(ColorMethod.ByColor, colorIndex)
                ' 将多段线添加到图形数据库并返回一个ObjectId(在绘图窗口动态显示多段线).
                polyEntId = AppendEntity(polyEnt)
            Else
                Using trans As Transaction = db.TransactionManager.StartTransaction
                    ' 打开多段线.
                    trans.GetObject(polyEntId, OpenMode.ForWrite)
                    ' 继续添加多段线的顶点.
                    Dim ptCurrent As New Point2d(ptNext(0), ptNext(1))
                    polyEnt.AddVertexAt(index - 1, ptCurrent, 0, width, width)
                    ' 重新设置多段线的颜色和线宽.
                    polyEnt.Color = Color.FromColorIndex(ColorMethod.ByColor, colorIndex)
                    polyEnt.ConstantWidth = width
                    trans.Commit()
                End Using
            End If
            index = index + 1
        End If
        ptPrevious = ptNext
        GoTo nextPoint
    End Sub

    ' 得到用户输入线宽的函数.
    Public Function getWidth() As Double
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 定义一个整数的用户交互类. 
        Dim optDou As New PromptDoubleOptions(vbCrLf & "请输入线宽")
        ' 不允许输入负数.
        optDou.AllowNegative = False
        ' 设置默认值.
        optDou.DefaultValue = 0
        Dim resDou As PromptDoubleResult = ed.GetDouble(optDou)
        If resDou.Status = PromptStatus.OK Then
            ' 得到用户输入的线宽.
            Dim width As Double = resDou.Value
            Return width
        Else
            Return 0
        End If
    End Function

    ' 得到用户输入颜色索引值的函数.
    Public Function getcolorindex() As Integer
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 定义一个整数的用户交互类. 
        Dim optInt As New PromptIntegerOptions(vbCrLf & "请输入颜色索引值(0～256)")
        ' 设置默认值.
        optInt.DefaultValue = 0
        ' 返回一个整数提示类.
        Dim resInt As PromptIntegerResult = ed.GetInteger(optInt)
        If resInt.Status = PromptStatus.OK Then
            ' 得到用户输入的颜色索引值.
            Dim colorIndex As Integer = resInt.Value
            If colorIndex > 256 Or colorIndex < 0 Then
                Return 0
            Else
                Return colorIndex
            End If
        Else
            Return 0
        End If
    End Function

    ' 将图形对象加入到模型空间的函数.
    Public Shared Function AppendEntity(ByVal ent As Entity) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim entId As ObjectId
        Using trans As Transaction = db.TransactionManager.StartTransaction
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            entId = btr.AppendEntity(ent)
            trans.AddNewlyCreatedDBObject(ent, True)
            trans.Commit()
        End Using
        Return entId
    End Function
End Class
