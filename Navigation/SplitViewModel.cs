namespace H2HY.Navigation
{
    /// <summary>
    /// a split view model which holds two view models. First and Second.
    /// </summary>
    public class SplitViewModel : ViewModelBase
    {
        private readonly ISplitNavigationStore _splitNavigationStore;

        /// <summary>
        ///
        /// </summary>
        /// <param name="splitNavigationStore"></param>
        public SplitViewModel(ISplitNavigationStore splitNavigationStore)
        {
            _splitNavigationStore = splitNavigationStore;

            _splitNavigationStore.FirstChanged += FirstChanged;
            _splitNavigationStore.SecondChanged += SecondChanged;
        }

        private void SecondChanged() => RaisePropertyChanged(nameof(SecondSideViewModel));

        private void FirstChanged() => RaisePropertyChanged(nameof(FirstSideViewModel));

        /// <summary>
        /// current first side view model
        /// </summary>
        public ViewModelBase? FirstSideViewModel => _splitNavigationStore.First;

        /// <summary>
        /// current second side view model
        /// </summary>
        public ViewModelBase? SecondSideViewModel => _splitNavigationStore.Second;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Dispose()
        {
            _splitNavigationStore.FirstChanged -= FirstChanged;
            _splitNavigationStore.SecondChanged -= SecondChanged;
        }
    }
}