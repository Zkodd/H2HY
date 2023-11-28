using System;

namespace H2HY.Navigation
{
    /// <summary>
    /// A split navigation store.
    /// splits the view into two sides. side one(first) and side two(second)
    /// </summary>
    public interface ISplitNavigationStore
    {
        /// <summary>
        /// one side changed the view model.
        /// </summary>
        event Action? FirstChanged;

        /// <summary>
        /// second side changed the view model.
        /// </summary>
        event Action? SecondChanged;

        /// <summary>
        /// the current view model for side one.
        /// </summary>
        public ViewModelBase? First { get; set; }

        /// <summary>
        /// the current view model for the second side.
        /// </summary>
        public ViewModelBase? Second { get; set; }
    }
}