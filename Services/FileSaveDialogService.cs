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
    ///      something.SavetoFile(openFileDialog.FileName);
    ///   }
    /// ]]>
    /// </summary>
    public class FileSaveDialogService : FileDialogServiceBase, IFileSaveDialog
    {
        /// <summary>
        /// constructor for creating a FileSaveDialogService
        /// </summary>
        public FileSaveDialogService()
        {
            _fileDialog = new SaveFileDialog();
        }
    }
}