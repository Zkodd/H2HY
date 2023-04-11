namespace H2HY.Stores
{
    public class StoreSingleEventArgs<T> : StoreEventArgsBase<T, StoreSingleChangedAction>
    {
        public StoreSingleEventArgs(T? value, StoreSingleChangedAction action) : base(value, action)
        {
        }
    }
}