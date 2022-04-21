using Microsoft.Win32;

namespace H2HY.Services
{
    public class FileOpenDialogService : FileDialogServiceBase
    {
        public FileOpenDialogService()
        {
            _fileDialog = new OpenFileDialog();
        }
    }
}
