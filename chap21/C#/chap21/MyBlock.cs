using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
namespace chap20
{
    public class MyBlock
    {
        [CommandMethod("CB")]
        public ObjectId CreateBlock()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId blockId = ObjectId.Null;//用于返回所创建的块的对象Id
            BlockTableRecord record = new BlockTableRecord();//创建一个BlockTableRecord类的对象，表示所要创建的块
            record.Name = "Room";//设置块名            
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Point3dCollection points = new Point3dCollection();//用于表示组成块的多段线的顶点
                points.Add(new Point3d(-18.0, -6.0, 0.0));
                points.Add(new Point3d(18.0, -6.0, 0.0));
                points.Add(new Point3d(18.0, 6.0, 0.0));
                points.Add(new Point3d(-18.0, 6.0, 0.0));
                Polyline2d pline = new Polyline2d(Poly2dType.SimplePoly, points, 0.0, true, 0.0, 0.0, null);//创建组成块的多段线
                record.Origin = points[3];//设置块的基点
                record.AppendEntity(pline);//将多段线加入到新建的BlockTableRecord对象
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);//以写的方式打开块表
                if (!bt.Has("Room"))//判断是否存在名为"Room"的块
                {
                    blockId = bt.Add(record);//在块表中加入"Room"块
                    trans.AddNewlyCreatedDBObject(record, true);//通知事务处理
                    trans.Commit();//提交事务
                }
            }
            return blockId;//返回创建的块的对象Id
        }
        [CommandMethod("CBWA")]
        public ObjectId CreateBlockWithAttributes()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId blockId = ObjectId.Null;
            BlockTableRecord record = new BlockTableRecord();
            record.Name = "RMNUM";
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                Point3dCollection points = new Point3dCollection();
                points.Add(new Point3d(-18.0, -6.0, 0.0));
                points.Add(new Point3d(18.0, -6.0, 0.0));
                points.Add(new Point3d(18.0, 6.0, 0.0));
                points.Add(new Point3d(-18.0, 6.0, 0.0));
                Polyline2d pline = new Polyline2d(Poly2dType.SimplePoly, points, 0.0, true, 0.0, 0.0, null);
                record.AppendEntity(pline);
                AttributeDefinition attdef = new AttributeDefinition();
                attdef.Position = new Point3d(0.0, 0.0, 0.0);
                attdef.Height = 8.0;//设置文字高度  
                attdef.Rotation = 0.0;//设置文字旋转角度  
                attdef.HorizontalMode = TextHorizontalMode.TextMid;//设置水平对齐方式 
                attdef.VerticalMode = TextVerticalMode.TextVerticalMid;//设置垂直对齐方式 
                attdef.Prompt = "Room Number:";//设置属性提示 
                attdef.TextString = "0000";//设置属性的缺省值  
                attdef.Tag = "NUMBER";//设置属性标签  
                attdef.Invisible = false;//设置不可见选项为假  
                attdef.Verifiable = false;//设置验证方式为假  
                attdef.Preset = false;//设置预置方式为假 
                attdef.Constant = false;//设置常数方式为假
                record.Origin = points[3];
                record.AppendEntity(attdef);
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);
                if (!bt.Has("RMNUM")) //判断是否存在名为"RMNUM"的块
                {
                    blockId = bt.Add(record); //在块表中加入"RMNUM"块
                    trans.AddNewlyCreatedDBObject(record, true);
                    trans.Commit();
                }
            }
            return blockId;
        }
        [CommandMethod("InsertBlock")]
        public void InsertBlock()
        {
            //插入一个不带属性的简单块"Room"
            InsertBlockRef("Room", new Point3d(100, 100, 0), new Scale3d(2), 0);
        }

        public void InsertBlockRef(string blockName, Point3d point, Scale3d scale, double rotateAngle)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //以读的方式打开块表
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                //如果没有blockName表示的块，则程序返回
                if (!bt.Has(blockName))
                {
                    return;
                }
                //以读的方式打开blockName表示的块
                BlockTableRecord block = (BlockTableRecord)trans.GetObject(bt[blockName], OpenMode.ForRead);
                //创建一个块参照并设置插入点
                BlockReference blockRef = new BlockReference(point, bt[blockName]);
                blockRef.ScaleFactors = scale;//设置块参照的缩放比例
                blockRef.Rotation = rotateAngle;//设置块参照的旋转角度
                //以写的方式打开当前空间（模型空间或图纸空间）
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                btr.AppendEntity(blockRef);//在当前空间加入创建的块参照
                //通知事务处理加入创建的块参照
                trans.AddNewlyCreatedDBObject(blockRef, true);
                trans.Commit();//提交事务处理以实现块参照的真实加入
            }
        }

        [CommandMethod("InsertBlockWithAtt")]
        public void InsertBlockWithAtt()
        {
            //插入一个带属性的块"RMNUM"
            InsertBlockRefWithAtt("RMNUM", new Point3d(100, 150, 0), new Scale3d(1), 0.5 * Math.PI, "2007");
            //插入一个不带属性的简单块"Room"
            InsertBlockRefWithAtt("Room", new Point3d(100, 200, 0), new Scale3d(1), 0, null);
        }
        public void InsertBlockRefWithAtt(string blockName, Point3d point, Scale3d scale, double rotateAngle, string roomnumber)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                if (!bt.Has(blockName))
                {
                    return;
                }
                BlockTableRecord blockwithatt = (BlockTableRecord)trans.GetObject(bt[blockName], OpenMode.ForRead);
                BlockReference blockRef = new BlockReference(point, bt[blockName]);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                blockRef.ScaleFactors = scale;
                blockRef.Rotation = rotateAngle;
                btr.AppendEntity(blockRef);
                trans.AddNewlyCreatedDBObject(blockRef, true);
                //获取blockName块的遍历器，以实现对块中对象的访问
                BlockTableRecordEnumerator iterator = blockwithatt.GetEnumerator();
                //如果blockName块包含属性
                if (blockwithatt.HasAttributeDefinitions)
                {
                    //利用块遍历器对块中的对象进行遍历
                    while (iterator.MoveNext())
                    {
                        //获取块遍历器当前指向的块中的对象
                        AttributeDefinition attdef = trans.GetObject(iterator.Current, OpenMode.ForRead) as AttributeDefinition;
                        //定义一个新的属性参照对象
                        AttributeReference att = new AttributeReference();
                        //判断块遍历器当前指向的块中的对象是否为属性定义
                        if (attdef != null)
                        {
                            //从属性定义对象中继承相关的属性到属性参照对象中
                            att.SetAttributeFromBlock(attdef, blockRef.BlockTransform);
                            //设置属性参照对象的位置为属性定义的位置+块参照的位置
                            att.Position = attdef.Position + blockRef.Position.GetAsVector();
                            //判断属性定义的名称
                            switch (attdef.Tag)
                            {
                                //如果为"NUMBER"，则设置块参照的属性值
                                case "NUMBER":
                                    att.TextString = roomnumber;
                                    break;
                            }
                            //判断块参照是否可写，如不可写，则切换为可写状态
                            if (!blockRef.IsWriteEnabled)
                            {
                                blockRef.UpgradeOpen();
                            }
                            //添加新创建的属性参照
                            blockRef.AttributeCollection.AppendAttribute(att);
                            //通知事务处理添加新创建的属性参照
                            trans.AddNewlyCreatedDBObject(att, true);
                        }
                    }
                }
                trans.Commit();//提交事务处理
            }
        }

        [CommandMethod("BrowseBlock")]
        public void BrowseBlock()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            //只选择块参照对象
            TypedValue[] filterValues = { new TypedValue((int)DxfCode.Start, "INSERT") };
            SelectionFilter filter = new SelectionFilter(filterValues);
            PromptSelectionOptions opts = new PromptSelectionOptions();
            //提示用户进行选择
            opts.MessageForAdding = "请选择图形中的块对象";
            //根据选择过滤器选择对象，本例中为块参照对象
            PromptSelectionResult res = ed.GetSelection(opts, filter);
            //如果选择失败，则返回
            if (res.Status != PromptStatus.OK)
                return;
            //获取选择集对象，表示所选择的对象集合
            SelectionSet ss = res.Value;
            //获取选择集中包含的对象ID，用于访问选择集中的对象
            ObjectId[] ids = ss.GetObjectIds();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //循环遍历选择集中的对象
                foreach (ObjectId blockId in ids)
                {
                    //获取选择集中当前的对象，由于是只选择块参照对象，所以直接赋值为块对象
                    BlockReference blockRef = (BlockReference)trans.GetObject(blockId, OpenMode.ForRead);
                    //获取当前块参照对象所属的块表记录对象
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(blockRef.BlockTableRecord, OpenMode.ForRead);
                    //在命令行上显示块名
                    ed.WriteMessage("\n块：" + btr.Name);
                    //销毁块表记录对象
                    btr.Dispose();
                    //获取块参照的属性集合对象
                    AttributeCollection atts = blockRef.AttributeCollection;
                    //循环遍历属性集合对象
                    foreach (ObjectId attId in atts)
                    {
                        //获取当前的块参照属性
                        AttributeReference attRef = (AttributeReference)trans.GetObject(attId, OpenMode.ForRead);
                        //获取块参照属性的属性名和属性值
                        string attString = " 属性名：" + attRef.Tag + " 属性值：" + attRef.TextString;
                        //在命令行上显示块的属性名和属性值
                        ed.WriteMessage(attString);
                    }
                }
            }
        }

        [CommandMethod("ChangeBlockAtt")]
        public void ChangeBlockAtt()
        {
            string roomNumber = "2008";
            ChangeBlock("RMNUM", roomNumber);
        }

        public void ChangeBlock(string blockName, string roomNumber)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //打开块表
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                //打开块表中名为blockName的块表记录
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[blockName], OpenMode.ForRead);
                //获取所有名为blockName的块参照
                ObjectIdCollection blcokRefIds = btr.GetBlockReferenceIds(true, true);
                //循环遍历块参照
                foreach (ObjectId blockRefId in blcokRefIds)
                {
                    //打开当前块参照
                    BlockReference blockRef = (BlockReference)trans.GetObject(blockRefId, OpenMode.ForRead);
                    //获取当前块参照的属性集合
                    AttributeCollection blockRefAtts = blockRef.AttributeCollection;
                    //循环遍历属性集合
                    foreach (ObjectId attId in blockRefAtts)
                    {
                        //获取当前属性参照对象
                        AttributeReference attRef = (AttributeReference)trans.GetObject(attId, OpenMode.ForRead);
                        //只改变NUMBER属性值为"0000"的属性值为roomNumber
                        switch (attRef.Tag)
                        {
                            case "NUMBER":
                                if (attRef.TextString == "0000")
                                {
                                    attRef.UpgradeOpen();//切换属性参照对象为可写状态
                                    attRef.TextString = roomNumber;
                                }
                                break;
                        }
                    }
                }
                trans.Commit();
            }
        }
    }
}



