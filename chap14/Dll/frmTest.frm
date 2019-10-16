VERSION 5.00
Begin VB.Form frmTest 
   Caption         =   "VBAÊ¾Àý"
   ClientHeight    =   1335
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   5385
   LinkTopic       =   "Form1"
   MinButton       =   0   'False
   ScaleHeight     =   1335
   ScaleWidth      =   5385
   StartUpPosition =   3  'Windows Default
   Begin VB.Label Label1 
      Caption         =   "Hello World"
      BeginProperty Font 
         Name            =   "ËÎÌå"
         Size            =   42
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   360
      TabIndex        =   0
      Top             =   240
      Width           =   4695
   End
End
Attribute VB_Name = "frmTest"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Declare Function SetParent Lib "user32" (ByVal hWndChild As Long, ByVal hWndNewParent As Long) As Long
Private Declare Function GetParent Lib "user32" (ByVal hwnd As Long) As Long
Private m_oApp As Object

Public Property Set Application(ByVal vNewValue As Object)
    Set m_oApp = vNewValue
End Property

Private Sub Form_Load()
    SetParent Me.hwnd, GetParent(GetParent(m_oApp.ActiveDocument.hwnd))
End Sub
