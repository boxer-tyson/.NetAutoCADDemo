using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
namespace chap21
{
    public class ContextMenu
    {
        [CommandMethod("AddDefaultContextMenu")]
        public void AddContextMenu()
        {
            //定义一个ContextMenuExtension对象，用于表示快捷菜单
            ContextMenuExtension contextMenu =new ContextMenuExtension();
            //设置快捷菜单的标题
            contextMenu.Title = "我的快捷菜单";
            //添加一个名为"复制"的菜单项，用于调用复制命令
            MenuItem mi =new MenuItem("复制");
            //为"复制"菜单项添加单击事件
            mi.Click +=new EventHandler(mi_Click);
            //将"复制"菜单项添加到快捷菜单中
            contextMenu.MenuItems.Add(mi);
            //添加一个名为"删除"的菜单项，用于调用删除命令
            mi =new MenuItem("删除");
            //为"删除"菜单项添加单击事件
            mi.Click +=new EventHandler(mi_Click);
            //将"删除"菜单项添加到快捷菜单中
            contextMenu.MenuItems.Add(mi);
            //为应用程序添加定义的快捷菜单
            Application.AddDefaultContextMenuExtension(contextMenu);
        }

        void mi_Click(object sender, EventArgs e)
        {
            //获取发出命令的快捷菜单项
            MenuItem mi = sender as MenuItem;
            //获取当前活动文档
            Document doc = Application.DocumentManager.MdiActiveDocument;
            //根据快捷菜单项的名字，分别调用对应的命令
            if(mi.Text=="复制")
            {
                doc.SendStringToExecute("_Copy ", true, false, true);  
            }
            else if (mi.Text == "删除")
            {
                doc.SendStringToExecute("_Erase ", true, false, true);
            }
        }

        [CommandMethod("AddObjectContextMenu")]
        public void AddObjectContextMenu()
        {
            //定义一个ContextMenuExtension对象，用于表示快捷菜单
            ContextMenuExtension contextMenu =new ContextMenuExtension();
            //对于对象级别的快捷菜单，不必设置菜单名
            contextMenu.Title = "圆的快捷菜单";
            //添加一个名为"圆面积"的菜单项，用于在AutoCAD命令行上显示所选择的圆面积
            MenuItem miCircle =new MenuItem("圆面积");
            //为"圆面积"菜单项添加单击事件
            miCircle.Click +=new EventHandler(miCircle_Click);
            //将"圆面积"菜单项添加到快捷菜单中
            contextMenu.MenuItems.Add(miCircle);
            //为圆对象添加定义的快捷菜单
            Application.AddObjectContextMenuExtension(RXClass.GetClass(typeof(Circle)), contextMenu);
        }

        void miCircle_Click(object sender, EventArgs e)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db=HostApplicationServices.WorkingDatabase;
            //获取当前的选择集对象
            SelectionSet ss = ed.SelectImplied().Value;
            using (Transaction trans=db.TransactionManager.StartTransaction())
            {
                //循环遍历选择集中的对象
                foreach (ObjectId id in ss.GetObjectIds())
                {
                    Circle circle = trans.GetObject(id, OpenMode.ForRead)as Circle;
                    //如果所选择的对象是圆
                    if (circle!=null)
                    {
                        //在命令行上显示圆面积信息
                        ed.WriteMessage("\n圆面积为:"+circle.Area.ToString());
                    }
                }  
            } 
        }
    }
}
