Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
<Assembly: ExtensionApplication(GetType(ManagedApp.Init))> 
<Assembly: CommandClass(GetType(ManagedApp.Commands))> 
Namespace ManagedApp
    Public Class Init
        Implements IExtensionApplication

        Public Sub Initialize() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Initialize
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            '在AutoCAD命令行上显示一些信息，它们会在程序载入时被显示
            ed.WriteMessage("程序开始初始化。")
        End Sub

        Public Sub Terminate() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Terminate
            '在Visual Studio 2005的输出窗口上显示程序结束的信息
            Debug.WriteLine("程序结束，你可以在里做一些程序的清理工作，如关闭AutoCAD文档")
        End Sub
        <CommandMethod("Test")> _
        Public Sub Test()
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            ed.WriteMessage("Test")
        End Sub
    End Class
    Public Class Commands
        <CommandMethod("LoadAssembly")> _
        Public Sub LoadAssembly()
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            'Hello.dll程序集的文件名
            Dim fileName As String = "C:\Hello.dll"
            '载入Hello.dll程序集
            ExtensionLoader.Load(fileName)
            '在命令行上显示信息，提示用户net1_VB.dll程序集已经被载入
            ed.WriteMessage(vbCrLf & fileName & "被载入，请输入Hello进行测试！")
        End Sub
    End Class
End Namespace
