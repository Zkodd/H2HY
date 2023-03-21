namespace H2HY
{
    /// <summary>
    /// Dialoginterface for save dialog.
    /// Can be used for FileSaveDialogService
    /// </summary>
    public interface IFileSaveDialog
    {
        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the
        /// "Save as file type" or "Files of type" box in the dialog box.
        /// Example: "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        /// </summary>
        string Extension { get; set; }

        /// <summary>
        /// Empty by default. Will hold the result of Showdialog().
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     Gets or sets the filter string that determines what types of files are displayed
        ///     from either the Microsoft.Win32.OpenFileDialog or Microsoft.Win32.SaveFileDialog.
        ///
        /// Returns:
        ///     A System.String that contains the filter. The default is System.String.Empty,
        ///     which means that no filter is applied and all file types are displayed.
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// ShowdDialog has return with a result in FileName.
        /// </summary>
        bool HasResult { get; }

        /// <summary>
        /// Openes the Filedialog. FileName contains result in case of success. Otherwise FileName is empty.
        /// </summary>
        void ShowDialog();
    }
}