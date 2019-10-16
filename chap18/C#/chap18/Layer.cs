using System;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;

namespace chap18
{
    public class Layer
    {
        [CommandMethod("netLayer")]
        public void CreateLayer()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                ObjectId layerId;
                if (lt.Has("abc") == false)
                {
                    LayerTableRecord ltr = new LayerTableRecord();

                    ltr.Name = "abc";
                    Color layerColor = Color.FromColorIndex(ColorMethod.ByColor, 120);
                    ltr.Color = layerColor;
                    LinetypeTable tt = (LinetypeTable)trans.GetObject(db.LinetypeTableId, OpenMode.ForRead);
                    LinetypeTableRecord ttr;
                    try
                    {
                        db.LoadLineTypeFile("CENTER", "acadiso.lin");
                    }
                    catch
                    {
                        // 此处无需操作.
                    }
                    ttr = (LinetypeTableRecord)trans.GetObject(tt["CENTER"], OpenMode.ForRead);
                    ltr.LinetypeObjectId = ttr.ObjectId;
                    ltr.LineWeight = LineWeight.LineWeight030;
                    db.LineWeightDisplay = true;
                    db.Ltscale = 2;
                    // 图层锁定
                    //ltr.IsLocked = True
                    // 图层冻结
                    //ltr.IsFrozen = True
                    // 图层关闭
                    //ltr.IsOff = True
                    layerId = lt.Add(ltr);
                    trans.AddNewlyCreatedDBObject(ltr, true);
                    db.Clayer = layerId;
                }
                trans.Commit();
            }
        }

        [CommandMethod("LayerColor")]
        public void EditLayerColor()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptStringOptions optStr = new PromptStringOptions("\n输入图层的名称");
            PromptResult resStr = ed.GetString(optStr);
            if (resStr.Status != PromptStatus.OK)
            {
                return;
            }
            String layName = resStr.StringResult;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);
                if (lt.Has(layName) == false)
                {
                    ed.WriteMessage("\n该图层不存在！");
                    return;
                }
                LayerTableRecord ltr = (LayerTableRecord)trans.GetObject(lt[layName], OpenMode.ForWrite);
                ColorDialog dialogObj = new ColorDialog();
                System.Windows.Forms.DialogResult dialogResultValue = dialogObj.ShowDialog();
                if (dialogResultValue == System.Windows.Forms.DialogResult.OK)
                {
                    Color newColor = dialogObj.Color;
                    ltr.Color = newColor;
                }
                trans.Commit();
            }
        }

        [CommandMethod("delLayer")]
        public void testdelLayer()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptStringOptions optStr = new PromptStringOptions("\n输入图层的名称");
            PromptResult resStr = ed.GetString(optStr);
            if (resStr.Status != PromptStatus.OK)
            {
                return;
            }
            String layName = resStr.StringResult;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForWrite);
                if (lt.Has(layName) == false)
                {
                    ed.WriteMessage("\n该图层不存在！");
                    return;
                }
                LayerTableRecord ltr = (LayerTableRecord)trans.GetObject(lt[layName], OpenMode.ForWrite);
                LayerTableRecord curLtr = (LayerTableRecord)trans.GetObject(db.Clayer, OpenMode.ForRead);
                if (layName == curLtr.Name)
                {
                    ed.WriteMessage("\n不能删除当前图层！");
                }
                else
                {
                    ltr.Erase(true);
                }
                trans.Commit();
            }
        }

        [CommandMethod("ExportLayer")]
        public void testLayerIterator()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            StreamWriter textFile = new StreamWriter("c:\\3.txt", false);
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);
                LinetypeTable tt = (LinetypeTable)trans.GetObject(db.LinetypeTableId, OpenMode.ForRead);
                foreach (ObjectId layerId in lt)
                {
                    LayerTableRecord ltr = (LayerTableRecord)trans.GetObject(layerId, OpenMode.ForRead);
                    String layerName = ltr.Name;
                    String colorName = ltr.Color.ToString();
                    ObjectId linetypeId = ltr.LinetypeObjectId;
                    LinetypeTableRecord ttr = (LinetypeTableRecord)trans.GetObject(linetypeId, OpenMode.ForRead);
                    String linetypeName = ttr.Name;
                    String withLayer = ltr.LineWeight.ToString();
                    String str = layerName + "   " + colorName + "   " + linetypeName + "   " + withLayer;
                    textFile.WriteLine(str, false);
                }
                textFile.Close();
                trans.Commit();
            }
        }
    }
}




