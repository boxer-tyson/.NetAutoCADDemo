using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace chap18
{
    public class ViewPost
    {
        [CommandMethod("netViewPost")]
        public void CreateViewPost()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                ViewportTable vpt =(ViewportTable) trans.GetObject(db.ViewportTableId, OpenMode.ForWrite);
                string vName = "abc";
                ViewportTableRecord vptr1 = new ViewportTableRecord();
                vptr1.LowerLeftCorner = new Point2d(0, 0);
                vptr1.UpperRightCorner = new Point2d(0.5, 0.5);
                vptr1.Name = vName;
                ViewportTableRecord vptr2 = new ViewportTableRecord();
                vptr2.LowerLeftCorner = new Point2d(0.5, 0);
                vptr2.UpperRightCorner = new Point2d(1, 0.5);
                vptr2.Name = vName;
                ViewportTableRecord vptr3 = new ViewportTableRecord();
                vptr3.LowerLeftCorner = new Point2d(0, 0.5);
                vptr3.UpperRightCorner = new Point2d(0.5, 1);
                vptr3.Name = vName;
                ViewportTableRecord vptr4 = new ViewportTableRecord();
                vptr4.LowerLeftCorner = new Point2d(0.5, 0.5);
                vptr4.UpperRightCorner = new Point2d(1, 1);
                vptr4.Name = vName;
                vpt.Add(vptr1);
                vpt.Add(vptr2);
                vpt.Add(vptr3);
                vpt.Add(vptr4);
                trans.AddNewlyCreatedDBObject(vptr1, true);
                trans.AddNewlyCreatedDBObject(vptr2, true);
                trans.AddNewlyCreatedDBObject(vptr3, true);
                trans.AddNewlyCreatedDBObject(vptr4, true);
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (Application.GetSystemVariable("TILEMODE") == (object)1)
                {
                    doc.SendStringToExecute("-VPORTS 4 ", false, false, false);
                }
                else
                {
                    doc.SendStringToExecute("-VPORTS 4 ", false, false, false);
                }
                trans.Commit();
            }
        }
    }
}


