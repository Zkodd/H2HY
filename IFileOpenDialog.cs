namespace H2HY
{
    /// <summary>
    /// Dialog interface for open dialog.
    /// Can be used for FileOpenDialogService
    /// </summary>
    public interface IFileOpenDialog
    {
        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the
        /// "Save as file type" or "Files of type" box in the dialog box.
        /// Example: "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        /// </summary>
        string Extension { get; set; }

        /// <summary>
        /// Empty by default. Will hold the result of ShowDialog().
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
        /// ShowDialog has return with a result in FileName.
        /// </summary>
        bool HasResult { get; }

        /// <summary>
        /// Opens the File dialog. FileName contains result in case of success. Otherwise FileName is empty.
        /// </summary>
        void ShowDialog();
    }
}