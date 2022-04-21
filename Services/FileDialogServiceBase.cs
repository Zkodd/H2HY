using Microsoft.Win32;

namespace H2HY.Services
{
    public class FileDialogServiceBase
    {
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
