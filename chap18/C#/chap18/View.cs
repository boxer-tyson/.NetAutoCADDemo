using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Internal;

namespace chap18
{
    public class View
    {
        [CommandMethod("netViewScale")]
        public void testViewScale()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                ViewTable vt = (ViewTable)trans.GetObject(db.ViewTableId, OpenMode.ForWrite);
                ViewTableRecord curVtr = ed.GetCurrentView();
                Point2d cen = curVtr.CenterPoint;
                double width = curVtr.Width;
                double height = curVtr.Height;
                ViewTableRecord newVtr = curVtr;
                newVtr.Name = "newView";
                newVtr.Width = width / 2;
                newVtr.Height = height / 2;
                ed.SetCurrentView(newVtr);
                trans.Commit();
            }
        }

        [CommandMethod("netZoom")]
        public void testZoom()
        {
            //·¶Î§Ëõ·Å
            Utils.ZoomObjects(true);
        }
    }
}


