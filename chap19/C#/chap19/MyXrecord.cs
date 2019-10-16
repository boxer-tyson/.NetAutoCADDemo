using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace chap19
{
    public class MyXrecord
    {
        [CommandMethod("CreateXrecord")]
        public void CreateXrecord()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //新建一个扩展记录对象
                Xrecord xrec = new Xrecord();
                //设置扩展记录中包含的数据列表，包括文本、坐标、数值、角度、颜色
                Point3d pt = new Point3d(1.0, 2.0, 0.0);
                xrec.Data = new ResultBuffer(
                            new TypedValue((int)(int)DxfCode.Text, "这是一个测试用的扩展记录列表"),
                            new TypedValue((int)DxfCode.XCoordinate, pt),
                            new TypedValue((int)DxfCode.Real, 3.14159),
                            new TypedValue((int)DxfCode.Angle, 3.14159),
                            new TypedValue((int)DxfCode.Color, 1),
                            new TypedValue((int)DxfCode.Int16, 180));
                //下面的操作用来选择要添加扩展记录的对象
                PromptEntityOptions opt = new PromptEntityOptions("请选择要添加扩展记录的对象");
                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status != PromptStatus.OK)
                    return;
                Entity ent = (Entity)trans.GetObject(res.ObjectId, OpenMode.ForWrite);
                //判断所选对象是否已包含扩展记录
                if (ent.ExtensionDictionary != ObjectId.Null)
                {
                    ed.WriteMessage("对象已包含扩展记录，无法再创建");
                    return;
                }
                //为所选择的对象创建一个扩展字典
                ent.CreateExtensionDictionary();
                ObjectId dictEntId = ent.ExtensionDictionary;
                DBDictionary entXrecord = (DBDictionary)trans.GetObject(dictEntId, OpenMode.ForWrite);
                //在扩展字典中加入上面新建的扩展记录对象，并指定它的搜索关键字为MyXrecord
                entXrecord.SetAt("MyXrecord", xrec);
                //通知事务处理完成扩展记录对象的加入
                trans.AddNewlyCreatedDBObject(xrec, true);
                trans.Commit();
            }
        }

        [CommandMethod("ListXrecord")]
        public void ListXrecord()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //下面的操作用来选择显示扩展记录的对象
                PromptEntityOptions opt = new PromptEntityOptions("请选择要显示扩展记录的对象");
                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status != PromptStatus.OK)
                    return;
                Entity ent = (Entity)trans.GetObject(res.ObjectId, OpenMode.ForRead);
                //打开所选择对象的扩展字典
                DBDictionary entXrecord = (DBDictionary)trans.GetObject(ent.ExtensionDictionary, OpenMode.ForRead);
                //在扩展字典中搜索关键字为MyXrecord的扩展记录对象，如果找到则返回它的ObjectId
                ObjectId xrecordId = entXrecord.GetAt("MyXrecord");
                //打开找到的扩展记录对象
                Xrecord xrecord = (Xrecord)trans.GetObject(xrecordId, OpenMode.ForRead);
                //获取扩展记录中包含的数据列表并循环遍历显示它们
                ResultBuffer rb = xrecord.Data;
                foreach (TypedValue value in rb)
                {
                    ed.WriteMessage(string.Format("\nTypeCode={0},Value={1}", value.TypeCode, value.Value));
                }
                trans.Commit();
            }
        }
    }
}
