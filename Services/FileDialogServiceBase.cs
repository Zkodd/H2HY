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

        public string Extension { get => _fileDialog.DefaultExt; set => _fileDialog.DefaultExt = value; }

        public string FileName { get; private set; }

        public string Filter { get => _fileDialog.Filter; set => _fileDialog.Filter = value; }

        public bool HasResult { get => FileName != string.Empty; }

        public void ShowDialog()
        {
            FileName = _fileDialog.ShowDialog() == true ? _fileDialog.FileName : string.Empty;
        }
    }
}
