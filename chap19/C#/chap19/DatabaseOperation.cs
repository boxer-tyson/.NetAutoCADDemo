using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
namespace chap19
{
    public class DatabaseOperation
    {
        [CommandMethod("CreateAndSaveDwg")]
        public void CreateAndSaveDwg()
        {
            //新建一个数据库对象以创建新的Dwg文件
            Database db = new Database();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //获取数据库的块表对象
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                //获取数据库的模型空间块表记录对象
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                //新建两个圆
                Circle cir1 = new Circle(new Point3d(1, 1, 0), Vector3d.ZAxis, 1.0);
                Circle cir2 = new Circle(new Point3d(4, 4, 0), Vector3d.ZAxis, 2.0);
                //在模型空间中加入新建的两个圆
                btr.AppendEntity(cir1);
                trans.AddNewlyCreatedDBObject(cir1, true);
                btr.AppendEntity(cir2);
                trans.AddNewlyCreatedDBObject(cir2, true);

                //定义保存文件对话框
                PromptSaveFileOptions opt = new PromptSaveFileOptions("\n请输入文件名：");
                //保存文件对话框的文件扩展名列表
                opt.Filter = "图形(*.dwg)|*.dwg|图形(*.dxf)|*.dxf";
                //文件过滤器列表中缺省显示的文件扩展名
                opt.FilterIndex = 0;
                //保存文件对话框的标题
                opt.DialogCaption = "图形另存为";
                //缺省保存目录
                opt.InitialDirectory = "C:\\";
                //缺省保存文件名（扩展名由扩展名列表中的当前值确定）
                opt.InitialFileName = "MyDwg";
                //获取当前数据库对象（不是上面新建的）的命令行对象
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //根据保存对话框中用户的选择，获取保存文件名
                string filename = ed.GetFileNameForSave(opt).StringResult;
                //保存文件为当前AutoCAD版本
                db.SaveAs(filename, DwgVersion.Current);
            }

        }

        [CommandMethod("ReadDwg")]
        public void ReadDwg()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //新建一个数据库对象以读取Dwg文件
            Database db = new Database(false, true);
            //文件名
            string fileName = "C:\\MyDwg.dwg";
            //如果指定文件名的文件存在，则
            if (System.IO.File.Exists(fileName))
            {
                //把文件读入到数据库中
                db.ReadDwgFile(fileName, System.IO.FileShare.Read, true, null);
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    //获取数据库的块表对象
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    //打开数据库的模型空间块表记录对象
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    //循环遍历模型空间中的实体
                    foreach (ObjectId id in btr)
                    {
                        Entity ent = trans.GetObject(id, OpenMode.ForRead) as Entity;
                        if (ent != null)
                        {
                            //显示实体的类名
                            ed.WriteMessage("\n" + ent.GetType().ToString());
                        }

                    }
                }
            }
            //销毁数据库对象
            db.Dispose();
        }

        [CommandMethod("CopyFromOtherDwg")]
        public void CopyFromOtherDwg()
        {
            //新建一个数据库对象以读取Dwg文件
            Database db = new Database(false, false);
            string fileName = "C:\\Blocks and Tables - Metric.dwg";
            if (System.IO.File.Exists(fileName))
            {
                db.ReadDwgFile(fileName, System.IO.FileShare.Read, true, null);
                //为了让插入块的函数在多个图形文件打开的情况下起作用，你必须使用下面的函数把Dwg文件关闭
                db.CloseInput(true);
                //获取当前数据库对象（不是新建的数据库）
                Database curdb = HostApplicationServices.WorkingDatabase;
                //把源数据库模型空间中的实体插入到当前数据库的一个新的块表记录中
                curdb.Insert(System.IO.Path.GetFileNameWithoutExtension(fileName), db, true);
            }
            //销毁数据库对象
            db.Dispose();
        }

        [CommandMethod("OpenDwg")]
        public void OpenDwg()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //获取文档管理器对象以打开Dwg文件
            DocumentCollection docs = Application.DocumentManager;
            //设置打开文件对话框的有关选项
            PromptOpenFileOptions opt = new PromptOpenFileOptions("\n请输入文件名：");
            opt.Filter = "图形(*.dwg)|*.dwg|图形(*.dxf)|*.dxf";
            opt.FilterIndex = 0;
            //根据打开文件对话框中用户的选择，获取文件名
            string filename = ed.GetFileNameForOpen(opt).StringResult;
            //打开所选择的Dwg文件
            Document doc = docs.Open(filename, true);
            //设置当前的活动文档为新打开的Dwg文件
            Application.DocumentManager.MdiActiveDocument = doc;
        }


        [CommandMethod("CopyEntities")]
        public void CopyEntities()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //新建一个数据库对象
            Database db = new Database(false, true);
            //获取当前数据库对象
            Database curdb = HostApplicationServices.WorkingDatabase;
            //下面的操作选择要复制到新建数据库中的实体
            PromptSelectionOptions opts = new PromptSelectionOptions();
            opts.MessageForAdding = "请输入复制到新文件的实体";
            SelectionSet ss = ed.GetSelection(opts).Value;
            //获取所选实体的ObjectId集合
            ObjectIdCollection ids = new ObjectIdCollection(ss.GetObjectIds());
            //把当前数据库中所选择的实体复制到新建的数据库中，并指定插入点为当前数据库的基点
            db = curdb.Wblock(ids, curdb.Ucsorg);
            //以2004格式保存数据库为Dwg文件
            db.SaveAs("C:\test.dwg", DwgVersion.AC1800);
        }
    }
}
