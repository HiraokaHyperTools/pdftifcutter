; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

!define APP "pdftifcutter"
!define VER "0.3"

; The name of the installer
Name "${APP} ${VER}"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\${APP}" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section ""

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File /r /x "*.vshost.*" "bin\x86\DEBUG\*.*"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM "SOFTWARE\${APP}" "Install_Dir" "$INSTDIR"
  WriteRegStr HKLM "SOFTWARE\${APP}" "Ver" "${VER}"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${APP}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayIcon" "$INSTDIR\${APP}.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayVersion" "${VER}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "HelpLink" "https://github.com/HiraokaHyperTools/pdftifcutter"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove files and uninstaller
  Delete "$INSTDIR\FreeImage.dll"
  Delete "$INSTDIR\FreeImageNET.dll"
  Delete "$INSTDIR\itextsharp.dll"
  Delete "$INSTDIR\pdftifcutter.exe"
  Delete "$INSTDIR\uninstall.exe"

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKLM "SOFTWARE\${APP}"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
