; This is an Inno Setup configuration file
; https://jrsoftware.org/isinfo.php

#define ApplicationVersion GetFileVersion('..\bin\Debug\Inferno.exe')

[CustomMessages]
AppName=InfernoRDN

[Messages]
WelcomeLabel2=This will install [name/ver] on your computer.%n%nInfernoRDN can perform various downstream data analysis, data reduction, and data comparison tasks including normalization, hypothesis testing, clustering, and heatmap generation.%n%nPlease install R 4.x prior to running InfernoRDN for the first time. R 3.6 or newer is also supported.

[Dirs]
Name: {commonappdata}\InfernoRDN; Flags: uninsalwaysuninstall
Name: "{app}\Documentation"
Name: "{app}\Sample_Data_Files"

[Files]
; Application files
; R scripts
; Icon, readme, help, config and license files
Source: ..\bin\Debug\InfernoHelp.chm;                       DestDir: {app}
Source: ..\bin\Debug\inferno.conf;                          DestDir: {app}
Source: ..\bin\Debug\Inferno.exe.config;                    DestDir: {app}
Source: ..\bin\Debug\ICSharpCode.SharpZipLib.dll;           DestDir: {app}
Source: ..\bin\Debug\InputBox.dll;                          DestDir: {app}
Source: ..\bin\Debug\Interop.IasHelperLib.dll;              DestDir: {app}
Source: ..\bin\Debug\LumenWorks.Framework.IO.dll;           DestDir: {app}
Source: ..\bin\Debug\RDotNet.dll;                           DestDir: {app}
Source: ..\bin\Debug\RDotNet.NativeLibrary.dll;             DestDir: {app}
Source: ..\bin\Debug\DynamicInterop.dll;                    DestDir: {app}
Source: ..\bin\Debug\extensibility.dll;                     DestDir: {app}
Source: ..\bin\Debug\ZedGraph.dll;                          DestDir: {app}
Source: ..\bin\Debug\Inferno.exe;                           DestDir: {app}
Source: ..\bin\Debug\Inferno.pdb;                           DestDir: {app}
Source: ..\bin\Debug\LumenWorks.Framework.IO.pdb;           DestDir: {app}
Source: ..\bin\Debug\Inferno.RData;                         DestDir: {app}
Source: ..\bin\Debug\Inferno_stdplots.RData;                DestDir: {app}
Source: ..\bin\Debug\ZedGraph.xml;                          DestDir: {app}
                                                             
Source: Images\textdoc.ico;                                 DestDir: {app}
Source: ..\Resources\delete_16x.ico;                        DestDir: {app}
Source: ..\Resources\inferno.ico;                           DestDir: {app}
Source: ..\Resources\inferno_help.ico;                      DestDir: {app}
Source: ..\README.md;                                       DestDir: {app}
Source: ..\RevisionHistory.txt;                             DestDir: {app}
                                                    
Source: License.rtf;                                        DestDir: {app}
Source: readme.rtf;                                         DestDir: {app}

Source: ..\Documentation\RollupMethods_InfernoRDN.pdf;                   DestDir: {app}\Documentation
Source: ..\Documentation\InfernoRDN_Overview.pdf;                        DestDir: {app}\Documentation
Source: ..\Documentation\InfernoRDN_Step_by_step_Instructions.pdf;       DestDir: {app}\Documentation
                                                                     
Source: ..\Sample_Input_Files\FactorDefinitionExample.txt;               DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\heatmap_SomeMissingData.csv;               DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\SampleInput4DAnTE.csv;                     DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\Sample_Session_File_with_Factors.dnt;      DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\Sample_Expressions_Session_File.dnt;       DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\VolcanoPlot_InputData.csv;                 DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\VolcanoPlot_Example.dnt;                   DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\VolcanoPlot_Example.xlsx;                  DestDir: {app}\Sample_Data_Files
Source: ..\Sample_Input_Files\Volcano_Plot_Steps.pdf;                    DestDir: {app}\Sample_Data_Files

Source: ..\Documentation\DAnTE_SupplementaryInfo\bioinformatics_supplement.dnt;    DestDir: {app}\Sample_Data_Files


