using System;
using System.Diagnostics;
using Autodesk.AutoCAD.Runtime; 
using Autodesk.AutoCAD.ApplicationServices; 
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

[assembly: ExtensionApplication(typeof(ManagedApp.Init))]
[assembly: CommandClass(typeof(ManagedApp.Commands))]
namespace ManagedApp
{
    public class Init : IExtensionApplication
    {

        public void Initialize()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("程序开始初始化。");
        }

        public void Terminate()
        {
            Debug.WriteLine("程序结束，你可以在里做一些程序的清理工作，如关闭AutoCAD文档");
        }

        [CommandMethod("Test")]
        public void Test()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Test");
        }
    }
    public class Commands
    {

        [CommandMethod("LoadAssembly")]
        public void LoadAssembly()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string fileName = "C:\\net1_VB.dll";
            ExtensionLoader.Load(fileName);
            ed.WriteMessage("\n" + fileName + "被载入，请输入Hello进行测试！");
        }
        [CommandMethod("GetVersion")]
        public void GetVersion()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            foreach (DwgVersion ver in db.GetSupportedDxfOutVersions())
            {
                ed.WriteMessage(ver.ToString());
            }
            foreach (DwgVersion ver in db.GetSupportedSaveVersions())
            {
                ed.WriteMessage(ver.ToString());
            }
        }
    }
}
