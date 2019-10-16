Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.GraphicsInterface
Imports Autodesk.AutoCAD.Runtime

Public Class EllipseJig
    ' 从EntityJig类继承.
    Inherits EntityJig
    ' 声明全局变量.
    Private mCenterPt, mMajorPt As Point3d
    Private mNormal, mMajorAxis As Vector3d
    Private mPromptCounter As Integer
    Private mRadiusRatio, radiusRatio As Double
    Private startAng, endAng, ang1, ang2 As Double
    ' 派生类的构造函数.
    Public Sub New(ByVal center As Point3d, ByVal vec As Vector3d)
        MyBase.New(New Ellipse())
        mCenterPt = center
        mNormal = vec
    End Sub

    ' Sampler函数用于检测用户的输入.
    Protected Overrides Function Sampler(ByVal prompts As JigPrompts) As SamplerStatus
        Select Case mPromptCounter
            Case 0
                ' 定义一个点拖动交互类.
                Dim optJigPoint As New JigPromptPointOptions(vbCrLf & "请指定椭圆弧轴上一点")
                ' 设置拖拽的光标类型.
                optJigPoint.Cursor = CursorType.RubberBand
                ' 设置拖动光标基点.
                optJigPoint.BasePoint = mCenterPt
                optJigPoint.UseBasePoint = True
                ' 用AcquirePoint函数得到用户输入的点.
                Dim resJigPoint As PromptPointResult = prompts.AcquirePoint(optJigPoint)
                Dim curPt As Point3d = resJigPoint.Value
                If curPt <> mMajorPt Then
                    ' 保存当前点. 
                    mMajorPt = curPt
                Else
                    Return SamplerStatus.NoChange
                End If
            Case 1
                ' 定义一个距离拖动交互类.
                Dim optJigDis As New JigPromptDistanceOptions(vbCrLf & "请指定另一条半轴的长度")
                ' 设置对拖拽的约束:禁止输入零和负值.
                optJigDis.UserInputControls = UserInputControls.NoZeroResponseAccepted Or UserInputControls.NoNegativeResponseAccepted
                ' 设置拖拽的光标类型.
                optJigDis.Cursor = CursorType.RubberBand
                ' 设置拖动光标基点.
                optJigDis.BasePoint = mCenterPt
                optJigDis.UseBasePoint = True
                ' 用AcquireDistance函数得到用户输入的距离值.
                Dim resJigDis As PromptDoubleResult = prompts.AcquireDistance(optJigDis)
                Dim mRadiusRatioTemp As Double = resJigDis.Value
                If mRadiusRatioTemp <> mRadiusRatio Then
                    ' 保存当前距离值.
                    mRadiusRatio = mRadiusRatioTemp
                Else
                    Return SamplerStatus.NoChange
                End If
            Case 2
                ' 设置椭圆弧0度基准角.
                Dim baseAng As Double
                Dim mMajorAxis2d As New Vector2d(mMajorAxis.X, mMajorAxis.Y)
                If radiusRatio < 1 Then
                    baseAng = mMajorAxis2d.Angle
                Else
                    baseAng = mMajorAxis2d.Angle + 0.5 * Math.PI
                End If
                ' 修改系统变量“ANGBASE”.
                Application.SetSystemVariable("ANGBASE", baseAng)

                ' 定义一个角度拖动交互类.
                Dim optJigAngle1 As New JigPromptAngleOptions(vbCrLf & "请指定椭圆弧的起始角度")
                ' 设置拖拽的光标类型.
                optJigAngle1.Cursor = CursorType.RubberBand
                ' 设置拖动光标基点.
                optJigAngle1.BasePoint = mCenterPt
                optJigAngle1.UseBasePoint = True
                ' 用AcquireAngle函数得到用户输入的角度值.
                Dim resJigAngle1 As PromptDoubleResult = prompts.AcquireAngle(optJigAngle1)
                ang1 = resJigAngle1.Value
                If startAng <> ang1 Then
                    ' 保存当前角度值.
                    startAng = ang1
                Else
                    Return SamplerStatus.NoChange
                End If
            Case 3
                ' 定义一个角度拖动交互类.
                Dim optJigAngle2 As New JigPromptAngleOptions(vbCrLf & "请指定椭圆弧的终止角度")
                ' 设置拖拽的光标类型.
                optJigAngle2.Cursor = CursorType.RubberBand
                ' 设置拖动光标基点.
                optJigAngle2.BasePoint = mCenterPt
                optJigAngle2.UseBasePoint = True
                ' 用AcquireAngle函数得到用户输入的角度值.
                Dim resJigAngle2 As PromptDoubleResult = prompts.AcquireAngle(optJigAngle2)
                ang2 = resJigAngle2.Value
                If endAng <> ang2 Then
                    ' 保存当前点角度值.
                    endAng = ang2
                Else
                    Return SamplerStatus.NoChange
                End If
        End Select
    End Function

    ' Update函数用于刷新屏幕上显示的图形.
    Protected Overrides Function Update() As Boolean
        Select Case mPromptCounter
            Case 0
                ' 第一次拖拽时，椭圆的半径比为1，屏幕上显示的是一个圆.
                radiusRatio = 1
                mMajorAxis = mMajorPt - mCenterPt
                startAng = 0
                endAng = 2 * Math.PI
            Case 1
                ' 第二次拖拽时，修改了椭圆的半径比，屏幕上显示的是一个完整椭圆.
                radiusRatio = mRadiusRatio / mMajorAxis.Length
            Case 2
                ' 第三次拖拽时，修改了椭圆的起初角度，屏幕上显示的是一个终止角度为360度的椭圆弧.
                startAng = ang1
            Case 3
                ' 第四次拖拽时，修改了椭圆的终止角度，屏幕上显示的是一个最终的椭圆弧.
                endAng = ang2
        End Select

        Try
            If radiusRatio < 1 Then
                ' 更新椭圆的参数.
                CType(Entity, Ellipse).Set(mCenterPt, mNormal, mMajorAxis, radiusRatio, startAng, endAng)
            Else
                ' 如另一半轴长度超过椭圆弧长轴方向矢量的长度，则要重新定义椭圆弧长轴方向矢量的方向和长度.
                Dim mMajorAxis2 As Vector3d = mMajorAxis.RotateBy(0.5 * Math.PI, Vector3d.ZAxis).DivideBy(1 / radiusRatio)
                ' 更新椭圆的参数.
                CType(Entity, Ellipse).Set(mCenterPt, mNormal, mMajorAxis2, 1 / radiusRatio, startAng, endAng)
            End If
        Catch
            '此处不需要处理.
        End Try
        Return True
    End Function

    ' GetEntity函数用于得到派生类的实体.
    Public Function GetEntity() As Entity
        Return Entity
    End Function

    ' setPromptCounter过程用于控制不同的拖拽.
    Public Sub setPromptCounter(ByVal i As Integer)
        mPromptCounter = i
    End Sub
