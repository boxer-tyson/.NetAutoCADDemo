Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Interop
Public Class MyMenu
<CommandMethod("AddMenu")> _
Public Sub AddMenu()
    'COM方式获取AutoCAD应用程序对象
    Dim acadApp As AcadApplication = Application.AcadApplication
    '为AutoCAD添加一个新的菜单，并设置标题为"我的菜单"
    Dim pm As AcadPopupMenu = acadApp.MenuGroups.Item(0).Menus.Add("我的菜单")
    '声明一个AutoCAD弹出菜单项，用于获取添加的菜单项对象
    Dim pmi As AcadPopupMenuItem
    '在新建的菜单中添加一个名为"圆"的菜单项，以调用绘制圆命令
    pmi = pm.AddMenuItem(pm.Count + 1, "圆", "_Circle ")
    '设置状态栏提示信息
    pmi.HelpString = "用指定半径创建圆"
    '添加名为"直线"的菜单项，以调用绘制直线命令
    pmi = pm.AddMenuItem(pm.Count + 1, "直线", "_Line ")
    '设置状态栏提示信息
    pmi.HelpString = "创建直线段"
    '添加名为"多段线"的菜单项，以调用绘制多段线命令
    pmi = pm.AddMenuItem(pm.Count + 1, "多段线", "_Polyline ")
    '设置状态栏提示信息
    pmi.HelpString = "创建二维多段线"
    '添加名为"矩形"的菜单项，以调用绘制矩形多段线命令
    pmi = pm.AddMenuItem(pm.Count + 1, "矩形", "_Rectangle ")
    '设置状态栏提示信息
    pmi.HelpString = "创建矩形多段线"
    '添加一个分隔条以区分不同类型的命令
    pm.AddSeparator(pm.Count + 1)
    '添加一个名为"修改"的子菜单
    Dim menuModify As AcadPopupMenu = pm.AddSubMenu(pm.Count + 1, "修改")
    '在"修改"子菜单下添加用于复制、删除、移动及旋转操作的菜单项，并设置相应的状态栏提示信息
    pmi = menuModify.AddMenuItem(menuModify.Count + 1, "复制", "_Copy ")
    pmi.HelpString = "复制对象"
    pmi = menuModify.AddMenuItem(menuModify.Count + 1, "删除", "_Erase ")
    pmi.HelpString = "从图形删除对象"
    pmi = menuModify.AddMenuItem(menuModify.Count + 1, "移动", "_Move ")
    pmi.HelpString = "移动对象"
    pmi = menuModify.AddMenuItem(menuModify.Count + 1, "旋转", "_Rotate ")
    pmi.HelpString = "绕基点旋转对象"
    '将定义的菜单显示在AutoCAD菜单栏的最后
    pm.InsertInMenuBar(acadApp.MenuBar.Count + 1)
End Sub
End Class
