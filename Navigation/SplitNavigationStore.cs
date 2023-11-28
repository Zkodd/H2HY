using System;

namespace H2HY.Navigation
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class SplitNavigationStore : ISplitNavigationStore
    {
        private ViewModelBase? _first;
        private ViewModelBase? _second;

        /// <summary>
        /// default constructor using empty views.
        /// </summary>
        public SplitNavigationStore()
        {
            _first = null;
            _second = null;
        }

        /// <summary>   
        /// constructor using predefined views.
        /// </summary>
        /// <param name="firstView"></param>
        /// <param name="secondView"></param>
        public SplitNavigationStore(ViewModelBase firstView, ViewModelBase secondView)
        {
            _first ??= firstView;
            _second ??= secondView;
        }

        /// <summary>
        /// current first side
        /// </summary>
        public ViewModelBase? First
        {
            get => _first;
            set
            {
                if (value != _first)
                {
                    _first = value;
                    FirstChanged?.Invoke();
                }
            }
        }

        /// <summary>
        /// current second side
        /// </summary>
        public ViewModelBase? Second
        {
            get => _second;
            set
            {
                if (value != _second)
                {
                    _second = value;
                    SecondChanged?.Invoke();
                }
            }
        }

        /// <summary>
        /// first side view model has changed.
        /// </summary>
        public event Action? FirstChanged;

        /// <summary>
        /// second side view model has changed.
        /// </summary>
        public event Action? SecondChanged;
    }
}