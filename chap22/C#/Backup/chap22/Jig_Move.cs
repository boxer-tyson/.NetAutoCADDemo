using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;

namespace chap22
{
    public class Jig_Move : DrawJig
    {
        // 声明全局变量.
        private Point3d sourcePt,targetPt,curPt;
        private Entity[] entCopy;
        private ObjectId[] ids;

        [CommandMethod("jigMove")]
        public void testJigMove()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            // 普通的选择集交互操作.
            PromptSelectionOptions opt = new PromptSelectionOptions();
            opt.MessageForAdding = "选择对象";
            opt.AllowDuplicates = true;
            PromptSelectionResult res = ed.GetSelection(opt);
            if (res.Status != PromptStatus.OK)
                return;
            SelectionSet sSet = res.Value;
            ids = sSet.GetObjectIds();

            Entity[] oldEnt = new Entity[ids.Length];

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                entCopy = new Entity[ids.Length];
                for (int i = 0; i <= ids.Length - 1; i++)
                {
                    oldEnt[i] = (Entity)trans.GetObject(ids[i], OpenMode.ForWrite);
                    // 将源对象设置为高亮状态.
                    oldEnt[i].Highlight();
                    // 复制.
                    entCopy[i] = (Entity)oldEnt[i].Clone();
                }

                // 得到移动的源点-----------------------------------------------
                PromptPointOptions optPoint = new PromptPointOptions("\n请输入基点<0,0,0>");
                optPoint.AllowNone = true;
                PromptPointResult resPoint = ed.GetPoint(optPoint);
                if (resPoint.Status != PromptStatus.Cancel)
                {
                    if (resPoint.Status == PromptStatus.None)
                        sourcePt = new Point3d(0, 0, 0);
                    else
                        sourcePt = resPoint.Value;
                }

                // 设置目标点和拖拽临时点的初值.
                targetPt = sourcePt;
                curPt = targetPt;

                // 开始拖拽.
                PromptResult jigRes = ed.Drag(this);
                if (jigRes.Status == PromptStatus.OK)
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    for (int i = 0; i <= ids.Length - 1; i++)
                    {
                        btr.AppendEntity(entCopy[i]);
                        trans.AddNewlyCreatedDBObject(entCopy[i], true);
                    }
                    // 删除源对象.
                    for (int i = 0; i <= ids.Length - 1; i++)
                        oldEnt[i].Erase();
                }
                else
                {
                    // 取消源对象的高亮状态.
                    for (int i = 0; i <= ids.Length - 1; i++)
                        oldEnt[i].Unhighlight();
                }
                trans.Commit();
            }
        }

        // Sampler函数用于检测用户的输入.
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            // 定义一个点拖动交互类.
            JigPromptPointOptions optJig = new JigPromptPointOptions("\n请指定第二点:");
            // 设置拖拽光标类型.
            optJig.Cursor = CursorType.RubberBand;
            // 设置拖动光标基点.
            optJig.BasePoint = sourcePt;
            optJig.UseBasePoint = true;
            // 用AcquirePoint函数得到用户输入的点.
            PromptPointResult resJig = prompts.AcquirePoint(optJig);
            targetPt = resJig.Value;
            // 如果用户拖拽，则用矩阵变换的方法移动选择集中的全部对象.
            if (curPt != targetPt)
            {
                Matrix3d moveMt = Matrix3d.Displacement(targetPt - curPt);
                for (int i = 0; i <= ids.Length - 1; i++)
                    entCopy[i].TransformBy(moveMt);
                // 保存当前点.
                curPt = targetPt;
                return SamplerStatus.OK;
            }
            else
                return SamplerStatus.NoChange;
        }

        // WorldDraw函数用于刷新屏幕上显示的图形.
        protected override bool WorldDraw(WorldDraw draw)
        {
            for (int i = 0; i <= ids.Length - 1; i++)
                // 刷新画面.
                draw.Geometry.Draw(entCopy[i]);
            return true;
        }
    }
}
