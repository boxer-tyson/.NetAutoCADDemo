<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModalForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
Me.pictureBoxBlock = New System.Windows.Forms.PictureBox
Me.buttonCancel = New System.Windows.Forms.Button
Me.buttonHelp = New System.Windows.Forms.Button
Me.buttonOK = New System.Windows.Forms.Button
Me.groupBox4 = New System.Windows.Forms.GroupBox
Me.textBoxBlockRatio = New System.Windows.Forms.TextBox
Me.textBoxBlockUnit = New System.Windows.Forms.TextBox
Me.label10 = New System.Windows.Forms.Label
Me.label9 = New System.Windows.Forms.Label
Me.groupBox3 = New System.Windows.Forms.GroupBox
Me.textBoxRotateAngle = New System.Windows.Forms.TextBox
Me.label11 = New System.Windows.Forms.Label
Me.checkBoxRotate = New System.Windows.Forms.CheckBox
Me.groupBox2 = New System.Windows.Forms.GroupBox
Me.textBoxScaleY = New System.Windows.Forms.TextBox
Me.label6 = New System.Windows.Forms.Label
Me.textBoxScaleZ = New System.Windows.Forms.TextBox
Me.label7 = New System.Windows.Forms.Label
Me.textBoxScaleX = New System.Windows.Forms.TextBox
Me.label8 = New System.Windows.Forms.Label
Me.checkBoxSameScale = New System.Windows.Forms.CheckBox
Me.checkBoxScale = New System.Windows.Forms.CheckBox
Me.groupBox1 = New System.Windows.Forms.GroupBox
Me.textBoxInsertPointY = New System.Windows.Forms.TextBox
Me.label5 = New System.Windows.Forms.Label
Me.textBoxInsertPointZ = New System.Windows.Forms.TextBox
Me.label4 = New System.Windows.Forms.Label
Me.textBoxInsertPointX = New System.Windows.Forms.TextBox
Me.label3 = New System.Windows.Forms.Label
Me.checkBoxInsertPoint = New System.Windows.Forms.CheckBox
Me.buttonBrowse = New System.Windows.Forms.Button
Me.comboBoxBlockName = New System.Windows.Forms.ComboBox
Me.labelPath = New System.Windows.Forms.Label
Me.label1 = New System.Windows.Forms.Label
CType(Me.pictureBoxBlock, System.ComponentModel.ISupportInitialize).BeginInit()
Me.groupBox4.SuspendLayout()
Me.groupBox3.SuspendLayout()
Me.groupBox2.SuspendLayout()
Me.groupBox1.SuspendLayout()
Me.SuspendLayout()
'
'pictureBoxBlock
'
Me.pictureBoxBlock.BackColor = System.Drawing.SystemColors.Control
Me.pictureBoxBlock.Location = New System.Drawing.Point(380, 6)
Me.pictureBoxBlock.Name = "pictureBoxBlock"
Me.pictureBoxBlock.Size = New System.Drawing.Size(75, 47)
Me.pictureBoxBlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
Me.pictureBoxBlock.TabIndex = 20
Me.pictureBoxBlock.TabStop = False
'
'buttonCancel
'
Me.buttonCancel.Location = New System.Drawing.Point(303, 216)
Me.buttonCancel.Name = "buttonCancel"
Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
Me.buttonCancel.TabIndex = 19
Me.buttonCancel.Text = "取消"
Me.buttonCancel.UseVisualStyleBackColor = True
'
'buttonHelp
'
Me.buttonHelp.Location = New System.Drawing.Point(389, 216)
Me.buttonHelp.Name = "buttonHelp"
Me.buttonHelp.Size = New System.Drawing.Size(75, 23)
Me.buttonHelp.TabIndex = 17
Me.buttonHelp.Text = "帮助(&H)"
Me.buttonHelp.UseVisualStyleBackColor = True
'
'buttonOK
'
Me.buttonOK.Location = New System.Drawing.Point(217, 217)
Me.buttonOK.Name = "buttonOK"
Me.buttonOK.Size = New System.Drawing.Size(75, 23)
Me.buttonOK.TabIndex = 18
Me.buttonOK.Text = "确定"
Me.buttonOK.UseVisualStyleBackColor = True
'
'groupBox4
'
Me.groupBox4.Controls.Add(Me.textBoxBlockRatio)
Me.groupBox4.Controls.Add(Me.textBoxBlockUnit)
Me.groupBox4.Controls.Add(Me.label10)
Me.groupBox4.Controls.Add(Me.label9)
Me.groupBox4.Location = New System.Drawing.Point(334, 138)
Me.groupBox4.Name = "groupBox4"
Me.groupBox4.Size = New System.Drawing.Size(130, 72)
Me.groupBox4.TabIndex = 15
Me.groupBox4.TabStop = False
Me.groupBox4.Text = "块单位"
'
'textBoxBlockRatio
'
Me.textBoxBlockRatio.Location = New System.Drawing.Point(46, 44)
Me.textBoxBlockRatio.Name = "textBoxBlockRatio"
Me.textBoxBlockRatio.ReadOnly = True
Me.textBoxBlockRatio.Size = New System.Drawing.Size(78, 21)
Me.textBoxBlockRatio.TabIndex = 2
'
'textBoxBlockUnit
'
Me.textBoxBlockUnit.Location = New System.Drawing.Point(46, 14)
Me.textBoxBlockUnit.Name = "textBoxBlockUnit"
Me.textBoxBlockUnit.ReadOnly = True
Me.textBoxBlockUnit.Size = New System.Drawing.Size(78, 21)
Me.textBoxBlockUnit.TabIndex = 2
'
'label10
'
Me.label10.AutoSize = True
Me.label10.Location = New System.Drawing.Point(5, 47)
Me.label10.Name = "label10"
Me.label10.Size = New System.Drawing.Size(35, 12)
Me.label10.TabIndex = 1
Me.label10.Text = "比例:"
'
'label9
'
Me.label9.AutoSize = True
Me.label9.Location = New System.Drawing.Point(5, 17)
Me.label9.Name = "label9"
Me.label9.Size = New System.Drawing.Size(35, 12)
Me.label9.TabIndex = 1
Me.label9.Text = "单位:"
'
'groupBox3
'
Me.groupBox3.Controls.Add(Me.textBoxRotateAngle)
Me.groupBox3.Controls.Add(Me.label11)
Me.groupBox3.Controls.Add(Me.checkBoxRotate)
Me.groupBox3.Location = New System.Drawing.Point(334, 59)
Me.groupBox3.Name = "groupBox3"
Me.groupBox3.Size = New System.Drawing.Size(130, 73)
Me.groupBox3.TabIndex = 16
Me.groupBox3.TabStop = False
Me.groupBox3.Text = "旋转"
'
'textBoxRotateAngle
'
Me.textBoxRotateAngle.Location = New System.Drawing.Point(55, 43)
Me.textBoxRotateAngle.Name = "textBoxRotateAngle"
Me.textBoxRotateAngle.Size = New System.Drawing.Size(69, 21)
Me.textBoxRotateAngle.TabIndex = 2
Me.textBoxRotateAngle.Text = "0"
'
'label11
'
Me.label11.AutoSize = True
Me.label11.Location = New System.Drawing.Point(5, 46)
Me.label11.Name = "label11"
Me.label11.Size = New System.Drawing.Size(53, 12)
Me.label11.TabIndex = 1
Me.label11.Text = "角度(&A):"
'
'checkBoxRotate
'
Me.checkBoxRotate.AutoSize = True
Me.checkBoxRotate.Location = New System.Drawing.Point(7, 21)
Me.checkBoxRotate.Name = "checkBoxRotate"
Me.checkBoxRotate.Size = New System.Drawing.Size(114, 16)
Me.checkBoxRotate.TabIndex = 0
Me.checkBoxRotate.Text = "在屏幕上指定(&C)"
Me.checkBoxRotate.UseVisualStyleBackColor = True
'
'groupBox2
'
Me.groupBox2.Controls.Add(Me.textBoxScaleY)
Me.groupBox2.Controls.Add(Me.label6)
Me.groupBox2.Controls.Add(Me.textBoxScaleZ)
Me.groupBox2.Controls.Add(Me.label7)
Me.groupBox2.Controls.Add(Me.textBoxScaleX)
Me.groupBox2.Controls.Add(Me.label8)
Me.groupBox2.Controls.Add(Me.checkBoxSameScale)
Me.groupBox2.Controls.Add(Me.checkBoxScale)
Me.groupBox2.Location = New System.Drawing.Point(177, 59)
Me.groupBox2.Name = "groupBox2"
Me.groupBox2.Size = New System.Drawing.Size(151, 151)
Me.groupBox2.TabIndex = 14
Me.groupBox2.TabStop = False
Me.groupBox2.Text = "缩放比例"
'
'textBoxScaleY
'
Me.textBoxScaleY.Location = New System.Drawing.Point(30, 70)
Me.textBoxScaleY.Name = "textBoxScaleY"
Me.textBoxScaleY.Size = New System.Drawing.Size(104, 21)
Me.textBoxScaleY.TabIndex = 2
Me.textBoxScaleY.Text = "1"
'
'label6
'
Me.label6.AutoSize = True
Me.label6.Location = New System.Drawing.Point(7, 73)
Me.label6.Name = "label6"
Me.label6.Size = New System.Drawing.Size(17, 12)
Me.label6.TabIndex = 1
Me.label6.Text = "&Y:"
'
'textBoxScaleZ
'
Me.textBoxScaleZ.BackColor = System.Drawing.SystemColors.Window
Me.textBoxScaleZ.Location = New System.Drawing.Point(30, 97)
Me.textBoxScaleZ.Name = "textBoxScaleZ"
Me.textBoxScaleZ.Size = New System.Drawing.Size(104, 21)
Me.textBoxScaleZ.TabIndex = 2
Me.textBoxScaleZ.Text = "1"
'
'label7
'
Me.label7.AutoSize = True
Me.label7.Location = New System.Drawing.Point(7, 100)
Me.label7.Name = "label7"
Me.label7.Size = New System.Drawing.Size(17, 12)
Me.label7.TabIndex = 1
Me.label7.Text = "&Z:"
'
'textBoxScaleX
'
Me.textBoxScaleX.Location = New System.Drawing.Point(30, 43)
Me.textBoxScaleX.Name = "textBoxScaleX"
Me.textBoxScaleX.Size = New System.Drawing.Size(104, 21)
Me.textBoxScaleX.TabIndex = 2
Me.textBoxScaleX.Text = "1"
'
'label8
'
Me.label8.AutoSize = True
Me.label8.Location = New System.Drawing.Point(7, 46)
Me.label8.Name = "label8"
Me.label8.Size = New System.Drawing.Size(17, 12)
Me.label8.TabIndex = 1
Me.label8.Text = "&X:"
'
'checkBoxSameScale
'
Me.checkBoxSameScale.AutoSize = True
Me.checkBoxSameScale.Location = New System.Drawing.Point(30, 128)
Me.checkBoxSameScale.Name = "checkBoxSameScale"
Me.checkBoxSameScale.Size = New System.Drawing.Size(72, 16)
Me.checkBoxSameScale.TabIndex = 0
Me.checkBoxSameScale.Text = "统一比例"
Me.checkBoxSameScale.UseVisualStyleBackColor = True
'
'checkBoxScale
'
Me.checkBoxScale.AutoSize = True
Me.checkBoxScale.Location = New System.Drawing.Point(7, 21)
Me.checkBoxScale.Name = "checkBoxScale"
Me.checkBoxScale.Size = New System.Drawing.Size(114, 16)
Me.checkBoxScale.TabIndex = 0
Me.checkBoxScale.Text = "在屏幕上指定(&E)"
Me.checkBoxScale.UseVisualStyleBackColor = True
'
'groupBox1
'
Me.groupBox1.Controls.Add(Me.textBoxInsertPointY)
Me.groupBox1.Controls.Add(Me.label5)
Me.groupBox1.Controls.Add(Me.textBoxInsertPointZ)
Me.groupBox1.Controls.Add(Me.label4)
Me.groupBox1.Controls.Add(Me.textBoxInsertPointX)
Me.groupBox1.Controls.Add(Me.label3)
Me.groupBox1.Controls.Add(Me.checkBoxInsertPoint)
Me.groupBox1.Location = New System.Drawing.Point(20, 59)
Me.groupBox1.Name = "groupBox1"
Me.groupBox1.Size = New System.Drawing.Size(151, 151)
Me.groupBox1.TabIndex = 13
Me.groupBox1.TabStop = False
Me.groupBox1.Text = "插入点"
'
'textBoxInsertPointY
'
Me.textBoxInsertPointY.BackColor = System.Drawing.SystemColors.Window
Me.textBoxInsertPointY.Location = New System.Drawing.Point(30, 70)
Me.textBoxInsertPointY.Name = "textBoxInsertPointY"
Me.textBoxInsertPointY.Size = New System.Drawing.Size(104, 21)
Me.textBoxInsertPointY.TabIndex = 2
Me.textBoxInsertPointY.Text = "0"
'
'label5
'
Me.label5.AutoSize = True
Me.label5.Location = New System.Drawing.Point(7, 73)
Me.label5.Name = "label5"
Me.label5.Size = New System.Drawing.Size(17, 12)
Me.label5.TabIndex = 1
Me.label5.Text = "&Y:"
'
'textBoxInsertPointZ
'
Me.textBoxInsertPointZ.BackColor = System.Drawing.SystemColors.Window
Me.textBoxInsertPointZ.Location = New System.Drawing.Point(30, 97)
Me.textBoxInsertPointZ.Name = "textBoxInsertPointZ"
Me.textBoxInsertPointZ.Size = New System.Drawing.Size(104, 21)
Me.textBoxInsertPointZ.TabIndex = 2
Me.textBoxInsertPointZ.Text = "0"
'
'label4
'
Me.label4.AutoSize = True
Me.label4.Location = New System.Drawing.Point(7, 100)
Me.label4.Name = "label4"
Me.label4.Size = New System.Drawing.Size(17, 12)
Me.label4.TabIndex = 1
Me.label4.Text = "&Z:"
'
'textBoxInsertPointX
'
Me.textBoxInsertPointX.BackColor = System.Drawing.SystemColors.Window
Me.textBoxInsertPointX.Location = New System.Drawing.Point(30, 43)
Me.textBoxInsertPointX.Name = "textBoxInsertPointX"
Me.textBoxInsertPointX.Size = New System.Drawing.Size(104, 21)
Me.textBoxInsertPointX.TabIndex = 2
Me.textBoxInsertPointX.Text = "0"
'
'label3
'
Me.label3.AutoSize = True
Me.label3.Location = New System.Drawing.Point(7, 46)
Me.label3.Name = "label3"
Me.label3.Size = New System.Drawing.Size(17, 12)
Me.label3.TabIndex = 1
Me.label3.Text = "&X:"
'
'checkBoxInsertPoint
'
Me.checkBoxInsertPoint.AutoSize = True
Me.checkBoxInsertPoint.Location = New System.Drawing.Point(7, 21)
Me.checkBoxInsertPoint.Name = "checkBoxInsertPoint"
Me.checkBoxInsertPoint.Size = New System.Drawing.Size(114, 16)
Me.checkBoxInsertPoint.TabIndex = 0
Me.checkBoxInsertPoint.Text = "在屏幕上指定(&S)"
Me.checkBoxInsertPoint.UseVisualStyleBackColor = True
'
'buttonBrowse
'
Me.buttonBrowse.Location = New System.Drawing.Point(284, 12)
Me.buttonBrowse.Name = "buttonBrowse"
Me.buttonBrowse.Size = New System.Drawing.Size(74, 23)
Me.buttonBrowse.TabIndex = 12
Me.buttonBrowse.Text = "浏览(&B)..."
Me.buttonBrowse.UseVisualStyleBackColor = True
'
'comboBoxBlockName
'
Me.comboBoxBlockName.FormattingEnabled = True
Me.comboBoxBlockName.Location = New System.Drawing.Point(77, 14)
Me.comboBoxBlockName.MaxDropDownItems = 20
Me.comboBoxBlockName.Name = "comboBoxBlockName"
Me.comboBoxBlockName.Size = New System.Drawing.Size(188, 20)
Me.comboBoxBlockName.Sorted = True
Me.comboBoxBlockName.TabIndex = 11
'
'labelPath
'
Me.labelPath.Location = New System.Drawing.Point(18, 44)
Me.labelPath.Name = "labelPath"
Me.labelPath.Size = New System.Drawing.Size(340, 12)
Me.labelPath.TabIndex = 9
Me.labelPath.Text = "路径:"
'
'label1
'
Me.label1.AutoSize = True
Me.label1.Location = New System.Drawing.Point(18, 17)
Me.label1.Name = "label1"
Me.label1.Size = New System.Drawing.Size(53, 12)
Me.label1.TabIndex = 10
Me.label1.Text = "名称(&N):"
'
'ModalForm
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(482, 246)
Me.Controls.Add(Me.pictureBoxBlock)
Me.Controls.Add(Me.buttonCancel)
Me.Controls.Add(Me.buttonHelp)
Me.Controls.Add(Me.buttonOK)
Me.Controls.Add(Me.groupBox4)
Me.Controls.Add(Me.groupBox3)
Me.Controls.Add(Me.groupBox2)
Me.Controls.Add(Me.groupBox1)
Me.Controls.Add(Me.buttonBrowse)
Me.Controls.Add(Me.comboBoxBlockName)
Me.Controls.Add(Me.labelPath)
Me.Controls.Add(Me.label1)
Me.Name = "ModalForm"
Me.Text = "插入"
CType(Me.pictureBoxBlock, System.ComponentModel.ISupportInitialize).EndInit()
Me.groupBox4.ResumeLayout(False)
Me.groupBox4.PerformLayout()
Me.groupBox3.ResumeLayout(False)
Me.groupBox3.PerformLayout()
Me.groupBox2.ResumeLayout(False)
Me.groupBox2.PerformLayout()
Me.groupBox1.ResumeLayout(False)
Me.groupBox1.PerformLayout()
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Private WithEvents pictureBoxBlock As System.Windows.Forms.PictureBox
    Private WithEvents buttonCancel As System.Windows.Forms.Button
    Private WithEvents buttonHelp As System.Windows.Forms.Button
    Private WithEvents buttonOK As System.Windows.Forms.Button
    Private WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Private WithEvents textBoxBlockRatio As System.Windows.Forms.TextBox
    Private WithEvents textBoxBlockUnit As System.Windows.Forms.TextBox
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Private WithEvents textBoxRotateAngle As System.Windows.Forms.TextBox
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents checkBoxRotate As System.Windows.Forms.CheckBox
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents textBoxScaleY As System.Windows.Forms.TextBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents textBoxScaleZ As System.Windows.Forms.TextBox
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents textBoxScaleX As System.Windows.Forms.TextBox
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents checkBoxSameScale As System.Windows.Forms.CheckBox
    Private WithEvents checkBoxScale As System.Windows.Forms.CheckBox
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents textBoxInsertPointY As System.Windows.Forms.TextBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents textBoxInsertPointZ As System.Windows.Forms.TextBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents textBoxInsertPointX As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents checkBoxInsertPoint As System.Windows.Forms.CheckBox
    Private WithEvents buttonBrowse As System.Windows.Forms.Button
    Private WithEvents comboBoxBlockName As System.Windows.Forms.ComboBox
    Private WithEvents labelPath As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
