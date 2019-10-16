using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
namespace chap21
{
    public partial class ModifyTools : UserControl
    {
        public ModifyTools()
        {
            InitializeComponent();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            //获取发出点击事件的按钮对象
            Button button = sender as Button;
            Document doc = AcadApp.DocumentManager.MdiActiveDocument;
            //根据按钮对象的名字，向AutoCAD命令行发出对应的命令
            switch (button.Name)
            {
                case "buttonCopy":
                    doc.SendStringToExecute("_Copy ", true, false, true);
                    break;
                case "buttonErase":
                    doc.SendStringToExecute("_Erase ", true, false, true);
                    break;
                case "buttonMove":
                    doc.SendStringToExecute("_Move ", true, false, true);
                    break;
                case "buttonRotate":
                    doc.SendStringToExecute("_Rotate ", true, false, true);
                    break;
            }
        }
    }
}
