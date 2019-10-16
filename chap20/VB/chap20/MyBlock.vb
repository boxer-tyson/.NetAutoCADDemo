Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Colors

Public Class MyBlock
<CommandMethod("CB")> _
Public Function CreateBlock() As ObjectId
    Dim db As Database = HostApplicationServices.WorkingDatabase
    '用于返回所创建的块的对象Id
    Dim blockId As ObjectId = ObjectId.Null
    '创建一个BlockTableRecord类的对象，表示所要创建的块
    Dim record As New BlockTableRecord()
    '设置块名
    record.Name = "Room"
    Using trans As Transaction = db.TransactionManager.StartTransaction()
        Dim points As New Point3dCollection() '用于表示组成块的多段线的顶点
        points.Add(New Point3d(-18.0, -6.0, 0.0))
        points.Add(New Point3d(18.0, -6.0, 0.0))
        points.Add(New Point3d(18.0, 6.0, 0.0))
        points.Add(New Point3d(-18.0, 6.0, 0.0))
        '创建组成块的多段线
        Dim pline As New Polyline2d(Poly2dType.SimplePoly, points, 0.0, True, 0.0, 0.0, Nothing)
        record.Origin = points(3) '设置块的基点
        record.AppendEntity(pline) '将多段线加入到新建的BlockTableRecord对象
        '以写的方式打开块表
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForWrite)
        If bt.Has("Room") = False Then '判断是否存在名为"Room"的块
            blockId = bt.Add(record) '在块表中加入"Room"块
            trans.AddNewlyCreatedDBObject(record, True) '通知事务处理
            trans.Commit() '提交事务
        End If
    End Using
    Return blockId '返回创建的块的对象Id
End Function

<CommandMethod("CBWA")> _
Public Function CreateBlockWithAttributes() As ObjectId
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Dim blockId As ObjectId = ObjectId.Null
    Dim record As New BlockTableRecord()
    record.Name = "RMNUM"
    Using trans As Transaction = db.TransactionManager.StartTransaction()
        Dim points As New Point3dCollection()
        points.Add(New Point3d(-18.0, -6.0, 0.0))
        points.Add(New Point3d(18.0, -6.0, 0.0))
        points.Add(New Point3d(18.0, 6.0, 0.0))
        points.Add(New Point3d(-18.0, 6.0, 0.0))
        Dim pline As New Polyline2d(Poly2dType.SimplePoly, points, 0.0, True, 0.0, 0.0, Nothing)
        record.Origin = points(3)
        record.AppendEntity(pline)
        Dim attdef As New AttributeDefinition
        attdef.Position = New Point3d(0.0, 0.0, 0.0)
        attdef.Height = 8.0 '设置文字高度
        attdef.Rotation = 0.0 '设置文字旋转角度
        attdef.HorizontalMode = TextHorizontalMode.TextMid '设置水平对齐方式
        attdef.VerticalMode = TextVerticalMode.TextVerticalMid '设置垂直对齐方式
        attdef.Prompt = "Room Number:" '设置属性提示
        attdef.TextString = "0000" '设置属性的缺省值
        attdef.Tag = "NUMBER" '设置属性标签
        attdef.Invisible = False '设置不可见选项为假，即块可见
        attdef.Verifiable = False '设置验证方式为假
        attdef.Preset = False '设置预置方式为假
        attdef.Constant = False '设置常数方式为假
        record.Origin = points(3)
        record.AppendEntity(attdef)
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForWrite)
        If bt.Has("RMNUM") = False Then '判断是否存在名为"RMNUM"的块
            blockId = bt.Add(record) '在块表中加入"RMNUM"块
            trans.AddNewlyCreatedDBObject(record, True)
            trans.Commit()
        End If
    End Using
    Return blockId
End Function

<CommandMethod("InsertBlock")> _
Public Sub InsertBlock()
    '插入一个不带属性的简单块"Room"
    InsertBlockRef("Room", New Point3d(100, 100, 0), New Scale3d(2), 0)
End Sub

