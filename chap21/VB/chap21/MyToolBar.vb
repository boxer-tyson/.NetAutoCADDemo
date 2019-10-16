Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Interop
Imports Autodesk.AutoCAD.ApplicationServices

Public Class MyToolBar

<CommandMethod("AddToolBar")> _
Public Sub AddToolBar()
    '获取当前运行的程序集
    Dim myModule As System.Reflection.Module = System.Reflection.Assembly.GetExecutingAssembly().GetModules()(0)
    '获取当前运行的程序集的完整路径（包含文件名）
    Dim modulePath As String = myModule.FullyQualifiedName
    '获取去除文件名后程序集的路径，即程序集所在的文件夹
    modulePath = modulePath.Substring(0, modulePath.LastIndexOf("\"))
    'COM方式获取AutoCAD应用程序对象
    Dim acadApp As AcadApplication = Application.AcadApplication
    '获取当前菜单组，用于加入工具栏
    Dim currMenuGroup As AcadMenuGroup = acadApp.MenuGroups.Item(0)
    '为AutoCAD添加一个新的工具栏，并设置标题为"我的工具栏"
    Dim tbModify As AcadToolbar = currMenuGroup.Toolbars.Add("我的工具栏")
    '在新建的工具栏中添加一个"复制"按钮，以调用复制命令
    Dim button0 As AcadToolbarItem = tbModify.AddToolbarButton("", "复制", "复制对象", "_Copy ")
    '设置复制按钮的图片
    button0.SetBitmaps(modulePath + "\Resources\Copy.bmp", modulePath + "\Resources\Copy.bmp")
    '添加一个"删除"按钮，以调用删除命令
    Dim button1 As AcadToolbarItem = tbModify.AddToolbarButton("", "删除", "从图形删除对象", "_Erase ")
    '设置删除按钮的图片
    button1.SetBitmaps(modulePath + "\Resources\Erase.bmp", modulePath + "\Resources\Erase.bmp")
    '添加一个"移动"按钮，以调用删除命令
    Dim button2 As AcadToolbarItem = tbModify.AddToolbarButton("", "移动", "移动对象", "_Move ")
    '设置移动按钮的图片
    button2.SetBitmaps(modulePath + "\Resources\Move.bmp", modulePath + "\Resources\Move.bmp")
    '添加一个"旋转"按钮，以调用旋转命令
    Dim button3 As AcadToolbarItem = tbModify.AddToolbarButton("", "旋转", "绕基点旋转对象", "_Rotate ")
    '设置旋转按钮的图片
    button3.SetBitmaps(modulePath + "\Resources\Rotate.bmp", modulePath + "\Resources\Rotate.bmp")

    '添加一个弹出按钮，该按钮只用来附着下面的画图工具栏
    Dim FlyoutButton As AcadToolbarItem = tbModify.AddToolbarButton("", "画图工具", "画图工具", " ", True)
    '创建第二个工具栏。该工具栏将通过弹出按钮附加到第一个工具栏。
    Dim tbDraw As AcadToolbar = currMenuGroup.Toolbars.Add("画图工具栏")
    '下面的语句分别在工具栏上设置绘制圆、直线、多段线、矩形的按钮
    Dim button4 As AcadToolbarItem = tbDraw.AddToolbarButton("", "圆", "用指定半径创建圆", "_Circle ")
    button4.SetBitmaps(modulePath + "\Resources\Circle.bmp", modulePath + "\Resources\Circle.bmp")
    Dim button5 As AcadToolbarItem = tbDraw.AddToolbarButton("", "直线", "创建直线段", "_Line ")
    button5.SetBitmaps(modulePath + "\Resources\Line.bmp", modulePath + "\Resources\Line.bmp")
    Dim button6 As AcadToolbarItem = tbDraw.AddToolbarButton("", "多段线", "创建二维多段线", "_Pline ")
    button6.SetBitmaps(modulePath + "\Resources\Polyline.bmp", modulePath + "\Resources\Polyline.bmp")
    Dim button7 As AcadToolbarItem = tbDraw.AddToolbarButton("", "矩形", "创建矩形多段线", "_Rectangle ")
    button7.SetBitmaps(modulePath + "\Resources\Rectangle.bmp", modulePath + "\Resources\Rectangle.bmp")
    '将第二个工具栏附着到第一个工具栏的弹出按钮上
    FlyoutButton.AttachToolbarToFlyout(currMenuGroup.Name, tbDraw.Name)
    '显示第一个工具栏
    tbModify.Visible = True
    '隐藏第二个工具栏
    tbDraw.Visible = False
End Sub
End Class
