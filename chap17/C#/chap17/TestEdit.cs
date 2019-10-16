using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace chap17
{
    public class TestEdit
    {
        [CommandMethod("testSel")]
        public void testSelection1()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection(optSel);
            SelectionSet sSet = resSel.Value;
            ObjectId[] ids = sSet.GetObjectIds();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // 遍历选择集.
                foreach (ObjectId sSetEntId in ids)
                {
                    Entity en = (Entity)trans.GetObject(sSetEntId, OpenMode.ForRead);
                    ed.WriteMessage(("\n您选择的是: " + en.GetType().Name));
                }
                trans.Commit();
            }
        }

        [CommandMethod("testFilSel")]
        public void testSelection2()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            TypedValue value1 = new TypedValue((int)DxfCode.Start, "CIRCLE,LINE");
            TypedValue value2 = new TypedValue((int)DxfCode.LayerName, "0");
            TypedValue value3 = new TypedValue((int)DxfCode.Color, "1");
            TypedValue[] values = { value1, value2, value3 };
            SelectionFilter sfilter = new SelectionFilter(values);

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择位于0层的红色的圆和红色的直线";
            PromptSelectionResult resSel = ed.GetSelection(optSel, sfilter);
            SelectionSet sSet = resSel.Value;
            ObjectId[] ids = sSet.GetObjectIds();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                foreach (ObjectId sSetEntId in ids)
                {
                    Entity ent = (Entity)trans.GetObject(sSetEntId, OpenMode.ForWrite);
                    ent.Color = Color.FromColorIndex(ColorMethod.ByColor, 2);
                    ed.WriteMessage(("\n您选择的是: " + ent.GetType().Name));
                }
                trans.Commit();
            }
        }

        // 移动.
        [CommandMethod("netMove")]
        public void testMove()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();

            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();

            foreach (ObjectId id in ids)
                Edit.Move(id, new Point3d(0, 0, 0), new Point3d(300, 200, 0));
        }

        // 复制.
        [CommandMethod("netCopy")]
        public void testCopy()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();

            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();
            foreach (ObjectId id in ids)
                Edit.Copy(id, new Point3d(0, 0, 0), new Point3d(300, 200, 0));
        }

        // 旋转.
        [CommandMethod("netRotate")]
        public void testRotate()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();

            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();
            foreach (ObjectId id in ids)
                Edit.Rotate(id, new Point3d(0, 0, 0), Edit.Rad2Ang(30));
        }

        // 缩放.
        [CommandMethod("netScale")]
        public void testScale()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();
            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();

            foreach (ObjectId id in ids)
                Edit.Scale(id, new Point3d(0, 0, 0), 3);
        }

        // 镜像.
        [CommandMethod("netMirror")]
        public void testMirror()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();
            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();

            foreach (ObjectId id in ids)
                Edit.Mirror(id, new Point3d(0, 0, 0), new Point3d(300, 200, 0), false);
        }

        // 偏移.
        [CommandMethod("netOffset")]
        public void testOffset()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityOptions optEnt = new PromptEntityOptions("\n请选择要偏移的对象");
            PromptEntityResult resEnt = ed.GetEntity(optEnt);
            if (resEnt.Status == PromptStatus.OK)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        Curve ent = (Curve)trans.GetObject(resEnt.ObjectId, OpenMode.ForRead);
                        Edit.Offset(ent, -10);
                    }
                    catch
                    {
                        ed.WriteMessage("\n无法偏移！");
                    }
                    trans.Commit();
                }
            }
        }

        // 矩形阵列.
        [CommandMethod("netArrayRectang")]
        public void testArrayRectang()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();

            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();

            foreach (ObjectId id in ids)
                Edit.ArrayRectang(id, 5, 8, 300, 200);
        }

        // 环形阵列.
        [CommandMethod("netArrayPolar")]
        public void testArrayPolar()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();
            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();

            foreach (ObjectId id in ids)
                Edit.ArrayPolar(id, new Point3d(0, 0, 0), 8, Edit.Rad2Ang(360));
        }

        // 删除.
        [CommandMethod("netErase")]
        public void testErase()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions optSel = new PromptSelectionOptions();
            optSel.MessageForAdding = "请选择对象";
            PromptSelectionResult resSel = ed.GetSelection();
            if (resSel.Status != PromptStatus.OK) return;

            SelectionSet sset = resSel.Value;
            ObjectId[] ids = sset.GetObjectIds();

            foreach (ObjectId id in ids)
                Edit.Erase(id);
        }
    }
}