End Class

Public Class EntityJig_Ellipse
    <CommandMethod("JigEllipse")> Public Sub CreateJigEllipse()
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
        ' 备份系统变量“ANGBASE”.
        Dim oldAngBase As Object = Application.GetSystemVariable("ANGBASE")
        ' 普通的点交互操作.
        Dim optPoint As New PromptPointOptions(vbCrLf & "请指定椭圆弧的圆心:")
        Dim resPoint As PromptPointResult = ed.GetPoint(optPoint)
        If resPoint.Status <> PromptStatus.OK Then Return
        ' 定义一个EntityJig派生类的实例.
        Dim myJig As New EllipseJig(resPoint.Value, Vector3d.ZAxis)
        ' 第一次拖拽.
        myJig.setPromptCounter(0)
        Dim resJig As PromptResult = ed.Drag(myJig)
        If resJig.Status = PromptStatus.OK Then
            ' 第二次拖拽.
            myJig.setPromptCounter(1)
            resJig = ed.Drag(myJig)
            If resJig.Status = PromptStatus.OK Then
                ' 第三次拖拽.
                myJig.setPromptCounter(2)
                resJig = ed.Drag(myJig)
                If resJig.Status = PromptStatus.OK Then
                    ' 第四次拖拽.
                    myJig.setPromptCounter(3)
                    resJig = ed.Drag(myJig)
                    If resJig.Status = PromptStatus.OK Then
                        Using trans As Transaction = db.TransactionManager.StartTransaction()
                            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
                            Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
                            ' 将EntityJig派生类实体加入到图形数据库中.
                            btr.AppendEntity(myJig.GetEntity())
                            trans.AddNewlyCreatedDBObject(myJig.GetEntity(), True)
                            trans.Commit()
                        End Using
                    End If
                End If
            End If
        End If
        ' 还原系统变量“ANGBASE”.
        Application.SetSystemVariable("ANGBASE", oldAngBase)
    End Sub
End Class
