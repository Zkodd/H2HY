﻿using System;

namespace H2HY.Services;

/// <summary>
/// Displaying buttons in the message box.
/// </summary>
public enum MessageBoxButton
{
    /// <summary>
    /// The message box displays an OK button.
    /// </summary>
    OK = 0,

    /// <summary>
    /// The message box displays OK and Cancel buttons.
    /// </summary>
    OKCancel = 1,

    /// <summary>
    /// The message box displays Yes, No, and Cancel buttons.
    /// </summary>
    YesNoCancel = 3,

    /// <summary>
    /// The message box displays Yes and No buttons.
    /// </summary>
    YesNo = 4,
}

/// <summary>
/// Specifies the icon that is displayed by a message box.
/// Source:
/// https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.messageboxicon?view=windowsdesktop-7.0
/// </summary>
public enum MessageBoxIcon
{
    /// <summary>
    /// No icon is displayed.
    /// </summary>
    None = 0,

    /// <summary>
    /// The message box contains a symbol consisting of white X in a circle with a red background.
    /// </summary>
    Error = 16,

    /// <summary>
    /// The message box contains a symbol consisting of a white X in a circle with a red background.
    /// </summary>
    Hand = 16,

    /// <summary>
    /// The message box contains a symbol consisting of white X in a circle with a red background.
    /// </summary>
    Stop = 16,

    /// <summary>
    /// The message box contains a symbol consisting of a question mark in a circle.
    /// </summary>
    Question = 32,

    /// <summary>
    /// The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.
    /// </summary>
    Exclamation = 48,

    /// <summary>
    /// The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.
    /// </summary>
    Warning = 48,

    /// <summary>
    /// The message box contains a symbol consisting of a lowercase letter i in a circle.
    /// </summary>
    Information = 64,

    /// <summary>
    /// The message box contains a symbol consisting of a lowercase letter i in a circle.
    /// </summary>
    Asterisk = 64,
}

/// <summary>
/// The message box result.
/// </summary>
public enum MessageBoxResult
{
    /// <summary>
    ///     The message box returns no result.
    /// </summary>
    None = 0,

    /// <summary>
    ///   The result value of the message box is OK.
    /// </summary>
    OK = 1,

    /// <summary>
    ///     The result value of the message box is Cancel.
    /// </summary>
    Cancel = 2,

    /// <summary>
    ///     The result value of the message box is Yes.
    /// </summary>
    Yes = 6,

    /// <summary>
    ///     The result value of the message box is No.
    /// </summary>
    No = 7,
}
/// <summary>
/// Opens view models in windows or dialogs.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Opens given view model as a dialog. Result on close will be true or false.
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="callBack">call back will return dialog result in case of an modal window.</param>
    void ShowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callBack);

    /// <summary>
    /// Opens a windows like confirmation box.
    /// </summary>
    /// <param name="message">Message, written inside the box</param>
    /// <param name="caption">Message box caption</param>
    /// <param name="buttons">shown buttons</param>
    /// <param name="icon">used icon</param>
    /// <returns></returns>
    MessageBoxResult ShowMessageBox(string message, string caption, MessageBoxButton buttons, MessageBoxIcon icon);

    /// <summary>
    ///  Opens given view model as a modal dialog. Result on close will be true or false.
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="callBack"></param>
    void ShowModalDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callBack);
}