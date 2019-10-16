namespace chap21
{
    partial class ModifyTools
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonErase = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ðý×ª";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "ÒÆ¶¯";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "É¾³ý";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "¸´ÖÆ";
            // 
            // buttonRotate
            // 
            this.buttonRotate.BackgroundImage = global::chap21.Properties.Resources.Rotate;
            this.buttonRotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRotate.Location = new System.Drawing.Point(28, 176);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(32, 31);
            this.buttonRotate.TabIndex = 3;
            this.buttonRotate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonMove
            // 
            this.buttonMove.BackgroundImage = global::chap21.Properties.Resources.Move;
            this.buttonMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMove.Location = new System.Drawing.Point(28, 127);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(32, 31);
            this.buttonMove.TabIndex = 2;
            this.buttonMove.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonErase
            // 
            this.buttonErase.BackgroundImage = global::chap21.Properties.Resources.Erase;
            this.buttonErase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonErase.Location = new System.Drawing.Point(28, 80);
            this.buttonErase.Name = "buttonErase";
            this.buttonErase.Size = new System.Drawing.Size(32, 31);
            this.buttonErase.TabIndex = 5;
            this.buttonErase.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonErase.UseVisualStyleBackColor = true;
            this.buttonErase.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.BackgroundImage = global::chap21.Properties.Resources.Copy;
            this.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCopy.Location = new System.Drawing.Point(28, 33);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(32, 31);
            this.buttonCopy.TabIndex = 4;
            this.buttonCopy.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // ModifyTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRotate);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonErase);
            this.Controls.Add(this.buttonCopy);
            this.Name = "ModifyTools";
            this.Size = new System.Drawing.Size(150, 240);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonErase;
        private System.Windows.Forms.Button buttonCopy;
    }
}
