# H2HY

Overall:

Stores:
-> use of mediator pattern

Version changes

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
