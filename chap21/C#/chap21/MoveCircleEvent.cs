using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
namespace chap21
{
    public class MoveCircleEvent
    {
        Database db = HostApplicationServices.WorkingDatabase;
        Document doc = Application.DocumentManager.MdiActiveDocument;
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        bool bMove;
        Point3d startPoint;

        void commandWillStart(object sender, CommandEventArgs e)
        {
            //如果AutoCAD命令为MOVE。
            if (e.GlobalCommandName == "MOVE")
            {
                //设置全局变量bMove为True，表示移动命令开始
                bMove = true;
            }
        }

        void objectOpenedForModify(object sender, ObjectEventArgs e)
        {
            //判断AutoCAD命令是否为移动
            if (bMove == false)
                //如果AutoCAD命令为非移动，则返回
                return;
            Circle circle = e.DBObject as Circle;
            //判断将要移动的对象是否为圆
            if (circle != null)
            {
                //获取圆的中心，就是同心圆的圆心
                startPoint = circle.Center;
            }
        }

        void objectModified(object sender, ObjectEventArgs e)
        {
            //判断AutoCAD命令是否为移动
            if (bMove == false)
                //如果AutoCAD命令为非移动，则返回
                return;
            //断开所有的事件处理函数
            removeEvents();
            //判断移动过的对象是否为圆
            Circle startCirlce = e.DBObject as Circle;
            if (startCirlce == null)
                return;
            //设置选择集过滤器，只选择图形中的圆
            TypedValue[] values ={ new TypedValue((int)DxfCode.Start, "Circle") };
            SelectionFilter filter = new SelectionFilter(values);
            PromptSelectionResult resSel = ed.SelectAll(filter);
            //如果选择的是圆
            if (resSel.Status == PromptStatus.OK)
            {
                //获取选择集中的圆对象
                SelectionSet sSet = resSel.Value;
                ObjectId[] ids = sSet.GetObjectIds();
                //开始事务处理
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    //循环遍历选择集中的圆
                    foreach (ObjectId id in ids)
                    {
                        //以读的方式打开圆对象
                        Circle followedCirlce = (Circle)trans.GetObject(id, OpenMode.ForRead);
                        //通过判断圆的圆心与所移动的圆的圆心是否相同，来移动所有的同心圆
                        if (followedCirlce.Center == startPoint)
                        {
                            //因为上面以读的方式打开了圆，所以为了改变圆的圆心必须改变为写
                            followedCirlce.UpgradeOpen();
                            //改变圆的圆心，以达到移动的目的
                            followedCirlce.Center = startCirlce.Center;
                        }
                    }
                    //提交事务处理
                    trans.Commit();
                }
            }
            //连接所有的事件处理函数
            addEvents();
        }

        void commandEnded(object sender, CommandEventArgs e)
        {
            //判断AutoCAD命令是否为移动
            if (bMove == true)
            {
                //设置全局变量bMove为False，表示移动命令结束
                bMove = false;
            }
        }

        [CommandMethod("AddEvents")]
        public void addEvents()
        {
            //把事件处理函数与相应的事件进行连接
            db.ObjectOpenedForModify +=new ObjectEventHandler(objectOpenedForModify);
            db.ObjectModified +=new ObjectEventHandler(objectModified);
            doc.CommandWillStart +=new CommandEventHandler(commandWillStart);
            doc.CommandEnded +=new CommandEventHandler(commandEnded);
        }

        [CommandMethod("RemoveEvents")]
        public void removeEvents()
        {
            //断开所有的事件处理函数
            db.ObjectOpenedForModify -=new ObjectEventHandler(objectOpenedForModify);
            db.ObjectModified -=new ObjectEventHandler(objectModified);
            doc.CommandWillStart -=new CommandEventHandler(commandWillStart);
            doc.CommandEnded -=new CommandEventHandler(commandEnded);
        }
    }
}