Public Sub InsertBlockRef(ByVal blockName As String, ByVal point As Point3d, ByVal scale As Scale3d, ByVal rotateAngle As Double)
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction
        '以读的方式打开块表
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
        '如果没有blockName表示的块，则程序返回
        If (bt.Has(blockName) = False) Then
            Return
        End If
        '以读的方式打开blockName表示的块
        Dim block As BlockTableRecord = trans.GetObject(bt(blockName), OpenMode.ForRead)
        '创建一个块参照并设置插入点
        Dim blockref As BlockReference = New BlockReference(point, bt(blockName))

        blockref.ScaleFactors = scale '设置块参照的缩放比例

        blockref.Rotation = rotateAngle '设置块参照的旋转角度
        '以写的方式打开当前空间（模型空间或图纸空间）
        Dim btr As BlockTableRecord = trans.GetObject(db.CurrentSpaceId, OpenMode.ForWrite)
        btr.AppendEntity(blockref) '在当前空间加入创建的块参照
        '通知事务处理加入创建的块参照
        trans.AddNewlyCreatedDBObject(blockref, True)
        trans.Commit() '提交事务处理以实现块参照的真实加入
    End Using
End Sub

<CommandMethod("InsertBlockWithAtt")> _
Public Sub InsertBlockWithAtt()
    '插入一个带属性的块"RMNUM"
    InsertBlockRefWithAtt("RMNUM", New Point3d(100, 150, 0), New Scale3d(1), 0.5 * Math.PI, "2007")
    '插入一个不带属性的简单块"Room"
    InsertBlockRefWithAtt("Room", New Point3d(100, 200, 0), New Scale3d(1), 0, Nothing)
End Sub

Public Sub InsertBlockRefWithAtt(ByVal blockName As String, ByVal point As Point3d, ByVal scale As Scale3d, ByVal rotateAngle As Double, ByVal roomnumber As String)
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
        If (bt.Has(blockName) = False) Then
            Return
        End If
        Dim block As BlockTableRecord = trans.GetObject(bt(blockName), OpenMode.ForRead)
        Dim blockref As BlockReference = New BlockReference(point, bt(blockName))
        blockref.ScaleFactors = scale
        blockref.Rotation = rotateAngle
        Dim btr As BlockTableRecord = trans.GetObject(db.CurrentSpaceId, OpenMode.ForWrite)
        btr.AppendEntity(blockref)
        trans.AddNewlyCreatedDBObject(blockref, True)
        '获取blockName块的遍历器，以实现对块中对象的访问
        Dim iterator As BlockTableRecordEnumerator = block.GetEnumerator()
        '如果blockName块包含属性
        If block.HasAttributeDefinitions Then
            '利用块遍历器对块中的对象进行遍历
            While iterator.MoveNext
                '获取块遍历器当前指向的块中的对象
                Dim obj As DBObject = trans.GetObject(iterator.Current, OpenMode.ForRead)
                '定义一个新的属性参照对象
                Dim att As New AttributeReference()
                '判断块遍历器当前指向的块中的对象是否为属性定义
                If TypeOf (obj) Is AttributeDefinition Then
                    '获取属性定义对象
                    Dim attdef As AttributeDefinition = obj
                    '从属性定义对象中继承相关的属性到属性参照对象中
                    att.SetAttributeFromBlock(attdef, blockref.BlockTransform)
                    '设置属性参照对象的位置为属性定义的位置+块参照的位置
                    att.Position = attdef.Position + blockref.Position.GetAsVector()
                    '判断属性定义的名称
                    Select Case attdef.Tag
                        '如果为"NUMBER"，则设置块参照的属性值
                        Case "NUMBER"
                            att.TextString = roomnumber
                    End Select
                    '判断块参照是否可写，如不可写，则切换为可写状态
                    If Not blockref.IsWriteEnabled Then
                        blockref.UpgradeOpen()
                    End If
                    '添加新创建的属性参照
                    blockref.AttributeCollection.AppendAttribute(att)
                    '通知事务处理添加新创建的属性参照
                    trans.AddNewlyCreatedDBObject(att, True)
                End If
            End While
        End If
        trans.Commit() '提交事务处理
    End Using
