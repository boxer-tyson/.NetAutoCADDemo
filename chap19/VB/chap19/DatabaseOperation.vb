Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput

Public Class DatabaseOperation
<CommandMethod("CreateAndSaveDwg")> _
Public Sub CreateAndSaveDwg()
    '新建一个数据库对象以创建新的Dwg文件
    Dim db As New Database()
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '获取数据库的块表对象
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
        '获取数据库的模型空间块表记录对象
        Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
        '新建两个圆
        Dim cir1 As New Circle(New Point3d(1, 1, 0), Vector3d.ZAxis, 1.0)
        Dim cir2 As New Circle(New Point3d(4, 4, 0), Vector3d.ZAxis, 2.0)
        '在模型空间中加入新建的两个圆
        btr.AppendEntity(cir1)
        trans.AddNewlyCreatedDBObject(cir1, True)
        btr.AppendEntity(cir2)
        trans.AddNewlyCreatedDBObject(cir2, True)

        '定义保存文件对话框
        Dim opt As New PromptSaveFileOptions(vbCrLf & "请输入文件名：")
        '保存文件对话框的文件扩展名列表
        opt.Filter = "图形(*.dwg)|*.dwg|图形(*.dxf)|*.dxf"
        '文件过滤器列表中缺省显示的文件扩展名
        opt.FilterIndex = 0
        '保存文件对话框的标题
        opt.DialogCaption = "图形另存为"
        '缺省保存目录
        opt.InitialDirectory = "C:\"
        '缺省保存文件名（扩展名由扩展名列表中的当前值确定）
        opt.InitialFileName = "MyDwg"
        '获取当前数据库对象（不是上面新建的）的命令行对象
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        '根据保存对话框中用户的选择，获取保存文件名
        Dim filename As String = ed.GetFileNameForSave(opt).StringResult
        '保存文件为当前AutoCAD版本
        db.SaveAs(filename, DwgVersion.Current)
    End Using
    '销毁数据库对象
    db.Dispose()
End Sub

<CommandMethod("ReadDwg")> _
Public Sub ReadDwg()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    '新建一个数据库对象以读取Dwg文件
    Dim db As New Database(False, True)
    '文件名
    Dim fileName As String = "C:\MyDwg.dwg"
    '如果指定文件名的文件存在，则
    If System.IO.File.Exists(fileName) Then
        '把文件读入到数据库中
        db.ReadDwgFile(fileName, System.IO.FileShare.Read, True, Nothing)
        Using trans As Transaction = db.TransactionManager.StartTransaction
            '获取数据库的块表对象
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            '打开数据库的模型空间块表记录对象
            Dim btr As BlockTableRecord = trans.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForRead)
            '循环遍历模型空间中的实体
            For Each id As ObjectId In btr
                Dim obj As DBObject = trans.GetObject(id, OpenMode.ForRead)
                If TypeOf (obj) Is Entity Then
                    '显示实体的类名
                    ed.WriteMessage(vbCrLf & obj.GetType.ToString)
                End If
            Next
        End Using
    End If
    '销毁数据库对象
    db.Dispose()
End Sub

<CommandMethod("CopyFromOtherDwg")> _
Public Sub CopyFromOtherDwg()
    '新建一个数据库对象以读取Dwg文件
    Dim db As New Database(False, False)
    Dim fileName As String = "C:\Blocks and Tables - Metric.dwg"
    If System.IO.File.Exists(fileName) Then
        db.ReadDwgFile(fileName, System.IO.FileShare.Read, True, Nothing)
        '为了让插入块的函数在多个图形文件打开的情况下起作用，你必须使用下面的函数把Dwg文件关闭
        db.CloseInput(True)
        '获取当前数据库对象（不是新建的数据库）
        Dim curdb As Database = HostApplicationServices.WorkingDatabase
        '把源数据库模型空间中的实体插入到当前数据库的一个新的块表记录中
        curdb.Insert(System.IO.Path.GetFileNameWithoutExtension(fileName), db, True)
    End If
    '销毁数据库对象
    db.Dispose()
End Sub

<CommandMethod("OpenDwg")> _
Public Sub OpenDwg()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    '获取文档管理器对象以打开Dwg文件
    Dim docs As DocumentCollection = Application.DocumentManager
    '设置打开文件对话框的有关选项
    Dim opt As New PromptOpenFileOptions(vbCrLf & "请输入文件名：")
    opt.Filter = "图形(*.dwg)|*.dwg|图形(*.dxf)|*.dxf"
    opt.FilterIndex = 0
    '根据打开文件对话框中用户的选择，获取文件名
    Dim filename As String = ed.GetFileNameForOpen(opt).StringResult
    '打开所选择的Dwg文件
    Dim doc As Document = docs.Open(filename, True)
    '设置当前的活动文档为新打开的Dwg文件
    Application.DocumentManager.MdiActiveDocument = doc
    End Sub

<CommandMethod("CopyEntities")> _
Public Sub CopyEntities()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    '新建一个数据库对象
    Dim db As New Database(False, True)
    '获取当前数据库对象
    Dim curdb As Database = HostApplicationServices.WorkingDatabase
    '下面的操作选择要复制到新建数据库中的实体
    Dim opts As New PromptSelectionOptions()
    opts.MessageForAdding = "请输入复制到新文件的实体"
    Dim ss As SelectionSet = ed.GetSelection(opts).Value
    '获取所选实体的ObjectId集合
    Dim ids As New ObjectIdCollection(ss.GetObjectIds())
    '把当前数据库中所选择的实体复制到新建的数据库中，并指定插入点为当前数据库的基点
    db = curdb.Wblock(ids, curdb.Ucsorg)
    '以2004格式保存数据库为Dwg文件
    db.SaveAs("C:\test.dwg", DwgVersion.AC1800)
End Sub

End Class
