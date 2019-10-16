<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModifyTools
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写 Dispose，以清理组件列表。
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
Me.label4 = New System.Windows.Forms.Label
Me.label3 = New System.Windows.Forms.Label
Me.label2 = New System.Windows.Forms.Label
Me.label1 = New System.Windows.Forms.Label
Me.buttonRotate = New System.Windows.Forms.Button
Me.buttonMove = New System.Windows.Forms.Button
Me.buttonErase = New System.Windows.Forms.Button
Me.buttonCopy = New System.Windows.Forms.Button
Me.SuspendLayout()
'
'label4
'
Me.label4.AutoSize = True
Me.label4.Location = New System.Drawing.Point(87, 187)
Me.label4.Name = "label4"
Me.label4.Size = New System.Drawing.Size(29, 12)
Me.label4.TabIndex = 15
Me.label4.Text = "旋转"
'
'label3
'
Me.label3.AutoSize = True
Me.label3.Location = New System.Drawing.Point(87, 138)
Me.label3.Name = "label3"
Me.label3.Size = New System.Drawing.Size(29, 12)
Me.label3.TabIndex = 14
Me.label3.Text = "移动"
'
'label2
'
Me.label2.AutoSize = True
Me.label2.Location = New System.Drawing.Point(87, 91)
Me.label2.Name = "label2"
Me.label2.Size = New System.Drawing.Size(29, 12)
Me.label2.TabIndex = 17
Me.label2.Text = "删除"
'
'label1
'
Me.label1.AutoSize = True
Me.label1.Location = New System.Drawing.Point(87, 44)
Me.label1.Name = "label1"
Me.label1.Size = New System.Drawing.Size(29, 12)
Me.label1.TabIndex = 16
Me.label1.Text = "复制"
'
'buttonRotate
'
        Me.buttonRotate.BackgroundImage = Global.chap21.My.Resources.Resources.Rotate
        Me.buttonRotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.buttonRotate.Location = New System.Drawing.Point(34, 176)
        Me.buttonRotate.Name = "buttonRotate"
        Me.buttonRotate.Size = New System.Drawing.Size(32, 31)
        Me.buttonRotate.TabIndex = 11
        Me.buttonRotate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.buttonRotate.UseVisualStyleBackColor = True
        '
        'buttonMove
        '
        Me.buttonMove.BackgroundImage = Global.chap21.My.Resources.Resources.Move
        Me.buttonMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.buttonMove.Location = New System.Drawing.Point(34, 127)
        Me.buttonMove.Name = "buttonMove"
        Me.buttonMove.Size = New System.Drawing.Size(32, 31)
        Me.buttonMove.TabIndex = 10
        Me.buttonMove.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.buttonMove.UseVisualStyleBackColor = True
        '
        'buttonErase
        '
        Me.buttonErase.BackgroundImage = Global.chap21.My.Resources.Resources._Erase
        Me.buttonErase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.buttonErase.Location = New System.Drawing.Point(34, 80)
        Me.buttonErase.Name = "buttonErase"
        Me.buttonErase.Size = New System.Drawing.Size(32, 31)
        Me.buttonErase.TabIndex = 13
        Me.buttonErase.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.buttonErase.UseVisualStyleBackColor = True
        '
        'buttonCopy
        '
        Me.buttonCopy.BackgroundImage = Global.chap21.My.Resources.Resources.Copy
Me.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
Me.buttonCopy.Location = New System.Drawing.Point(34, 33)
Me.buttonCopy.Name = "buttonCopy"
Me.buttonCopy.Size = New System.Drawing.Size(32, 31)
Me.buttonCopy.TabIndex = 12
Me.buttonCopy.TextAlign = System.Drawing.ContentAlignment.TopCenter
Me.buttonCopy.UseVisualStyleBackColor = True
'
'ModifyTools
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.Controls.Add(Me.label4)
Me.Controls.Add(Me.label3)
Me.Controls.Add(Me.label2)
Me.Controls.Add(Me.label1)
Me.Controls.Add(Me.buttonRotate)
Me.Controls.Add(Me.buttonMove)
Me.Controls.Add(Me.buttonErase)
Me.Controls.Add(Me.buttonCopy)
Me.Name = "ModifyTools"
Me.Size = New System.Drawing.Size(150, 240)
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents buttonRotate As System.Windows.Forms.Button
    Private WithEvents buttonMove As System.Windows.Forms.Button
    Private WithEvents buttonErase As System.Windows.Forms.Button
    Private WithEvents buttonCopy As System.Windows.Forms.Button

End Class