End Sub

<CommandMethod("BrowseBlock")> _
Public Sub BrowseBlock()
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = HostApplicationServices.WorkingDatabase
    '只选择块参照对象
    Dim filterValues As TypedValue() = {New TypedValue(DxfCode.Start, "INSERT")}
    Dim filter As New SelectionFilter(filterValues)
    Dim opts As New PromptSelectionOptions()
    '提示用户进行选择
    opts.MessageForAdding = "请选择图形中的块对象"
    '根据选择过滤器选择对象，本例中为块参照对象
    Dim res As PromptSelectionResult = ed.GetSelection(opts, filter)
    '如果选择失败，则返回
    If res.Status <> PromptStatus.OK Then
        Return
    End If
    '获取选择集对象，表示所选择的对象集合
    Dim ss As SelectionSet = res.Value
    '获取选择集中包含的对象ID，用于访问选择集中的对象
    Dim ids As ObjectId() = ss.GetObjectIds()
    Using trans As Transaction = db.TransactionManager.StartTransaction()
        '循环遍历选择集中的对象
        For Each blockId As ObjectId In ids
            '获取选择集中当前的对象，由于是只选择块参照对象，所以直接赋值为块对象
            Dim blockRef As BlockReference = trans.GetObject(blockId, OpenMode.ForRead)
            '获取当前块参照对象所属的块表记录对象
            Dim btr As BlockTableRecord = trans.GetObject(blockRef.BlockTableRecord, OpenMode.ForRead)
            '在命令行上显示块名
            ed.WriteMessage(vbCrLf & "块：" & btr.Name)
            '销毁块表记录对象
            btr.Dispose()
            '获取块参照的属性集合对象
            Dim atts As AttributeCollection = blockRef.AttributeCollection
            '循环遍历属性集合对象
            For Each attId As ObjectId In atts
                '获取当前的块参照属性
                Dim attRef As AttributeReference = trans.GetObject(attId, OpenMode.ForRead)
                '获取块参照属性的属性名和属性值
                Dim attString As String = " 属性名：" & attRef.Tag & " 属性值：" & attRef.TextString
                '在命令行上显示块的属性名和属性值
                ed.WriteMessage(attString)
            Next
        Next
    End Using
End Sub
<CommandMethod("ChangeBlockAtt")> _
Public Sub ChangeBlockAtt()
    '房间的新号码
    Dim roomNumber As String = "2008"
    '改变块中实体的颜色和块参照属性的值
    ChangeBlock("RMNUM", roomNumber)
End Sub

Public Sub ChangeBlock(ByVal blockName As String, ByVal roomNumber As String)
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Using trans As Transaction = db.TransactionManager.StartTransaction()
        '打开块表
        Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
        '打开块表中名为blockName的块表记录
        Dim btr As BlockTableRecord = trans.GetObject(bt(blockName), OpenMode.ForRead)
        '获取所有名为blockName的块参照
        Dim blcokRefIds As ObjectIdCollection = btr.GetBlockReferenceIds(True, True)
        '循环遍历块参照
        For Each blockRefId As ObjectId In blcokRefIds
            '打开当前块参照
            Dim blockRef As BlockReference = trans.GetObject(blockRefId, OpenMode.ForRead)
            '获取当前块参照的属性集合
            Dim blockRefAtts As AttributeCollection = blockRef.AttributeCollection
            '循环遍历属性集合
            For Each attId As ObjectId In blockRefAtts
                '获取当前属性参照对象
                Dim attRef As AttributeReference = trans.GetObject(attId, OpenMode.ForRead)
                '只改变NUMBER属性值为"0000"的属性值为roomNumber
                Select Case attRef.Tag
                    Case "NUMBER"
                        If attRef.TextString = "0000" Then
                            attRef.UpgradeOpen() '切换属性参照对象为可写状态
                            attRef.TextString = roomNumber
                        End If
                End Select
            Next attId
        Next blockRefId
        trans.Commit()
    End Using
Application.UpdateScreen()
End Sub
End Class
