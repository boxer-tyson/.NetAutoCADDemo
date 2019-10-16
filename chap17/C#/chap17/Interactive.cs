using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace chap17
{
    public class Interactive
    {
        [CommandMethod("AddPoly")]
        public void CreatePoly()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double width = 0;
            short colorIndex = 0;
            int index = 2;
            Polyline polyEnt = new Polyline();
            ObjectId polyEntId = ObjectId.Null;

            PromptPointOptions optPoint = new PromptPointOptions("\n请输入第一个点<100,200>");
            optPoint.AllowNone = true;
            PromptPointResult resPoint = ed.GetPoint(optPoint);
            if (resPoint.Status == PromptStatus.Cancel)
                return;
            Point3d ptStart;
            if (resPoint.Status == PromptStatus.None)
                ptStart = new Point3d(100, 200, 0);
            else
                ptStart = resPoint.Value;
            Point3d ptPrevious = ptStart;
        nextPoint:
            PromptPointOptions optPtKey = new PromptPointOptions("\n请输入下一个点或[线宽(W)/颜色(C)/完成(O)]<O>");
            optPtKey.UseBasePoint = true;
            optPtKey.BasePoint = ptPrevious;
            optPtKey.Keywords.Add("W", "W", "W", false, true);
            optPtKey.Keywords.Add("C", "C", "C", false, true);
            optPtKey.Keywords.Add("O", "O", "O", false, true);
            optPtKey.Keywords.Default = "O";
            PromptPointResult resKey = ed.GetPoint(optPtKey);
            if (resKey.Status == PromptStatus.Cancel)
                return;
            Point3d ptNext;

            if (resKey.Status == PromptStatus.Keyword)
            {
                switch (resKey.StringResult)
                {
                    case "W":
                        width = getWidth();
                        break;
                    case "C":
                        colorIndex = getcolorindex();
                        break;
                    case "O":
                        using (Transaction trans = db.TransactionManager.StartTransaction())
                        {
                            try
                            {
                                trans.GetObject(polyEntId, OpenMode.ForWrite);
                                polyEnt.Color = Color.FromColorIndex(ColorMethod.ByColor, colorIndex);
                                polyEnt.ConstantWidth = width;
                            }
                            catch
                            {
                                // 此处无需处理.
                            }
                            trans.Commit();
                        }
                        return;
                }
                goto nextPoint;
            }
            else
            {
                ptNext = resKey.Value;
                if (index == 2)
                {
                    Point2d pt1 = new Point2d(ptPrevious[0], ptPrevious[1]);
                    Point2d pt2 = new Point2d(ptNext[0], ptNext[1]);
                    polyEnt.AddVertexAt(0, pt1, 0, width, width);
                    polyEnt.AddVertexAt(1, pt2, 0, width, width);
                    polyEnt.Color = Color.FromColorIndex(ColorMethod.ByColor, colorIndex);
                    polyEntId = AppendEntity(polyEnt);
                }
                else
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        trans.GetObject(polyEntId, OpenMode.ForWrite);
                        Point2d ptCurrent = new Point2d(ptNext[0], ptNext[1]);
                        polyEnt.AddVertexAt(index - 1, ptCurrent, 0, width, width);
                        polyEnt.Color = Color.FromColorIndex(ColorMethod.ByColor, colorIndex);
                        polyEnt.ConstantWidth = width;
                        trans.Commit();
                    }
                }
                index++;
            }
            ptPrevious = ptNext;
            goto nextPoint;
        }

        // 得到用户输入线宽的函数.
        public double getWidth()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptDoubleOptions optDou = new PromptDoubleOptions("\n请输入线宽");
            optDou.AllowNegative = false;
            optDou.DefaultValue = 0;
            PromptDoubleResult resDou = ed.GetDouble(optDou);
            if (resDou.Status == PromptStatus.OK)
            {
                Double width = resDou.Value;
                return width;
            }
            else
                return 0;
        }

        // 得到用户输入颜色索引值的函数.
        public short getcolorindex()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptIntegerOptions optInt = new PromptIntegerOptions("\n请输入颜色索引值(0～256)");
            optInt.DefaultValue = 0;
            PromptIntegerResult resInt = ed.GetInteger(optInt);
            if (resInt.Status == PromptStatus.OK)
            {
                short colorIndex = (short)resInt.Value;
                if (colorIndex > 256 | colorIndex < 0)
                    return 0;
                else
                    return colorIndex;
            }
            else
                return 0;
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
