using System;

namespace H2HY.Stores
{
    public interface IStoreBase<T>
    {
        event Action<StoreEventArgs<T>>? Changed;
    }
}