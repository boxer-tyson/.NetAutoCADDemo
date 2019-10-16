Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Windows
Imports Autodesk.AutoCAD.ApplicationServices

Public Class AppPane

<CommandMethod("CreateAppPane")> _
Public Sub AddApplicationPane()
    '定义一个程序窗格对象
    Dim appPaneButton As New Pane
    '设置窗格的属性
    appPaneButton.Enabled = True
    appPaneButton.Visible = True
    '设置窗格初始状态是弹出的
    appPaneButton.Style = PaneStyles.Normal
    '设置窗格的标题
    appPaneButton.Text = "程序窗格"
    '显示窗格的提示信息
    appPaneButton.ToolTipText = "欢迎进行入.net的世界！"
    '添加MouseDown事件，当鼠标被按下时运行
    AddHandler appPaneButton.MouseDown, AddressOf OnAppMouseDown
    '把窗格添加到AutoCAD的状态栏区域
    Application.StatusBar.Panes.Add(appPaneButton)
End Sub

Sub OnAppMouseDown(ByVal sender As Object, ByVal e As StatusBarMouseDownEventArgs)
    '获取窗格按钮对象
    Dim paneButton As Pane = CType(sender, Pane)
    Dim alertMessage As String
    '如果点击的不是鼠标左键，则返回
    If e.Button <> Windows.Forms.MouseButtons.Left Then
        Return
    End If
    '切换窗格按钮的状态
    If paneButton.Style = PaneStyles.PopOut Then '如果窗格按钮是弹出的，则切换为凹进
        paneButton.Style = PaneStyles.Normal
        alertMessage = "程序窗格按钮被按下"
    Else
        paneButton.Style = PaneStyles.PopOut
        alertMessage = "程序窗格按钮没有被按下。"
    End If
    '更新状态栏以反映窗格按钮的状态变化
    Application.StatusBar.Update()
    '显示反映窗格按钮变化的信息
    Application.ShowAlertDialog(alertMessage)
End Sub
End Class
