using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace chap17
{
    public class Edit
    {
        // 移动的函数.
        public static void Move(Entity ent, Point3d sourcePt, Point3d targetPt)
        {
            Vector3d vec = targetPt - sourcePt;
            Matrix3d mt = Matrix3d.Displacement(vec);
            ent.TransformBy(mt);
        }

        // 移动的函数.
        public static void Move(ObjectId id, Point3d sourcePt, Point3d targetPt)
        {
            Matrix3d mt = Matrix3d.Displacement(targetPt - sourcePt);
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                ent.TransformBy(mt);
                trans.Commit();
            }
        }

        // 复制的函数.
        public static void Copy(Entity ent, Point3d sourcePt, Point3d targetPt)
        {
            Matrix3d mt = Matrix3d.Displacement(targetPt - sourcePt);
            Entity entCopy = ent.GetTransformedCopy(mt);
            AppendEntity(entCopy);
        }

        // 复制的函数.
        public static void Copy(ObjectId id, Point3d sourcePt, Point3d targetPt)
        {
            Matrix3d mt = Matrix3d.Displacement(targetPt - sourcePt);
            Database db = HostApplicationServices.WorkingDatabase;
            Entity entCopy;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                entCopy = ent.GetTransformedCopy(mt);
                trans.Commit();
            }
            AppendEntity(entCopy);
        }

        // 旋转的函数.
        public static void Rotate(Entity ent, Point3d basePt, Double angle)
        {
            Matrix3d mt = Matrix3d.Rotation(angle, Vector3d.ZAxis, basePt);
            ent.TransformBy(mt);
        }

        // 旋转的函数.
        public static void Rotate(ObjectId id, Point3d basePt, Double angle)
        {
            Matrix3d mt = Matrix3d.Rotation(angle, Vector3d.ZAxis, basePt);
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                ent.TransformBy(mt);
                trans.Commit();
            }
        }

        // 缩放的函数.
        public static void Scale(Entity ent, Point3d basePt, Double scaleFactor)
        {
            Matrix3d mt = Matrix3d.Scaling(scaleFactor, basePt);
            ent.TransformBy(mt);
        }

        // 缩放的函数.
        public static void Scale(ObjectId id, Point3d basePt, Double scaleFactor)
        {
            Matrix3d mt = Matrix3d.Scaling(scaleFactor, basePt);
            Database db = HostApplicationServices.WorkingDatabase;
            Entity ent;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                ent.TransformBy(mt);
                trans.Commit();
            }
        }

        // 镜像的函数.
        public static void Mirror(Entity ent, Point3d mirrorPt1, Point3d mirrorPt2, bool eraseSourceObject)
        {
            Line3d mirrorLine = new Line3d(mirrorPt1, mirrorPt2);

            Matrix3d mt = Matrix3d.Mirroring(mirrorLine);
            if (eraseSourceObject == true)
                ent.TransformBy(mt);
            else
            {
                Entity entCopy = ent.GetTransformedCopy(mt);
                AppendEntity(entCopy);
            }
        }

        // 镜像的函数.
        public static void Mirror(ObjectId id, Point3d mirrorPt1, Point3d mirrorPt2, bool eraseSourceObject)
        {
            Line3d miLine = new Line3d(mirrorPt1, mirrorPt2);
            Matrix3d mt = Matrix3d.Mirroring(miLine);
            Database db = HostApplicationServices.WorkingDatabase;

            Entity ent;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                if (eraseSourceObject == true)
                    ent.TransformBy(mt);
                else
                {
                    Entity entCopy = ent.GetTransformedCopy(mt);
                    AppendEntity(entCopy);
                }
                trans.Commit();
            }
        }

        // 偏移的函数.
        public static void Offset(Curve cur, Double dis)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                DBObjectCollection offsetCur = cur.GetOffsetCurves(dis);
                for (int i = 0; i < offsetCur.Count; i++)
                    AppendEntity((Entity)offsetCur[i]);
            }
            catch
            {
                ed.WriteMessage("无法偏移！");
            }
        }

        // 偏移的函数.
        public static void Offset(ObjectId id, Double dis)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                if (ent is Curve)
                {
                    Curve cur = (Curve)ent;
                    try
                    {
                        DBObjectCollection offsetCur = cur.GetOffsetCurves(dis);
                        for (int i = 0; i < offsetCur.Count; i++)
                            AppendEntity((Entity)offsetCur[i]);
                        trans.Commit();
                    }
                    catch
                    {
                        ed.WriteMessage("\n无法偏移！");
                    }
                }
                else
                    ed.WriteMessage("\n无法偏移！");
            }
        }

        // 矩形阵列的函数.
        public static void ArrayRectang(Entity ent, int numRows, int numCols, double disRows, double disCols)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                for (int m = 0; m < numRows; m++)
                {
                    for (int n = 0; n < numCols; n++)
                    {
                        Matrix3d mt = Matrix3d.Displacement(new Vector3d(n * disCols, m * disRows, 0));
                        Entity entCopy = ent.GetTransformedCopy(mt);
                        AppendEntity(entCopy);
                    }
                }
                ent.Erase();
                trans.Commit();
            }
        }

        // 矩形阵列的函数.
        public static void ArrayRectang(ObjectId id, int numRows, int numCols, double disRows, double disCols)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                for (int m = 0; m < numRows; m++)
                {
                    for (int n = 0; n < numCols; n++)
                    {
                        Matrix3d mt = Matrix3d.Displacement(new Vector3d(n * disCols, m * disRows, 0));
                        Entity entCopy = (Entity)ent.GetTransformedCopy(mt);
                        AppendEntity(entCopy);
                    }
                }
                ent.Erase();
                trans.Commit();
            }
        }

        // 环形阵列的函数.
        public static void ArrayPolar(Entity ent, Point3d cenPt, int numObj, double Angle)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                for (int i = 0; i < numObj - 1; i++)
                {
                    Matrix3d mt = Matrix3d.Rotation(Angle * (i + 1) / numObj, Vector3d.ZAxis, cenPt);
                    Entity entCopy = ent.GetTransformedCopy(mt);
                    AppendEntity(entCopy);
                }
                trans.Commit();
            }
        }

        // 环形阵列的函数.
        public static void ArrayPolar(ObjectId id, Point3d cenPt, int numObj, double Angle)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                for (int i = 0; i < numObj - 1; i++)
                {
                    Matrix3d mt = Matrix3d.Rotation(Angle * (i + 1) / numObj, Vector3d.ZAxis, cenPt);
                    Entity entCopy = ent.GetTransformedCopy(mt);
                    AppendEntity(entCopy);
                }
                trans.Commit();
            }
        }

        // 删除的函数.
        public static void Erase(Entity ent)
        {
            ent.Erase();
        }

        // 删除的函数.
        public static void Erase(ObjectId id)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                ent.Erase();
                trans.Commit();
            }
        }

        // 度化弧度的函数.
        public static double Rad2Ang(double angle)
        {
            double rad = angle * Math.PI / 180;
            return rad;
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
