namespace H2HY.Stores
{
    /// <summary>
    /// Generic args for store-single-changed action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoreSingleEventArgs<T> : StoreEventArgsBase<T, StoreSingleChangedAction>
    {
        /// <summary>
        /// Generic args for store-single-changed action.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="action"></param>
        public StoreSingleEventArgs(T? value, StoreSingleChangedAction action) : base(value, action)
        {
        }
    }
}