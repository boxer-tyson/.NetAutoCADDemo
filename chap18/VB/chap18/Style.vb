Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime

Public Class Style
    <CommandMethod("netStyle")> Public Sub CreateStyle()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction
            ' 得到文字样式表
            Dim st As TextStyleTable = trans.GetObject(db.TextStyleTableId, OpenMode.ForWrite)
            Dim StyleName As String = "工程图"
            ' 如果名为"工程图"的文字样式不存在，则新建一个文字样式.
            If st.Has(StyleName) = False Then
                ' 新建一个文字样式表记录.
                Dim str As New TextStyleTableRecord()
                ' 设置文字样式名.
                str.Name = StyleName
                ' 设置TrueType字体(仿宋体）
                str.FileName = "simfang.ttf"
                '---------------------------------------------
                ' 设置SHX字体
                ' str.FileName = "gbenor"
                ' 设置大字体.
                ' str.BigFontFileName = "gbcbig"
                ' --------------------------------------------
                ' 设置倾斜角(弧度).
                str.ObliquingAngle = 15 * Math.PI / 180
                ' 设置宽度比例.
                str.XScale = 0.67
                ' 把文字样式表记录添加到文字样式表中.
                Dim TextstyleId As ObjectId = st.Add(str)
                ' 把文字样式表记录添加到事务处理中.
                trans.AddNewlyCreatedDBObject(str, True)
                ' 将文字样式"工程图"设置为当前文字样式  
                db.Textstyle = TextstyleId
                trans.Commit()
            End If
        End Using
    End Sub

    <CommandMethod("getTextStyle")> Public Sub TestgetTextStyle()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 定义一个拾取对象的用户交互类.
        Dim optEnt As New PromptEntityOptions(vbCrLf & "请选择文字")
        ' 设置用户选择错误时的命令行提示文本.
        optEnt.SetRejectMessage(vbCrLf & "您选择的对象不是文字，请重新选择！")
        ' 限定选择的类型为单行文字和多行文字.
        optEnt.AddAllowedClass(GetType(DBText), True)
        optEnt.AddAllowedClass(GetType(MText), True)
        ' 返回一个拾取对象的用户提示类.
        Dim resEnt As PromptEntityResult = ed.GetEntity(optEnt)
        If resEnt.Status <> PromptStatus.OK Then Return
        ' 得到拾取对象的ObjectId.
        Dim entId As ObjectId = resEnt.ObjectId
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 得到拾取的对象.
            Dim ent As Entity = trans.GetObject(entId, OpenMode.ForRead)
            ' 得到文字字体的ObjectId.
            Dim textEnt As DBText, mtextEnt As MText, textStyle As ObjectId
            Try
                textEnt = ent
                textStyle = textEnt.TextStyle
            Catch
                mtextEnt = ent
                textStyle = mtextEnt.TextStyle
            End Try
            ' 得到文字样式记录表.
            Dim str As TextStyleTableRecord = trans.GetObject(textStyle, OpenMode.ForRead)
            If str.Font.TypeFace = "" Then
                MsgBox("您选择的文字是SHX字体" & vbCrLf & "字体名：" & str.FileName & vbCrLf & "大字体名：" & str.BigFontFileName)
            Else
                MsgBox("您选择的文字是TrueType字体" & vbCrLf & "字体文件名：" & str.FileName & vbCrLf & "字体名：" & str.Font.TypeFace)
            End If
        End Using
    End Sub
End Class


