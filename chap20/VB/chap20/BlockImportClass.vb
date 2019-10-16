Imports System
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Public Class BlockImportClass
<CommandMethod("ImportBlocks")> _
Public Sub ImportBlocks()
    '外部文件名
    Dim filename As String = "C:\Blocks and Tables - Metric.dwg"
    '从所指定的外部文件中导入块
    ImportBlocksFromDwg(filename)
End Sub

Public Sub ImportBlocksFromDwg(ByVal sourceFileName As String)
    Dim dm As DocumentCollection = Application.DocumentManager
    Dim ed As Editor = dm.MdiActiveDocument.Editor
    '获取当前数据库作为目标数据库
    Dim destDb As Database = dm.MdiActiveDocument.Database
    '创建一个新的数据库对象，作为源数据库，以读入外部文件中的对象
    Dim sourceDb As Database = New Database(False, True)
    '定义一个异常处理对象，以处理可能发生的异常
    Dim ex As Autodesk.AutoCAD.Runtime.Exception
    Try
        '将DWG文件读入到源数据库中
        sourceDb.ReadDwgFile(sourceFileName, System.IO.FileShare.Read, True, "")
        '创建一个变量存储块的对象Id列表
        Dim blockIds As ObjectIdCollection = New ObjectIdCollection()
        '获取源数据库的事务处理管理器
        Dim tm As Autodesk.AutoCAD.DatabaseServices.TransactionManager = sourceDb.TransactionManager
        '在源数据库中开始事务处理
        Dim myT As Transaction = tm.StartTransaction()
        Using myT
            '打开源数据库中的块表
            Dim bt As BlockTable = CType(tm.GetObject(sourceDb.BlockTableId, OpenMode.ForRead, False), BlockTable)
            '遍历每个块
            Dim btrId As ObjectId
            For Each btrId In bt
                Dim btr As BlockTableRecord = CType(tm.GetObject(btrId, OpenMode.ForRead, False), BlockTableRecord)
                '只加入命名块和非布局块到复制列表中
                If Not btr.IsAnonymous And Not btr.IsLayout Then
                    blockIds.Add(btrId)
                    btr.Dispose()
                End If
            Next
            bt.Dispose()
            myT.Dispose()
        End Using
        '定义一个IdMapping对象
        Dim mapping As IdMapping = New IdMapping
        '从源数据库向目标数据库复制块表记录
        sourceDb.WblockCloneObjects(blockIds, destDb.BlockTableId, mapping, DuplicateRecordCloning.Replace, False)
        '复制完成后，命令行显示复制了多少个块的信息
        ed.WriteMessage("复制了 " + blockIds.Count.ToString() + " 个块，从 " + sourceFileName + " 到当前图形")
    Catch ex
        ed.WriteMessage("复制出错: " + ex.Message)
    End Try
    '操作完成，销毁源数据库
    sourceDb.Dispose()
End Sub
End Class
