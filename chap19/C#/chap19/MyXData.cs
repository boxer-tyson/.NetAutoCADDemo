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
    public class MyXData
    {
        [CommandMethod("AddXData")]
        public void AddXData()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //下面的操作用来选择实体以添加扩展数据
                PromptEntityOptions opt = new PromptEntityOptions("请选择实体来添加扩展数据");
                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status != PromptStatus.OK)
                {
                    return;
                }
                Circle circ = (Circle)trans.GetObject(res.ObjectId, OpenMode.ForWrite);
                //获取当前数据库的注册应用程序表
                RegAppTable reg = (RegAppTable)trans.GetObject(db.RegAppTableId, OpenMode.ForWrite);
                //如果没有名为"实体扩展数据"的注册应用程序表记录，则
                if (!reg.Has("实体扩展数据"))
                {
                    //创建一个注册应用程序表记录用来表示扩展数据
                    RegAppTableRecord app = new RegAppTableRecord();
                    //设置扩展数据的名字
                    app.Name = "实体扩展数据";
                    //在注册应用程序表加入扩展数据
                    reg.Add(app);
                    trans.AddNewlyCreatedDBObject(app, true);
                }
                //设置扩展数据的内容
                ResultBuffer rb = new ResultBuffer(
                new TypedValue((int)DxfCode.ExtendedDataRegAppName, "实体扩展数据"),
                new TypedValue((int)DxfCode.ExtendedDataAsciiString, "字符串扩展数据"),
                new TypedValue((int)DxfCode.ExtendedDataLayerName, "0"),
                new TypedValue((int)DxfCode.ExtendedDataReal, 1.23479137438413E+40),
                new TypedValue((int)DxfCode.ExtendedDataInteger16, 32767),
                new TypedValue((int)DxfCode.ExtendedDataInteger32, 32767),
                new TypedValue((int)DxfCode.ExtendedDataScale, 10),
                new TypedValue((int)DxfCode.ExtendedDataWorldXCoordinate, new Point3d(10, 10, 0)));
                //将新建的扩展数据附加到所选择的实体中
                circ.XData = rb;
                trans.Commit();
            }
        }

        [CommandMethod("ListXData")]
        public void ListXData()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //下面的操作用来选择实体来显示它的扩展数据
                PromptEntityOptions opt = new PromptEntityOptions("请选择实体来显示它的扩展数据");
                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status != PromptStatus.OK)
                {
                    return;
                }
                Entity ent = (Entity)trans.GetObject(res.ObjectId, OpenMode.ForRead);
                //获取所选择实体中名为“实体扩展数据”的扩展数据
                ResultBuffer rb = ent.GetXDataForApplication("实体扩展数据");
                //如果没有，就返回
                if (rb == null)
                {
                    return;
                }
                //循环遍历扩展数据
                foreach (TypedValue entXData in rb)
                {
                    ed.WriteMessage(string.Format("\nTypeCode={0},Value={1}", entXData.TypeCode, entXData.Value));
                }
            }
        }
    }
}
