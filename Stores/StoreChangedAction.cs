﻿namespace H2HY.Stores
{
    /// <summary>
    /// Indicates a perfomed action on an sinlge item.
    /// </summary>
    public enum StoreChangedAction
    {
        /// <summary>
        /// An item was added to the collection.
        /// </summary>
        Add = 0,

        /// <summary>
        /// An item was removed from the collection.
        /// </summary>
        Remove = 1,

        /// <summary>
        /// The item has changed.
        /// </summary>
        Changed = 2,

        /// <summary>
        /// The store has changed drasticly.
        /// </summary>
        Reset = 3,
    }
}