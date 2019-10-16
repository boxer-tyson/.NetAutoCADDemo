Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Windows
Imports AcadApp = Autodesk.AutoCAD.ApplicationServices.Application
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry

Public Class ToolPaletteSample
   Private Shared ps As PaletteSet

   <CommandMethod("ShowPalette")> _
   Public Sub ShowPalette()
      '如果面板还没有被创建
      If ps Is Nothing Then
        '新建一个面板对象，标题为"工具面板"
        ps = New PaletteSet("工具面板")
        '设置面板的最小尺寸为控件的尺寸
        ps.MinimumSize = New System.Drawing.Size(150, 240)
        '添加命令工具面板项
        ps.Add("命令工具", New CommandTools())
            '添加修改工具面板项
            ps.Add("修改工具", New ModifyTools())
      End If
         '获取命令行编辑器对象，主要是为了坐标转换用
         Dim ed As Editor = AcadApp.DocumentManager.MdiActiveDocument.Editor
         '在设置停靠属性之前，必须让面板可见，否则总是停靠在窗口的左侧
         ps.Visible = True
         '设置面板不停靠在窗口的任一边
         ps.Dock = DockSides.None
         '设置面板开始的位置
         Dim pt As New Point3d(400, 800, 0)
         '将Point3d的值转换为System.Point的值，即将AutoCAD的点坐标转换为屏幕坐标，再将该屏幕坐标值设置为面板的初始位置
         ps.Location = ed.PointToScreen(pt, 0)
         '设置面板为半透明状
        ps.Opacity = 50
   End Sub

   <CommandMethod("DockRight")> _
   Public Sub DockRight()
      '判断面板是否被创建
      If Not (ps Is Nothing) Then
         '面板停靠在窗口的右侧
         ps.Dock = DockSides.Right
      End If
   End Sub
End Class
