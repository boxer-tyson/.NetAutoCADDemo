Attribute VB_Name = "Module1"
Option Explicit

Private Declare Function GetWindowThreadProcessId Lib "user32" _
    (ByVal hWnd As Long, lpdwProcessId As Long) As Long
Private Declare Function AttachThreadInput Lib "user32" _
    (ByVal idAttach As Long, ByVal idAttachTo As Long, ByVal fAttach As Long) As Long
Private Declare Function GetForegroundWindow Lib "user32" () As Long
Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As Long) As Long
Private Declare Function IsIconic Lib "user32" (ByVal hWnd As Long) As Long
Private Declare Function ShowWindow Lib "user32" _
    (ByVal hWnd As Long, ByVal nCmdShow As Long) As Long

Private Const SW_SHOW = 5
Private Const SW_RESTORE = 9

Public Function ForceForegroundWindow(ByVal hWnd As Long) As Boolean
   Dim ThreadID1 As Long    ' 线程ID
   Dim ThreadID2 As Long    ' 线程ID
   Dim nRet As Long
   
   ' 如果指定的窗体已经在前台，不做任何操作
   If hWnd = GetForegroundWindow() Then
      ForceForegroundWindow = True
   Else
      ' 首先获得指定窗体相关的线程和当前前台窗口所在的线程
      ThreadID1 = GetWindowThreadProcessId(GetForegroundWindow, ByVal 0&)
      ThreadID2 = GetWindowThreadProcessId(hWnd, ByVal 0&)
      
      ' 通过共享输入状态，两个线程分享当前窗口
      If ThreadID1 <> ThreadID2 Then
         Call AttachThreadInput(ThreadID1, ThreadID2, True)
         nRet = SetForegroundWindow(hWnd)
         Call AttachThreadInput(ThreadID1, ThreadID2, False)
      Else
         nRet = SetForegroundWindow(hWnd)
      End If
      
      ' 恢复和重画
      If IsIconic(hWnd) Then
         Call ShowWindow(hWnd, SW_RESTORE)
      Else
         Call ShowWindow(hWnd, SW_SHOW)
      End If
      
      ' 精确地返回函数执行结果
      ForceForegroundWindow = CBool(nRet)
   End If
End Function

