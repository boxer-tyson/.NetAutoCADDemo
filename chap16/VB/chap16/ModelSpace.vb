Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Public Class ModelSpace
    ' 由两点创建直线的函数.
    Public Shared Function AddLine(ByVal startPt As Point3d, ByVal endPt As Point3d) As ObjectId
        ' 在内存中创建一个直线对象
        Dim ent As New Line(startPt, endPt)
        ' 调用AppendEntity函数，将直线加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由圆心和半径创建圆的函数.
    Public Shared Function AddCircle(ByVal cenPt As Point3d, ByVal radius As Double) As ObjectId
        ' 在内存中创建一个圆对象
        Dim ent As New Circle(cenPt, Vector3d.ZAxis, radius)
        ' 调用AppendEntity函数，将圆加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由给定圆上三点创建圆的函数.
    Public Shared Function AddCircle(ByVal pt1 As Point2d, ByVal pt2 As Point2d, ByVal pt3 As Point2d) As ObjectId
        ' 先判断三点是否共线-----------------------------
        ' 得到pt1点指向pt2点的矢量.
        Dim va As Vector2d = Pt1.GetVectorTo(pt2)
        ' 得到pt1点指向pt3点的矢量.
        Dim vb As Vector2d = Pt1.GetVectorTo(pt3)

        ' 如两矢量夹角为0或180度（π弧度),则三点共线.
        If va.GetAngleTo(vb) = 0 Or va.GetAngleTo(vb) = Math.PI Then
            ' 三点共线，创建失败,返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        Else
            ' 创建一个几何类的圆弧对象.
            Dim geoArc As New CircularArc2d(pt1, pt2, pt3)
            ' 得到圆弧的圆心. 
            Dim cenPt As New Point3d(geoArc.Center.X, geoArc.Center.Y, 0)
            ' 得到圆弧的半径.
            Dim radius As Double = geoArc.Radius
            ' 在内存中创建一个圆对象.
            Dim ent As New Circle(cenPt, Vector3d.ZAxis, radius)
            ' 调用AppendEntity函数，将圆加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        End If
    End Function

    ' 由圆心、半径、起始角度和终止角度创建圆弧的函数.
    Public Shared Function AddArc(ByVal cenPt As Point3d, ByVal radius As Double, ByVal startAng As Double, ByVal endAng As Double) As ObjectId
        ' 在内存中创建一个圆弧对象
        Dim ent As New Arc(cenPt, radius, startAng, endAng)
        ' 调用AppendEntity函数，将圆弧加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由椭圆中心、半长轴方向矢量和短长轴半径比创建椭圆的函数.
    Public Shared Function AddEllipse(ByVal cenPt As Point3d, ByVal majorAxis As Vector3d, ByVal radiusRatio As Double) As ObjectId
        ' 在内存中创建一个椭圆对象
        Dim ent As New Ellipse(cenPt, Vector3d.ZAxis, majorAxis, radiusRatio, 0, 2 * Math.PI)
        ' 调用AppendEntity函数，将椭圆加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由三维点集合创建样条曲线的函数.
    Public Shared Function AddSpline(ByVal pts As Point3dCollection) As ObjectId
        ' 在内存中创建样条曲线对象.
        Dim ent As New Spline(pts, 4, 0)
        ' 调用EntityToModelSpace函数，将样条曲线加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由二维点集合和线宽创建二维优化多段线的函数.
    Public Shared Function AddPline(ByVal pts As Point2dCollection, ByVal width As Double) As ObjectId
        Try
            ' 得到点集合的数量.
            Dim n As Integer = pts.Count
            ' 在内存中创建一个二维优化多段线对象.
            Dim ent As New Polyline(n)
            ' 向多段线添加顶点.
            For i As Integer = 0 To n - 1
                ent.AddVertexAt(i, pts.Item(i), 0, width, width)
            Next
            ' 调用EntityToModelSpace函数，将二维多段线加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由三维点集合创建三维多段线的函数.
    Public Shared Function Add3dPoly(ByVal pts As Point3dCollection) As ObjectId
        Try
            ' 在内存中创建一个未经拟合的标准三维多段线对象.
            Dim ent As New Polyline3d(Poly3dType.SimplePoly, pts, False)
            ' 调用EntityToModelSpace函数，将三维多段线加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由插入点、文字内容、文字高度和倾斜角度创建单行文字的函数.
    Public Shared Function AddText(ByVal position As Point3d, ByVal textString As String, ByVal height As Double, ByVal oblique As Double) As ObjectId
        Try
            ' 在内存中创建单行文字对象.
            Dim ent As New DBText()
            ' 设置文字插入点.
            ent.Position = position
            '设置文字内容.
            ent.TextString = textString
            '设置文字高度.
            ent.Height = height
            '设置文字倾斜角.
            ent.Oblique = oblique
            ' 调用EntityToModelSpace函数，将单行文字加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由插入点、文字内容、文字样式、文字高度、倾斜角度和旋转角度创建单行文字的函数.
    Public Shared Function AddText(ByVal position As Point3d, ByVal textString As String, ByVal style As ObjectId, ByVal height As Double, ByVal oblique As Double, ByVal rotation As Double) As ObjectId
        Try
            ' 在内存中创建单行文字对象.
            Dim ent As New DBText()
            ' 设置文字插入点.
            ent.Position = position
            '设置文字内容.
            ent.TextString = textString
            ' 设置文字样式.
            ent.TextStyle = style
            '设置文字高度.
            ent.Height = height
            '设置文字倾斜角.
            ent.Oblique = oblique
            ' 设置文字旋转角度.
            ent.Rotation = rotation
            ' 调用EntityToModelSpace函数，将单行文字加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由插入点、文字内容、文字高度、文本框宽度创建多行文字的函数.
    Public Shared Function AddMtext(ByVal location As Point3d, ByVal textString As String, ByVal height As Double, ByVal width As Double) As ObjectId
        Try
            ' 在内存中创建多行文字对象.
            Dim ent As New MText()
            '设置文字插入点.
            ent.Location = location
            '设置文字内容.
            ent.Contents = textString
            '设置文字高度.
            ent.TextHeight = height
            '设置文本框宽度.
            ent.Width = width
            ' 调用AppendEntity函数，将多行文字加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    '  由插入点、文字内容、文字样式、对齐方式、文字高度、文字宽度创建多行文字的函数.
    Public Shared Function AddMtext(ByVal location As Point3d, ByVal textString As String, ByVal style As ObjectId, ByVal attachmentPoint As AttachmentPoint, ByVal height As Double, ByVal width As Double) As ObjectId
        Try
            ' 在内存中创建多行文字对象.
            Dim ent As New MText()
            '设置文字插入点.
            ent.Location = location
            '设置文字内容.
            ent.Contents = textString
            '设置文字样式.
            ent.TextStyle = style
            '设置文本对齐方式.
            ent.Attachment = attachmentPoint
            '设置文字高度.
            ent.TextHeight = height
            '设置文本框宽度.
            ent.Width = width
            ' 调用AppendEntity函数，将多行文字加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由边界对象集合数组、图案填充类型、填充图案名称、填充角度和填充比例创建图案填充的函数.
    ' partType:0为预定义图案；1为用户定义图案；2为自定义图案.
    Public Shared Function AddHatch(ByVal objIds() As ObjectIdCollection, ByVal patType As Integer, ByVal patName As String, ByVal patternAngle As Double, ByVal patternScale As Double) As ObjectId
        Try
            ' 在内存中创建填充对象.
            Dim ent As New Hatch()
            ' 设置填充类型为图案填充.
            ent.HatchObjectType = HatchObjectType.HatchObject

            Dim entId As ObjectId
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Using trans As Transaction = db.TransactionManager.StartTransaction()
                Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
                Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
                entId = btr.AppendEntity(ent)
                trans.AddNewlyCreatedDBObject(ent, True)
                ' 设置填充角度.
                ent.PatternAngle = patternAngle
                ' 设置填充比例.
                ent.PatternScale = patternScale
                ' 设置填充图案.
                ent.SetHatchPattern(patType, patName)
                ' 设置填充的关联性.
                ent.Associative = True
                ' 设置填充边界.
                For i As Integer = 0 To objIds.Length - 1
                    ent.InsertLoopAt(i, HatchLoopTypes.Default, objIds(i))
                Next
                trans.Commit()
            End Using
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由边界对象集合数组、渐变色填充类型、渐变填充的起始颜色、渐变填充的终止颜色、渐变填充图案名称和填充角度创建渐变色填充的函数.
    ' gradientType: 0为预定义图案；1为用户定义图案.
    ' gradientName: "LINEAR"(直线形）, "CYLINDER"(圆柱形), "INVCYLINDER"(反转圆柱形), "SPHERICAL"(球形), "HEMISPHERICAL"(半球形), "CURVED"(曲线形), "INVSPHERICAL"(反转球形), "INVHEMISPHERICAL"(反转半球形), "INVCURVED"(反转曲线形)
    Public Shared Function AddHatch(ByVal objIds() As ObjectIdCollection, ByVal gradientType As Integer, ByVal hColor1 As Color, ByVal hColor2 As Color, ByVal gradientName As String, ByVal gradientAngle As Double) As ObjectId
        Try
            ' 在内存中创建填充对象.
            Dim ent As New Hatch()
            ' 设置填充类型为渐变色填充.
            ent.HatchObjectType = HatchObjectType.GradientObject

            Dim entId As ObjectId
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Using trans As Transaction = db.TransactionManager.StartTransaction()
                Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
                Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
                entId = btr.AppendEntity(ent)
                trans.AddNewlyCreatedDBObject(ent, True)
                ' 设置渐变色的起始和终止颜色.
                Dim gColor0 As New GradientColor(hColor1, 0)
                Dim gColor1 As New GradientColor(hColor2, 1)
                Dim gColor As GradientColor() = New GradientColor(1) {gColor0, gColor1}
                ent.SetGradientColors(gColor)
                ' 设置渐变色的图案.
                ent.SetGradient(gradientType, gradientName)
                ' 设置填充角度.
                ent.GradientAngle = gradientAngle
                ' 设置填充的关联性.
                ent.Associative = True
                ' 设置填充边界.
                For i As Integer = 0 To objIds.Length - 1
                    ent.InsertLoopAt(i, HatchLoopTypes.Default, objIds(i))
                Next
                trans.Commit()
            End Using
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由图形对象集合创建面域的函数.
    Public Shared Function AddRegion(ByVal ents As DBObjectCollection) As ObjectIdCollection
        Try
            ' 在内存中创建面域对象集合.
            Dim regions As DBObjectCollection = Region.CreateFromCurves(ents)
            Dim entIds As New ObjectIdCollection
            For i As Integer = 0 To Regions.Count - 1
                ' 调用AppendEntity函数，将面域加入到模型空间.
                Dim entId As ObjectId = AppendEntity(regions(i))
                entIds.Add(entId)
            Next
            Return entIds
        Catch
            ' 创建失败,则返回一个空的ObjectIdCollection.
            Dim nullId As ObjectId = ObjectId.Null
            Dim nullIds As New ObjectIdCollection
            nullIds.Add(nullId)
            Return nullIds
        End Try
    End Function

    ' 由图形对象ObjectId集合创建面域的函数.
    Public Shared Function AddRegion(ByVal ids As ObjectIdCollection) As ObjectIdCollection
        Try
            ' 在内存中创建面域对象集合.
            Dim db As Database = HostApplicationServices.WorkingDatabase
            Dim ent As Entity, ents As New DBObjectCollection
            Using trans As Transaction = db.TransactionManager.StartTransaction()
                For i As Integer = 0 To Ids.Count - 1
                    ent = trans.GetObject(ids(i), OpenMode.ForWrite)
                    ents.Add(ent)
                Next
            End Using

            Dim regions As DBObjectCollection = Region.CreateFromCurves(ents)
            Dim entIds As New ObjectIdCollection
            For i As Integer = 0 To Regions.Count - 1
                ' 调用AppendEntity函数，将面域加入到模型空间.
                Dim entId As ObjectId = AppendEntity(regions(i))
                entIds.Add(entId)
            Next
            Return entIds
        Catch
            ' 创建失败,则返回一个空的ObjectIdCollection.
            Dim nullId As ObjectId = ObjectId.Null
            Dim nullIds As New ObjectIdCollection
            nullIds.Add(nullId)
            Return nullIds
        End Try
    End Function

    ' 由中心点、长度、宽度和高度创建长方体的函数.
    Public Shared Function AddBox(ByVal cenPt As Point3d, ByVal lengthAlongX As Double, ByVal lengthAlongY As Double, ByVal lengthAlongZ As Double) As ObjectId
        ' 在内存中创建三维实体对象.
        Dim ent As New Solid3d
        ent.CreateBox(lengthAlongX, lengthAlongY, lengthAlongZ)
        Dim mt As Matrix3d = Matrix3d.Displacement(cenPt - Point3d.Origin)
        ent.TransformBy(mt)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由中心点、半径和高度创建圆柱体的函数.
    Public Shared Function AddCylinder(ByVal cenPt As Point3d, ByVal radius As Double, ByVal height As Double) As ObjectId
        Dim ent As New Solid3d
        ent.CreateFrustum(height, radius, radius, radius)
        Dim mt As Matrix3d = Matrix3d.Displacement(cenPt - Point3d.Origin)
        ent.TransformBy(mt)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由中心点、半径和高度创建圆锥体的函数.
    Public Shared Function AddCone(ByVal cenPt As Point3d, ByVal radius As Double, ByVal height As Double) As ObjectId
        Dim ent As New Solid3d
        ent.CreateFrustum(height, radius, radius, 0)
        Dim mt As Matrix3d = Matrix3d.Displacement(cenPt - Point3d.Origin)
        ent.TransformBy(mt)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由中心点和半径创建球体的函数.
    Public Shared Function AddSphere(ByVal cenPt As Point3d, ByVal radius As Double) As ObjectId
        Dim ent As New Solid3d
        ent.CreateSphere(radius)
        Dim mt As Matrix3d = Matrix3d.Displacement(cenPt - Point3d.Origin)
        ent.TransformBy(mt)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由中心点、圆环半径和圆管半径创建圆环体的函数.
    Public Shared Function AddTorus(ByVal cenPt As Point3d, ByVal majorRadius As Double, ByVal minorRadius As Double) As ObjectId
        Dim ent As New Solid3d
        ent.CreateTorus(majorRadius, minorRadius)
        Dim mt As Matrix3d = Matrix3d.Displacement(cenPt - Point3d.Origin)
        ent.TransformBy(mt)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由中心点、长度、宽度和高度创建楔体的函数.
    Public Shared Function AddWedge(ByVal cenPt As Point3d, ByVal lengthAlongX As Double, ByVal lengthAlongY As Double, ByVal lengthAlongZ As Double) As ObjectId
        Dim ent As New Solid3d
        ent.CreateWedge(lengthAlongX, lengthAlongY, lengthAlongZ)
        Dim mt As Matrix3d = Matrix3d.Displacement(cenPt - Point3d.Origin)
        ent.TransformBy(mt)
        ' 调用AppendEntity函数，将三维实体加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由截面面域、拉伸高度和拉伸角度创建拉伸体的函数.
    Public Shared Function AddExtrudedSolid(ByVal region As Region, ByVal height As Double, ByVal taperAngle As Double) As ObjectId
        Try
            ' 在内存中创建三维实体对象.
            Dim ent As New Solid3d
            ent.Extrude(region, height, taperAngle)
            ' 调用AppendEntity函数，将三维实体加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由截面面域、拉伸路径曲线和拉伸角度创建拉伸体的函数.
    Public Shared Function AddExtrudedSolid(ByVal region As Region, ByVal path As Curve, ByVal taperAngle As Double) As ObjectId
        Try
            ' 在内存中创建三维实体对象.
            Dim ent As New Solid3d
            ent.ExtrudeAlongPath(region, path, taperAngle)
            ' 调用AppendEntity函数，将三维实体加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由截面面域、旋转轴起点、旋转轴终点和旋转角度创建旋转体的函数.
    Public Shared Function AddRevolvedSolid(ByVal region As Region, ByVal axisPt1 As Point3d, ByVal axisPt2 As Point3d, ByVal angle As Double) As ObjectId
        Try
            ' 在内存中创建三维实体对象.
            Dim ent As New Solid3d
            ent.Revolve(region, axisPt1, axisPt2 - axisPt1, angle)
            ' 调用AppendEntity函数，将三维实体加入到模型空间.
            Dim entId As ObjectId = AppendEntity(ent)
            Return entId
        Catch
            ' 创建失败,则返回一个空的ObjectId.
            Dim nullId As ObjectId = ObjectId.Null
            Return nullId
        End Try
    End Function

    ' 由尺寸线旋转角度、两条尺寸界线原点和尺寸文本位置创建转角标注的函数.
    Public Shared Function AddDimRotated(ByVal angle As Double, ByVal pt1 As Point3d, ByVal pt2 As Point3d, ByVal ptText As Point3d) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        ' 得到当前标注样式.
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim p2dt1 As New Point2d(pt1.X, pt1.Y)
        Dim p2td2 As New Point2d(pt2.X, pt2.Y)
        Dim vec As Vector2d = p2td2 - p2dt1
        ' 按当前系统变量Dimdec值设置小数位数.
        Dim text As String = Math.Round(Math.Abs(vec.Length * Math.Cos(vec.Angle - angle)), db.Dimdec).ToString
        ' 在内存中创建转角标注对象.
        Dim ent As New RotatedDimension(angle, pt1, pt2, ptText, text, style)
        ' 调用AppendEntity函数，将转角标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由尺寸线旋转角度、两条尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建转角标注的函数.
    Public Shared Function AddDimRotated(ByVal ang As Double, ByVal pt1 As Point3d, ByVal pt2 As Point3d, ByVal ptText As Point3d, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建转角标注对象.
        Dim ent As New RotatedDimension(ang, pt1, pt2, ptText, text, style)
        ' 调用AppendEntity函数，将转角标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由两条尺寸界线原点和尺寸文本位置创建对齐标注的函数.
    Public Shared Function AddDimAligned(ByVal pt1 As Point3d, ByVal pt2 As Point3d, ByVal ptText As Point3d) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim text As String = Math.Round(pt1.DistanceTo(pt2), db.Dimdec).ToString
        ' 在内存中创建对齐标注对象.
        Dim ent As New AlignedDimension(pt1, pt2, ptText, text, style)
        ' 调用AppendEntity函数，将对齐标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由两条尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建对齐标注的函数.
    Public Shared Function AddDimAligned(ByVal pt1 As Point3d, ByVal pt2 As Point3d, ByVal ptText As Point3d, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建对齐标注对象.
        Dim ent As New AlignedDimension(pt1, pt2, ptText, text, style)
        ' 调用AppendEntity函数，将对齐标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由圆心、引线附着点和引线长度创建半径标注的函数.
    Public Shared Function AddDimRadial(ByVal cenPt As Point3d, ByVal ptChord As Point3d, ByVal leaderLength As Double) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim text As String = "R" & Math.Round(cenPt.DistanceTo(ptChord), db.Dimdec).ToString
        ' 在内存中创建半径标注对象.
        Dim ent As New RadialDimension(cenPt, ptChord, leaderLength, text, style)
        ' 调用AppendEntity函数，将半径标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由圆心、引线附着点、引线长度、尺寸文本和标注样式创建半径标注的函数.
    Public Shared Function AddDimRadial(ByVal cenPt As Point3d, ByVal ptChord As Point3d, ByVal leaderLength As Double, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建半径标注对象.
        Dim ent As New RadialDimension(cenPt, ptChord, leaderLength, text, style)
        ' 调用AppendEntity函数，将半径标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由两个引线附着点和引线长度创建直径标注的函数.
    Public Shared Function AddDimDiametric(ByVal ptChord1 As Point3d, ByVal ptChord2 As Point3d, ByVal leaderLength As Double) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim text As String = "%%c" & Math.Round(ptChord1.DistanceTo(ptChord2), db.Dimdec).ToString
        ' 在内存中创建直径标注对象.
        Dim ent As New DiametricDimension(ptChord1, ptChord2, leaderLength, text, style)
        ' 调用AppendEntity函数，将直径标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由两个引线附着点、引线长度、尺寸文本和标注样式创建直径标注的函数.
    Public Shared Function AddDimDiametric(ByVal ptChord1 As Point3d, ByVal ptChord2 As Point3d, ByVal leaderLength As Double, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建直径标注对象.
        Dim ent As New DiametricDimension(ptChord1, ptChord2, leaderLength, text, style)
        ' 调用AppendEntity函数，将直径标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由两条直线的起点和终点以及尺寸文本位置创建角度标注的函数.
    Public Shared Function AddDimLineAngular(ByVal line1StartPt As Point3d, ByVal line1EndPt As Point3d, ByVal line2StartPt As Point3d, ByVal line2EndPt As Point3d, ByVal arcPt As Point3d) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim vec1 As Vector3d = line1EndPt - line1StartPt
        Dim vec2 As Vector3d = line2EndPt - line2StartPt
        Dim ang As Double = vec1.GetAngleTo(vec2) * 180 / Math.PI
        ' 按当前系统变量Dimadec值设置角度小数位数.
        Dim text As String = Math.Round(ang, db.Dimadec).ToString & "%%d"
        ' 在内存中创建角度标注对象.
        Dim ent As New LineAngularDimension2(line1StartPt, line1EndPt, line2StartPt, line2EndPt, arcPt, text, style)
        ' 调用AppendEntity函数，将角度标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由两条直线的起点和终点以及尺寸文本位置、尺寸文本、标注样式创建角度标注的函数.
    Public Shared Function AddDimLineAngular(ByVal line1StartPt As Point3d, ByVal line1EndPt As Point3d, ByVal line2StartPt As Point3d, ByVal line2EndPt As Point3d, ByVal arcPt As Point3d, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建角度标注对象.
        Dim ent As New LineAngularDimension2(line1StartPt, line1EndPt, line2StartPt, line2EndPt, arcPt, text, style)
        ' 调用AppendEntity函数，将角度标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由角度顶点、两个尺寸界线原点和尺寸文本位置创建角度标注的函数.
    Public Shared Function AddDimLineAngular(ByVal cenPt As Point3d, ByVal line1Pt As Point3d, ByVal line2Pt As Point3d, ByVal arcPt As Point3d) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim vec1 As Vector3d = line1Pt - cenPt
        Dim vec2 As Vector3d = line2Pt - cenPt
        Dim ang As Double = vec1.GetAngleTo(vec2, vec1) * 180 / Math.PI
        Dim text As String = Math.Round(ang, db.Dimadec).ToString & "%%d"
        ' 在内存中创建角度标注对象.
        Dim ent As New Point3AngularDimension(cenPt, line1Pt, line2Pt, arcPt, text, style)
        ' 调用AppendEntity函数，将角度标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由角顶点、两个尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建角度标注的函数.
    Public Shared Function AddDimLineAngular(ByVal cenPt As Point3d, ByVal line1Pt As Point3d, ByVal line2Pt As Point3d, ByVal arcPt As Point3d, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建角度标注对象.
        Dim ent As New Point3AngularDimension(cenPt, line1Pt, line2Pt, arcPt, text, style)
        ' 调用AppendEntity函数，将角度标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由圆心、两条尺寸界线原点和尺寸文本位置创建弧长标注的函数.
    Public Shared Function AddDimArc(ByVal cenPt As Point3d, ByVal pt1 As Point3d, ByVal pt2 As Point3d, ByVal arcPt As Point3d) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim vec1 As Vector3d = cenPt.GetVectorTo(pt1)
        Dim vec2 As Vector3d = cenPt.GetVectorTo(pt2)
        Dim ang As Double = vec1.GetAngleTo(vec2)
        Dim radius As Double = cenPt.DistanceTo(pt1)
        Dim arcLength As Double = ang * radius
        Dim text As String = Math.Round(arcLength, db.Dimdec)
        ' 在内存中创建弧长标注对象.
        Dim ent As New ArcDimension(cenPt, pt1, pt2, arcPt, text, style)
        ' 调用AppendEntity函数，将弧长标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由圆心、两条尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建弧长标注的函数.
    Public Shared Function AddDimArc(ByVal cenPt As Point3d, ByVal pt1 As Point3d, ByVal pt2 As Point3d, ByVal arcPt As Point3d, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建弧长标注对象.
        Dim ent As New ArcDimension(cenPt, pt1, pt2, arcPt, text, style)
        ' 调用AppendEntity函数，将弧长标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由标注类型（是否是X坐标）、标注箭头的起始位置和标注箭头的终止位置创建坐标标注的函数.
    Public Shared Function AddDimOrdinate(ByVal useXAxis As Boolean, ByVal ordPt As Point3d, ByVal pt As Point3d) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本.
        Dim text As String
        If useXAxis = True Then
            text = Math.Round(ordPt.X, db.Dimdec).ToString
        Else
            text = Math.Round(ordPt.Y, db.Dimdec).ToString
        End If
        ' 在内存中创建坐标标注对象.
        Dim ent As New OrdinateDimension(useXAxis, ordPt, pt, text, style)
        ' 调用AppendEntity函数，将坐标标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由标注类型（是否是X坐标）、标注箭头的起始位置、标注箭头的终止位置、尺寸文本和标注样式创建坐标标注的函数.
    Public Shared Function AddDimOrdinate(ByVal useXAxis As Boolean, ByVal ordPt As Point3d, ByVal pt As Point3d, ByVal text As String, ByVal style As ObjectId) As ObjectId
        ' 在内存中创建坐标标注对象.
        Dim ent As New OrdinateDimension(useXAxis, ordPt, pt, text, style)
        ' 调用EntityToModelSpace函数，将坐标标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由标注箭头的起始位置、标注箭头的X终止位置和标注箭头的Y终止位置创建坐标标注的函数(X坐标和Y坐标).
    Public Shared Function AddDimOrdinate(ByVal ordPt As Point3d, ByVal ptX As Point3d, ByVal ptY As Point3d) As ObjectIdCollection
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim style As ObjectId = db.Dimstyle
        ' 计算默认的尺寸文本
        Dim textX As String = Math.Round(ordPt.X, db.Dimdec).ToString
        Dim textY As String = Math.Round(ordPt.Y, db.Dimdec).ToString
        ' 在内存中创建坐标标注对象.
        Dim entX As New OrdinateDimension(True, ordPt, ptX, textX, style)
        Dim entY As New OrdinateDimension(False, ordPt, ptY, textY, style)
        ' 调用EntityToModelSpace函数，将坐标标注加入到模型空间.
        Dim objIdX As ObjectId = AppendEntity(entX)
        Dim objIdY As ObjectId = AppendEntity(entY)
        ' 将ObjectId添加到ObjectId集合
        Dim entIds As New ObjectIdCollection
        entIds.Add(objIdX)
        entIds.Add(objIdY)
        Return entIds
    End Function

    ' 由标注箭头的起始位置、标注箭头的X终止位置、标注箭头的Y终止位置、X坐标标注文字、Y坐标标注文字和标注样式创建坐标标注的函数.
    Public Shared Function AddDimOrdinate(ByVal ordPt As Point3d, ByVal ptX As Point3d, ByVal ptY As Point3d, ByVal textX As String, ByVal textY As String, ByVal style As ObjectId) As ObjectIdCollection
        Try
            ' 在内存中创建坐标标注对象.
            Dim entX As New OrdinateDimension(True, ordPt, ptX, textX, style)
            Dim entY As New OrdinateDimension(False, ordPt, ptY, textY, style)
            ' 调用EntityToModelSpace函数，将坐标标注加入到模型空间.
            Dim objIdX As ObjectId = AppendEntity(entX)
            Dim objIdY As ObjectId = AppendEntity(entY)
            ' 将ObjectId添加到ObjectIdCollection
            Dim entIds As New ObjectIdCollection
            entIds.Add(objIdX)
            entIds.Add(objIdY)
            Return entIds
        Catch
            ' 创建失败,则返回一个空的ObjectIdCollection.
            Dim nullId As ObjectId = ObjectId.Null
            Dim nullIds As New ObjectIdCollection
            nullIds.Add(nullId)
            Return nullIds
        End Try
    End Function

    ' 由三维点集合创建引线标注的函数.
    Public Shared Function AddLeader(ByVal pts As Point3dCollection, ByVal splBool As Boolean) As ObjectId
        ' 在内存中创建形位公差标注对象.
        Dim ent As New Leader
        ' 引线是否为样条曲线.
        ent.IsSplined = splBool
        ' 为引线添加顶点.
        For i As Integer = 0 To pts.Count - 1
            ent.AppendVertex(pts.Item(i))
            ent.SetVertexAt(i, pts.Item(i))
        Next
        ' 调用EntityToModelSpace函数，将坐标标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 由形位公差替代文字、插入点、法线向量和形位公差x方向向量创建形位公差标注的函数.
    Public Shared Function AddTolerance(ByVal codes As String, ByVal inPt As Point3d, ByVal norVec As Vector3d, ByVal xVec As Vector3d) As ObjectId
        ' 在内存中创建形位公差标注对象.
        Dim ent As New FeatureControlFrame(codes, inPt, norVec, xVec)
        ' 调用EntityToModelSpace函数，将坐标标注加入到模型空间.
        Dim entId As ObjectId = AppendEntity(ent)
        Return entId
    End Function

    ' 度化弧度的函数.
    Public Shared Function Rad2Ang(ByVal angle As Double) As Double
        Rad2Ang = angle * Math.PI / 180
    End Function

    ' 获取与给定点指定角度和距离的点.
    Public Shared Function PolarPoint(ByVal basePt As Point3d, ByVal angle As Double, ByVal distance As Double) As Point3d
        Dim pt(2) As Double
        pt(0) = basePt(0) + distance * Math.Cos(angle)
        pt(1) = basePt(1) + distance * Math.Sin(angle)
        pt(2) = basePt(2)
        PolarPoint = New Point3d(pt(0), pt(1), pt(2))
    End Function

    ' 将图形对象加入到模型空间的函数.
    Public Shared Function AppendEntity(ByVal ent As Entity) As ObjectId
        ' 得到当前文档图形数据库.
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim entId As ObjectId
        Using trans As Transaction = db.TransactionManager.StartTransaction
            ' 以读方式打开块表.
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            ' 以写方式打开模型空间块表记录.
            Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            ' 将图形对象的信息添加到块表记录中,并返回ObjectId对象.
            entId = btr.AppendEntity(ent)
            ' 把图形对象添加到事务处理中.
            trans.AddNewlyCreatedDBObject(ent, True)
            ' 提交事务处理.
            trans.Commit()
        End Using
        Return entId
    End Function
End Class
