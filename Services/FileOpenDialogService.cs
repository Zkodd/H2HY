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

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box allows multiple files to be selected.
        /// </summary>
        public bool MultiSelect { get => (_fileDialog as OpenFileDialog).Multiselect; set => (_fileDialog as OpenFileDialog).Multiselect = value; }
    }
}
