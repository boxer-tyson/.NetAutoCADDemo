Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime

Public Class DimStyle
    ' 获得一个与ISO-25相同的标注样式.
    Public Function ISO25(ByVal dimStyleName As String) As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim dimstyleId As ObjectId
        Try
            Using trans As Transaction = db.TransactionManager.StartTransaction
                ' 得到标注样式表
                Dim dt As DimStyleTable = trans.GetObject(db.DimStyleTableId, OpenMode.ForWrite)
                ' 新建一个标注样式表记录.
                Dim dtr As New DimStyleTableRecord
                ' 换算精度
                dtr.Dimaltd = 3
                ' 换算比例因子
                dtr.Dimaltf = 0.03937008
                ' 换算公差精度
                dtr.Dimalttd = 3
                ' 箭头大小
                dtr.Dimasz = 2.5
                ' 圆心标记大小
                dtr.Dimcen = 2.5
                ' 精度
                dtr.Dimdec = 2
                ' 尺寸线间距
                dtr.Dimdli = 3.75
                ' 小数分隔符
                dtr.Dimdsep = ","
                ' 尺寸界线超出量
                dtr.Dimexe = 1.25
                ' 尺寸界线偏移
                dtr.Dimexo = 0.625
                ' 文字偏移
                dtr.Dimgap = 0.625
                ' 文字位置垂直
                dtr.Dimtad = 1
                ' 公差精度
                dtr.Dimtdec = 2
                ' 文字在内对齐
                dtr.Dimtih = False
                ' 尺寸线强制
                dtr.Dimtofl = True
                ' 文字外部对齐
                dtr.Dimtoh = False
                ' 公差位置垂直
                dtr.Dimtolj = 0
                ' 文字高度
                dtr.Dimtxt = 2.5
                ' 公差消零
                dtr.Dimtzin = 8
                ' 消零
                dtr.Dimzin = 8
                ' 设置标注样式名称.
                dtr.Name = dimStyleName
                ' 把标注样式表记录添加到标注样式表中.
                dimstyleId = dt.Add(dtr)
                ' 把标注样式dimStyleName添加到事务处理中.
                trans.AddNewlyCreatedDBObject(dtr, True)
                trans.Commit()
            End Using
            Return dimstyleId
        Catch
            ' 如果已存在同名的标注样式，则返回一个空的ObjectId.
            Dim NullId As ObjectId = ObjectId.Null
            Return NullId
        End Try
    End Function

    <CommandMethod("netdimStyle")> Public Sub CreatedimStyle()
        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Using trans As Transaction = db.TransactionManager.StartTransaction
            ' 以写方式打开标注样式表.
            Dim dt As DimStyleTable = trans.GetObject(db.DimStyleTableId, OpenMode.ForWrite)
            Dim dimName As String = "abc"
            Dim dimId As ObjectId = ISO25(dimName)
            ' 如果不存在名为“abc”的标注样式
            If dimId <> ObjectId.Null Then
                ' 以写方式打开标注样式记录表.
                Dim dtr As DimStyleTableRecord = trans.GetObject(dimId, OpenMode.ForWrite)
                ' 修改箭头大小.
                dtr.Dimasz = 3
            Else
                ed.WriteMessage(vbCr & "标注样式 " & dimName & " 已存在！")
            End If
            trans.Commit()
        End Using
    End Sub
End Class
