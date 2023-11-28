namespace H2HY.Models
{
    /// <summary>
    /// Indicates store changes.
    /// </summary>
    public enum H2HYListChanged
    {
        /// <summary>
        /// An item was added to the store.
        /// </summary>
        Add = 0,

        /// <summary>
        /// An item was removed from the store.
        /// </summary>
        Remove = 1,

        /// <summary>
        /// The contents of the store changed dramatically.
        /// </summary>
        Reset = 2,

        /// <summary>
        /// An item has updated.
        /// </summary>
        Changed = 3
    }
}
