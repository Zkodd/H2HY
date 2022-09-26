using Microsoft.Win32;

namespace H2HY.Services
{
    /// <summary>
    /// DialogBase for Save and Load Dialog - don't use.
    /// </summary>
    public class FileDialogServiceBase
    {
        /// <summary>
        /// Created by the construtor. Is neither a FileOpen or an FileSaveDialog.
        /// </summary>
        protected FileDialog _fileDialog;

        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the 
        /// "Save as file type" or "Files of type" box in the dialog box.
        /// Example: "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        /// </summary>
        public string Extension { get => _fileDialog.DefaultExt; set => _fileDialog.DefaultExt = value; }

        /// <summary>
        /// Empty on default. Will hold the result of Showdialog().
        /// </summary>
        public string FileName { get; private set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the filter string that determines what types of files are displayed
        ///     from either the Microsoft.Win32.OpenFileDialog or Microsoft.Win32.SaveFileDialog.
        ///
        /// Returns:
        ///     A System.String that contains the filter. The default is System.String.Empty,
        ///     which means that no filter is applied and all file types are displayed.
        /// </summary>
        public string Filter { get => _fileDialog.Filter; set => _fileDialog.Filter = value; }

        /// <summary>
        /// ShowdDialog has return with a result in FileName.
        /// </summary>
        public bool HasResult { get => FileName != string.Empty; }

        /// <summary>
        /// Openes the Filedialog. FileName contains result in case of success. Otherwise FileName is empty.
        /// </summary>
        public void ShowDialog()
        {
            FileName = _fileDialog.ShowDialog() == true ? _fileDialog.FileName : string.Empty;
        }
    }
}
