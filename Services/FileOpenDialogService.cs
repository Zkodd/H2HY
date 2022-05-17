using Microsoft.Win32;

namespace H2HY.Services
{
    /// <summary>
    /// Usage like this:
    /// 
    ///  FileOpenDialogService openFileDialog = new FileOpenDialogService();
    ///  openFileDialog.ShowDialog();
    ///
    ///   if (openFileDialog.HasResult)
    ///   {
    ///      something.SaveToFile(openFileDialog.FileName);
    //    }
    /// </summary>
    public class FileOpenDialogService : FileDialogServiceBase
    {
        public FileOpenDialogService()
        {
            _fileDialog = new OpenFileDialog();
        }
    }
}
