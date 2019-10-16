using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;

namespace chap21
{
    public class TabbedDialog
    {
        [CommandMethod("CreateNewOptionsTab")]
        public void CreateNewOptionsTab()
        {
            //当DisplayingOptionDialog事件被触发时（即选项对话框显示），运行displayingOptionDialog函数
            Application.DisplayingOptionDialog += new TabbedDialogEventHandler(displayingOptionDialog);
        }

        void displayingOptionDialog(object sender, TabbedDialogEventArgs e)
        {
            //创建一个自定义控件的实例，这里使用的是显示图片的自定义控件
            PictureTab optionTab = new PictureTab();
            //为确定按钮添加动作，也可以为取消、应用、帮助按钮添加动作
            TabbedDialogAction onOkPress =new TabbedDialogAction(OnOptionOK);
            //将显示图片的控件作为标签页添加到选项对话框，并处理确定按钮所引发的动作
            e.AddTab("图片", new TabbedDialogExtension(optionTab, onOkPress));
        }

        void OnOptionOK()
        {
            //当确定按钮被按下时，显示一个警告对话框
            Application.ShowAlertDialog("添加选项对话框标签页成功！");
        }
    }
}
