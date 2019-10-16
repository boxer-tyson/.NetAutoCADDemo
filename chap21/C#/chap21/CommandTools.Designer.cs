namespace chap21
{
    partial class CommandTools
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBoxRectangle = new System.Windows.Forms.PictureBox();
            this.pictureBoxPolyline = new System.Windows.Forms.PictureBox();
            this.pictureBoxLine = new System.Windows.Forms.PictureBox();
            this.pictureBoxCircle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRectangle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPolyline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCircle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "圆";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "直线";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "多段线";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "矩形";
            // 
            // pictureBoxRectangle
            // 
            this.pictureBoxRectangle.Image = global::chap21.Properties.Resources.Rectangle;
            this.pictureBoxRectangle.Location = new System.Drawing.Point(32, 181);
            this.pictureBoxRectangle.Name = "pictureBoxRectangle";
            this.pictureBoxRectangle.Size = new System.Drawing.Size(31, 32);
            this.pictureBoxRectangle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRectangle.TabIndex = 17;
            this.pictureBoxRectangle.TabStop = false;
            this.pictureBoxRectangle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            // 
            // pictureBoxPolyline
            // 
            this.pictureBoxPolyline.Image = global::chap21.Properties.Resources.Polyline;
            this.pictureBoxPolyline.Location = new System.Drawing.Point(32, 132);
            this.pictureBoxPolyline.Name = "pictureBoxPolyline";
            this.pictureBoxPolyline.Size = new System.Drawing.Size(31, 32);
            this.pictureBoxPolyline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPolyline.TabIndex = 16;
            this.pictureBoxPolyline.TabStop = false;
            this.pictureBoxPolyline.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            // 
            // pictureBoxLine
            // 
            this.pictureBoxLine.Image = global::chap21.Properties.Resources.Line;
            this.pictureBoxLine.Location = new System.Drawing.Point(32, 85);
            this.pictureBoxLine.Name = "pictureBoxLine";
            this.pictureBoxLine.Size = new System.Drawing.Size(31, 32);
            this.pictureBoxLine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLine.TabIndex = 15;
            this.pictureBoxLine.TabStop = false;
            this.pictureBoxLine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            // 
            // pictureBoxCircle
            // 
            this.pictureBoxCircle.Image = global::chap21.Properties.Resources.Cirlce;
            this.pictureBoxCircle.Location = new System.Drawing.Point(32, 27);
            this.pictureBoxCircle.Name = "pictureBoxCircle";
            this.pictureBoxCircle.Size = new System.Drawing.Size(31, 32);
            this.pictureBoxCircle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCircle.TabIndex = 14;
            this.pictureBoxCircle.TabStop = false;
            this.pictureBoxCircle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            // 
            // CommandTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxRectangle);
            this.Controls.Add(this.pictureBoxPolyline);
            this.Controls.Add(this.pictureBoxLine);
            this.Controls.Add(this.pictureBoxCircle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CommandTools";
            this.Size = new System.Drawing.Size(150, 240);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRectangle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPolyline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCircle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.PictureBox pictureBoxRectangle;
        internal System.Windows.Forms.PictureBox pictureBoxPolyline;
        internal System.Windows.Forms.PictureBox pictureBoxLine;
        internal System.Windows.Forms.PictureBox pictureBoxCircle;
    }
}
