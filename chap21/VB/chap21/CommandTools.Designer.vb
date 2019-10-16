<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CommandTools
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
Me.pictureBoxCircle = New System.Windows.Forms.PictureBox
Me.pictureBoxLine = New System.Windows.Forms.PictureBox
Me.pictureBoxPolyline = New System.Windows.Forms.PictureBox
Me.pictureBoxRectangle = New System.Windows.Forms.PictureBox
CType(Me.pictureBoxCircle, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.pictureBoxLine, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.pictureBoxPolyline, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.pictureBoxRectangle, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'label4
'
Me.label4.AutoSize = True
Me.label4.Location = New System.Drawing.Point(81, 198)
Me.label4.Name = "label4"
Me.label4.Size = New System.Drawing.Size(29, 12)
Me.label4.TabIndex = 6
Me.label4.Text = "矩形"
'
'label3
'
Me.label3.AutoSize = True
Me.label3.Location = New System.Drawing.Point(81, 149)
Me.label3.Name = "label3"
Me.label3.Size = New System.Drawing.Size(41, 12)
Me.label3.TabIndex = 7
Me.label3.Text = "多段线"
'
'label2
'
Me.label2.AutoSize = True
Me.label2.Location = New System.Drawing.Point(81, 102)
Me.label2.Name = "label2"
Me.label2.Size = New System.Drawing.Size(29, 12)
Me.label2.TabIndex = 8
Me.label2.Text = "直线"
'
'label1
'
Me.label1.AutoSize = True
Me.label1.Location = New System.Drawing.Point(81, 44)
Me.label1.Name = "label1"
Me.label1.Size = New System.Drawing.Size(17, 12)
Me.label1.TabIndex = 9
Me.label1.Text = "圆"
'
'pictureBoxCircle
'
        Me.pictureBoxCircle.Image = Global.chap21.My.Resources.Resources.Circle
        Me.pictureBoxCircle.Location = New System.Drawing.Point(33, 33)
        Me.pictureBoxCircle.Name = "pictureBoxCircle"
        Me.pictureBoxCircle.Size = New System.Drawing.Size(31, 32)
        Me.pictureBoxCircle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxCircle.TabIndex = 10
        Me.pictureBoxCircle.TabStop = False
        '
        'pictureBoxLine
        '
        Me.pictureBoxLine.Image = Global.chap21.My.Resources.Resources.Line
        Me.pictureBoxLine.Location = New System.Drawing.Point(33, 91)
        Me.pictureBoxLine.Name = "pictureBoxLine"
        Me.pictureBoxLine.Size = New System.Drawing.Size(31, 32)
        Me.pictureBoxLine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxLine.TabIndex = 11
        Me.pictureBoxLine.TabStop = False
        '
        'pictureBoxPolyline
        '
        Me.pictureBoxPolyline.Image = Global.chap21.My.Resources.Resources.Polyline
        Me.pictureBoxPolyline.Location = New System.Drawing.Point(33, 138)
        Me.pictureBoxPolyline.Name = "pictureBoxPolyline"
        Me.pictureBoxPolyline.Size = New System.Drawing.Size(31, 32)
        Me.pictureBoxPolyline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxPolyline.TabIndex = 12
        Me.pictureBoxPolyline.TabStop = False
        '
        'pictureBoxRectangle
        '
        Me.pictureBoxRectangle.Image = Global.chap21.My.Resources.Resources.Rectangle
Me.pictureBoxRectangle.Location = New System.Drawing.Point(33, 187)
Me.pictureBoxRectangle.Name = "pictureBoxRectangle"
Me.pictureBoxRectangle.Size = New System.Drawing.Size(31, 32)
Me.pictureBoxRectangle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
Me.pictureBoxRectangle.TabIndex = 13
Me.pictureBoxRectangle.TabStop = False
'
'CommandTools
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.Controls.Add(Me.pictureBoxRectangle)
Me.Controls.Add(Me.pictureBoxPolyline)
Me.Controls.Add(Me.pictureBoxLine)
Me.Controls.Add(Me.pictureBoxCircle)
Me.Controls.Add(Me.label4)
Me.Controls.Add(Me.label3)
Me.Controls.Add(Me.label2)
Me.Controls.Add(Me.label1)
Me.Name = "CommandTools"
Me.Size = New System.Drawing.Size(150, 240)
CType(Me.pictureBoxCircle, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.pictureBoxLine, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.pictureBoxPolyline, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.pictureBoxRectangle, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents pictureBoxCircle As System.Windows.Forms.PictureBox
    Friend WithEvents pictureBoxLine As System.Windows.Forms.PictureBox
    Friend WithEvents pictureBoxPolyline As System.Windows.Forms.PictureBox
    Friend WithEvents pictureBoxRectangle As System.Windows.Forms.PictureBox

End Class
