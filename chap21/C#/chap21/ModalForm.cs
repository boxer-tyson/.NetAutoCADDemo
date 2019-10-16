using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using AcadWnd = Autodesk.AutoCAD.Windows;
using chap20;
using Autodesk.AutoCAD.Internal;
namespace chap21
{
    public partial class ModalForm : Form
    {
        public ModalForm()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            //清空显示块名的下拉列表框中的内容
            this.comboBoxBlockName.Items.Clear();
            //新建一个打开文件对话框，并设置对话框的标题和显示文件类型为dwg或者dxf
            AcadWnd.OpenFileDialog dlg = new AcadWnd.OpenFileDialog("选择图形文件", null, "dwg;dxf", null, AcadWnd.OpenFileDialog.OpenFileDialogFlags.AllowMultiple);
            //如果打开对话框成功
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //显示所选择文件的路径
                this.labelPath.Text = "路径:  " + dlg.Filename;
                //导入所选择文件中的块
                BlockImportClass blockImport = new BlockImportClass();
                blockImport.ImportBlocksFromDwg(dlg.Filename);
                //开始事务处理
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    //打开块表
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    //循环遍历块表中的块表记录
                    foreach (ObjectId blockRecordId in bt)
                    {
                        //打开块表记录对象
                        BlockTableRecord btr = (BlockTableRecord)trans.GetObject(blockRecordId, OpenMode.ForRead);
                        //在下拉列表框中只加入非匿名块和非布局块的名称
                        if (!btr.IsAnonymous && !btr.IsLayout)
                            this.comboBoxBlockName.Items.Add(btr.Name);
                    }
                }
                //在下拉列表框中显示字母顺序排在第一个的块名
                if (this.comboBoxBlockName.Items.Count > 0)
                    this.comboBoxBlockName.Text = this.comboBoxBlockName.Items[0].ToString();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxBlockName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            //获取下拉列表框对象
            ComboBox blockNames = sender as ComboBox;
            //获取下拉列表框中当前选择的块名
            string blockName = blockNames.SelectedItem.ToString();
            //开始事务处理
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //打开块表
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                //打开名字为下拉列表框中当前选择的块名的块
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[blockName], OpenMode.ForRead);
                //利用acmgdinternal.dll中的块浏览器对象来获取块的预览图案
                Bitmap blockThumbnail = BlockThumbnailHelper.GetBlockThumbanail(btr.ObjectId);
                //如果获取块预览图案成功，则设置图片框中的图案为该预览图案
                if (blockThumbnail != null)
                    this.pictureBoxBlock.Image = blockThumbnail;
                //根据当前选择的块，设置块的单位和比例
                switch (btr.Units)
                {
                    case UnitsValue.Inches:
                        this.textBoxBlockUnit.Text = "英寸";
                        this.textBoxBlockRatio.Text = "25.4";
                        break;
                    case UnitsValue.Meters:
                        this.textBoxBlockUnit.Text = "米";
                        this.textBoxBlockRatio.Text = "1000";
                        break;
                    case UnitsValue.Millimeters:
                        this.textBoxBlockUnit.Text = "毫米";
                        this.textBoxBlockRatio.Text = "1";
                        break;
                    case UnitsValue.Undefined:
                        this.textBoxBlockUnit.Text = "无单位";
                        this.textBoxBlockRatio.Text = "1";
                        break;
                    default:
                        this.textBoxBlockUnit.Text = btr.Units.ToString();
                        this.textBoxBlockRatio.Text = "";
                        break;
                }
            }
        }

        private void checkBoxSameScale_CheckedChanged(object sender, EventArgs e)
        {
            //获取检查框对象
            CheckBox uniformScale = sender as CheckBox;
            //如果检查框对象处于选中状态
            if (uniformScale.Checked == true)
            {
                //Y、Z方向的缩放比例与X一致
                this.textBoxScaleY.Text = this.textBoxScaleX.Text;
                this.textBoxScaleZ.Text = this.textBoxScaleX.Text;
                //Y、Z方向的缩放比例设置文本框不能输入
                this.textBoxScaleY.Enabled = false;
                this.textBoxScaleZ.Enabled = false;
            }
            else
            {
                //Y、Z方向的缩放比例设置文本框可以输入
                this.textBoxScaleY.Enabled = true;
                this.textBoxScaleZ.Enabled = true;
            }
        }

        private void textBoxScaleX_TextChanged(object sender, EventArgs e)
        {
            //如果检查框处于选中状态，则Y、Z方向的缩放比例与X方向一致
            if (this.checkBoxSameScale.Checked == true)
            {
                this.textBoxScaleY.Text = this.textBoxScaleX.Text;
                this.textBoxScaleZ.Text = this.textBoxScaleX.Text;
            }
        }

        private void checkBoxScale_CheckedChanged(object sender, EventArgs e)
        {
            //获取检查框对象
            CheckBox bchecked = sender as CheckBox;
            //如果检查框对象处于选中状态，则不能在设置缩放比例的文本框输入
            if (bchecked.Checked == true)
            {
                this.textBoxScaleX.Enabled = false;
                this.textBoxScaleY.Enabled = false;
                this.textBoxScaleZ.Enabled = false;
            }
            //否则，可以在设置缩放比例的文本框输入
            else
            {
                this.textBoxScaleX.Enabled = true;
                this.textBoxScaleY.Enabled = true;
                this.textBoxScaleZ.Enabled = true;
            }
        }

        private void checkBoxInsertPoint_CheckedChanged(object sender, EventArgs e)
        {
            //获取检查框对象
            CheckBox bchecked = sender as CheckBox;
            //如果检查框对象处于选中状态，则不能在设置插入点的文本框输入
            if (bchecked.Checked == true)
            {
                this.textBoxInsertPointX.Enabled = false;
                this.textBoxInsertPointY.Enabled = false;
                this.textBoxInsertPointZ.Enabled = false;
            }
            //否则，可以在设置插入点的文本框输入
            else
            {
                this.textBoxInsertPointX.Enabled = true;
                this.textBoxInsertPointY.Enabled = true;
                this.textBoxInsertPointZ.Enabled = true;
            }
        }

        private void checkBoxRotate_CheckedChanged(object sender, EventArgs e)
        {
            //获取检查框对象
            CheckBox bchecked = sender as CheckBox;
            //如果检查框对象处于选中状态，则不能在设置旋转角度的文本框输入
            if (bchecked.Checked == true)
            {
                this.textBoxRotateAngle.Enabled = false;
            }
            //否则，可以在设置旋转角度的文本框输入
            else
            {
                this.textBoxRotateAngle.Enabled = true;
            }
        }

        private void ModalForm_Load(object sender, EventArgs e)
        {
            //初始状态下图形中没有块，所以显示为无单位字样和比例为1
            this.textBoxBlockUnit.Text = "无单位";
            this.textBoxBlockRatio.Text = "1";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //获取块参照插入点坐标
            double insertPointX = Convert.ToDouble(this.textBoxInsertPointX.Text);
            double insertPointY = Convert.ToDouble(this.textBoxInsertPointY.Text);
            double insertPointZ = Convert.ToDouble(this.textBoxInsertPointZ.Text);
            Point3d insertPoint = new Point3d(insertPointX, insertPointY, insertPointZ);
            //获取块参照的缩放比例
            double scaleX = Convert.ToDouble(this.textBoxScaleX.Text);
            double scaleY = Convert.ToDouble(this.textBoxScaleY.Text);
            double scaleZ = Convert.ToDouble(this.textBoxScaleZ.Text);
            Scale3d scale = new Scale3d(scaleX, scaleY, scaleZ);
            //获取块参照的旋转角度
            double rotationAngle = Convert.ToDouble(this.textBoxRotateAngle.Text);
            //关闭窗体
            this.Close();
            //插入块参照
            MyBlock myBlock = new MyBlock();
            myBlock.InsertBlockRef(this.comboBoxBlockName.Text, insertPoint, scale, rotationAngle);
        }
    }
}

