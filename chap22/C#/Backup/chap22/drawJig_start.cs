using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;

namespace chap22
{
    public class drawJig_start : DrawJig
    {
        // 声明五角星对象.
        private Polyline ent;
        // 声明五角星的中心和一个顶点.
        private Point3d mCenterPt, peakPt;
        [CommandMethod("FiveStart")]
        public void CreateDrawJigFiveStart()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            // 普通的点交互操作.
            PromptPointOptions optPoint = new PromptPointOptions("\n请指定五角星的中心");
            PromptPointResult resPoint = ed.GetPoint(optPoint);
            if (resPoint.Status != PromptStatus.OK)
                return;
            mCenterPt = resPoint.Value;

            // 在内存中创建一个具有10个顶点的封闭多段线对象.
            Point2d[] pt = new Point2d[10];
            pt[0] = new Point2d(0, 0);
            pt[1] = new Point2d(0, 0);
            pt[2] = new Point2d(0, 0);
            pt[3] = new Point2d(0, 0);
            pt[4] = new Point2d(0, 0);
            pt[5] = new Point2d(0, 0);
            pt[6] = new Point2d(0, 0);
            pt[7] = new Point2d(0, 0);
            pt[8] = new Point2d(0, 0);
            pt[9] = new Point2d(0, 0);
            Point2dCollection pts = new Point2dCollection(pt);
            ent = (Polyline)new Polyline();
            for (int i = 0; i <= 9; i++)
                ent.AddVertexAt(i, pts[i], 0, 0, 0);
            ent.Closed = true;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                // 开始拖拽.
                PromptResult resJig = ed.Drag(this);
                if (resJig.Status == PromptStatus.OK)
                {
                    // 将五角星对象加入到图形数据库中.
                    btr.AppendEntity(ent);
                    trans.AddNewlyCreatedDBObject(ent, true);
                    trans.Commit();
                }
            }
        }

        // Sampler函数用于检测用户的输入.
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            // 定义一个点拖动交互类.
            JigPromptPointOptions optJigPoint = new JigPromptPointOptions("\n请指定五角星的一个角点:");
            // 设置拖拽光标类型.
            optJigPoint.Cursor = CursorType.RubberBand;
            // 设置拖动光标基点.
            optJigPoint.BasePoint = mCenterPt;
            optJigPoint.UseBasePoint = true;
            // 用AcquirePoint函数得到用户输入的点.
            PromptPointResult resJigPoint1 = prompts.AcquirePoint(optJigPoint);
            Point3d curPt = resJigPoint1.Value;
            if (curPt != peakPt)
            {
                // 重新设置椭圆参数--------------------------------------------.
                // 五角星的中心.
                Point2d p0 = new Point2d(mCenterPt.X, mCenterPt.Y);

                // 计算五角星的第一个顶点坐标.
                Point2d p1 = new Point2d(curPt[0], curPt[1]);

                // 为计算其他9个顶点的坐标进行准备.
                double d1 = p1.GetDistanceTo(p0);
                double d2 = d1 * Math.Sin(Rad2Ang(18)) / Math.Sin(Rad2Ang(54));
                Vector2d vec = p1 - p0;
                double ang = vec.Angle;

                // 计算五角星另外9个顶点的坐标.
                Point2d p2 = PolarPoint(p0, ang + Rad2Ang(36), d2);
                Point2d p3 = PolarPoint(p0, ang + Rad2Ang(72), d1);
                Point2d p4 = PolarPoint(p0, ang + Rad2Ang(108), d2);
                Point2d p5 = PolarPoint(p0, ang + Rad2Ang(144), d1);
                Point2d p6 = PolarPoint(p0, ang + Rad2Ang(180), d2);
                Point2d p7 = PolarPoint(p0, ang + Rad2Ang(216), d1);
                Point2d p8 = PolarPoint(p0, ang + Rad2Ang(252), d2);
                Point2d p9 = PolarPoint(p0, ang + Rad2Ang(288), d1);
                Point2d p10 = PolarPoint(p0, ang + Rad2Ang(324), d2);

                // 更新五角星各个顶点的坐标.
                ent.SetPointAt(0, p1);
                ent.SetPointAt(1, p2);
                ent.SetPointAt(2, p3);
                ent.SetPointAt(3, p4);
                ent.SetPointAt(4, p5);
                ent.SetPointAt(5, p6);
                ent.SetPointAt(6, p7);
                ent.SetPointAt(7, p8);
                ent.SetPointAt(8, p9);
                ent.SetPointAt(9, p10);
                peakPt = curPt;
                return SamplerStatus.OK;
            }
            else
                return SamplerStatus.NoChange;
        }

        // WorldDraw函数用于刷新屏幕上显示的图形.
        protected override bool WorldDraw(WorldDraw draw)
        {
            // 刷新画面.
            draw.Geometry.Draw(ent);
            return true;
        }

        // 获取与给定点指定角度和距离的点.
        public Point2d PolarPoint(Point2d basePt, double angle, double distance)
        {
            double[] pt = new double[2];
            pt[0] = basePt[0] + distance * Math.Cos(angle);
            pt[1] = basePt[1] + distance * Math.Sin(angle);
            Point2d point = new Point2d(pt[0], pt[1]);
            return point;
        }
        // 度化弧度的函数.
        public double Rad2Ang(double angle)
        {
            double rad = angle * Math.PI / 180;
            return rad;
        }
    }
}
