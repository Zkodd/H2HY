namespace H2HY.Stores
{
    /// <summary>
    /// Generic Args for store-changed action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoreEventArgs<T> : StoreEventArgsBase<T, StoreChangedAction>
    {
        public StoreEventArgs(T? value, StoreChangedAction action) : base(value, action)
        {
        }
    }
}