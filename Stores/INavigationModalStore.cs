namespace H2HY.Stores
{
    /// <summary>
    /// Interface for a Modal-Navigation-Store.
    /// </summary>
    public interface INavigationModalStore
    {
        /// <summary>
        /// set the currentviewmodel opens a new window using the given viewmodel.
        /// </summary>
        ViewModelBase? CurrentViewModel { get; set; }
        /// <summary>
        /// closes all open views
        /// </summary>
        void Close();
    }
}