using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace chap18
{
    public class UCS
    {
        [CommandMethod("netUCS")]
        public void CreateUCS()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                UcsTable ut = (UcsTable)trans.GetObject(db.UcsTableId, OpenMode.ForWrite);
                String ucsName = "myUCS";
                if (ut.Has(ucsName) == false)
                {
                    UcsTableRecord utr = new UcsTableRecord();
                    utr.Name = ucsName;
                    utr.Origin = new Point3d(0, 0, 0);
                    utr.XAxis = new Vector3d(0, 1, 0);
                    utr.YAxis = new Vector3d(-1, 0, 0);
                    Matrix3d mt = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, utr.Origin, utr.XAxis, utr.YAxis, utr.XAxis.CrossProduct(utr.YAxis));
                    ed.CurrentUserCoordinateSystem = mt;
                }
                trans.Commit();
            }
        }

        [CommandMethod("netUCSO")]
        public void CreateUcsOrigin()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptPointOptions opt = new PromptPointOptions("指定新原点");
            PromptPointResult res = ed.GetPoint(opt);
            Point3d pt = res.Value;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Vector3d xAxis = db.Ucsxdir;
                Vector3d yAxis = db.Ucsydir;
                Vector3d zAxis = xAxis.CrossProduct(yAxis);
                Matrix3d cmt = ed.CurrentUserCoordinateSystem;
                Point3d newOrg = pt.TransformBy(cmt);
                Matrix3d mT = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, newOrg, xAxis, yAxis, zAxis);
                ed.CurrentUserCoordinateSystem = mT;
                trans.Commit();
            }
        }

        [CommandMethod("netUCSX")]
        public void CreateUcsxAxis()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptAngleOptions opt = new PromptAngleOptions("指定绕 X 轴的旋转角度");
            opt.UseDefaultValue = true;
            opt.DefaultValue = Math.PI / 2;
            PromptDoubleResult res = ed.GetAngle(opt);
            Double ang = res.Value;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Vector3d xAxis = db.Ucsxdir;
                Vector3d yAxis = db.Ucsydir.RotateBy(ang, xAxis);
                Vector3d zAxis = xAxis.CrossProduct(yAxis);
                Point3d org = db.Ucsorg;
                Matrix3d mT = Matrix3d.AlignCoordinateSystem(Point3d.Origin, Vector3d.XAxis, Vector3d.YAxis, Vector3d.ZAxis, org, xAxis, yAxis, zAxis);
                ed.CurrentUserCoordinateSystem = mT;
                trans.Commit();
            }
        }

        [CommandMethod("ucsCircle")]
        public void CreateUcsCircle()
        {
            Circle ent = new Circle(new Point3d(90, 30, 0), Vector3d.ZAxis, 80);
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Matrix3d mt = ed.CurrentUserCoordinateSystem;
            ent.TransformBy(mt);
            AppendEntity(ent);
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


