namespace chap21
{
    partial class ModalForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxBlock = new System.Windows.Forms.PictureBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxBlockRatio = new System.Windows.Forms.TextBox();
            this.textBoxBlockUnit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxRotateAngle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxRotate = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxScaleY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxScaleZ = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxScaleX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxSameScale = new System.Windows.Forms.CheckBox();
            this.checkBoxScale = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxInsertPointY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxInsertPointZ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxInsertPointX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxInsertPoint = new System.Windows.Forms.CheckBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.comboBoxBlockName = new System.Windows.Forms.ComboBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlock)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxBlock
            // 
            this.pictureBoxBlock.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxBlock.Location = new System.Drawing.Point(380, 16);
            this.pictureBoxBlock.Name = "pictureBoxBlock";
            this.pictureBoxBlock.Size = new System.Drawing.Size(75, 47);
            this.pictureBoxBlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxBlock.TabIndex = 32;
            this.pictureBoxBlock.TabStop = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(303, 226);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 31;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(389, 226);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonHelp.TabIndex = 29;
            this.buttonHelp.Text = "帮助(&H)";
            this.buttonHelp.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(217, 227);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 30;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxBlockRatio);
            this.groupBox4.Controls.Add(this.textBoxBlockUnit);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(334, 148);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(130, 72);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "块单位";
            // 
            // textBoxBlockRatio
            // 
            this.textBoxBlockRatio.Location = new System.Drawing.Point(46, 44);
            this.textBoxBlockRatio.Name = "textBoxBlockRatio";
            this.textBoxBlockRatio.ReadOnly = true;
            this.textBoxBlockRatio.Size = new System.Drawing.Size(78, 21);
            this.textBoxBlockRatio.TabIndex = 2;
            // 
            // textBoxBlockUnit
            // 
            this.textBoxBlockUnit.Location = new System.Drawing.Point(46, 14);
            this.textBoxBlockUnit.Name = "textBoxBlockUnit";
            this.textBoxBlockUnit.ReadOnly = true;
            this.textBoxBlockUnit.Size = new System.Drawing.Size(78, 21);
            this.textBoxBlockUnit.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "比例:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "单位:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxRotateAngle);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.checkBoxRotate);
            this.groupBox3.Location = new System.Drawing.Point(334, 69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(130, 73);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "旋转";
            // 
            // textBoxRotateAngle
            // 
            this.textBoxRotateAngle.Location = new System.Drawing.Point(55, 43);
            this.textBoxRotateAngle.Name = "textBoxRotateAngle";
            this.textBoxRotateAngle.Size = new System.Drawing.Size(69, 21);
            this.textBoxRotateAngle.TabIndex = 2;
            this.textBoxRotateAngle.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "角度(&A):";
            // 
            // checkBoxRotate
            // 
            this.checkBoxRotate.AutoSize = true;
            this.checkBoxRotate.Location = new System.Drawing.Point(7, 21);
            this.checkBoxRotate.Name = "checkBoxRotate";
            this.checkBoxRotate.Size = new System.Drawing.Size(114, 16);
            this.checkBoxRotate.TabIndex = 0;
            this.checkBoxRotate.Text = "在屏幕上指定(&C)";
            this.checkBoxRotate.UseVisualStyleBackColor = true;
            this.checkBoxRotate.CheckedChanged += new System.EventHandler(this.checkBoxRotate_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxScaleY);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxScaleZ);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxScaleX);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.checkBoxSameScale);
            this.groupBox2.Controls.Add(this.checkBoxScale);
            this.groupBox2.Location = new System.Drawing.Point(177, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 151);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缩放比例";
            // 
            // textBoxScaleY
            // 
            this.textBoxScaleY.Location = new System.Drawing.Point(30, 70);
            this.textBoxScaleY.Name = "textBoxScaleY";
            this.textBoxScaleY.Size = new System.Drawing.Size(104, 21);
            this.textBoxScaleY.TabIndex = 2;
            this.textBoxScaleY.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "&Y:";
            // 
            // textBoxScaleZ
            // 
            this.textBoxScaleZ.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxScaleZ.Location = new System.Drawing.Point(30, 97);
            this.textBoxScaleZ.Name = "textBoxScaleZ";
            this.textBoxScaleZ.Size = new System.Drawing.Size(104, 21);
            this.textBoxScaleZ.TabIndex = 2;
            this.textBoxScaleZ.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "&Z:";
            // 
            // textBoxScaleX
            // 
            this.textBoxScaleX.Location = new System.Drawing.Point(30, 43);
            this.textBoxScaleX.Name = "textBoxScaleX";
            this.textBoxScaleX.Size = new System.Drawing.Size(104, 21);
            this.textBoxScaleX.TabIndex = 2;
            this.textBoxScaleX.Text = "1";
            this.textBoxScaleX.TextChanged += new System.EventHandler(this.textBoxScaleX_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "&X:";
            // 
            // checkBoxSameScale
            // 
            this.checkBoxSameScale.AutoSize = true;
            this.checkBoxSameScale.Location = new System.Drawing.Point(30, 128);
            this.checkBoxSameScale.Name = "checkBoxSameScale";
            this.checkBoxSameScale.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSameScale.TabIndex = 0;
            this.checkBoxSameScale.Text = "统一比例";
            this.checkBoxSameScale.UseVisualStyleBackColor = true;
            this.checkBoxSameScale.CheckedChanged += new System.EventHandler(this.checkBoxSameScale_CheckedChanged);
            // 
            // checkBoxScale
            // 
            this.checkBoxScale.AutoSize = true;
            this.checkBoxScale.Location = new System.Drawing.Point(7, 21);
            this.checkBoxScale.Name = "checkBoxScale";
            this.checkBoxScale.Size = new System.Drawing.Size(114, 16);
            this.checkBoxScale.TabIndex = 0;
            this.checkBoxScale.Text = "在屏幕上指定(&E)";
            this.checkBoxScale.UseVisualStyleBackColor = true;
            this.checkBoxScale.CheckedChanged += new System.EventHandler(this.checkBoxScale_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxInsertPointY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxInsertPointZ);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxInsertPointX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBoxInsertPoint);
            this.groupBox1.Location = new System.Drawing.Point(20, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 151);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插入点";
            // 
            // textBoxInsertPointY
            // 
            this.textBoxInsertPointY.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxInsertPointY.Location = new System.Drawing.Point(30, 70);
            this.textBoxInsertPointY.Name = "textBoxInsertPointY";
            this.textBoxInsertPointY.Size = new System.Drawing.Size(104, 21);
            this.textBoxInsertPointY.TabIndex = 2;
            this.textBoxInsertPointY.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "&Y:";
            // 
            // textBoxInsertPointZ
            // 
            this.textBoxInsertPointZ.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxInsertPointZ.Location = new System.Drawing.Point(30, 97);
            this.textBoxInsertPointZ.Name = "textBoxInsertPointZ";
            this.textBoxInsertPointZ.Size = new System.Drawing.Size(104, 21);
            this.textBoxInsertPointZ.TabIndex = 2;
            this.textBoxInsertPointZ.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "&Z:";
            // 
            // textBoxInsertPointX
            // 
            this.textBoxInsertPointX.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxInsertPointX.Location = new System.Drawing.Point(30, 43);
            this.textBoxInsertPointX.Name = "textBoxInsertPointX";
            this.textBoxInsertPointX.Size = new System.Drawing.Size(104, 21);
            this.textBoxInsertPointX.TabIndex = 2;
            this.textBoxInsertPointX.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "&X:";
            // 
            // checkBoxInsertPoint
            // 
            this.checkBoxInsertPoint.AutoSize = true;
            this.checkBoxInsertPoint.Location = new System.Drawing.Point(7, 21);
            this.checkBoxInsertPoint.Name = "checkBoxInsertPoint";
            this.checkBoxInsertPoint.Size = new System.Drawing.Size(114, 16);
            this.checkBoxInsertPoint.TabIndex = 0;
            this.checkBoxInsertPoint.Text = "在屏幕上指定(&S)";
            this.checkBoxInsertPoint.UseVisualStyleBackColor = true;
            this.checkBoxInsertPoint.CheckedChanged += new System.EventHandler(this.checkBoxInsertPoint_CheckedChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(284, 22);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(74, 23);
            this.buttonBrowse.TabIndex = 24;
            this.buttonBrowse.Text = "浏览(&B)...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // comboBoxBlockName
            // 
            this.comboBoxBlockName.FormattingEnabled = true;
            this.comboBoxBlockName.Location = new System.Drawing.Point(77, 24);
            this.comboBoxBlockName.MaxDropDownItems = 20;
            this.comboBoxBlockName.Name = "comboBoxBlockName";
            this.comboBoxBlockName.Size = new System.Drawing.Size(188, 20);
            this.comboBoxBlockName.Sorted = true;
            this.comboBoxBlockName.TabIndex = 23;
            this.comboBoxBlockName.SelectedIndexChanged += new System.EventHandler(this.comboBoxBlockName_SelectedIndexChanged);
            // 
            // labelPath
            // 
            this.labelPath.Location = new System.Drawing.Point(18, 54);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(340, 12);
            this.labelPath.TabIndex = 21;
            this.labelPath.Text = "路径:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "名称(&N):";
            // 
            // ModalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 266);
            this.Controls.Add(this.pictureBoxBlock);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.comboBoxBlockName);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.label1);
            this.Name = "ModalForm";
            this.Text = "模态对话框示例";
            this.Load += new System.EventHandler(this.ModalForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlock)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBlock;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxBlockRatio;
        private System.Windows.Forms.TextBox textBoxBlockUnit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxRotateAngle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxRotate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxScaleY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxScaleZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxScaleX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxSameScale;
        private System.Windows.Forms.CheckBox checkBoxScale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxInsertPointY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxInsertPointZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxInsertPointX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxInsertPoint;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ComboBox comboBoxBlockName;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label label1;


    }
}