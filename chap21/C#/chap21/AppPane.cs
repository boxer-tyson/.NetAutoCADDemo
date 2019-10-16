using System;

using Autodesk.AutoCAD.Runtime;

using Autodesk.AutoCAD.Windows;

using Autodesk.AutoCAD.ApplicationServices;

namespace chap21
{
    public class AppPane
    {
        [CommandMethod("CreateAppPane")]
        public void AddApplicationPane()
        {
            //定义一个程序窗格对象
            Pane appPaneButton = new Pane();
            //设置窗格的属性
            appPaneButton.Enabled = true;
            appPaneButton.Visible = true;
            //设置窗格初始状态是弹出的
            appPaneButton.Style = PaneStyles.Normal;
            //设置窗格的标题
            appPaneButton.Text = "程序窗格";
            //显示窗格的提示信息
            appPaneButton.ToolTipText = "欢迎进行入.net的世界！";
            //添加MouseDown事件，当鼠标被按下时运行
            appPaneButton.MouseDown += new StatusBarMouseDownEventHandler(OnAppMouseDown);
            //把窗格添加到AutoCAD的状态栏区域
            Application.StatusBar.Panes.Add(appPaneButton);
        }

        static void OnAppMouseDown(object sender, StatusBarMouseDownEventArgs e)
        {
            //获取窗格按钮对象
            Pane paneButton = (Pane)sender;
            string alertMessage;
            //如果点击的不是鼠标左键，则返回
            if (e.Button!=System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }
            //切换窗格按钮的状态
            if (paneButton.Style == PaneStyles.PopOut)//如果窗格按钮是弹出的，则切换为凹进
            {
                paneButton.Style = PaneStyles.Normal;
                alertMessage = "程序窗格按钮被按下";
            }
            else
            {
                paneButton.Style = PaneStyles.PopOut;
                alertMessage = "程序窗格按钮没有被按下";
            }
            //更新状态栏以反映窗格按钮的状态变化
            Application.StatusBar.Update();
            //显示反映窗格按钮变化的信息
            Application.ShowAlertDialog(alertMessage);
        }
    }
}
