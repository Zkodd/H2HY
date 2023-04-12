namespace H2HY.Stores
{
    /// <summary>
    /// Generic args for store-changed action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoreEventArgs<T> : StoreEventArgsBase<T, StoreChangedAction>
    {
        /// <summary>
        /// Generic args for store-changed action.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="action"></param>
        public StoreEventArgs(T? value, StoreChangedAction action) : base(value, action)
        {
        }
    }
}