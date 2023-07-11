namespace H2HY.Navigation
{
    /// <summary>
    /// Interface for a Modal-Navigation-Store.
    /// </summary>
    public interface INavigationModalStore
    {
        /// <summary>
        /// set the current view model opens a new window using the given view model.
        /// </summary>
        ViewModelBase? CurrentViewModel { get; set; }
    }
}