Imports Autodesk.AutoCAD.ApplicationServices
Imports AcadApp = Autodesk.AutoCAD.ApplicationServices.Application
Public Class CommandTools

Private Sub PictureBox_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pictureBoxCircle.MouseMove, pictureBoxRectangle.MouseMove, pictureBoxPolyline.MouseMove, pictureBoxLine.MouseMove
        '获取发出移动操作的图片框对象
        Dim pictureBox As Windows.Forms.PictureBox = sender
        '只执行左键移动操作，以表示进行了拖放操作
        If System.Windows.Forms.Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
            '图片框对象触发拖放事件，调用拖放操作事件处理函数，并传入图片框对象的Name属性供事件处理函数进行判断
            AcadApp.DoDragDrop(Me, pictureBox.Name, Windows.Forms.DragDropEffects.All, New MyDropTarget())
        End If
End Sub

End Class
Public Class MyDropTarget
    Inherits Autodesk.AutoCAD.Windows.DropTarget

Public Overrides Sub OnDrop(ByVal e As System.Windows.Forms.DragEventArgs)
    Dim doc As Document = AcadApp.DocumentManager.MdiActiveDocument
        '判断发出托放操作的对象的名字
        Select Case e.Data.GetData("Text")
            '如果是圆图片控件，则画一个圆
            Case "pictureBoxCircle"
                doc.SendStringToExecute("_Circle 100,100 50 ", True, False, True)
                '如果是直线图片控件，则画一条直线
            Case "pictureBoxLine"
                doc.SendStringToExecute("_Line 100,100 150,100  ", True, False, True)
                '如果是多段线图片控件，则画一个表示三角形的多段线
            Case "pictureBoxPolyline"
                doc.SendStringToExecute("_Pline 100,100 150,100 100,150 100,100  ", True, False, True)
                '如果是矩形图片控件，则画一个矩形
            Case "pictureBoxRectangle"
                doc.SendStringToExecute("_Rectangle 50,150 150,50 ", True, False, True)
        End Select
End Sub
End Class
