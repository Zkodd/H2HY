namespace H2HY.Stores
{
    public enum StoreChanged
    {
        //
        // Summary:
        //     An item was added to the store.
        Add = 0,
        //
        // Summary:
        //     An item was removed from the store.
        Remove = 1,
        //
        // Summary:
        //     The contents of the store changed dramatically.
        Reset = 2,
        //
        // Summary:
        //     An item has been updated.
        Changed = 3
    }
}
