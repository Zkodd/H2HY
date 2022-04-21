using Microsoft.Win32;

namespace H2HY.Services
{
    public class FileSaveDialogService : FileDialogServiceBase
    {
        public FileSaveDialogService()
        {
            _fileDialog = new SaveFileDialog();
        }
    }
}
