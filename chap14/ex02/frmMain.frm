VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Begin VB.Form frmMain 
   Caption         =   "批量文字替换"
   ClientHeight    =   3885
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4500
   LinkTopic       =   "Form1"
   ScaleHeight     =   3885
   ScaleWidth      =   4500
   StartUpPosition =   3  'Windows Default
   Begin MSComDlg.CommonDialog comDlg 
      Left            =   3600
      Top             =   480
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.TextBox txtFind 
      Height          =   300
      Left            =   120
      TabIndex        =   6
      Top             =   480
      Width           =   1335
   End
   Begin VB.TextBox txtReplace 
      Height          =   300
      Left            =   2280
      TabIndex        =   5
      Top             =   480
      Width           =   1335
   End
   Begin VB.ListBox lstFile 
      Height          =   2010
      Left            =   120
      TabIndex        =   4
      Top             =   1200
      Width           =   4215
   End
   Begin VB.CommandButton cmdOpen 
      Caption         =   "添加(&A)"
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   3360
      Width           =   855
   End
   Begin VB.CommandButton cmdDelete 
      Caption         =   "删除(&D)"
      Height          =   375
      Left            =   1080
      TabIndex        =   2
      Top             =   3360
      Width           =   855
   End
   Begin VB.CommandButton cmdOk 
      Caption         =   "确定(&O)"
      Height          =   375
      Left            =   2040
      TabIndex        =   1
      Top             =   3360
      Width           =   1095
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "取消(&C)"
      Height          =   375
      Left            =   3240
      TabIndex        =   0
      Top             =   3360
      Width           =   1095
   End
   Begin VB.Label Label1 
      Caption         =   "文字替换："
      Height          =   255
      Left            =   120
      TabIndex        =   9
      Top             =   120
      Width           =   975
   End
   Begin VB.Label Label2 
      Caption         =   "替换为->"
      Height          =   255
      Left            =   1560
      TabIndex        =   8
      Top             =   555
      Width           =   735
   End
   Begin VB.Label Label3 
      Caption         =   "文件列表"
      Height          =   255
      Left            =   120
      TabIndex        =   7
      Top             =   900
      Width           =   1815
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim acadApp As AcadApplication      ' AutoCAD应用程序对象
Dim acadDoc As AcadDocument         ' 当前活动文档对象

Const LB_ITEMFROMPOINT = &H1A9

Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
        (ByVal hWnd As Long, ByVal wMsg As Long, _
        ByVal wParam As Long, lParam As Any) As Long

Private Sub cmdCancel_Click()
    acadApp.Quit
    End
End Sub

Private Sub cmdDelete_Click()
    ' 确认列表框包含列表项
    If lstFile.ListCount >= 1 Then
        ' 如果没有选中的内容，用上一次的列表项
        If lstFile.ListIndex = -1 Then
            MsgBox "请选择列表中的图形名称！"
            Exit Sub
        End If
        lstFile.RemoveItem (lstFile.ListIndex)
    End If
End Sub

Private Sub cmdOk_Click()
    Dim adText As AcadText
    Dim adMText As AcadMText
    Dim adSS As AcadSelectionSet
    Dim fType(0 To 1) As Integer, fData(0 To 1)
    Dim i As Integer
    
    If txtFind.Text = "" Or txtReplace.Text = "" Then
        MsgBox "输入所要替换的字符串内容！"
        Exit Sub
    End If
    If lstFile.ListCount = 0 Then
        MsgBox "请添加所要操作的图形！"
        Exit Sub
    End If
    
    ' 获得替换数据
    Dim strFind As String
    Dim strReplace As String
    strFind = txtFind.Text
    strReplace = txtReplace.Text

    ' 打开图形进行操作
    For i = 0 To lstFile.ListCount - 1
        Call ReplaceTextInDwg(lstFile.List(i), strFind, strReplace)
    Next i
    
    ' 在退出应用程序之前关闭AutoCAD
    acadApp.Quit
    End
End Sub

