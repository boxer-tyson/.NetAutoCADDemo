Imports Autodesk.AutoCAD.ApplicationServices
Imports AcadApp = Autodesk.AutoCAD.ApplicationServices.Application
Public Class ModifyTools

Private Sub buttonModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonCopy.Click, buttonRotate.Click, buttonMove.Click, buttonErase.Click
    '获取发出点击事件的按钮对象
    Dim button As Windows.Forms.Button = sender
    Dim doc As Document = AcadApp.DocumentManager.MdiActiveDocument
    '根据按钮对象的名字，向AutoCAD命令行发出对应的命令
    Select Case button.Name
        Case "buttonCopy"
            doc.SendStringToExecute("_Copy ", True, False, True)
        Case "buttonErase"
            doc.SendStringToExecute("_Erase ", True, False, True)
        Case "buttonMove"
            doc.SendStringToExecute("_Move ", True, False, True)
        Case "buttonRotate"
            doc.SendStringToExecute("_Rotate ", True, False, True)
    End Select
End Sub
End Class
