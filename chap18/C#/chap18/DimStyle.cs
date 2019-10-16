using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace chap18
{
    public class DimStyle
    {
        public ObjectId ISO25(String dimStyleName)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            ObjectId dimstyleId;
            try
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    DimStyleTable dt = (DimStyleTable)trans.GetObject(db.DimStyleTableId, OpenMode.ForWrite);
                    // 新建一个标注样式表记录.
                    DimStyleTableRecord dtr = new DimStyleTableRecord();
                    // 换算精度
                    dtr.Dimaltd = 3;
                    // 换算比例因子
                    dtr.Dimaltf = 0.03937008;
                    // 换算公差精度
                    dtr.Dimalttd = 3;
                    // 箭头大小
                    dtr.Dimasz = 2.5;
                    // 圆心标记大小
                    dtr.Dimcen = 2.5;
                    // 精度
                    dtr.Dimdec = 2;
                    // 尺寸线间距
                    dtr.Dimdli = 3.75;
                    // 小数分隔符
                    dtr.Dimdsep = ',';
                    //尺寸界线超出量
                    dtr.Dimexe = 1.25;
                    // 尺寸界线偏移
                    dtr.Dimexo = 0.625;
                    // 文字偏移
                    dtr.Dimgap = 0.625;
                    // 文字位置垂直
                    dtr.Dimtad = 1;
                    // 公差精度
                    dtr.Dimtdec = 2;
                    // 文字在内对齐
                    dtr.Dimtih = false;
                    // 尺寸线强制
                    dtr.Dimtofl = true;
                    // 文字外部对齐
                    dtr.Dimtoh = false;
                    // 公差位置垂直
                    dtr.Dimtolj = 0;
                    // 文字高度
                    dtr.Dimtxt = 2.5;
                    // 公差消零
                    dtr.Dimtzin = 8;
                    // 消零
                    dtr.Dimzin = 8;
                    //设置标注样式名称.
                    dtr.Name = dimStyleName;
                    dimstyleId = dt.Add(dtr);
                    trans.AddNewlyCreatedDBObject(dtr, true);
                    trans.Commit();
                }
                return dimstyleId;
            }
            catch
            {
                ObjectId NullId = ObjectId.Null;
                return NullId;
            }
        }

        [CommandMethod("netdimStyle")]
        public void CreatedimStyle()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                DimStyleTable dt = (DimStyleTable)trans.GetObject(db.DimStyleTableId, OpenMode.ForWrite);
                String dimName = "abc";
                ObjectId dimId = ISO25(dimName);
                if (dimId != ObjectId.Null)
                {
                    DimStyleTableRecord dtr = (DimStyleTableRecord)trans.GetObject(dimId, OpenMode.ForWrite);
                    // 修改箭头大小.
                    dtr.Dimasz = 3;
                }
                else
                {
                    ed.WriteMessage("\n标注样式 " + dimName + " 已存在！");
                }
                trans.Commit();
            }
        }
    }
}


