using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;

namespace chap16
{
    public class Class1
    {
        // 创建直线的命令.
        [CommandMethod("FirstLine")]
        public void testLine()
        {
            Line ent = new Line(new Point3d(30, 40, 0), new Point3d(80, 60, 0));
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction ta = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)ta.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)ta.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                btr.AppendEntity(ent);
                ta.AddNewlyCreatedDBObject(ent, true);
                ta.Commit();
            }
        }

        // 创建直线的命令.
        [CommandMethod("netLine")]
        public void CreateLine()
        {
            ObjectId entId = ModelSpace.AddLine(new Point3d(20, 10, 0), new Point3d(90, 50, 0));
        }

        // 创建圆的命令.
        [CommandMethod("netCircle")]
        public void CreateCircle()
        {
            ObjectId entId = ModelSpace.AddCircle(new Point3d(20, 10, 0), 50);
        }

        // 创建圆的命令.
        [CommandMethod("netCircle3P")]
        public void CreateCircle3P()
        {
            ObjectId circle3pId = ModelSpace.AddCircle(new Point2d(0, 0), new Point2d(0, 30), new Point2d(20, 15));
        }

        // 创建圆弧的命令.
        [CommandMethod("netArc")]
        public void CreateArc()
        {
            ObjectId arcId = ModelSpace.AddArc(new Point3d(20, 10, 0), 20, ModelSpace.Rad2Ang(60), ModelSpace.Rad2Ang(180));
        }

        // 创建椭圆的命令.
        [CommandMethod("netEllipse")]
        public void CreateEllipse()
        {
            ObjectId ellipseId = ModelSpace.AddEllipse(new Point3d(20, 10, 0), new Vector3d(30, 20, 0), 0.5);
        }

        // 创建样条曲线的命令.
        [CommandMethod("netSpline")]
        public void CreateSpline()
        {
            Point3d[] pt = new Point3d[4];
            pt[0] = new Point3d(0, 0, 0);
            pt[1] = new Point3d(10, 0, 0);
            pt[2] = new Point3d(30, 20, 0);
            pt[3] = new Point3d(60, 50, 0);
            Point3dCollection pts = new Point3dCollection(pt);
            ObjectId splineId = ModelSpace.AddSpline(pts);
        }

        // 创建二维优化多段线的命令.
        [CommandMethod("netPline")]
        public void CreatePline()
        {
            Point2d[] pt = new Point2d[4];
            pt[0] = new Point2d(0, 0);
            pt[1] = new Point2d(10, 0);
            pt[2] = new Point2d(30, 20);
            pt[3] = new Point2d(-20, 50);
            Point2dCollection pts = new Point2dCollection(pt);
            ObjectId plineId = ModelSpace.AddPline(pts, 0);
        }

        // 创建三维多段线的命令.
        [CommandMethod("net3dPoly")]
        public void Create3dPoly()
        {
            Point3d[] pt = new Point3d[4];
            pt[0] = new Point3d(0, 0, 0);
            pt[1] = new Point3d(10, 0, 50);
            pt[2] = new Point3d(30, 20, 60);
            pt[3] = new Point3d(-30, 50, 70);
            Point3dCollection pts = new Point3dCollection(pt);
            ObjectId poly3dId = ModelSpace.Add3dPoly(pts);
        }

        // 创建单行文字的命令.
        [CommandMethod("netText")]
        public void CreateText()
        {
            string textStr = "%%u" + "单行文字ABC123" + "%%u";
            ObjectId textId = ModelSpace.AddText(new Point3d(20, 10, 0), textStr, 5, 0);
        }

        // 创建多行文字的命令.
        [CommandMethod("netMtext")]
        public void CreateMtext()
        {
            string mtextStr = MText.UnderlineOn + "多行" + MText.UnderlineOff + MText.OverlineOn + "文字" + MText.OverlineOff;
            ObjectId mtextId = ModelSpace.AddMtext(new Point3d(60, 30, 0), mtextStr, 5, 0);
        }

        // 创建图案填充的命令.
        [CommandMethod("netHatch1")]
        public void CreateHatch1()
        {
            // 创建填充边界.
            ObjectId loopId1 = ModelSpace.AddLine(new Point3d(100, 0, 0), new Point3d(0, 0, 0));
            ObjectId loopId2 = ModelSpace.AddLine(new Point3d(100, 0, 0), new Point3d(80, 60, 0));
            ObjectId loopId3 = ModelSpace.AddLine(new Point3d(80, 60, 0), new Point3d(0, 0, 0));
            ObjectId loopId4 = ModelSpace.AddCircle(new Point3d(150, 50, 0), 40);

            // 定义两个ObjectId集合.
            ObjectIdCollection loops1 = new ObjectIdCollection();
            loops1.Add(loopId1);
            loops1.Add(loopId2);
            loops1.Add(loopId3);
            ObjectIdCollection loops2 = new ObjectIdCollection();
            loops2.Add(loopId4);

            // 定义一个ObjectId集合数组.
            ObjectIdCollection[] loops = new ObjectIdCollection[2];
            loops.SetValue(loops1, 0);
            loops.SetValue(loops2, 1);

            // 实施填充.
            ObjectId hatchId = ModelSpace.AddHatch(loops, 0, "ANGLE", ModelSpace.Rad2Ang(30), 2);
        }

        // 创建渐变色填充的命令.
        [CommandMethod("netHatch2")]
        public void CreateHatch2()
        {
            // 创建填充边界.
            ObjectId loopId1 = ModelSpace.AddLine(new Point3d(100, 0, 0), new Point3d(0, 0, 0));
            ObjectId loopId2 = ModelSpace.AddLine(new Point3d(100, 0, 0), new Point3d(80, 60, 0));
            ObjectId loopId3 = ModelSpace.AddLine(new Point3d(80, 60, 0), new Point3d(0, 0, 0));
            ObjectId loopId4 = ModelSpace.AddCircle(new Point3d(150, 50, 0), 40);

            // 定义两个ObjectId集合.
            ObjectIdCollection loops1 = new ObjectIdCollection();
            loops1.Add(loopId1);
            loops1.Add(loopId2);
            loops1.Add(loopId3);
            ObjectIdCollection loops2 = new ObjectIdCollection();
            loops2.Add(loopId4);

            // 定义一个ObjectId集合数组.
            ObjectIdCollection[] loops = new ObjectIdCollection[2];
            loops.SetValue(loops1, 0);
            loops.SetValue(loops2, 1);

            // 实施填充.
            Color c1 = Color.FromRgb(200, 200, 100);
            Color c2 = Color.FromRgb(250, 20, 10);
            ObjectId hatchId = ModelSpace.AddHatch(loops, GradientPatternType.PreDefinedGradient, c1, c2, "LINEAR", ModelSpace.Rad2Ang(30));
        }


        // 创建表格的命令.
        [CommandMethod("netTable")]
        public void CreateTable()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Table tableEnt = new Table();
            // 插入列.
            tableEnt.InsertColumns(0, 12, 1);
            tableEnt.InsertColumns(1, 40, 1);
            tableEnt.InsertColumns(2, 40, 1);
            tableEnt.InsertColumns(3, 40, 1);
            tableEnt.InsertColumns(4, 16, 1);
            tableEnt.InsertColumns(5, 30, 1);
            // 插入行.
            tableEnt.InsertRows(0, 8, 10);
            // 添加文字.
            tableEnt.SetTextString(0, 0, "序号");
            tableEnt.SetTextString(0, 1, "标准号");
            tableEnt.SetTextString(0, 2, "名称");
            tableEnt.SetTextString(0, 3, "材料");
            tableEnt.SetTextString(0, 4, "数量");
            tableEnt.SetTextString(0, 5, "备注");
            tableEnt.SetTextString(1, 0, "1");
            tableEnt.SetTextString(1, 1, "GB000");
            tableEnt.SetTextString(1, 2, "螺母M12X50");
            tableEnt.SetTextString(1, 3, "SUS303");
            tableEnt.SetTextString(1, 4, "12");
            tableEnt.Position = new Point3d(180, 80, 0);
            ModelSpace.AppendEntity(tableEnt);
        }

        // 创建面域的命令.
        [CommandMethod("netRegion1")]
        public void CreateRegion1()
        {
            ObjectId loopId1 = ModelSpace.AddLine(new Point3d(100, 0, 0), new Point3d(0, 0, 0));
            ObjectId loopId2 = ModelSpace.AddLine(new Point3d(100, 0, 0), new Point3d(80, 60, 0));
            ObjectId loopId3 = ModelSpace.AddLine(new Point3d(80, 60, 0), new Point3d(0, 0, 0));
            DBObject ent1;
            DBObject ent2;
            DBObject ent3;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                ent1 = (Entity)trans.GetObject(loopId1, OpenMode.ForWrite);
                ent2 = (Entity)trans.GetObject(loopId2, OpenMode.ForWrite);
                ent3 = (Entity)trans.GetObject(loopId3, OpenMode.ForWrite);
                trans.Commit();
            }

            DBObjectCollection objIds = new DBObjectCollection();
            objIds.Add(ent1);
            objIds.Add(ent2);
            objIds.Add(ent3);
            ObjectIdCollection regionId = ModelSpace.AddRegion(objIds);
        }

        // 创建面域的命令.
        [CommandMethod("netRegion2")]
        public void CreateRegion2()
        {
            // 在内存中创建面域的边界对象.
            Line ent1 = new Line(new Point3d(100, 0, 0), new Point3d(0, 0, 0));
            Line ent2 = new Line(new Point3d(100, 0, 0), new Point3d(80, 60, 0));
            Line ent3 = new Line(new Point3d(80, 60, 0), new Point3d(0, 0, 0));
            Circle ent4 = new Circle(new Point3d(200, 50, 0), Vector3d.ZAxis, 60);

            // 边界对象添加到对象集合.
            DBObjectCollection ents = new DBObjectCollection();
            ents.Add(ent1);
            ents.Add(ent2);
            ents.Add(ent3);
            ents.Add(ent4);

            // 创建面域并加入到图形数据库.
            ObjectIdCollection regionIds = ModelSpace.AddRegion(ents);
        }

        // 创建长方体的命令.
        [CommandMethod("netBox")]
        public void CreateBox()
        {
            ObjectId boxId = ModelSpace.AddBox(new Point3d(300, 200, 100), 600, 400, 300);
        }

        // 创建圆柱体的命令.
        [CommandMethod("netCylinder")]
        public void CreateCylinder()
        {
            ObjectId cylinderId = ModelSpace.AddCylinder(new Point3d(300, 200, 100), 600, 400);
        }

        // 创建圆锥体的命令.
        [CommandMethod("netCone")]
        public void CreateCone()
        {
            ObjectId coneId = ModelSpace.AddCone(new Point3d(300, 200, 100), 600, 400);
        }

        // 创建球体的命令.
        [CommandMethod("netSphere")]
        public void CreateSphere()
        {
            ObjectId SphereId = ModelSpace.AddSphere(new Point3d(300, 200, 100), 600);
        }

        // 创建圆环体的命令.
        [CommandMethod("netTorus")]
        public void CreateTorus()
        {
            ObjectId torusId = ModelSpace.AddTorus(new Point3d(300, 200, 100), 600, 400);
        }

        // 创建楔体的命令.
        [CommandMethod("netWedge")]
        public void CreateWedge()
        {
            ObjectId wedgeId = ModelSpace.AddWedge(new Point3d(300, 200, 100), 600, 400, 200);
        }

        // 创建拉伸体的命令.
        [CommandMethod("netExt1")]
        public void CreateExtrudedSolid()
        {
            // 在内存中创建拉伸截面对象.
            Circle ent = new Circle(new Point3d(200, 100, 0), Vector3d.ZAxis, 100);
            // 截面对象添加到对象集合.
            DBObjectCollection ents = new DBObjectCollection();
            ents.Add(ent);
            // 在内存中创建面域对象集合.
            DBObjectCollection regions = Region.CreateFromCurves(ents);
            // 实施拉伸，并将拉伸体添加到图形数据库.
            ObjectId extrudedSolidId = ModelSpace.AddExtrudedSolid((Region)regions[0], 500, 0);
        }

        // 创建拉伸体的命令.
        [CommandMethod("netExt2")]
        public void CreateExtrudeAlongPath()
        {
            // 在内存中创建拉伸截面对象.
            Circle ent = new Circle(new Point3d(200, 0, 0), Vector3d.ZAxis, 100);
            // 截面对象添加到对象集合.
            DBObjectCollection ents = new DBObjectCollection();
            ents.Add(ent);
            // 在内存中创建面域对象集合.
            DBObjectCollection regions = Region.CreateFromCurves(ents);
            // 在内存中创建拉伸路径对象.
            Arc pathEnt = new Arc(new Point3d(500, 0, 0), new Vector3d(0, 1, 0), 300, 0, Math.PI);
            // 实施拉伸，并将拉伸体添加到图形数据库.
            ObjectId extrudeAlongPathId = ModelSpace.AddExtrudedSolid((Region)regions[0], pathEnt, 0);
        }

        // 创建旋转体的命令.
        [CommandMethod("netRevolved")]
        public void CreateRevolvedSolid()
        {
            // 在内存中创建旋转截面对象.
            Circle ent = new Circle(new Point3d(200, 0, 0), Vector3d.ZAxis, 100);
            // 截面对象添加到对象集合.
            DBObjectCollection ents = new DBObjectCollection();
            ents.Add(ent);
            // 在内存中创建面域对象集合.
            DBObjectCollection regions = Region.CreateFromCurves(ents);
            // 实施旋转，并将旋转体添加到图形数据库.
            ObjectId revolvedSolidId = ModelSpace.AddRevolvedSolid((Region)regions[0], new Point3d(300, 200, 100), new Point3d(600, 400, 200), 2 * Math.PI);
        }

        // 布尔示例的命令.
        [CommandMethod("netBool")]
        public void CreateBoolSolid()
        {
            // 在内存中创建旋转截面对象.
            Solid3d ent1 = new Solid3d();
            Solid3d ent2 = new Solid3d();
            ent1.CreateBox(100, 60, 40);
            ent2.CreateFrustum(90, 20, 20, 20);
            // 差集操作.
            ent1.BooleanOperation(BooleanOperationType.BoolSubtract,ent2);
            ModelSpace.AppendEntity(ent1);
        }

        [CommandMethod("netDim")]
        public void CreateDimension()
        {
            // 创建要标注的图形---------------------------------------------
            ModelSpace.AddLine(new Point3d(30, 20, 0), new Point3d(120, 20, 0));
            ModelSpace.AddLine(new Point3d(120, 20, 0), new Point3d(120, 40, 0));
            ModelSpace.AddLine(new Point3d(120, 40, 0), new Point3d(90, 80, 0));
            ModelSpace.AddLine(new Point3d(90, 80, 0), new Point3d(30, 80, 0));
            ModelSpace.AddArc(new Point3d(30, 50, 0), 30, ModelSpace.Rad2Ang(90), ModelSpace.Rad2Ang(270));
            ModelSpace.AddCircle(new Point3d(30, 50, 0), 15);
            ModelSpace.AddCircle(new Point3d(70, 50, 0), 10);

            // 得到当前标注样式---------------------------------------------
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId curDimstyle = db.Dimstyle;

            // (水平)转角标注-----------------------------------------------
            ModelSpace.AddDimRotated(0, new Point3d(30, 20, 0), new Point3d(120, 20, 0), new Point3d(75, 10, 0));
            // (垂直)转角标注-----------------------------------------------
            ModelSpace.AddDimRotated(ModelSpace.Rad2Ang(90), new Point3d(120, 20, 0), new Point3d(120, 40, 0), new Point3d(130, 30, 0));

            // 对齐标注、尺寸替代-------------------------------------------
            ModelSpace.AddDimAligned(new Point3d(120, 40, 0), new Point3d(90, 80, 0), new Point3d(113, 66, 0), "50%%p0.2", curDimstyle);

            // 半径标注-----------------------------------------------------
            Point3d ptCen = new Point3d(30, 50, 0);
            Point3d p2 = ModelSpace.PolarPoint(ptCen, ModelSpace.Rad2Ang(30), 15);
            ModelSpace.AddDimRadial(ptCen, p2, 10);

            // 直径标注-----------------------------------------------------
            Point3d dcen = new Point3d(70, 50, 0);
            Point3d ptChord1 = ModelSpace.PolarPoint(dcen, ModelSpace.Rad2Ang(45), 10);
            Point3d ptChord2 = ModelSpace.PolarPoint(dcen, ModelSpace.Rad2Ang(-135), 10);
            ModelSpace.AddDimDiametric(ptChord1, ptChord2, 0);

            // 角度标注-----------------------------------------------------
            Point3d angPtCen = new Point3d(120, 20, 0);
            Point3d p5 = ModelSpace.PolarPoint(angPtCen, ModelSpace.Rad2Ang(135), 10);
            ModelSpace.AddDimLineAngular(angPtCen, new Point3d(30, 20, 0), new Point3d(120, 40, 0), p5);

            // 弧长标注-----------------------------------------------------
            ModelSpace.AddDimArc(new Point3d(30, 50, 0), new Point3d(30, 20, 0), new Point3d(30, 80, 0), new Point3d(-10, 50, 0));

            // 坐标标注-----------------------------------------------------
            ModelSpace.AddDimOrdinate(new Point3d(70, 50, 0), new Point3d(70, 30, 0), new Point3d(90, 50, 0));

            // 引线标注-----------------------------------------------------
            Point3dCollection pts = new Point3dCollection();
            pts.Add(new Point3d(90, 70, 0));
            pts.Add(new Point3d(110, 80, 0));
            pts.Add(new Point3d(120, 80, 0));
            ModelSpace.AddLeader(pts, false);
            // 添加引线标注的文字.
            ModelSpace.AddMtext(new Point3d(120, 80, 0), "{\\L引线标注示例\\l}", curDimstyle, AttachmentPoint.BottomLeft, 2.5, 0);

            // 尺寸公差标注--------------------------------------------------
            ModelSpace.AddDimRotated(0, new Point3d(30, 80, 0), new Point3d(90, 80, 0), new Point3d(30, 90, 0), "60{\\H0.7x;\\S+0.026^-0.025;}", curDimstyle);

            // 形位公差标注--------------------------------------------------
            string dimText = "{\\fgdt;r}" + "%%v" + "{\\fgdt;n0.03}" + "%%v" + "B";
            ModelSpace.AddTolerance(dimText, new Point3d(80, 100, 0), new Vector3d(0, 0, 1), new Vector3d(1, 0, 0));
            // 为形位公差标注添加引线.
            Point3dCollection ptss = new Point3dCollection();
            ptss.Add(new Point3d(70, 80, 0));
            ptss.Add(new Point3d(70, 100, 0));
            ptss.Add(new Point3d(80, 100, 0));
            ModelSpace.AddLeader(ptss, false);
        }
    }
}