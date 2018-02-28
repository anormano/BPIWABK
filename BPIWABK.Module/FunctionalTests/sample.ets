;#DropDB BPIWABKEasyTest

#Application BPIWABK.Win
#Application BPIWABK.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef BPIWABK.Win

*Action About...
 
*Action OK

#EndIf

#IfDef BPIWABK.Web
*Action Navigation(My Details)
#EndIf
