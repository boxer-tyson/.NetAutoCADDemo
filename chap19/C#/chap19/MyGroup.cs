using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace chap19
{
    public class MyGroup
    {
        [CommandMethod("MakeGroup")]
        public void MakeGroup()
        {
            //创建名为MyGroup的组
            createGroup("MyGroup");
        }
        private void createGroup(string groupName)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans=db.TransactionManager.StartTransaction())
            {
                //新建一个组对象
                Group gp = new Group(groupName, true);
                //打开当前数据库的组字典对象以加入新建的组对象
                DBDictionary dict = (DBDictionary)trans.GetObject(db.GroupDictionaryId, OpenMode.ForWrite);
                //在组字典中将组对象作为一个新条目加入，并指定它的搜索关键字为groupName
                dict.SetAt(groupName, gp);
                //下面的操作用来选择组中要包含的对象
                PromptSelectionOptions opt = new PromptSelectionOptions();
                opt.MessageForAdding = "请选择组中要包含的对象";
                PromptSelectionResult res = ed.GetSelection(opt);
                if (res.Status!=PromptStatus.OK)
                {
                    return;
                }
                //获取所选择对象的ObjectId集合
                SelectionSet ss = res.Value;
                ObjectIdCollection ids = new ObjectIdCollection(ss.GetObjectIds());
                //在组对象中加入所选择的对象
                gp.Append(ids);
                //通知事务处理完成组对象的加入
                trans.AddNewlyCreatedDBObject(gp, true);
                trans.Commit();
            }
        }

        [CommandMethod("RemoveButLines")]
        public void RemoveButLines()
        {
            //在MyGroup组中移除所有不是直线的对象
            removeAllButLines("MyGroup");
        }

        private void removeAllButLines(string groupName)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //打开当前数据库的组字典对象
                DBDictionary dict = (DBDictionary)trans.GetObject(db.GroupDictionaryId, OpenMode.ForRead);
                //在组字典中搜索关键字为groupName的组对象，如果找到则返回它的ObjectId
                ObjectId gpid = dict.GetAt(groupName);
                //由于要在组中进行去除对象的操作，因此以写的方式打开找到的组对象
                Group gp = (Group)trans.GetObject(gpid, OpenMode.ForWrite);
                //获取组对象中的所有实体的ObjectId并进行循环遍历
                ObjectId[] ids = gp.GetAllEntityIds();
                foreach (ObjectId id in ids)
                {
                    //打开组中的当前对象，并判断是否为直线
                    Line obj = trans.GetObject(id, OpenMode.ForRead) as Line;
                    if (obj==null)
                    {
                        //如果对象不是直线，则在组中移除它
                        gp.Remove(id);
                    }
                }
                //设置组中所有实体的颜色为红色
                gp.SetColorIndex(1);
                trans.Commit();
            }
        }
    }
}
