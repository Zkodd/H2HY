using Microsoft.Win32;

namespace H2HY.Services
{
    /// <summary>
    /// Usage like this:
    /// <![CDATA[ 
    ///  FileOpenDialogService openFileDialog = new FileOpenDialogService();
    ///  openFileDialog.ShowDialog();
    ///
    ///   if (openFileDialog.HasResult)
    ///   {
    ///      something.SaveToFile(openFileDialog.FileName);
    ///   }
    /// ]]>
    /// </summary>
    public class FileOpenDialogService : FileDialogServiceBase
    {
        /// <summary>
        /// constructor for creating a OpenFileDialog
        /// </summary>
        public FileOpenDialogService()
        {
            _fileDialog = new OpenFileDialog();
        }
    }
}
