[CustomMessages]
AppName=Inferno
[Files]
; Application files
; R scripts
Source: temp.png; DestDir: {commonappdata}\Inferno
; Icon, readme, help, config and license files
; Tools (Peptide File Extractor etc)
Source: ..\bin\Debug\Tools\PRISM.dll; DestDir: {app}\Tools
Source: ..\bin\Debug\Tools\FileConcatenator.dll; DestDir: {app}\Tools
Source: ..\bin\Debug\Tools\PeptideFileExtractor.dll; DestDir: {app}\Tools
Source: ..\bin\Debug\Tools\PeptideFileExtractorConsole.exe; DestDir: {app}\Tools
Source: ..\bin\Release\Inferno.vshost.exe; DestDir: {app}
Source: ..\bin\Release\Inferno.exe; DestDir: {app}
Source: ..\bin\Release\Interop.IasHelperLib.dll; DestDir: {app}
Source: ..\bin\Release\Interop.StatConnControls.dll; DestDir: {app}
Source: ..\bin\Release\Interop.STATCONNECTORCLNTLib.dll; DestDir: {app}
Source: ..\bin\Release\LumenWorks.Framework.IO.dll; DestDir: {app}
Source: ..\bin\Release\LumenWorks.Framework.IO.pdb; DestDir: {app}
Source: ..\bin\Release\LumenWorks.Framework.IO.xml; DestDir: {app}
Source: ..\bin\Release\Inferno.exe.config; DestDir: {app}
Source: ..\bin\Release\Inferno.vshost.exe.config; DestDir: {app}
Source: ..\bin\Release\Inferno.vshost.exe.manifest; DestDir: {app}
Source: ..\bin\Release\InputBox.dll; DestDir: {app}
Source: ..\bin\Release\AxInterop.STATCONNECTORCLNTLib.dll; DestDir: {app}
Source: ..\bin\Release\Interop.StatConnectorCommonLib.dll; DestDir: {app}
Source: ..\bin\Release\Interop.STATCONNECTORSRVLib.dll; DestDir: {app}
Source: ..\bin\Release\ICSharpCode.SharpZipLib.dll; DestDir: {app}
Source: ..\bin\Release\ZedGraph.dll; DestDir: {app}
Source: ..\bin\Release\ZedGraph.xml; DestDir: {app}
Source: ..\bin\Debug\inferno.conf; DestDir: {app}
Source: ..\bin\Debug\Inferno.RData; DestDir: {app}
Source: ..\bin\Debug\Inferno_ggplots.RData; DestDir: {app}
Source: ..\bin\Debug\Inferno_stdplots.RData; DestDir: {app}
Source: ..\bin\Debug\InfernoHelp.chm; DestDir: {app}
Source: Images\textdoc.ico; DestDir: {app}
Source: ..\Resources\delete_16x.ico; DestDir: {app}
Source: ..\Resources\inferno.ico; DestDir: {app}
Source: ..\Resources\inferno_help.ico; DestDir: {app}
Source: readme.txt; DestDir: {app}
Source: License.rtf; DestDir: {app}
Source: readme.rtf; DestDir: {app}

[Dirs]
Name: {commonappdata}\Inferno; Flags: uninsalwaysuninstall
Name: {app}\Tools; Flags: uninsalwaysuninstall
[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
[Icons]
Name: {group}\Inferno; Filename: {app}\inferno.exe; IconFilename: {app}\inferno.ico; IconIndex: 0; Comment: Inferno for Proteomics
Name: {group}\Inferno Help; Filename: {app}\InfernoHelp.chm; IconFilename: {app}\inferno_help.ico; Comment: Inferno Help File; IconIndex: 0
Name: {group}\ReadMe File; Filename: {app}\readme.rtf; IconFilename: {app}\textdoc.ico; Comment: Inferno ReadMe; IconIndex: 0
Name: {group}\License File; Filename: {app}\License.rtf; IconFilename: {app}\textdoc.ico; Comment: Inferno License; IconIndex: 0
Name: {group}\Uninstall Inferno; Filename: {uninstallexe}; IconFilename: {app}\delete_16x.ico; IconIndex: 0
Name: {commondesktop}\{cm:AppName}; Filename: {app}\{cm:AppName}.exe; Tasks: desktopicon; IconFilename: {app}\inferno.ico; Comment: Inferno; IconIndex: 0
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\{cm:AppName}; Filename: {app}\{cm:AppName}.exe; Tasks: quicklaunchicon; IconFilename: {app}\inferno.ico; Comment: Inferno for Proteomics; IconIndex: 0
[Setup]
AppName={cm:AppName}
AppVerName=Inferno
AppID=InfernoId
AppPublisher=Translational Genomics Research Institute
AppPublisherURL=http://www.tgen.org
AppSupportURL=http://www.tgen.org
AppUpdatesURL=http://www.tgen.org
DefaultDirName={pf}\Inferno
DefaultGroupName=Inferno
AppCopyright=© Translational Genomics Research Institute
LicenseFile=.\License.rtf
PrivilegesRequired=poweruser
OutputBaseFilename=InfernoSetup
VersionInfoVersion=1.0
VersionInfoCompany=TGen
VersionInfoDescription=Inferno for Proteomics
VersionInfoCopyright=TGen
DisableFinishedPage=true
ShowLanguageDialog=no
SetupIconFile=..\Deploy\Images\infernoSetup.ico
InfoBeforeFile=.\readme.rtf
ChangesAssociations=true
WizardImageFile=..\Deploy\Images\InfernoSetupSideImage.bmp
WizardSmallImageFile=..\Deploy\Images\InfernoSetupSmallImage.bmp
InfoAfterFile=.\postinstall.rtf
EnableDirDoesntExistWarning=true
AlwaysShowDirOnReadyPage=true
UninstallDisplayIcon={app}\dante_setup.ico
ShowTasksTreeLines=true
OutputDir=.\Output
[Registry]
;Root: HKCR; Subkey: .dnt; ValueType: string; ValueName: ; ValueData: DAnTEFile; Flags: uninsdeletevalue
;Root: HKCR; Subkey: DAnTEFile; ValueType: string; ValueName: ; ValueData: DAnTE File; Flags: uninsdeletekey
;Root: HKCR; Subkey: DAnTEFile\DefaultIcon; ValueType: string; ValueName: ; ValueData: {app}\DAnTE.EXE,0
;Root: HKCR; Subkey: DAnTEFile\shell\open\command; ValueType: string; ValueName: ; ValueData: """{app}\DAnTE.EXE"" ""%1"""
Root: HKCR; SubKey: .dnt; ValueType: string; ValueData: InfernoSessionfile; Flags: uninsdeletekey
Root: HKCR; SubKey: InfernoSessionfile; ValueType: string; ValueData: Inferno Session File; Flags: uninsdeletekey
Root: HKCR; SubKey: InfernoSessionfile\Shell\Open\Command; ValueType: string; ValueData: """{app}\Inferno.exe"" ""%1"""; Flags: uninsdeletevalue
Root: HKCR; Subkey: InfernoSessionfile\DefaultIcon; ValueType: string; ValueData: {app}\inferno.ico,0; Flags: uninsdeletevalue
[UninstallDelete]
Name: {app}; Type: filesandordirs
Name: {app}\Tools; Type: filesandordirs
