Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Windows
Imports System.IO

Public Class Layer
    <CommandMethod("netLayer")> Public Sub CreateLayer()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 以写方式打开层表.
            Dim lt As LayerTable = trans.GetObject(db.LayerTableId, OpenMode.ForWrite)
            ' 声明一个用于图层的ObjectId.
            Dim layerId As ObjectId
            ' 如果不存在名为"abc"的图层，则新建一个图层.
            If lt.Has("abc") = False Then
                ' 定义一个新的层表记录.
                Dim ltr As New LayerTableRecord()
                ' 设置图层名.
                ltr.Name = "abc"
                ' 通过颜色索引值的方式定义一个颜色.
                Dim layerColor As Color = Color.FromColorIndex(ColorMethod.ByColor, 120)
                ' 设置图层颜色.
                ltr.Color = layerColor
                ' 以读方式打开线型表.
                Dim tt As LinetypeTable = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead)
                ' 声明一个线型表记录.
                Dim ttr As LinetypeTableRecord
                Try
                    ' 加载线型文件"acadiso.lin"中的"CENTER"线型.
                    db.LoadLineTypeFile("CENTER", "acadiso.lin")
                Catch
                    ' 如果"CENTER"线型程序运行前已加载，则产生一个错误.
                    Exit Try
                Finally
                    ' 以读方式打开"CENTER"线型的线型表记录.
                    ttr = trans.GetObject(tt("CENTER"), OpenMode.ForRead)
                End Try
                ' 设置图层线型
                ltr.LinetypeObjectId = ttr.ObjectId
                ' 设置图层线宽
                ltr.LineWeight = LineWeight.LineWeight030
                ' 显示线宽
                db.LineWeightDisplay = True
                ' 线型比例
                db.Ltscale = 2
                ' 图层锁定
                'ltr.IsLocked = True
                ' 图层冻结
                'ltr.IsFrozen = True
                ' 图层关闭
                'ltr.IsOff = True
                ' 将层表记录的信息添加到层表中,并返回ObjectId对象.
                layerId = lt.Add(ltr)
                ' 把层表记录添加到事务处理中.
                trans.AddNewlyCreatedDBObject(ltr, True)
                ' 将图层“abc”设置当前层
                db.Clayer = layerId
            End If
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("LayerColor")> Public Sub EditLayerColor()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 定义一个字符串交互类.
        Dim optStr As New PromptStringOptions(vbCrLf & "输入图层的名称")
        ' 返回字符串提示类.
        Dim resStr As PromptResult = ed.GetString(optStr)
        ' 如果用户取消则退出.
        If resStr.Status <> PromptStatus.OK Then Return
        ' 得到输入的图层名称.
        Dim layName As String = resStr.StringResult
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            ' 以读方式打开层表.
            Dim lt As LayerTable = trans.GetObject(db.LayerTableId, OpenMode.ForRead)
            If lt.Has(layName) = False Then
                ed.WriteMessage(vbCrLf & "该图层不存在！")
                Return
            End If

            Dim ltr As LayerTableRecord = trans.GetObject(lt(layName), OpenMode.ForWrite)
            ' 定义颜色对话框.
            Dim dialogObj As New ColorDialog
            ' 得到颜色对话框的返回值.
            Dim dialogResultValue As System.Windows.Forms.DialogResult = dialogObj.ShowDialog()
            ' 如果用户是单击了颜色对话框的确定按钮……
            If dialogResultValue = System.Windows.Forms.DialogResult.OK Then
                ' 得到颜色对话框的颜色.
                Dim newColor As Color = dialogObj.Color
                ' 重新设置图层的颜色.
                ltr.Color = newColor
            End If
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("delLayer")> Public Sub testdelLayer()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim optStr As New PromptStringOptions(vbCrLf & "输入图层的名称")
        Dim resStr As PromptResult = ed.GetString(optStr)
        If resStr.Status <> PromptStatus.OK Then Return

        Dim layName As String = resStr.StringResult
        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim lt As LayerTable = trans.GetObject(db.LayerTableId, OpenMode.ForWrite)
            If lt.Has(layName) = False Then
                ed.WriteMessage(vbCrLf & "该图层不存在！")
                Return
            End If
            
            Dim ltr As LayerTableRecord = trans.GetObject(lt(layName), OpenMode.ForWrite)
            ' 得到当前图层.
            Dim curLtr As LayerTableRecord = trans.GetObject(db.Clayer, OpenMode.ForRead)
            ' 不能删除当前图层.
            If layName = curLtr.Name Then
                ed.WriteMessage("不能删除当前图层！")
            Else
                ltr.Erase(True)
            End If
            trans.Commit()
        End Using
    End Sub

    <CommandMethod("ExportLayer")> Public Sub testLayerIterator()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        ' 新建文本文件或对其改写其内容.
        Dim textFile As New StreamWriter("c:\3.txt", False)
        ' 关闭该文本文件.
        textFile.Close()

        Using trans As Transaction = db.TransactionManager.StartTransaction()
            Dim lt As LayerTable = trans.GetObject(db.LayerTableId, OpenMode.ForRead)
            Dim tt As LinetypeTable = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead)
            ' 遍历层表.
            For Each layerId As ObjectId In lt
                Dim ltr As LayerTableRecord = trans.GetObject(layerId, OpenMode.ForRead)
                ' 得到图层名.
                Dim layerName As String = ltr.Name
                ' 得到颜色字符.
                Dim colorName As String = ltr.Color.ToString
                ' 得到线型字符.
                Dim linetypeId As ObjectId = ltr.LinetypeObjectId
                Dim ttr As LinetypeTableRecord = trans.GetObject(linetypeId, OpenMode.ForRead)
                Dim linetypeName As String = ttr.Name
                ' 得到线宽字符.
                Dim withLayer As String = ltr.LineWeight.ToString
                ' 定义要写入的一行文本.
                Dim str As String = layerName & "   " & colorName & "   " & linetypeName & "   " & withLayer & vbCrLf
                ' 向文本文件写入一和行文本.
                My.Computer.FileSystem.WriteAllText("c:\3.txt", str, True)
            Next
            trans.Commit()
        End Using
    End Sub
End Class
