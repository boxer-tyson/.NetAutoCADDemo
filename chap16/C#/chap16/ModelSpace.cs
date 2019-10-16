using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;

namespace chap16
{
    class ModelSpace
    {
        // 由两点创建直线的函数.
        public static ObjectId AddLine(Point3d pt1, Point3d pt2)
        {
            Line ent = new Line(pt1, pt2);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由圆心和半径创建圆的函数.
        public static ObjectId AddCircle(Point3d cenPt, double radius)
        {
            Circle ent = new Circle(cenPt, Vector3d.ZAxis, radius);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由给定圆上三点创建圆的函数.
        public static ObjectId AddCircle(Point2d pt1, Point2d pt2, Point2d pt3)
        {
            const double pi = Math.PI;
            Vector2d va = pt1.GetVectorTo(pt2);
            Vector2d vb = pt1.GetVectorTo(pt3);
            if (va.GetAngleTo(vb) == 0 | va.GetAngleTo(vb) == pi)
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
            else
            {
                CircularArc2d geoArc = new CircularArc2d(pt1, pt2, pt3);
                Point3d cenPt = new Point3d(geoArc.Center.X, geoArc.Center.Y, 0);
                double radius = geoArc.Radius;
                Circle ent = new Circle(cenPt, Vector3d.ZAxis, radius);
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
        }

        // 由圆心、半径、起始角度和终止角度创建圆弧的函数.
        public static ObjectId AddArc(Point3d cenPt, double radius, double startAng, double endAng)
        {
            Arc ent = new Arc(cenPt, radius, startAng, endAng);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        //由椭圆中心、半长轴方向矢量和短长轴半径比创建椭圆的函数.
        public static ObjectId AddEllipse(Point3d cenPt, Vector3d majorAxis, double radiusRatio)
        {
            Ellipse ent = new Ellipse(cenPt, Vector3d.ZAxis, majorAxis, radiusRatio, 0, 2 * Math.PI);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由三维点集合创建样条曲线的函数.
        public static ObjectId AddSpline(Point3dCollection pts)
        {
            Spline ent = new Spline(pts, 4, 0);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由二维点集合和线宽创建二维优化多段线的函数.
        public static ObjectId AddPline(Point2dCollection pts, double width)
        {
            try
            {
                int n = pts.Count;
                Polyline ent = new Polyline();
                for (int i = 0; i < n; i++)
                    ent.AddVertexAt(i, pts[i], 0, width, width);
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由三维点集合创建三维多段线的函数.
        public static ObjectId Add3dPoly(Point3dCollection pts)
        {
            try
            {
                Polyline3d ent = new Polyline3d(Poly3dType.SimplePoly, pts, false);
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由插入点、文字内容、文字高度和倾斜角度创建单行文字的函数.
        public static ObjectId AddText(Point3d position, string textString, double height, double oblique)
        {
            try
            {
                DBText ent = new DBText();
                ent.Position = position;
                ent.TextString = textString;
                ent.Height = height;
                ent.Oblique = oblique;
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由插入点、文字内容、文字样式、文字高度、倾斜角度和旋转角度创建单行文字的函数.
        public static ObjectId AddText(Point3d position, string textString, ObjectId style, double height, double oblique, double rotation)
        {
            try
            {
                DBText ent = new DBText();
                ent.Position = position;
                ent.TextString = textString;
                ent.Height = height;
                ent.Oblique = oblique;
                ent.Rotation = rotation;
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由插入点、文字内容、文字高度、文本框宽度创建多行文字的函数.
        public static ObjectId AddMtext(Point3d location, string textString, double height, double width)
        {
            try
            {
                MText ent = new MText();
                ent.Location = location;
                ent.Contents = textString;
                ent.TextHeight = height;
                ent.Width = width;
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由插入点、文字内容、文字样式、对齐方式、文字高度、文字宽度创建多行文字的函数.
        public static ObjectId AddMtext(Point3d location, string textString, ObjectId style, AttachmentPoint attachmentPoint, double height, double width)
        {
            try
            {
                MText ent = new MText();
                ent.Location = location;
                ent.Contents = textString;
                ent.Attachment = attachmentPoint;
                ent.TextHeight = height;
                ent.Width = width;
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由边界对象集合数组、图案填充类型、填充图案名称、填充角度和填充比例创建图案填充的函数.
        // partType:0为预定义图案；1为用户定义图案；2为自定义图案.
        public static ObjectId AddHatch(ObjectIdCollection[] objIds, HatchPatternType patType, string patName, double patternAngle, double patternScale)
        {
            try
            {
                Hatch ent = new Hatch();
                ent.HatchObjectType = HatchObjectType.HatchObject;
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    ObjectId entId = btr.AppendEntity(ent);
                    trans.AddNewlyCreatedDBObject(ent, true);
                    ent.PatternAngle = patternAngle;
                    ent.PatternScale = patternScale;
                    ent.SetHatchPattern(patType, patName);
                    ent.Associative = true;
                    for (int i = 0; i < objIds.Length; i++)
                        ent.InsertLoopAt(i, HatchLoopTypes.Default, objIds[i]);
                    trans.Commit();
                    return entId;
                }
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }
        // 由边界对象集合数组、渐变色填充类型、渐变填充的起始颜色、渐变填充的终止颜色、渐变填充图案名称和填充角度创建渐变色填充的函数.
        // gradientType: 0为预定义图案；1为用户定义图案.
        // gradientName: "LINEAR"(直线形）, "CYLINDER"(圆柱形), "INVCYLINDER"(反转圆柱形), "SPHERICAL"(球形), "HEMISPHERICAL"(半球形), "CURVED"(曲线形), "INVSPHERICAL"(反转球形), "INVHEMISPHERICAL"(反转半球形), "INVCURVED"(反转曲线形)
        public static ObjectId AddHatch(ObjectIdCollection[] objIds, GradientPatternType gradientType, Color hColor1, Color hColor2, string gradientName, double gradientAngle)
        {
            try
            {
                Hatch ent = new Hatch();
                ent.HatchObjectType = HatchObjectType.GradientObject;
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    ObjectId entId = btr.AppendEntity(ent);
                    trans.AddNewlyCreatedDBObject(ent, true);
                    GradientColor gColor0 = new GradientColor(hColor1, 0);
                    GradientColor gColor1 = new GradientColor(hColor2, 1);
                    GradientColor[] gColor = new GradientColor[2] { gColor0, gColor1 };
                    ent.SetGradientColors(gColor);
                    ent.SetGradient(gradientType, gradientName);
                    ent.GradientAngle = gradientAngle;
                    ent.Associative = true;
                    for (int i = 0; i < objIds.Length; i++)
                        ent.InsertLoopAt(i, HatchLoopTypes.Default, objIds[i]);
                    trans.Commit();
                    return entId;
                }
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由图形对象集合创建面域的函数.
        public static ObjectIdCollection AddRegion(DBObjectCollection ents)
        {
            try
            {
                DBObjectCollection regions = Region.CreateFromCurves(ents);
                ObjectIdCollection entIds = new ObjectIdCollection();
                for (int i = 0; i < regions.Count; i++)
                {
                    ObjectId entId = AppendEntity((Entity)regions[i]);
                    entIds.Add(entId);
                }
                return entIds;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                ObjectIdCollection nullIds = new ObjectIdCollection();
                nullIds.Add(nullId);
                return nullIds;
            }
        }

        // 由图形对象ObjectId集合创建面域的函数.
        public static ObjectIdCollection AddRegion(ObjectIdCollection ids)
        {
            try
            {
                Database db = HostApplicationServices.WorkingDatabase;
                Entity ent;
                DBObjectCollection ents = new DBObjectCollection();
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        ent = (Entity)trans.GetObject(ids[i], OpenMode.ForWrite);
                        ents.Add(ent);
                    }
                }
                DBObjectCollection regions = Region.CreateFromCurves(ents);
                ObjectIdCollection entIds = new ObjectIdCollection();
                for (int i = 0; i < regions.Count; i++)
                {
                    ObjectId entId = AppendEntity((Entity)regions[i]);
                    entIds.Add(entId);
                }
                return entIds;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                ObjectIdCollection nullIds = new ObjectIdCollection();
                nullIds.Add(nullId);
                return nullIds;
            }
        }

        // 由中心点、长度、宽度和高度创建长方体的函数.
        public static ObjectId AddBox(Point3d cenPt, double lengthAlongX, double lengthAlongY, double lengthAlongZ)
        {
            Solid3d ent = new Solid3d();
            ent.CreateBox(lengthAlongX, lengthAlongY, lengthAlongZ);
            Matrix3d mt = Matrix3d.Displacement(cenPt - Point3d.Origin);
            ent.TransformBy(mt);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由中心点、半径和高度创建圆柱体的函数.
        public static ObjectId AddCylinder(Point3d cenPt, double radius, double height)
        {
            Solid3d ent = new Solid3d();
            ent.CreateFrustum(height, radius, radius, radius);
            Matrix3d mt = Matrix3d.Displacement(cenPt - Point3d.Origin);
            ent.TransformBy(mt);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由中心点、半径和高度创建圆锥体的函数.
        public static ObjectId AddCone(Point3d cenPt, double radius, double height)
        {
            Solid3d ent = new Solid3d();
            ent.CreateFrustum(height, radius, radius, 0);
            Matrix3d mt = Matrix3d.Displacement(cenPt - Point3d.Origin);
            ent.TransformBy(mt);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由中心点和半径创建球体的函数.
        public static ObjectId AddSphere(Point3d cenPt, double radius)
        {
            Solid3d ent = new Solid3d();
            ent.CreateSphere(radius);
            Matrix3d mt = Matrix3d.Displacement(cenPt - Point3d.Origin);
            ent.TransformBy(mt);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由中心点、圆环半径和圆管半径创建圆环体的函数.
        public static ObjectId AddTorus(Point3d cenPt, double majorRadius, double minorRadius)
        {
            Solid3d ent = new Solid3d();
            ent.CreateTorus(majorRadius, minorRadius);
            Matrix3d mt = Matrix3d.Displacement(cenPt - Point3d.Origin);
            ent.TransformBy(mt);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由中心点、长度、宽度和高度创建楔体的函数.
        public static ObjectId AddWedge(Point3d cenPt, double lengthAlongX, double lengthAlongY, double lengthAlongZ)
        {
            Solid3d ent = new Solid3d();
            ent.CreateWedge(lengthAlongX, lengthAlongY, lengthAlongZ);
            Matrix3d mt = Matrix3d.Displacement(cenPt - Point3d.Origin);
            ent.TransformBy(mt);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由截面面域、拉伸高度和拉伸角度创建拉伸体的函数.
        public static ObjectId AddExtrudedSolid(Region region, double height, double taperAngle)
        {
            try
            {
                Solid3d ent = new Solid3d();
                ent.Extrude(region, height, taperAngle);
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由截面面域、拉伸路径曲线和拉伸角度创建拉伸体的函数.
        public static ObjectId AddExtrudedSolid(Region region, Curve path, double taperAngle)
        {
            try
            {
                Solid3d ent = new Solid3d();
                ent.ExtrudeAlongPath(region, path, taperAngle);
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由截面面域、旋转轴起点、旋转轴终点和旋转角度创建旋转体的函数.
        public static ObjectId AddRevolvedSolid(Region region, Point3d axisPt1, Point3d axisPt2, double angle)
        {
            try
            {
                Solid3d ent = new Solid3d();
                ent.Revolve(region, axisPt1, axisPt2 - axisPt1, angle);
                ObjectId entId = AppendEntity(ent);
                return entId;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                return nullId;
            }
        }

        // 由布尔操作类型和两个三维实体创建旋转体的函数.  
        public static void AddBoolSolid(BooleanOperationType boolType, ObjectId solid3dId1, ObjectId solid3dId2)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    Entity ent1 = (Entity)trans.GetObject(solid3dId1, OpenMode.ForWrite);
                    Entity ent2 = (Entity)trans.GetObject(solid3dId2, OpenMode.ForWrite);
                    if (ent1 is Solid3d & ent2 is Solid3d)
                    {
                        Solid3d solid3dEnt1 = (Solid3d)ent1;
                        Solid3d solid3dEnt2 = (Solid3d)ent2;
                        solid3dEnt1.BooleanOperation(boolType, solid3dEnt2);
                        ent2.Erase();
                    }
                    if (ent1 is Region & ent2 is Region)
                    {
                        Region regionEnt1 = (Region)ent1;
                        Region regionEnt2 = (Region)ent2;
                        regionEnt1.BooleanOperation(boolType, regionEnt2);
                        ent2.Erase();
                    }
                }
                catch
                {
                    // 此处无需要操作.
                }
                trans.Commit();
            }
        }

        // 由尺寸线旋转角度、两条尺寸界线原点和尺寸文本位置创建转角标注的函数.
        public static ObjectId AddDimRotated(double angle, Point3d pt1, Point3d pt2, Point3d ptText)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;

            Point2d p2dt1 = new Point2d(pt1.X, pt1.Y);
            Point2d p2dt2 = new Point2d(pt2.X, pt2.Y);
            Vector2d vec = p2dt2 - p2dt1;

            string text = Math.Round(Math.Abs(vec.Length*Math.Cos(vec.Angle-angle)),db.Dimdec).ToString();
            RotatedDimension ent = new RotatedDimension(angle, pt1, pt2, ptText, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由尺寸线旋转角度、两条尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建转角标注的函数.
        public static ObjectId AddDimRotated(double angle, Point3d pt1, Point3d pt2, Point3d ptText, string text, ObjectId style)
        {
            RotatedDimension ent = new RotatedDimension(angle, pt1, pt2, ptText, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由两条尺寸界线原点和尺寸文本位置创建对齐标注的函数.
        public static ObjectId AddDimAligned(Point3d pt1, Point3d pt2, Point3d ptText)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            string text = Math.Round(pt1.DistanceTo(pt2),db.Dimdec).ToString();
            AlignedDimension ent = new AlignedDimension(pt1, pt2, ptText, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由两条尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建对齐标注的函数.
        public static ObjectId AddDimAligned(Point3d pt1, Point3d pt2, Point3d ptText, string text, ObjectId style)
        {
            AlignedDimension ent = new AlignedDimension(pt1, pt2, ptText, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由圆心、引线附着点和引线长度创建半径标注的函数.
        public static ObjectId AddDimRadial(Point3d cenPt, Point3d ptChord, double leaderLength)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            string text = "R" + Math.Round(cenPt.DistanceTo(ptChord),db.Dimdec).ToString();
            RadialDimension ent = new RadialDimension(cenPt, ptChord, leaderLength, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由圆心、引线附着点、引线长度、尺寸文本和标注样式创建半径标注的函数.
        public static ObjectId AddDimRadial(Point3d cenPt, Point3d ptChord, double leaderLength, string text, ObjectId style)
        {
            RadialDimension ent = new RadialDimension(cenPt, ptChord, leaderLength, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由两个引线附着点和引线长度创建直径标注的函数.
        public static ObjectId AddDimDiametric(Point3d ptChord1, Point3d ptChord2, double leaderLength)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            string text = "%%c" + Math.Round(ptChord1.DistanceTo(ptChord2),db.Dimdec).ToString();
            DiametricDimension ent = new DiametricDimension(ptChord1, ptChord2, leaderLength, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由两个引线附着点、引线长度、尺寸文本和标注样式创建直径标注的函数.
        public static ObjectId AddDimDiametric(Point3d ptChord1, Point3d ptChord2, double leaderLength, string text, ObjectId style)
        {
            DiametricDimension ent = new DiametricDimension(ptChord1, ptChord2, leaderLength, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由两条直线的起点和终点以及尺寸文本位置创建角度标注的函数.
        public static ObjectId AddDimLineAngular(Point3d line1StartPt, Point3d line1EndPt, Point3d line2StartPt, Point3d line2EndPt, Point3d arcPt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            Vector3d vec1 = line1EndPt - line1StartPt;
            Vector3d vec2 = line2EndPt - line2StartPt;
            double ang = vec1.GetAngleTo(vec2) * 180 / Math.PI;
            string text = Math.Round(ang,db.Dimadec).ToString() + "%%d";
            LineAngularDimension2 ent = new LineAngularDimension2(line1StartPt, line1EndPt, line2StartPt, line2EndPt, arcPt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由两条直线的起点和终点以及尺寸文本位置、尺寸文本、标注样式创建角度标注的函数.
        public static ObjectId AddDimLineAngular(Point3d line1StartPt, Point3d line1EndPt, Point3d line2StartPt, Point3d line2EndPt, Point3d arcPt, string text, ObjectId style)
        {
            LineAngularDimension2 ent = new LineAngularDimension2(line1StartPt, line1EndPt, line2StartPt, line2EndPt, arcPt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由角度顶点、两个尺寸界线原点和尺寸文本位置创建角度标注的函数.
        public static ObjectId AddDimLineAngular(Point3d cenPt, Point3d line1Pt, Point3d line2Pt, Point3d arcPt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            Vector3d vec1 = line1Pt - cenPt;
            Vector3d vec2 = line2Pt - cenPt;
            double ang = vec1.GetAngleTo(vec2) * 180 / Math.PI;
            string text = Math.Round(ang, db.Dimadec).ToString() + "%%d";
            Point3AngularDimension ent = new Point3AngularDimension(cenPt, line1Pt, line2Pt, arcPt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由角顶点、两个尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建角度标注的函数.
        public static ObjectId AddDimLineAngular(Point3d cenPt, Point3d line1Pt, Point3d line2Pt, Point3d arcPt, string text, ObjectId style)
        {
            Point3AngularDimension ent = new Point3AngularDimension(cenPt, line1Pt, line2Pt, arcPt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由圆心、两条尺寸界线原点和尺寸文本位置创建弧长标注的函数.
        public static ObjectId AddDimArc(Point3d cenPt, Point3d pt1, Point3d pt2, Point3d arcPt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            Vector3d vec1 = cenPt.GetVectorTo(pt1);
            Vector3d vec2 = cenPt.GetVectorTo(pt2);
            double ang = vec1.GetAngleTo(vec2);
            double radius = cenPt.DistanceTo(pt1);
            double arcLength = ang * radius;
            string text = (Math.Round(arcLength, db.Dimdec)).ToString();
            ArcDimension ent = new ArcDimension(cenPt, pt1, pt2, arcPt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由圆心、两条尺寸界线原点、尺寸文本位置、尺寸文本和标注样式创建弧长标注的函数.
        public static ObjectId AddDimArc(Point3d cenPt, Point3d pt1, Point3d pt2, Point3d arcPt, string text, ObjectId style)
        {
            ArcDimension ent = new ArcDimension(cenPt, pt1, pt2, arcPt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由标注类型（是否是X坐标）、标注箭头的起始位置和标注箭头的终止位置创建坐标标注的函数.
        public static ObjectId AddDimOrdinate(bool useXAxis, Point3d ordPt, Point3d pt)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            string text;
            if (useXAxis == true)
                text = Math.Round(ordPt.X,db.Dimdec).ToString();
            else
                text = Math.Round(ordPt.Y,db.Dimdec).ToString();
            OrdinateDimension ent = new OrdinateDimension(useXAxis, ordPt, pt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由标注类型（是否是X坐标）、标注箭头的起始位置、标注箭头的终止位置、尺寸文本和标注样式创建坐标标注的函数.
        public static ObjectId AddDimOrdinate(bool useXAxis, Point3d ordPt, Point3d pt, string text, ObjectId style)
        {
            OrdinateDimension ent = new OrdinateDimension(useXAxis, ordPt, pt, text, style);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由标注箭头的起始位置、标注箭头的X终止位置和标注箭头的Y终止位置创建坐标标注的函数(X坐标和Y坐标).
        public static ObjectIdCollection AddDimOrdinate(Point3d ordPt, Point3d ptX, Point3d ptY)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId style = db.Dimstyle;
            string textX = Math.Round(ordPt.X,db.Dimdec).ToString();
            string textY = Math.Round(ordPt.Y,db.Dimdec).ToString();
            OrdinateDimension entX = new OrdinateDimension(true, ordPt, ptX, textX, style);
            OrdinateDimension entY = new OrdinateDimension(false, ordPt, ptY, textY, style);
            ObjectId objIdX = AppendEntity(entX);
            ObjectId objIdY = AppendEntity(entY);
            ObjectIdCollection entIds = new ObjectIdCollection();
            entIds.Add(objIdX);
            entIds.Add(objIdY);
            return entIds;
        }

        // 由标注箭头的起始位置、标注箭头的X终止位置、标注箭头的Y终止位置、X坐标标注文字、Y坐标标注文字和标注样式创建坐标标注的函数.
        public static ObjectIdCollection AddDimOrdinate(Point3d ordPt, Point3d ptX, Point3d ptY, string textX, string textY, ObjectId style)
        {
            try
            {
                OrdinateDimension entX = new OrdinateDimension(true, ordPt, ptX, textX, style);
                OrdinateDimension entY = new OrdinateDimension(false, ordPt, ptX, textX, style);
                ObjectId objIdX = AppendEntity(entX);
                ObjectId objIdY = AppendEntity(entY);
                ObjectIdCollection entIds = new ObjectIdCollection();
                entIds.Add(objIdX);
                entIds.Add(objIdY);
                return entIds;
            }
            catch
            {
                ObjectId nullId = ObjectId.Null;
                ObjectIdCollection nullIds = new ObjectIdCollection();
                nullIds.Add(nullId);
                return nullIds;
            }
        }

        // 由三维点集合创建引线标注的函数.
        public static ObjectId AddLeader(Point3dCollection pts, bool splBool)
        {
            Leader ent = new Leader();
            ent.IsSplined = splBool;
            for (int i = 0; i < pts.Count; i++)
            {
                ent.AppendVertex(pts[i]);
                ent.SetVertexAt(i, pts[i]);
            }
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 由形位公差替代文字、插入点、法线向量和形位公差x方向向量创建形位公差标注的函数.
        public static ObjectId AddTolerance(string codes, Point3d inPt, Vector3d norVec, Vector3d xVec)
        {
            FeatureControlFrame ent = new FeatureControlFrame(codes, inPt, norVec, xVec);
            ObjectId entId = AppendEntity(ent);
            return entId;
        }

        // 度化弧度的函数.
        public static double Rad2Ang(double angle)
        {
            double rad = angle * Math.PI / 180;
            return rad;
        }

        // 获取与给定点指定角度和距离的点.
        public static Point3d PolarPoint(Point3d basePt, double angle, double distance)
        {
            double[] pt = new double[3];
            pt[0] = basePt[0] + distance * Math.Cos(angle);
            pt[1] = basePt[1] + distance * Math.Sin(angle);
            pt[2] = basePt[2];
            Point3d point = new Point3d(pt[0], pt[1], pt[2]);
            return point;
        }

        // 将图形对象加入到模型空间的函数.
        public static ObjectId AppendEntity(Entity ent)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId entId;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                entId = btr.AppendEntity(ent);
                trans.AddNewlyCreatedDBObject(ent, true);
                trans.Commit();
            }
            return entId;
        }
    }
}
