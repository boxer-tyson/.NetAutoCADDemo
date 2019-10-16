using System;
using System.Collections.Generic;
using System.Text;

namespace Hello
{
    using Autodesk.AutoCAD.Runtime;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.EditorInput;
    public class Class1
    {

        [CommandMethod("Hello",CommandFlags.Modal)]
        public void HelloWorld()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("欢迎进入.NET开发AutoCAD的世界！");
        }
    }
}
