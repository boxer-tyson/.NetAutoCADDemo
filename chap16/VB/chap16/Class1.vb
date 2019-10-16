Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class Class1
    ' 创建直线的命令.
    <CommandMethod("FirstLine")> Public Sub TestLine()
        ' 得到当前文档的数据库对象
        Dim db As Database = HostApplicationServices.WorkingDatabase
        ' 定义直线对象的起点
        Dim pt1 As New Point3d(30, 40, 0)
        ' 定义直线对象的终点
        Dim pt2 As New Point3d(80, 60, 0)
        ' 在内存中创建一个直线对象
        Dim lineEnt As New Line(pt1, pt2)
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 以读方式打开块表.
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            ' 以写方式打开模型空间块表记录.
            Dim btr As BlockTableRecord = trans.GetObject(bt.Item(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            ' 将图形对象的信息添加到块表记录中.
            btr.AppendEntity(lineEnt)
            ' 把直线添加到事务处理中.
            trans.AddNewlyCreatedDBObject(lineEnt, True)
            ' 提交事务处理.
            trans.Commit()
        End Using
    End Sub

    ' 创建直线的命令.
    <CommandMethod("netLine")> Public Sub CreateLine()
        Dim lineId As ObjectId = ModelSpace.AddLine(New Point3d(20, 10, 0), New Point3d(90, 50, 0))
    End Sub

    ' 创建圆的命令.
    <CommandMethod("netCircle")> Public Sub CreateCircle()
        Dim circleId As ObjectId = ModelSpace.AddCircle(New Point3d(20, 10, 0), 50)
    End Sub

    ' 创建圆的命令.
    <CommandMethod("netCircle3P")> Public Sub CreateCircle3P()
        Dim circle3pId As ObjectId = ModelSpace.AddCircle(New Point2d(0, 0), New Point2d(0, 30), New Point2d(20, 15))
    End Sub

    ' 创建圆弧的命令.
    <CommandMethod("netArc")> Public Sub CreateArc()
        Dim arcId As ObjectId = ModelSpace.AddArc(New Point3d(20, 10, 0), 20, ModelSpace.Rad2Ang(60), ModelSpace.Rad2Ang(180))
    End Sub

    ' 创建椭圆的命令.
    <CommandMethod("netEllipse")> Public Sub CreateEllipse()
        Dim ellipseId As ObjectId = ModelSpace.AddEllipse(New Point3d(20, 10, 0), New Vector3d(30, 20, 0), 0.5)
    End Sub

    ' 创建样条曲线的命令.
    <CommandMethod("netSpline")> Public Sub CreateSpline()
        Dim pt(3) As Point3d
        pt(0) = New Point3d(0, 0, 0)
        pt(1) = New Point3d(10, 0, 0)
        pt(2) = New Point3d(30, 20, 0)
        pt(3) = New Point3d(60, 50, 0)
        Dim pts As New Point3dCollection(pt)
        Dim splineId As ObjectId = ModelSpace.AddSpline(pts)
    End Sub

    ' 创建二维优化多段线的命令.
    <CommandMethod("netPline")> Public Sub CreatePline()
        Dim pt(3) As Point2d
        pt(0) = New Point2d(0, 0)
        pt(1) = New Point2d(10, 0)
        pt(2) = New Point2d(30, 20)
        pt(3) = New Point2d(-20, 50)
        Dim pts As New Point2dCollection(pt)
        Dim plineId As ObjectId = ModelSpace.AddPline(pts, 0)
    End Sub

    ' 创建三维多段线的命令.
    <CommandMethod("net3dPoly")> Public Sub Create3dPoly()
        Dim pt(3) As Point3d
        pt(0) = New Point3d(0, 0, 0)
        pt(1) = New Point3d(10, 0, 50)
        pt(2) = New Point3d(30, 20, 60)
        pt(3) = New Point3d(-30, 50, 70)
        Dim pts As New Point3dCollection(pt)
        Dim poly3dId As ObjectId = ModelSpace.Add3dPoly(pts)
    End Sub

    ' 创建单行文字的命令.
    <CommandMethod("netText")> Public Sub CreateText()
        Dim textStr As String = "%%u" & "单行文字ABC123" & "%%u"
        Dim textId As ObjectId = ModelSpace.AddText(New Point3d(20, 10, 0), textStr, 5, 0)
    End Sub

    ' 创建多行文字的命令.
    <CommandMethod("netMtext")> Public Sub CreateMtext()
        Dim mtextStr As String = MText.UnderlineOn & "多行" & MText.UnderlineOff & MText.OverlineOn & "文字" & MText.OverlineOff
        Dim mtextId As ObjectId = ModelSpace.AddMtext(New Point3d(60, 30, 0), mtextStr, 5, 0)
    End Sub

    ' 创建图案填充的命令.
    <CommandMethod("netHatch1")> Public Sub CreateHatch1()
        ' 创建填充边界.
        Dim loopId1 As ObjectId = ModelSpace.AddLine(New Point3d(100, 0, 0), New Point3d(0, 0, 0))
        Dim loopId2 As ObjectId = ModelSpace.AddLine(New Point3d(100, 0, 0), New Point3d(80, 60, 0))
        Dim loopId3 As ObjectId = ModelSpace.AddLine(New Point3d(80, 60, 0), New Point3d(0, 0, 0))
        Dim loopId4 As ObjectId = ModelSpace.AddCircle(New Point3d(150, 50, 0), 40)

        ' 定义两个ObjectId集合.
        Dim loops1 As New ObjectIdCollection
        loops1.Add(loopId1)
        loops1.Add(loopId2)
        loops1.Add(loopId3)
        Dim loops2 As New ObjectIdCollection
        loops2.Add(loopId4)

        ' 定义一个ObjectId集合数组.
        Dim loops(1) As ObjectIdCollection
        loops.SetValue(loops1, 0)
        loops.SetValue(loops2, 1)

        ' 实施填充.
        Dim hatchId As ObjectId = ModelSpace.AddHatch(loops, 0, "ANGLE", ModelSpace.Rad2Ang(30), 2)
    End Sub

    ' 创建渐变色填充的命令.
    <CommandMethod("netHatch2")> Public Sub CreateHatch2()
        ' 创建填充边界.
        Dim loopId1 As ObjectId = ModelSpace.AddLine(New Point3d(100, 0, 0), New Point3d(0, 0, 0))
        Dim loopId2 As ObjectId = ModelSpace.AddLine(New Point3d(100, 0, 0), New Point3d(80, 60, 0))
        Dim loopId3 As ObjectId = ModelSpace.AddLine(New Point3d(80, 60, 0), New Point3d(0, 0, 0))
        Dim loopId4 As ObjectId = ModelSpace.AddCircle(New Point3d(150, 50, 0), 40)

        ' 定义两个ObjectId集合.
        Dim loops1 As New ObjectIdCollection
        loops1.Add(loopId1)
        loops1.Add(loopId2)
        loops1.Add(loopId3)
        Dim loops2 As New ObjectIdCollection
        loops2.Add(loopId4)

        ' 定义一个ObjectId集合数组.
        Dim loops(1) As ObjectIdCollection
        loops.SetValue(loops1, 0)
        loops.SetValue(loops2, 1)

        ' 实施填充.
        Dim c1 As Color = Color.FromRgb(200, 200, 100)
        Dim c2 As Color = Color.FromRgb(250, 20, 10)
        Dim hatchId As ObjectId = ModelSpace.AddHatch(loops, 0, c1, c2, "LINEAR", ModelSpace.Rad2Ang(30))
    End Sub

    ' 创建表格的命令.
    <CommandMethod("netTable")> Public Sub CreateTable()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim tableEnt As New Table()
        ' 插入列.
        tableEnt.InsertColumns(0, 12, 1)
        tableEnt.InsertColumns(1, 40, 1)
        tableEnt.InsertColumns(2, 40, 1)
        tableEnt.InsertColumns(3, 40, 1)
        tableEnt.InsertColumns(4, 16, 1)
        tableEnt.InsertColumns(5, 30, 1)
        ' 插入行.
        tableEnt.InsertRows(0, 8, 10)
        ' 添加文字.
        tableEnt.SetTextString(0, 0, "序号")
        tableEnt.SetTextString(0, 1, "标准号")
        tableEnt.SetTextString(0, 2, "名称")
        TableEnt.SetTextString(0, 3, "材料")
        tableEnt.SetTextString(0, 4, "数量")
        tableEnt.SetTextString(0, 5, "备注")
        tableEnt.SetTextString(1, 0, "1")
        tableEnt.SetTextString(1, 1, "GB000")
        tableEnt.SetTextString(1, 2, "螺母M12X50")
        tableEnt.SetTextString(1, 3, "SUS303")
        tableEnt.SetTextString(1, 4, "12")
        ' 确定表格位置.
        tableEnt.Position = New Point3d(180, 80, 0)
        ModelSpace.AppendEntity(tableEnt)
    End Sub

    ' 创建面域的命令.
    <CommandMethod("netRegion1")> Public Sub CreateRegion1()
        Dim loopId1 As ObjectId = ModelSpace.AddLine(New Point3d(100, 0, 0), New Point3d(0, 0, 0))
        Dim loopId2 As ObjectId = ModelSpace.AddLine(New Point3d(100, 0, 0), New Point3d(80, 60, 0))
        Dim loopId3 As ObjectId = ModelSpace.AddLine(New Point3d(80, 60, 0), New Point3d(0, 0, 0))
        Dim ent1 As DBObject
        Dim ent2 As DBObject
        Dim ent3 As DBObject

        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using ta As Transaction = db.TransactionManager.StartTransaction()
            ent1 = ta.GetObject(loopId1, OpenMode.ForWrite)
            ent2 = ta.GetObject(loopId2, OpenMode.ForWrite)
            ent3 = ta.GetObject(loopId3, OpenMode.ForWrite)
            ta.Commit()
        End Using
        Dim objIds As New DBObjectCollection
        objIds.Add(ent1)
        objIds.Add(ent2)
        objIds.Add(ent3)
        Dim regionId As ObjectIdCollection = ModelSpace.AddRegion(objIds)
    End Sub

    ' 创建面域的命令.
    <CommandMethod("netRegion2")> Public Sub CreateRegion2()
        ' 在内存中创建面域的边界对象.
        Dim ent1 As New Line(New Point3d(100, 0, 0), New Point3d(0, 0, 0))
        Dim ent2 As New Line(New Point3d(100, 0, 0), New Point3d(80, 60, 0))
        Dim ent3 As New Line(New Point3d(80, 60, 0), New Point3d(0, 0, 0))
        Dim ent4 As New Circle(New Point3d(200, 50, 0), Vector3d.ZAxis, 60)

        ' 边界对象添加到对象集合.
        Dim ents As New DBObjectCollection
        ents.Add(ent1)
        ents.Add(ent2)
        ents.Add(ent3)
        ents.Add(ent4)

        ' 创建面域并加入到图形数据库.
        Dim regionIds As ObjectIdCollection = ModelSpace.AddRegion(ents)
    End Sub

    ' 创建长方体的命令.
    <CommandMethod("netBox")> Public Sub CreateBox()
        Dim boxId As ObjectId = ModelSpace.AddBox(New Point3d(300, 200, 100), 600, 400, 300)
    End Sub

    ' 创建圆柱体的命令.
    <CommandMethod("netCylinder")> Public Sub CreateCylinder()
        Dim cylinderId As ObjectId = ModelSpace.AddCylinder(New Point3d(300, 200, 100), 600, 400)
    End Sub

    ' 创建圆锥体的命令.
    <CommandMethod("netCone")> Public Sub CreateCone()
        Dim coneId As ObjectId = ModelSpace.AddCone(New Point3d(300, 200, 100), 600, 400)
    End Sub

    ' 创建球体的命令.
    <CommandMethod("netSphere")> Public Sub CreateSphere()
        Dim SphereId As ObjectId = ModelSpace.AddSphere(New Point3d(300, 200, 100), 600)
    End Sub

    ' 创建圆环体的命令.
    <CommandMethod("netTorus")> Public Sub CreateTorus()
        Dim torusId As ObjectId = ModelSpace.AddTorus(New Point3d(300, 200, 100), 600, 400)
    End Sub

    ' 创建楔体的命令.
    <CommandMethod("netWedge")> Public Sub CreateWedge()
        Dim wedgeId As ObjectId = ModelSpace.AddWedge(New Point3d(300, 200, 100), 600, 400, 200)
    End Sub

    ' 创建拉伸体的命令.
    <CommandMethod("netExt1")> Public Sub CreateExtrudedSolid()
        ' 在内存中创建拉伸截面对象.
        Dim ent As New Circle(New Point3d(200, 100, 0), Vector3d.ZAxis, 100)
        ' 截面对象添加到对象集合.
        Dim ents As New DBObjectCollection
        ents.Add(ent)
        ' 在内存中创建面域对象集合.
        Dim regions As DBObjectCollection = Region.CreateFromCurves(ents)
        ' 实施拉伸，并将拉伸体添加到图形数据库.
        Dim extrudedSolidId As ObjectId = ModelSpace.AddExtrudedSolid(regions(0), 500, 0)
    End Sub

    ' 创建拉伸体的命令.
    <CommandMethod("netExt2")> Public Sub CreateExtrudeAlongPath()
        ' 在内存中创建拉伸截面对象.
        Dim ent As New Circle(New Point3d(200, 0, 0), Vector3d.ZAxis, 100)
        ' 截面对象添加到对象集合.
        Dim ents As New DBObjectCollection
        ents.Add(ent)
        ' 在内存中创建面域对象集合.
        Dim regions As DBObjectCollection = Region.CreateFromCurves(ents)
        ' 在内存中创建拉伸路径对象.
        Dim pathEnt As New Arc(New Point3d(500, 0, 0), New Vector3d(0, 1, 0), 300, 0, Math.PI)
        ' 实施拉伸，并将拉伸体添加到图形数据库.
        Dim extrudeAlongPathId As ObjectId = ModelSpace.AddExtrudedSolid(regions(0), pathEnt, 0)
    End Sub

    ' 创建旋转体的命令.
    <CommandMethod("netRevolved")> Public Sub CreateRevolvedSolid()
        ' 在内存中创建旋转截面对象.
        Dim ent As New Circle(New Point3d(200, 0, 0), Vector3d.ZAxis, 100)
        ' 截面对象添加到对象集合.
        Dim ents As New DBObjectCollection
        ents.Add(ent)
        ' 在内存中创建面域对象集合.
        Dim regions As DBObjectCollection = Region.CreateFromCurves(ents)
        ' 实施旋转，并将旋转体添加到图形数据库.
        Dim revolvedSolidId As ObjectId = ModelSpace.AddRevolvedSolid(regions(0), New Point3d(300, 200, 100), New Point3d(600, 400, 200), 2 * Math.PI)
    End Sub

    ' 布尔示例的命令.
    <CommandMethod("netBool")> Public Sub CreateBoolSolid()
        ' 布尔示例-------------------------------------------
        ' 在内存中创建旋转截面对象.
        Dim ent1, ent2 As New Solid3d
        ent1.CreateBox(100, 60, 40)
        ent2.CreateFrustum(90, 20, 20, 20)
        ' 差集操作.
        ent1.BooleanOperation(BooleanOperationType.BoolSubtract, ent2)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        ModelSpace.AppendEntity(ent1)
    End Sub

    ' 创建标注的命令.
    <CommandMethod("netDim")> Public Sub CreateDimension()
        ' 创建要标注的图形---------------------------------------------
        ModelSpace.AddLine(New Point3d(30, 20, 0), New Point3d(120, 20, 0))
        ModelSpace.AddLine(New Point3d(120, 20, 0), New Point3d(120, 40, 0))
        ModelSpace.AddLine(New Point3d(120, 40, 0), New Point3d(90, 80, 0))
        ModelSpace.AddLine(New Point3d(90, 80, 0), New Point3d(30, 80, 0))
        ModelSpace.AddArc(New Point3d(30, 50, 0), 30, ModelSpace.Rad2Ang(90), ModelSpace.Rad2Ang(270))
        ModelSpace.AddCircle(New Point3d(30, 50, 0), 15)
        ModelSpace.AddCircle(New Point3d(70, 50, 0), 10)

        ' 得到当前标注样式---------------------------------------------
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim curDimstyle As ObjectId = db.Dimstyle

        '(水平)转角标注-----------------------------------------------
        ModelSpace.AddDimRotated(0, New Point3d(30, 20, 0), New Point3d(120, 20, 0), New Point3d(55, 10, 0))
        ' (垂直)转角标注-----------------------------------------------
        ModelSpace.AddDimRotated(ModelSpace.Rad2Ang(90), New Point3d(120, 20, 0), New Point3d(120, 40, 0), New Point3d(130, 30, 0))

        ' 对齐标注、尺寸替代-------------------------------------------
        ModelSpace.AddDimAligned(New Point3d(120, 40, 0), New Point3d(90, 80, 0), New Point3d(113, 66, 0), "50%%p0.2", curDimstyle)

        ' 半径标注-----------------------------------------------------
        Dim ptCen As New Point3d(30, 50, 0)
        Dim p2 As Point3d = ModelSpace.PolarPoint(ptCen, ModelSpace.Rad2Ang(30), 15)
        ModelSpace.AddDimRadial(ptCen, p2, 10)

        ' 直径标注-----------------------------------------------------
        Dim dcen As New Point3d(70, 50, 0)
        Dim ptChord1 As Point3d = ModelSpace.PolarPoint(dcen, ModelSpace.Rad2Ang(45), 10)
        Dim ptChord2 As Point3d = ModelSpace.PolarPoint(dcen, ModelSpace.Rad2Ang(-135), 10)
        ModelSpace.AddDimDiametric(ptChord1, ptChord2, 0)

        ' 角度标注-----------------------------------------------------
        Dim angPtCen As New Point3d(120, 20, 0)
        Dim p5 As Point3d = ModelSpace.PolarPoint(angPtCen, ModelSpace.Rad2Ang(135), 10)
        ModelSpace.AddDimLineAngular(angPtCen, New Point3d(30, 20, 0), New Point3d(120, 40, 0), p5)

        ' 弧长标注-----------------------------------------------------
        ModelSpace.AddDimArc(New Point3d(30, 50, 0), New Point3d(30, 20, 0), New Point3d(30, 80, 0), New Point3d(-10, 50, 0))

        ' 坐标标注-----------------------------------------------------
        ModelSpace.AddDimOrdinate(New Point3d(70, 50, 0), New Point3d(70, 30, 0), New Point3d(90, 50, 0))

        ' 引线标注-----------------------------------------------------
        Dim pts As New Point3dCollection
        pts.Add(New Point3d(90, 70, 0))
        pts.Add(New Point3d(110, 80, 0))
        pts.Add(New Point3d(120, 80, 0))
        ModelSpace.AddLeader(pts, False)
        ' 添加引线标注的文字.
        ModelSpace.AddMtext(New Point3d(120, 80, 0), "{\L引线标注示例\l}", curDimstyle, AttachmentPoint.BottomLeft, 2.5, 0)

        ' 尺寸公差标注--------------------------------------------------
        ModelSpace.AddDimRotated(0, New Point3d(30, 80, 0), New Point3d(90, 80, 0), New Point3d(30, 90, 0), "60{\H0.7x;\S+0.026^-0.025;}", curDimstyle)

        ' 形位公差标注--------------------------------------------------
        Dim dimText As String = "{\fgdt;r}" & "%%v" & "{\fgdt;n0.03}" & "%%v" & "B"
        ModelSpace.AddTolerance(dimText, New Point3d(80, 100, 0), New Vector3d(0, 0, 1), New Vector3d(1, 0, 0))
        ' 为形位公差标注添加引线.
        Dim ptss As New Point3dCollection
        ptss.Add(New Point3d(70, 80, 0))
        ptss.Add(New Point3d(70, 100, 0))
        ptss.Add(New Point3d(80, 100, 0))
        ModelSpace.AddLeader(ptss, False)
    End Sub
End Class
