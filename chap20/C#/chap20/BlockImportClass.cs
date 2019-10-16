using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
namespace chap20
{
    public class BlockImportClass
    {
        [CommandMethod("ImportBlocks")]
        public void ImportBlocks()
        {
            //外部文件名
            string filename="C:\\Blocks and Tables - Metric.dwg";
            //从所指定的外部文件中导入块
            ImportBlocksFromDwg(filename);
        }

        public void ImportBlocksFromDwg(string sourceFileName)
        {
            DocumentCollection dm = Application.DocumentManager;
            Editor ed = dm.MdiActiveDocument.Editor;
            //获取当前数据库作为目标数据库
            Database destDb = dm.MdiActiveDocument.Database;
            //创建一个新的数据库对象，作为源数据库，以读入外部文件中的对象
            Database sourceDb = new Database(false, true);
            try
            {
                //把DWG文件读入到一个临时的数据库中
                sourceDb.ReadDwgFile(sourceFileName, System.IO.FileShare.Read, true, null);
                //创建一个变量用来存储块的ObjectId列表
                ObjectIdCollection blockIds = new ObjectIdCollection();
                //获取源数据库的事务处理管理器
                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = sourceDb.TransactionManager;
                //在源数据库中开始事务处理
                using (Transaction myT = tm.StartTransaction())
                {
                    //打开源数据库中的块表
                    BlockTable bt = (BlockTable)tm.GetObject(sourceDb.BlockTableId, OpenMode.ForRead, false);
                    //遍历每个块
                    foreach (ObjectId btrId in bt)
                    {
                        BlockTableRecord btr = (BlockTableRecord)tm.GetObject(btrId, OpenMode.ForRead, false);
                        //只加入命名块和非布局块到复制列表中
                        if (!btr.IsAnonymous && !btr.IsLayout)
                        {
                            blockIds.Add(btrId);
                        }
                        btr.Dispose();
                    }
                    bt.Dispose();
                }
                //定义一个IdMapping对象
                IdMapping mapping = new IdMapping();
                //从源数据库向目标数据库复制块表记录
                sourceDb.WblockCloneObjects(blockIds, destDb.BlockTableId, mapping, DuplicateRecordCloning.Replace, false);
                //'复制完成后，命令行显示复制了多少个块的信息
                ed.WriteMessage("复制了 " + blockIds.Count.ToString() + " 个块，从 " + sourceFileName + " 到当前图形");
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage("\nError during copy: " + ex.Message);
            }
            //操作完成，销毁源数据库
            sourceDb.Dispose();
        }
    }
}