[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon};               GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon};       GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Icons]
Name: {group}\InfernoRDN;           Filename: {app}\inferno.exe;         IconFilename: {app}\inferno.ico; IconIndex: 0; Comment: InfernoRDN for Proteomics
Name: {group}\Inferno Help;         Filename: {app}\InfernoHelp.chm;     IconFilename: {app}\inferno_help.ico; Comment: Inferno Help File; IconIndex: 0
Name: {group}\ReadMe File;          Filename: {app}\readme.rtf;          IconFilename: {app}\textdoc.ico; Comment: Inferno ReadMe; IconIndex: 0
Name: {group}\License File;         Filename: {app}\License.rtf;         IconFilename: {app}\textdoc.ico; Comment: Inferno License; IconIndex: 0
Name: {group}\Uninstall Inferno;    Filename: {uninstallexe};            IconFilename: {app}\delete_16x.ico; IconIndex: 0
Name: {group}\Documentation;        Filename: {app}\Documentation
Name: {group}\Sample Data Files;    Filename: {app}\Sample_Data_Files

Name: {commondesktop}\{cm:AppName};                                        Filename: {app}\Inferno.exe; Tasks: desktopicon; IconFilename: {app}\inferno.ico; Comment: Inferno; IconIndex: 0
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\{cm:AppName}; Filename: {app}\Inferno.exe; Tasks: quicklaunchicon; IconFilename: {app}\inferno.ico; Comment: Inferno for Proteomics; IconIndex: 0

[Setup]
AppName=InfernoRDN
;AppVerName=InfernoRDN
AppVersion={#ApplicationVersion}
AppID=InfernoId
AppPublisher=Pacific Northwest National Laboratory
AppPublisherURL=http://omics.pnl.gov/software
AppSupportURL=http://omics.pnl.gov/software
AppUpdatesURL=http://omics.pnl.gov/software
ArchitecturesAllowed=x64 x86
ArchitecturesInstallIn64BitMode=x64
DefaultDirName={autopf}\InfernoRDN
DefaultGroupName=InfernoRDN
AppCopyright=© PNNL and TGEN
LicenseFile=.\License.rtf
PrivilegesRequired=admin
OutputBaseFilename=InfernoRDNSetup
; The following is set to "no" to silence the warning about updating a used-based registry key (in HKCU)
UsedUserAreasWarning=no
VersionInfoVersion={#ApplicationVersion}
VersionInfoCompany=PNNL
VersionInfoDescription=InfernoRDN for Proteomics
VersionInfoCopyright=PNNL
DisableFinishedPage=yes
DisableWelcomePage=no
ShowLanguageDialog=no
SetupIconFile=.\Images\infernoSetup.ico
InfoBeforeFile=.\readme.rtf
ChangesAssociations=yes
WizardImageFile=.\Images\InfernoSetupSideImage.bmp
WizardSmallImageFile=.\Images\InfernoSetupSmallImage.bmp
WizardStyle=modern
InfoAfterFile=.\postinstall.rtf
EnableDirDoesntExistWarning=no
AlwaysShowDirOnReadyPage=yes
UninstallDisplayIcon={app}\dante_setup.ico
ShowTasksTreeLines=yes
OutputDir=.\Output
[Registry]
;Root: HKCR; Subkey: .dnt; ValueType: string; ValueName: ; ValueData: DAnTEFile; Flags: uninsdeletevalue
;Root: HKCR; Subkey: DAnTEFile; ValueType: string; ValueName: ; ValueData: DAnTE File; Flags: uninsdeletekey
;Root: HKCR; Subkey: DAnTEFile\DefaultIcon; ValueType: string; ValueName: ; ValueData: {app}\DAnTE.EXE,0
;Root: HKCR; Subkey: DAnTEFile\shell\open\command; ValueType: string; ValueName: ; ValueData: """{app}\DAnTE.EXE"" ""%1"""
Root: HKCR; SubKey: ".dnt"; ValueType: string; ValueData: "InfernoSessionfile"; Flags: uninsdeletekey
Root: HKCR; SubKey: "InfernoSessionfile"; ValueType: string; ValueData: "Inferno Session File"; Flags: uninsdeletekey
Root: HKCR; SubKey: "InfernoSessionfile\Shell\Open\Command"; ValueType: string; ValueData: """{app}\Inferno.exe"" ""%1"""; Flags: uninsdeletevalue
Root: HKCR; Subkey: "InfernoSessionfile\DefaultIcon"; ValueType: string; ValueData: "{app}\inferno.ico,0"; Flags: uninsdeletevalue
Root: HKCU; Subkey: "Software\PNNL\Inferno"; ValueType: string; ValueName: "BioconductorCheckLatestInfernoVersion"; ValueData: ""; Flags: uninsdeletevalue

[UninstallDelete]
Name: {app}; Type: filesandordirs
Name: {app}\Tools; Type: filesandordirs
