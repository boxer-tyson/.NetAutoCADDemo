using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using AcadApp=Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.ApplicationServices;
namespace chap21
{
    public partial class CommandTools : UserControl
    {
        public CommandTools()
        {
            InitializeComponent();
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //获取发出移动操作的图片框对象
            PictureBox pictureBox = sender as PictureBox;
            //只执行左键移动操作，以表示进行了拖放操作
            if (System.Windows.Forms.Control.MouseButtons==MouseButtons.Left)
            {
                //图片框对象触发拖放事件，调用拖放操作事件处理函数，并传入图片框对象的Name属性供事件处理函数进行判断
                AcadApp.DoDragDrop(this,pictureBox.Name,DragDropEffects.All,new MyDropTarget());
            }
        }
    }

    public class MyDropTarget : Autodesk.AutoCAD.Windows.DropTarget
    {
        public override void OnDrop(System.Windows.Forms.DragEventArgs e)
        {
            Document doc = AcadApp.DocumentManager.MdiActiveDocument;
            //判断发出托放操作的对象的名字
            switch ((string)e.Data.GetData("Text"))
            {
                //如果是圆图片控件，则画一个圆
                case "pictureBoxCircle":
                    doc.SendStringToExecute("_Circle 100,100 50 ", true, false, true);
                    break;
                //如果是直线图片控件，则画一条直线
                case "pictureBoxLine":
                    doc.SendStringToExecute("_Line 100,100 150,100  ", true, false, true);
                    break;
                //如果是多段线图片控件，则画一个表示三角形的多段线
                case "pictureBoxPolyline":
                    doc.SendStringToExecute("_Pline 100,100 150,100 100,150 100,100  ", true, false, true);
                    break;
                //如果是矩形图片控件，则画一个矩形
                case "pictureBoxRectangle":
                    doc.SendStringToExecute("_Rectangle 50,150 150,50 ", true, false, true);
                    break;
            }
        }
    }
}
