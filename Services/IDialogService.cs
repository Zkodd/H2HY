using System;

namespace H2HY.Services
{

    public enum MessageBoxButton
    {
        // Summary:
        //     The message box displays an OK button.
        OK = 0,
        //
        // Summary:
        //     The message box displays OK and Cancel buttons.
        OKCancel = 1,
        //
        // Summary:
        //     The message box displays Yes, No, and Cancel buttons.
        YesNoCancel = 3,
        //
        // Summary:
        //     The message box displays Yes and No buttons.
        YesNo = 4,
    }

    public enum MessageBoxResult
    {
        // Summary:
        //     The message box returns no result.
        None = 0,
        //
        // Summary:
        //     The result value of the message box is OK.
        OK = 1,
        //
        // Summary:
        //     The result value of the message box is Cancel.
        Cancel = 2,
        //
        // Summary:
        //     The result value of the message box is Yes.
        Yes = 6,
        //
        // Summary:
        //     The result value of the message box is No.
        No = 7,
    }

    // Summary:
    //     Specifies the icon that is displayed by a message box.
    public enum MessageBoxIcon
    {
        // Summary:
        //     No icon is displayed.
        None = 0,
        //
        // Summary:
        //     The message box contains a symbol consisting of white X in a circle with
        //     a red background.
        Error = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of a white X in a circle with
        //     a red background.
        Hand = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of white X in a circle with
        //     a red background.
        Stop = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of a question mark in a circle.
        Question = 32,
        //
        // Summary:
        //     The message box contains a symbol consisting of an exclamation point in a
        //     triangle with a yellow background.
        Exclamation = 48,
        //
        // Summary:
        //     The message box contains a symbol consisting of an exclamation point in a
        //     triangle with a yellow background.
        Warning = 48,
        //
        // Summary:
        //     The message box contains a symbol consisting of a lowercase letter i in a
        //     circle.
        Information = 64,
        //
        // Summary:
        //     The message box contains a symbol consisting of a lowercase letter i in a
        //     circle.
        Asterisk = 64,
    }


    /// <summary>
    /// Opens viewmodels in windows or dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens given viewmodel as Dialog. Result on close will be true or false.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">callback will return dialog result in case of an modal window.</param>
        void ShowDialog(ViewModelBase viewmodel, Action<ViewModelBase, bool> callback);

        /// <summary>
        /// Opens a windows like confirmationbox.
        /// </summary>
        /// <param name="message">Message, writen inside the box</param>
        /// <param name="caption">Messagebox caption</param>
        /// <param name="buttons">shown buttons</param>
        /// <param name="icon">used icon</param>
        /// <returns></returns>
        MessageBoxResult ShowMessageBox(string message, string caption, MessageBoxButton buttons, MessageBoxIcon icon);
    }
}