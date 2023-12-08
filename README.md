# H2HY -  Deprecated

This package has been deprecated as it is legacy and is no longer maintained.

My personal handy-dandy basic helper for MVVVM issues. Light ware - no schnickschnack.
INotifyPropertyChanged in view model base, RelayCommand, ICommand/CommandManager,
Basic Stores, Fluent-Syntax, Provider, file open/save service, FolderPicker, etc.


Project Settings:

<Nullable>enable</Nullable>
<TargetFramework>net6.0-windows</TargetFramework>
<OutputType>Library</OutputType>
<GenerateAssemblyInfo>false</GenerateAssemblyInfo>
<UseWPF>true</UseWPF>
<ImportWindowsDesktopTargets>true</ImportWindowsDesktopTargets>

Version changes

6.9.0.93

	- H2HYFluentCollection added
	- H2H2YFluentList: removed AddRange and LoadAll

6.0.0.92

	- added NavigationServiceDI
	- added dependency Microsoft.Extensions.DependencyInjection

6.0.0.91
 
	- fixed bug in showModelDialog

6.0.0.89

	- typo in DialogService(RegisterDialog)
	- added INavigationDialogStore
	- removed NavigationDialogService(use NavigationService instead)

6.0.0.88

	- fixed modal/dialog behaviour in DialogServiceWPF
	
6.0.0.87

	- add MakeValidFileName

6.0.0.86

	- separate ViewModel base and INotifyPropertyChanged

6.0.0.85

	- added NavigationDialogService
	- fixed typos

6.0.0.84

	- added parameter constructor for SplitnavigationStore.
	- fixed typos

6.0.0.83

	- INavigationStore changes. Removed INavigationModal store. All navigation services should use the navigation store instead.
	- renamed Toolkit/ToolKit to Tools

6.0.0.82

	- created namespace Navigation, moved all navigation stores
	- added split navigation
	- fixed typos
	- removed modal namespace