Private Sub cmdOpen_Click()
    On Error GoTo errHandle
    
    Dim i As Integer
    Dim Y As Integer
    Dim Z As Integer
    Dim fileNames() As String
    
    With comDlg
        .CancelError = True
        .MaxFileSize = 32767
        .Flags = cdlOFNHideReadOnly Or cdlOFNAllowMultiselect Or cdlOFNExplorer Or cdlOFNNoDereferenceLinks
        .DialogTitle = "选择图形文件"
        .Filter = "图形文件(*.dwg)|*.dwg|所有文件(*.*)|*.*"
        .FileName = ""
        .ShowOpen
    End With
    
    comDlg.FileName = comDlg.FileName & Chr(0)  '这些文件名是用空字符Chr(0)分隔符，而不是空格分隔符隔开
    
    Z = 1
    For i = 1 To Len(comDlg.FileName)
        'InStr函数，返回 Variant (Long)，指定一字符串在另一字符串中最先出现的位置
        '语法 InStr(起点位置, string1, string2)
        i = InStr(Z, comDlg.FileName, Chr(0))
        If i = 0 Then Exit For
        ReDim Preserve fileNames(Y)
        'Mid函数，返回 Variant (String)，其中包含字符串中指定数量的字符
        '语法 Mid(string, start[, length])
        fileNames(Y) = Mid(comDlg.FileName, Z, i - Z)
        Z = i + 1
        Y = Y + 1
    Next i

    '向列表框中添加对象
    Dim count As Integer
    count = lstFile.ListCount
    If Y = 1 Then
        If Not HasItem(fileNames(Y - 1)) Then
            lstFile.AddItem fileNames(Y - 1), count
        End If
    Else
        For i = 1 To Y - 1
            If StrComp(Right$(fileNames(0), 1), "\") = 0 Then
                fileNames(i) = fileNames(0) & fileNames(i)
            Else
                fileNames(i) = fileNames(0) & "\" & fileNames(i)
            End If
            
            If Not HasItem(fileNames(i)) Then
                lstFile.AddItem fileNames(i), i - 1 + count
            End If
        Next i
    End If

errHandle:

End Sub

Private Sub Form_Load()
    On Error Resume Next
    ' 获得正在运行的AutoCAD应用程序对象
    Set acadApp = GetObject(, "AutoCAD.Application.17")

    If Err Then
        Err.Clear
        ' 创建一个新的AutoCAD应用程序对象
        Set acadApp = CreateObject("AutoCAD.Application.17")
        
        If Err Then
            MsgBox Err.Description
            Exit Sub
        End If
    End If
    
    ' 显示AutoCAD应用程序
    acadApp.Visible = True
    
    lstFile.Clear
End Sub

' 对某个图形进行文字替换
Private Sub ReplaceTextInDwg(ByVal strDwgName As String, ByVal strFind As String, _
                            ByVal strReplace As String)
    ' 打开指定的图形
    acadApp.Documents.Open strDwgName
    Set acadDoc = acadApp.ActiveDocument
    
    Dim ent As AcadEntity
    For Each ent In acadDoc.ModelSpace
        If TypeOf ent Is AcadText Or TypeOf ent Is AcadMText Then
            With ent
                If InStr(.TextString, strFind) Then .TextString = ReplaceStr(.TextString, strFind, strReplace, False)
            End With
        End If
    Next ent
    
    ' 保存并关闭图形
    acadDoc.Close True
End Sub

' 对字符串中指定的字符进行替换
Public Function ReplaceStr(ByVal searchStr As String, ByVal oldStr As String, _
        ByVal newStr As String, ByVal firstOnly As Boolean) As String
    '对错误操作的处理
    If searchStr = "" Then Exit Function
    If oldStr = "" Then Exit Function

    ReplaceStr = ""
    Dim i As Integer, oldStrLen As Integer, holdStr As String, StrLoc As Integer
    
    '计算原来字符串的长度
    oldStrLen = Len(oldStr)
    StrLoc = InStr(searchStr, oldStr)
    
    While StrLoc > 0
        '获得图形中文字对象位于查找字符串之前的字符串
        holdStr = holdStr & Left(searchStr, StrLoc - 1) & newStr
        '获得文字对象位于查找字符串之后的字符串
        searchStr = Mid(searchStr, StrLoc + oldStrLen)
        StrLoc = InStr(searchStr, oldStr)
        If firstOnly Then ReplaceStr = holdStr & searchStr: Exit Function
    Wend
    
    ReplaceStr = holdStr & searchStr
End Function

' 列表框中是否存在指定名称的项目
Private Function HasItem(ByVal strDwgName As String) As Boolean
    HasItem = False
    
    Dim i As Integer
    For i = 0 To lstFile.ListCount - 1
        If StrComp(lstFile.List(i), strDwgName, vbTextCompare) = 0 Then
            HasItem = True
            Exit Function
        End If
    Next i
End Function

Private Sub lstFile_DblClick()
    Dim pt As Variant
    ' 将焦点切换到AutoCAD
    ForceForegroundWindow acadApp.hWnd
    pt = acadApp.ActiveDocument.Utility.GetPoint(, "拾取一点：")
    
    ' 焦点切换回当前的窗体
    ForceForegroundWindow frmMain.hWnd
    
    ' 显示点的坐标
    MsgBox "拾取点的坐标为：(" & pt(0) & "," & pt(1) & "," & pt(2) & ")"
End Sub

Private Sub lstFile_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
    Dim lXPoint As Long
    Dim lYPoint As Long
    Dim lIndex As Long
    
    If Button = 0 Then ' 确定在移动鼠标的同时没有按下功能键或者鼠标键
        ' 获得光标的位置，以像素为单位
        lXPoint = CLng(X / Screen.TwipsPerPixelX)
        lYPoint = CLng(Y / Screen.TwipsPerPixelY)
        
        ' 显示列表框中元素的内容
        With lstFile
            ' 获得光标所在的行的索引
            lIndex = SendMessage(.hWnd, LB_ITEMFROMPOINT, 0, _
                            ByVal ((lYPoint * 65536) + lXPoint))
            
            ' 将ListBox的Tooltip属性设置为该行的文本
            If (lIndex >= 0) And (lIndex <= .ListCount) Then
                .ToolTipText = .List(lIndex)
            Else
                .ToolTipText = ""
            End If
        End With
    End If
End Sub


