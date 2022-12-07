namespace H2HY.Stores
{
    /// <summary>
    /// Indicates a perfomed action on an sinlge item.
    /// </summary>
    public enum NotifyStoreChangedAction
    {
        /// <summary>
        /// An item was added to the collection.
        /// </summary>
        Add = 0,

        /// <summary>
        /// An item was removed from the collection.
        /// </summary>
        Remove = 1,
    }
}